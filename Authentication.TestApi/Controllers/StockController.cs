using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.TestApi.Controllers;


[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    [HttpGet]
    public IActionResult GetStock()
    {
        var userName = HttpContext.User.Identity?.Name;

        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        //veri tabanında  userId veya userName alanları üzerinden gerekli dataları çek

        // stockId stockQuantity  Category  UserId/UserName

        return Ok($"Stock işlemleri  =>UserName: {userName}- UserId:{userIdClaim.Value}");
    }
}