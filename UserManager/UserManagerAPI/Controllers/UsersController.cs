using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagerAPI.Models;

namespace UserManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager, UserContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        private IActionResult View(IQueryable<ApplicationUser> users)
        {
            throw new NotImplementedException();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserVm>> GetUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<ApplicationUserVm>(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, ApplicationUserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                var model = await _userManager.FindByIdAsync(user.Id.ToString());
                var token = await _userManager.GeneratePasswordResetTokenAsync(model);
                var result = await _userManager.ResetPasswordAsync(model, token, user.Password);
                model.UserName = user.UserName;
                model.Email = user.Email;

                await _userManager.UpdateAsync(model);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return BadRequest(new RegistrationResponse()
                    {
                        IsSuccessfulRegistration = result.Succeeded,
                        Errors = errors
                    });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostUser(ApplicationUserDto user)
        {
            var entity = _mapper.Map<ApplicationUser>(user);
            var result = await _userManager.CreateAsync(entity, user.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse()
                {
                    IsSuccessfulRegistration = result.Succeeded,
                    Errors = errors
                });
            }
            
            user.Id = entity.Id;
            return CreatedAtAction("GetUser", new { id = entity.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }
    }
}
