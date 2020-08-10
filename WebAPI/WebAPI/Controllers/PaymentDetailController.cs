using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Helpers;
using WebAPI.Models;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
        
      

        public PaymentDetailController(PaymentDetailContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

      

        // PUT: api/PaymentDetail/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetail paymentDetail)
        {
            if (id != paymentDetail.PMId)
            {
                return BadRequest();
            }

            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
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

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {

            //using (var ctx = _context)
            //{
            //    var student = (from s in ctx.PaymentDetails
            //                   where s.UserId == 2014
            //                   select s);
            //}

            //var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            //var currentUserId = claimsIdentity.Name;


            //if (_context.PaymentDetails.Any(x => x.UserId == Convert.ToInt32(currentUserId)))
            //{ 
            
            //}

            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            return paymentDetail;
        }
       

        // POST: api/PaymentDetail
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
   
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var currentUserId = claimsIdentity.Name;
            paymentDetail.UserId = Convert.ToInt32(currentUserId);

            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PMId }, paymentDetail);
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetail>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return paymentDetail;
        }

        private bool PaymentDetailExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PMId == id);
        }
    }
}
