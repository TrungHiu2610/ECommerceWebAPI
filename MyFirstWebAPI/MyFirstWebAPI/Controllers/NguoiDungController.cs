using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;
using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyFirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private readonly AppSettingJWT _appSetting;

        public NguoiDungController(MyDbContext dbContext, IOptionsMonitor<AppSettingJWT> optionsMonitor)
        {
            _dbContext = dbContext;
            _appSetting = optionsMonitor.CurrentValue;
        }

        [HttpPost("Login")]
        public IActionResult Validate(NguoiDungModel model)
        {
            var user = _dbContext.NguoiDungs.SingleOrDefault(h => h.TenDangNhap == model.TenDangNhap && h.MatKhau == model.MatKhau);
            if(user==null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username or password",
                    Data = null
                });
            }

            return Ok(new ApiResponse
            {
                Success = false,
                Message = "Authenticate success",
                Data = GenerateToken(user)
            });
        }

        private string GenerateToken(NguoiDung nguoiDung)
        {
            // tao object JWTSecurityTokenHandler de sinh token
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            // ma hoa bit cho SecretKey
            var secretKeyBites = Encoding.UTF8.GetBytes(_appSetting.SecretKey);

            // tao SecurityTokenDescriptor de truyen vao JwtSecurityTokenHandler
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Username", nguoiDung.TenDangNhap),
                    new Claim("Id", nguoiDung.MaND.ToString()),
                    new Claim(ClaimTypes.Name, nguoiDung.HoTen),
                    new Claim(ClaimTypes.StreetAddress, nguoiDung.DiaChi),
                    new Claim(ClaimTypes.Email, nguoiDung.Email),

                    //role
                    new Claim("TokenId","Admin") // fix cung admin
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBites), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(token);
        }

    }
}
