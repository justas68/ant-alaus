using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Alus.Core.Models;
using Alus.WebService.Models;

namespace Alus.WebService.Controllers
{
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly AlusContext _context;

        public FeedbackController(AlusContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Feedback>> Get()
        {
            return await _context
                .FeedbackItems
                .Select(f => new Feedback(f))
                .ToListAsync();
        }
        /*
        // GET api/values/5
        [HttpGet("{id}")]
        public Feedback Get(int id)
        {
            return new Feedback()
            {
                EMail = "andrius.bentkus@gmail.com",
                Text = "Make the app better",
                Type = FeedbackType.General
            };
        }
        */

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]Feedback feedback)
        {
            _context.FeedbackItems.Add(
                new DatabaseFeedback()
                {
                    EMail = feedback.EMail,
                    Text = feedback.Text,
                    Type = feedback.Type,
                    Created = DateTime.Now
                }
            );
            await _context.SaveChangesAsync();
        }
        /*
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotSupportedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotSupportedException();
        }
        */
    }
}
