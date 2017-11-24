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

        [HttpGet]
        public async Task<IEnumerable<Feedback>> Get(int offset = 0, int count = 0)
        {

            var result = _context
                .FeedbackItems
                .Select(f => new Feedback(f));

            if (!(offset == 0 && count == 0))
            {
                result = result.Skip(offset).Take(0);
            }

            return await result.ToListAsync();
        }

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
    }
}
