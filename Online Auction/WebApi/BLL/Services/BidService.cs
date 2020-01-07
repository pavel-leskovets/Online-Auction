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
        public BidDTO CreateBid(BidDTO bid)
        {
            var currentLot = _uow.Lots.GetById(bid.LotId);

            if (currentLot == null)
            {
                throw new NotFoundException("Lot is not found");
            }
            if (currentLot.Bids.Any())
            {
                if (bid.BidPrice <= currentLot.Bids.Last().BidPrice)
                    throw new BLValidationException("Bid price should be higher than current price");
            }
            if (!currentLot.Bids.Any())
            {
                if (bid.BidPrice <= currentLot.InitialPrice)
                    throw new BLValidationException("Bid price should be higher than current price");
            }
            
            if (bid.BidDate > currentLot.EndDate)
                throw new BLValidationException("Bid can’t be placed after auction end");
            if (bid.BidDate < currentLot.BeginDate)
                throw new BLValidationException("Bid can’t be placed before auction start");
            if (bid.UserId == currentLot.UserId)
                throw new BLValidationException("Lot owner can't place bid on own auction");

            var mapped = _mapper.Map<Bid>(bid);
           
            _uow.Bids.Create(mapped);
           
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
            var mapped = _mapper.Map<BidDTO>(_uow.Bids.GetById(id));
            return mapped;
        }

        /// <summary>
        /// Method for deleting bid by ID.
        /// </summary>
        /// <param name="id">The bid ID.</param>
        public void DeleteBid(int id)
        {
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
