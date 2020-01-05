using AutoMapper;
using BLL.ModelsDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lot, LotDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Bid, BidDTO>().ReverseMap();
            CreateMap<AppUser, AppUserDTO>().ForMember(x => x.Role, x => x.MapFrom(x => x.UserRoles.Select(x => x.Role.Name).FirstOrDefault()));
            CreateMap<AppUserDTO, AppUser>();



        }
    }
}
