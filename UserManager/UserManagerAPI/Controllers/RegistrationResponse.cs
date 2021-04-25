using System.Collections.Generic;

namespace UserManagerAPI.Controllers
{
    public class RegistrationResponse
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
