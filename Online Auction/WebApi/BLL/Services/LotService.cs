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
    public class LotService : ILotService
    {        
        private IUnitOfWork _uow { get; set; }

        private IMapper _mapper;
         
        public LotService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
       

        public IEnumerable<LotDTO> GetLots()
        {
            var mapped = _mapper.Map<IEnumerable<LotDTO>>(_uow.Lots.GetAll());
            return mapped;
        }

        public LotDTO GetLot(int? id)
        {
            var mapped = _mapper.Map<LotDTO>(_uow.Lots.Get(id));
            
            return mapped;
        }

        public LotDTO CreateLot(LotDTO item)
        {
            if (item.BeginDate >= item.EndDate)
            {
                throw new BLValidationException("Auction begin date must be earlier than the end.");
            }
            if (item.InitialPrice <= 0)
            {
                throw new BLValidationException("Lot price must be greater than zero.");
            }

            var mapped = _mapper.Map<Lot>(item);

            _uow.Lots.Create(mapped);
            _uow.Save();
            return _mapper.Map<LotDTO>(_uow.Lots.Get(mapped.Id));
        }

        public void Update(LotDTO item)
        {
            var mapped = _mapper.Map<Lot>(item);
            _uow.Lots.Update(mapped);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _uow.Lots.Delete(id);
            _uow.Save();
        }


        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
