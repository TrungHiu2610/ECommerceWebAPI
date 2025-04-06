using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models.ViewModels;

namespace MyFirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoaiController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Loais.ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetLoaiById(int id)
        {
            Loai loai = _context.Loais.SingleOrDefault(h => h.MaLoai == id);

            if(loai==null)
            {
                return NotFound();
            }    

            return Ok(loai);
        }
        [HttpPost]
        public IActionResult CreateNew(LoaiVM model)
        {
            try
            {
                Loai loai = new Loai()
                {
                    TenLoai = model.TenLoai
                };
                _context.Loais.Add(loai);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created,loai);
            }
            catch
            {
                return BadRequest();
            }
        }
        // test thử authorize sẽ trả về status gì => 401 Unauthorized
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit(int id, LoaiVM model)
        {
            try
            {
                Loai loai = _context.Loais.SingleOrDefault(h => h.MaLoai == id);
                if(loai==null)
                {
                    return NotFound();
                }
                loai.TenLoai = model.TenLoai;

                _context.Loais.Update(loai);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Loai loai = _context.Loais.SingleOrDefault(h => h.MaLoai == id);
                if (loai == null)
                {
                    return NotFound();
                }

                _context.Loais.Remove(loai);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
