using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models.ViewModels;
using MyFirstWebAPI.Services;

namespace MyFirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILoaiRepository _loaiRepository;

        public LoaiController(MyDbContext context, ILoaiRepository loaiRepository)
        {
            _context = context;
            _loaiRepository = loaiRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_loaiRepository.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetLoaiById(int id)
        {
            try
            {
                LoaiVM loai = _loaiRepository.GetById(id);
                if(loai!=null)
                {
                    return Ok(loai);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult CreateNew(LoaiModel model)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created,_loaiRepository.CreateNew(model));
            }
            catch
            {
                return BadRequest();
            }
        }
        // test thử authorize sẽ trả về status gì => 401 Unauthorized
        [HttpPut("{id}")]
        //[Authorize]
        public IActionResult Edit(int id, LoaiVM model)
        {
            try
            {
                _loaiRepository.Update(id, model);

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
                _loaiRepository.Delete(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
