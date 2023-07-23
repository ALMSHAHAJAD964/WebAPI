
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Data;
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
   [ApiController]
    [Route("api/controller")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbcontext;

        public ContactsController(ContactsAPIDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet("GetContact/{id}")]
        //[Route("{ id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)

        {
            var contact = await dbcontext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
                
        }
            


        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbcontext.Contacts.ToListAsync());
            
        }

        [HttpPost]
        
        public async Task<IActionResult> AddContact(AddConactRequest addConactRequest)
        {
            var contact = new Contact()

            {
                id = Guid.NewGuid(),
                FirstName = addConactRequest.FirstName,
                LastName = addConactRequest.LastName,
                PhoneNumber = addConactRequest.PhoneNumber,
                Class = addConactRequest.Class
            };
            await dbcontext.Contacts.AddAsync(contact);
            await dbcontext.SaveChangesAsync();

            return Ok(contact);

        }

        [HttpPost("UpdateContact/{id}")] 
        //[Route("{ id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactRequest updateContactRequest)
        {

           var contact = await dbcontext.Contacts.FindAsync(id);

            if(contact != null)
            {
                contact.FirstName = updateContactRequest.FirstName;
                contact.LastName = updateContactRequest.LastName;
                contact.PhoneNumber = updateContactRequest.PhoneNumber;
                contact.Class = updateContactRequest.Class;

               await dbcontext.SaveChangesAsync();

                return Ok(contact);


            }
            return NotFound();
          
        }
        [HttpDelete("Deletecontact/{id}")]
        //[Route("{ id:guid}")]

        public async Task<IActionResult> Deletecontact([FromRoute] Guid id)
        {
            var contact = await dbcontext.Contacts.FindAsync(id);
            if (contact != null) 
            {
                dbcontext.Remove(contact);
               await dbcontext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

    }

   
}
