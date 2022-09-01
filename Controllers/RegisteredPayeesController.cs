#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankManagementDotnetApi.Models;

namespace BankManagementDotnetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredPayeesController : ControllerBase
    {
        private readonly BankApiDbContext _context;

        public RegisteredPayeesController(BankApiDbContext context)
        {
            _context = context;
        }

        // GET: api/RegisteredPayees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisteredPayee>>> GetRegisteredPayees()
        {
            return await _context.RegisteredPayees.ToListAsync();
        }

        // GET: api/RegisteredPayees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisteredPayee>> GetRegisteredPayee(int id)
        {
            var registeredPayee = await _context.RegisteredPayees.FindAsync(id);

            if (registeredPayee == null)
            {
                return NotFound();
            }

            return registeredPayee;
        }
        [Route("[action]/{customerId}")]

         [HttpGet]
        public async Task<ActionResult<RegisteredPayee>> MyRegPayees(int customerId)
        {
            List<RegisteredPayee> regpayees = await _context.RegisteredPayees.Where(m => m.CustomerId == customerId).ToListAsync();

            if (regpayees == null)
            {
                return NoContent();
            }

            return Ok(regpayees);
        }
       


        // PUT: api/RegisteredPayees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegisteredPayee(int id, RegisteredPayee registeredPayee)
        {
            if (id != registeredPayee.Id)
            {
                return BadRequest();
            }

            _context.Entry(registeredPayee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisteredPayeeExists(id))
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

        // POST: api/RegisteredPayees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegisteredPayee>> PostRegisteredPayee(RegisteredPayee registeredPayee)
        {
            _context.RegisteredPayees.Add(registeredPayee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegisteredPayee", new { id = registeredPayee.Id }, registeredPayee);
        }

        // DELETE: api/RegisteredPayees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegisteredPayee(int id)
        {
            var registeredPayee = await _context.RegisteredPayees.FindAsync(id);
            if (registeredPayee == null)
            {
                return NotFound();
            }

            _context.RegisteredPayees.Remove(registeredPayee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegisteredPayeeExists(int id)
        {
            return _context.RegisteredPayees.Any(e => e.Id == id);
        }
    }
}
