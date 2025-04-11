using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Models.ViewModels;
using MyFirstWebAPI.Services;

namespace MyFirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly IHangHoaRepository _hangHoaRepository;

        public HangHoaController(IHangHoaRepository hangHoaRepository)
        {
            _hangHoaRepository = hangHoaRepository;
        }

        [HttpGet]
        public IActionResult GetAll(string? search, double? from, double? to, string? sortBy, int page = 1)
        {
            return Ok(_hangHoaRepository.GetAll(search,from,to,sortBy,page));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                HangHoaVM hh = _hangHoaRepository.GetById(id);
                if (hh==null)
                {
                    return NotFound();
                }
                return Ok(hh);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(HangHoaModel model)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _hangHoaRepository.CreateNew(model));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoaModel hhEdit)
        {
            try
            {
                _hangHoaRepository.Update(id, hhEdit);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                _hangHoaRepository.Delete(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
