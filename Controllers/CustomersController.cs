#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankManagementDotnetApi.Models;
using BankManagementDotnetApi.ViewModels;
namespace BankManagementDotnetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BankApiDbContext _context;
          private readonly IWebHostEnvironment webHostEnvironment;

        public CustomersController(BankApiDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
        
        [Route("[action]/{userId}")]
        [HttpGet]
        public async Task<ActionResult<Customer>> CurrentCustomer(int userId)
        {
        var customer = await _context.Customers.FirstOrDefaultAsync(m=>m.UserId==userId);
        // string filename=customer.Image;
        // customer.Image = Path.Combine(webHostEnvironment.WebRootPath, "Images", filename);
        if (customer == null)
                    {
                        return NoContent();
                    }

                    return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromForm]CustomerViewModel model)
        {   
           string uniqueFileName = UploadedFile(model);
            string uniquesignature = Uploadsiganture(model);
                    Customer customer = new Customer
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        AdharNumber = model.AdharNumber,
                        PanNumber = model.PanNumber,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        DateOfBirth = model.DateOfBirth,
                        Image = uniqueFileName,
                        Signature = uniquesignature,
                        UserId=model.UserId,
                    };
                     _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();
            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }
 private string UploadedFile(CustomerViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string Uploadsiganture(CustomerViewModel model)
        {
            string uniquesignature = null;

            if (model.Signature != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniquesignature = Guid.NewGuid().ToString() + "_" + model.Signature.FileName;
                string filePath = Path.Combine(uploadsFolder, uniquesignature);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Signature.CopyTo(fileStream);
                }
            }
            return uniquesignature;
        }
        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
