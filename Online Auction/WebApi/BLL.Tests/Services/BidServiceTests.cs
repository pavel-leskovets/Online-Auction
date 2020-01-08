using AutoMapper;
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
using BLL.Exceptions;

namespace BLL.Tests.Services
{
    [TestFixture]
    public class BidServiceTests
    {
        private IBidService _service;
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
            _service = new BidService(_mockUnitOfWork.Object, mapper);
            _lots = new List<Lot>()
            {
                new Lot() { Id = 1, InitialPrice = 1, Bids = new List<Bid>() { new Bid { Id = 1, LotId = 1, BidPrice = 21} } },
                new Lot() { Id = 2, InitialPrice = 1, Bids = new List<Bid>()},
                new Lot() { Id = 3, InitialPrice = 1, Bids = new List<Bid>() }
            };
            _bids = new List<Bid>()
            {
                new Bid() {Id = 1, LotId = 1, UserId = 1, UserName = "User" , BidPrice = 10, BidDate = DateTime.Now },
                new Bid() {Id = 2, LotId = 2, UserId = 2, UserName = "User1" ,BidPrice = 15, BidDate = DateTime.Now },
                new Bid() {Id = 3, LotId = 2, UserId = 2, UserName = "User1" ,BidPrice = 20, BidDate = DateTime.Now },
                new Bid() {Id = 4, LotId = 2, UserId = 2, UserName = "User1" ,BidPrice = 25, BidDate = DateTime.Now },
                new Bid() {Id = 5, LotId = 3, UserId = 3, UserName = "User2" ,BidPrice = 30, BidDate = DateTime.Now },
            };
        }


        [Test]
        public void GetBidsTest_GetALLBids_AllBidsReturned()
        {
            //arrange
            _mockUnitOfWork.Setup(x => x.Bids.GetAll()).Returns(_bids) ;
            
            //act
            var result = _service.GetAllBids();

            //assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(_bids.Count()));
        }

        [Test]
        public void GetBidTest_GetBidById_BidReturned()
        {
            //arrange
            int id = 4;
            _mockUnitOfWork.Setup(x => x.Bids.GetById(4)).Returns(_bids.Find(x => x.Id == id));
            
            //act
            var result = _service.GetBid(id);

            //assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
        }

        [Test]
        public void DeleteBidTest_DeleteBid_BidDeleted()
        {
            //arrange
            var bidId = 3;
            _mockUnitOfWork.Setup(x => x.Bids.Delete(bidId));

            //act
            _service.DeleteBid(bidId);

            //assert
            _mockUnitOfWork.Verify(x => x.Bids.Delete(bidId), Times.Once);
        }

        [Test]
        public void CreateBidTest_CreateBidAtNotActiveAuction_BLValidationExceptionThrown()
        {
            //arrange
            var bid = new BidDTO()
            {
                Id = 6,
                BidDate = DateTime.Now,
                UserName = "User",
                LotId = 1,
                BidPrice = 20
            };
            _mockUnitOfWork.Setup(x => x.Lots.GetById(bid.LotId)).Returns(new Lot()
            {
                Id = 1,
                BeginDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(-5),
                InitialPrice = 1,
                Bids = new List<Bid>()
            }) ;

            //act

            //assert
            Assert.Throws<BLValidationException>(() => _service.CreateBid(bid), "Bid can’t be placed after auction end");
        }

        [Test]
        public void CreateBidTest_CreateBidAtNonexistentAuction_BLValidationExeptionThrown()
        {
            //arrange
            var bid = new BidDTO()
            {
                Id = 6,
                BidDate = DateTime.Now,
                UserName = "User",
                LotId = 1,
                BidPrice = 20
            };
            _mockUnitOfWork.Setup(x => x.Lots.GetById(bid.Id)).Returns(_lots.Find(x => x.Id.Equals(bid.LotId)));

            //act

            //assert
            Assert.Throws<NotFoundException>(() => _service.CreateBid(bid));
        }

        [Test]
        public void CreateBidTest_CreateBidWithLowerPriceThanLastBid_BLValidationExceptionThrown()
        {
            //arrange
            var bid = new BidDTO()
            {
                Id = 1,
                BidDate = DateTime.Now,
                UserName = "User",
                LotId = 1,
                BidPrice = 20
            };
            _mockUnitOfWork.Setup(x => x.Lots.GetById(bid.Id)).Returns(_lots.Find(x => x.Id.Equals(bid.LotId)));

            //act

            //assert
            Assert.Throws<BLValidationException>(() => _service.CreateBid(bid), "Bid price should be higher than current price");
        }
    }
}
