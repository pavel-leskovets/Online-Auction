using AutoMapper;
using BLL.Exceptions;
using BLL.Mapping;
using BLL.ModelsDTO;
using BLL.Services;
using DAL.Interfaces;
using DAL.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Tests.Services
{
    public class LotServiceTests
    {
        private ILotService _service;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private List<Bid> _bids;
        private List<Lot> _lots;
        private MappingProfile _mapper;

        [OneTimeSetUp]
        public void Init()
        {
            _mapper = new MappingProfile();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(_mapper));
            IMapper mapper = new Mapper(configuration);
            _service = new LotService(_mockUnitOfWork.Object, mapper);
            _bids = new List<Bid>()
            {
                new Bid() {Id = 1, LotId = 1, UserId = 1, UserName = "User" , BidPrice = 10, BidDate = DateTime.Now.AddMinutes(1) },
                new Bid() {Id = 2, LotId = 1, UserId = 1, UserName = "User" , BidPrice = 15, BidDate = DateTime.Now.AddMinutes(2) },
                new Bid() {Id = 3, LotId = 1, UserId = 1, UserName = "User" , BidPrice = 20, BidDate = DateTime.Now.AddMinutes(3) },
                new Bid() {Id = 4, LotId = 1, UserId = 1, UserName = "User" , BidPrice = 25, BidDate = DateTime.Now.AddMinutes(4) },
                new Bid() {Id = 5, LotId = 1, UserId = 1, UserName = "User" , BidPrice = 30, BidDate = DateTime.Now.AddMinutes(5) },
            };
            _lots = new List<Lot>()
            {
                new Lot() { Id = 1, InitialPrice = 1, Bids = new List<Bid>(_bids)},
                new Lot() { Id = 2, InitialPrice = 1, Bids = new List<Bid>()},
                new Lot() { Id = 3, InitialPrice = 1, Bids = new List<Bid>()}
            };
            
        }

        [Test]
        public void GetAllLotsTest_GetAllLots_AllLotsReturned()
        {
            //arrange
            _mockUnitOfWork.Setup(x => x.Lots.GetAll()).Returns(_lots);

            //act
            var result = _service.GetAllLots();

            //assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(_lots.Count()));
        }

        [Test]
        public void LotCreateTest_CreateLotWithInitPriceLessThenZero_BLValidationExceptionThrown()
        {
            //arrange
            LotDTO lot = new LotDTO { InitialPrice = -1, Id = 1, BeginDate = DateTime.Now.AddMinutes(1), EndDate = DateTime.Now.AddDays(1)};
            
            //act

            //assert
            Assert.Throws<BLValidationException>(() => _service.CreateLot(lot), "Lot price must be greater than zero.");
        }


    }
}
