using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prs_server.Model;

namespace prs_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

    
        
        //Creating Change Method?
        public async void Change(Request request) {
           await  _context.SaveChangesAsync();
            
        }

        //GET REVIEWS METHOD
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestInReview(int userId) {
            var requests =await _context.Requests
                .Where(x => x.Status == "REVIEW" && x.UserId != userId)
                .ToListAsync();
            return  requests;
        }

        //Update Function
        // PUT: api/requests/5/review
        [HttpPut]
        public async Task<IActionResult> SetReview(Request request) {
            if(request.Total <= 50) {
                request.Status = "APPROVED";
                await _context.SaveChangesAsync();
            } else {
                request.Status = "REVIEW";
            }
            Change(request);
            return NoContent();
        }

        //SET APPROVED
        // GET: api/request/5/approve
        [HttpPut]
        public async Task<ActionResult>  SetApproved(Request request) {
            request.Status = "APPROVED";
            Change(request);
            await _context.SaveChangesAsync();
            return NoContent();            
        }

        //SET REJECTED
        // GET: api/request/5/rejected
        [HttpPut]
        public async Task<ActionResult> SetRejected(Request request) {
            request.Status = "REJECTED";
            Change(request);
            await _context.SaveChangesAsync();
            return NoContent();            
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
