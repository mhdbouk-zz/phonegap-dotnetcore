using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatasController : ControllerBase
    {
        private readonly DataContext _context;

        public DatasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Datas
        [HttpGet]
        public IEnumerable<Data> GetDatas()
        {
            return _context.Datas;
        }

        // GET: api/Datas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = await _context.Datas.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // PUT: api/Datas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutData([FromRoute] int id, [FromBody] Data data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != data.Id)
            {
                return BadRequest();
            }

            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataExists(id))
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

        // POST: api/Datas
        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] Data data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Datas.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetData", new { id = data.Id }, data);
        }

        // DELETE: api/Datas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = await _context.Datas.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            _context.Datas.Remove(data);
            await _context.SaveChangesAsync();

            return Ok(data);
        }

        private bool DataExists(int id)
        {
            return _context.Datas.Any(e => e.Id == id);
        }
    }
}