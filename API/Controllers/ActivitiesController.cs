using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly DataContext _context;
        public ActivitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            var dataActivity = await _context.Activities.ToListAsync();

            return Ok(dataActivity);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetDetailActivity(Guid id)
        {
            var dataActivity = await _context.Activities.FindAsync(id);

            if (dataActivity == null)
            {
                return NotFound(new { Message = "Not Found" });
            }

            return Ok(dataActivity);
        }

        [HttpPost]
        public async Task<ActionResult<String>> AddActivity(Activity val)
        {
            await _context.Activities.AddRangeAsync(val);
            await _context.SaveChangesAsync();
            return Ok("Add data has been success");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<String>> UpdateActivity(Guid id, Activity val)
        {
            var detailData = await _context.Activities.FindAsync(id);

            if (detailData == null)
            {
                return NotFound(new { Message = "Not Found" });
            }

            detailData.Category = val.Category;
            detailData.City = val.City;

            await _context.SaveChangesAsync();

            return Ok("Data Successfully Updated");
        }
    }
}