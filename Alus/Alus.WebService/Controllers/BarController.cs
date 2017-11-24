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
    public class DbSetController<T> : Controller where T : class
    {
        private readonly AlusContext _context;
        private readonly DbSet<T> _dbSet;

        public DbSetController(AlusContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        [HttpGet]
        public async Task<IEnumerable<T>> Get(int offset = 0, int count = 0)
        {

            var result = _dbSet.Select(b => b);

            if (!(offset == 0 && count == 0))
            {
                result = result.Skip(offset).Take(0);
            }

            return await result.ToListAsync();
        }

        [HttpPost]
        public async Task Post([FromBody]T bar)
        {
            _dbSet.Add(bar);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task Post([FromBody]IEnumerable<T> bar)
        {
            foreach (var p in _dbSet)
            {
                _context.Entry(p).State = EntityState.Deleted;
            }

            await _dbSet.AddRangeAsync(bar);
        }
    }

    /*
    [Route("api/[controller]")]
    public class BarController : DbSetController<Bar>
    {
        public BarController(AlusContext context)
            : base(context, context.Bars)
        {
        }

    }
    */
    [Route("api/[controller]")]
    public class BarController : Controller
    {
        private readonly AlusContext _context;

        public BarController(AlusContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Bar>> Get(int offset = 0, int count = 0)
        {
            var result = _context
                .Bars
                .Select(b => new Bar(b));

            if (!(offset == 0 && count == 0))
            {
                result = result.Skip(offset).Take(0);
            }

            return await result.ToListAsync();
        }

        [HttpPost]
        [Route("add")]
        public async Task Post([FromBody]Bar bar)
        {
            _context.Bars.Add(new DatabaseBar(bar));
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task Post([FromBody]IEnumerable<Bar> bars)
        {
            foreach (var p in _context.Bars)
            {
                _context.Entry(p).State = EntityState.Deleted;
            }

            await _context.Bars.AddRangeAsync(bars.Select(bar => new DatabaseBar(bar)));
            await _context.SaveChangesAsync();
        }
    }
}