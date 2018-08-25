namespace Blyss.Web.Mapping
{

    using AutoMapper;
    using Entities;
    using Models.ViewModels;

    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            this.CreateMap<User, UserIndexViewModel>();

            this.CreateMap<Course, CourseDetailsViewModel>().ForMember(x => x.ProfilePicture,
                    x => x.MapFrom(c => c.User.ProfilePicture))
                .ForMember(x => x.Country, x => x.MapFrom(c => c.User.Country))
                .ForMember(x => x.Video, x => x.MapFrom(c => c.Video.YouTubeId))
                .ForMember(x => x.CourseDescription, x => x.MapFrom(c => c.Description))
                .ForMember(x => x.UserDescription, x => x.MapFrom(c => c.User.Description))
                .ForMember(x => x.CourseId, x => x.MapFrom(c => c.Id))
                .ForMember(x => x.CourseCategory, x => x.MapFrom(c => c.Category.Name))
                .ForMember(x => x.CourseLanguage, x => x.MapFrom(c => c.Language.Name));

            this.CreateMap<User, AdminUserViewModel>()
                .ForMember(x => x.Id, x => x.MapFrom(c => c.Id))
                .ForMember(x => x.UserName, x => x.MapFrom(c => c.UserName));

            this.CreateMap<Course, AdminCourseViewModel>()
                .ForMember(x => x.AuthorId, x => x.MapFrom(c => c.User.Id))
                .ForMember(x => x.AuthorName, x => x.MapFrom(c => c.User.FullName));

            this.CreateMap<User, UserDetailsViewModel>();
        }

    }

}