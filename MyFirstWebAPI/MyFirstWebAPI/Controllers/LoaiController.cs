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
        private readonly ILoaiRepository _loaiRepository;

        public LoaiController(ILoaiRepository loaiRepository)
        {
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
                if (loai != null)
                {
                    return Ok(loai);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        [HttpPost]
        public IActionResult CreateNew(LoaiModel model)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _loaiRepository.CreateNew(model));
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
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
        public IActionResult Delete(int id)
        {
            try
            {
                _loaiRepository.Delete(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(FormatException ex)
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
