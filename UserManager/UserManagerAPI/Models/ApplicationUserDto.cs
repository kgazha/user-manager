using AutoMapper;
using UserManagerAPI.Interfaces;

namespace UserManagerAPI.Models
{
    public class ApplicationUserDto : IMapFrom<ApplicationUser>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(i => i.Password, 
                    opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
