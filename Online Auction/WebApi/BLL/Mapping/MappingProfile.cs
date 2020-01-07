using AutoMapper;
using BLL.ModelsDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Mapping
{
    /// <summary>
    /// Automapper profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lot, LotDTO>().ForMember(x => x.CurrentPrice, x => x.MapFrom(
                src => !src.Bids.Any() ? src.InitialPrice : src.Bids.LastOrDefault().BidPrice));
            CreateMap<LotDTO, Lot>();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Bid, BidDTO>().ReverseMap();
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
        }
    }
}
