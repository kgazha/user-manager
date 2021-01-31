using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagerAPI.Models;

namespace UserManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers() // Task<ActionResult<IEnumerable<User>>>
        {
            //return await _userManager.Users.ToListAsync();
            return await _userManager.Users.ToListAsync();
        }

        private IActionResult View(IQueryable<User> users)
        {
            throw new NotImplementedException();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
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
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var result = await _userManager.CreateAsync(user, user.Password);

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse() { 
                    IsSuccessfulRegistration = result.Succeeded, 
                    Errors=errors 
                });
            }
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString()); //Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

             //.Users.Remove(user);
            await _userManager.DeleteAsync(user); //_userManager.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }
    }
}
