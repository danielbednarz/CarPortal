using API.DTOs;
using API.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderBrandId, opt => opt.MapFrom(src => src.Sender.BrandId))
                .ForMember(dest => dest.SenderModelId, opt => opt.MapFrom(src => src.Sender.ModelId))
                .ForMember(dest => dest.RecipientBrandId, opt => opt.MapFrom(src => src.Recipient.BrandId))
                .ForMember(dest => dest.RecipientModelId, opt => opt.MapFrom(src => src.Recipient.ModelId));
        }
    }
}
