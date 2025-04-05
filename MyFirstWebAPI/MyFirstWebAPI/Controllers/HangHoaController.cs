using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Models.ViewModels;

namespace MyFirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>() {
            new HangHoa("Cocacola",10000)
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                HangHoa hh = hangHoas.SingleOrDefault(p => p.MaHH == Guid.Parse(id));
                if(hh==null)
                {
                    return NotFound();
                }
                return Ok(hh);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(HangHoaVM model)
        {
            HangHoa hh = new HangHoa();
            hh.MaHH = Guid.NewGuid();
            hh.TenHH = model.TenHH;
            hh.DonGia = model.DonGia;

            hangHoas.Add(hh);
            return Ok(new
            {
                Successs = true,
                Data = hh
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa hhEdit)
        {
            try
            {
                HangHoa hh = hangHoas.SingleOrDefault(p => p.MaHH == Guid.Parse(id));
                if (hh == null)
                {
                    return NotFound();
                }
                if (hhEdit.MaHH.ToString() != id)
                {
                    return BadRequest();
                }

                // cap nhat
                hh.TenHH = hhEdit.TenHH;
                hh.DonGia = hhEdit.DonGia;

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                HangHoa hh = hangHoas.SingleOrDefault(p => p.MaHH == Guid.Parse(id));
                if (hh == null)
                {
                    return NotFound();
                }

                hangHoas.Remove(hh);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
