using AutoMapper;
using BLL.Services;
using BLL.ModelsDTO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using BLL.Exeptions;

namespace BLL.Services
{
    /// <summary>
    /// Contains methods for managing bids.
    /// </summary>
    public class BidService : IBidService
    {
        private IUnitOfWork _uow { get; set; }

        private IMapper _mapper;

        public BidService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for getting all bids.
        /// </summary>
        /// <returns>Collection of bids DTOs.</returns>
        public IEnumerable<BidDTO> GetAllBids()
        {
            return _mapper.Map<IEnumerable<BidDTO>>(_uow.Bids.GetAll());
        }

        /// <summary>
        /// Method for creating bid.
        /// </summary>
        /// <param name="bid">The bid DTO.</param>
        /// <returns>The Task, containing created bid DTO.</returns>
        /// <exception cref="NotFoundException">Thrown if lot is not found.</exception>
        /// <exception cref="BLValidationException">Thrown when validation is failed.</exception>
        public BidDTO Create(BidDTO bid)
        {
            var currentLot = _uow.Lots.Get(bid.LotId);

            if (currentLot == null)
            {
                throw new NotFoundException("Lot is not found");
            }

            if (bid.BidPrice <= currentLot.CurrentPrice)
                throw new BLValidationException("Bid price should be higher than current price ");
            else if (bid.BidDate > currentLot.EndDate)
                throw new BLValidationException("Bid can’t be placed after auction end");
            else if (bid.BidDate < currentLot.BeginDate)
                throw new BLValidationException("Bid can’t be placed before auction start");
            else if (bid.UserId == currentLot.UserId)
                throw new BLValidationException("Lot owner can't place bid on own auction");
      
            currentLot.CurrentPrice = bid.BidPrice;

             var mapped = _mapper.Map<Bid>(bid);
            _uow.Bids.Create(mapped);
            _uow.Lots.Update(currentLot);
            _uow.Save();

            return _mapper.Map<BidDTO>(mapped);
        }


        /// <summary>
        /// Method for getting bid by ID.
        /// </summary>
        /// <param name="id">The bid ID.</param>
        /// <returns>Bid DTO.</returns>
        public BidDTO GetBid(int id)
        {
            return _mapper.Map<BidDTO>(_uow.Bids.Get(id));
        }

        /// <summary>
        /// Method for deleting bid by ID.
        /// </summary>
        /// <param name="id">The bid ID.</param>
        public void Delete(int id)
        {
            var bidToDelete = _uow.Bids.Get(id);
         
            var currentLot = _uow.Lots.Get(bidToDelete.LotId);

            if (currentLot != null)
            {
                if (bidToDelete.BidPrice == currentLot.CurrentPrice)
                {
                    if (currentLot.Bids.Count() == 1)
                    {
                        currentLot.CurrentPrice = currentLot.InitialPrice;
                    }
                    else
                    {
                        currentLot.CurrentPrice = currentLot.Bids.OrderByDescending(x => x.BidPrice).Skip(1).First().BidPrice;
                    }
                    _uow.Lots.Update(currentLot);
                }
            }
           
            _uow.Bids.Delete(id);
            _uow.Save();
        }


        #region IDisposable Support
        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _uow.Dispose();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
