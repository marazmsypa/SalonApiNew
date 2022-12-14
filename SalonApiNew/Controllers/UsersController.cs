using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonApiNew.Models;

namespace SalonApiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BeatySalonApiContext _context;

        public UsersController(BeatySalonApiContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Users>> GetUsers(string login, string password)
        {
            var users = await _context.Users.FirstOrDefaultAsync(x => x.UserLogin == login && x.UserPassword == password);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // GET: api/Users/5
        [HttpGet("{login}")]
        public async Task<ActionResult<Users>> GetUsersLogin(string login)
        {
            var users = await _context.Users.FirstOrDefaultAsync(x => x.UserLogin == login);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(string id, Users users)
        {
            if (id != users.UserLogin)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            try
            {
                await _context.SaveChangesAsync();
                return users;
            }
            catch (DbUpdateException)
            {
                if (UsersExists(users.UserLogin))
                {
                    return BadRequest();
                }
                
            }

            return CreatedAtAction("GetUsers", new { id = users.UserLogin }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(string id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.UserLogin == id);
        }
    }
}
