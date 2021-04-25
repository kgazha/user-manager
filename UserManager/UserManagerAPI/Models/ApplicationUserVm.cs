using AutoMapper;
using System.ComponentModel;
using UserManagerAPI.Interfaces;

namespace UserManagerAPI.Models
{
    public class ApplicationUserVm : IMapFrom<ApplicationUser>
    {
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }

        [DisplayName("Электронная почта")]
        public string Email { get; set; }
    }
}
