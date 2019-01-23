using System.Linq;
using AutoMapper;
using NS.API.DTO;
using NS.API.Models;

namespace NS.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDTO>()
                .ForMember(dest => dest.PhotoUrl, opts => {
                    opts.MapFrom(
                        source => source.Photos.FirstOrDefault(p => p.ProfilePic).Url
                    );
                })
                .ForMember(dest => dest.Age, opts => {
                    opts.ResolveUsing(d => d.DateOfBirth.CalCulateAge());
                });
            CreateMap<User, UserForDetailedDTO>()
                .ForMember(dest => dest.PhotoUrl, opts => {
                    opts.MapFrom(
                        source => source.Photos.FirstOrDefault(p => p.ProfilePic).Url
                    );
                })
                .ForMember(dest => dest.Age, opts => {
                    opts.ResolveUsing(d => d.DateOfBirth.CalCulateAge());
                });
            CreateMap<Photo, PhotosForDetailedDTO>();
        }
    }
}