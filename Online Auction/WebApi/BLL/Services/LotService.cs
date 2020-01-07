using AutoMapper;
using BLL.Services;
using BLL.ModelsDTO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.IO;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BLL.Infrastructure;
using Newtonsoft.Json;
using BLL.Exeptions;

namespace BLL.Services
{
    /// <summary>
    /// Contains methods for managing lots.
    /// </summary>
    public class LotService : ILotService
    {        
        private IUnitOfWork _uow { get; set; }

        private IMapper _mapper;
         
        public LotService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for getting all lots.
        /// </summary>
        /// <returns>Collection of lots DTOs.</returns>
        public IEnumerable<LotDTO> GetAllLots()
        {
            var mapped = _mapper.Map<IEnumerable<LotDTO>>(_uow.Lots.GetAll());
            return mapped;
        }

        /// <summary>
        /// Method for getting lot by ID.
        /// </summary>
        /// <returns>Lot DTO.</returns>
        /// <param name="id">The lot ID.</param>
        public LotDTO GetLot(int id)
        {
            var mapped = _mapper.Map<LotDTO>(_uow.Lots.GetById(id));
            return mapped;
        }

        /// <summary>
        /// Method for creating lot.
        /// </summary>
        /// <returns>Created lot DTO.</returns>
        /// <param name="lot">The lot DTO.</param>
        /// <exception cref="BLValidationException">Thrown if business logic validation failed.</exception>
        public LotDTO CreateLot(LotDTO lot)
        {
            if (lot.BeginDate >= lot.EndDate)
            {
                throw new BLValidationException("Auction begin date must be earlier than the end.");
            }
            if (lot.InitialPrice <= 0)
            {
                throw new BLValidationException("Lot price must be greater than zero.");
            }

            var mapped = _mapper.Map<Lot>(lot);

            _uow.Lots.Create(mapped);
            _uow.Save();
            return _mapper.Map<LotDTO>(_uow.Lots.GetById(mapped.Id));
        }

        /// <summary>
        /// Method for updating lot.
        /// </summary>
        /// <param name="lot">The lot DTO.</param>
        public void UpdateLot(LotDTO lot)
        {
            
            var mapped = _mapper.Map<Lot>(lot);
            _uow.Lots.Update(mapped);
            _uow.Save();
        }

        /// <summary>
        /// Method for deleting lot.
        /// </summary>
        /// <param name="id">Lot ID</param>
        public void DeleteLot(int id)
        {
            _uow.Lots.Delete(id);
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
