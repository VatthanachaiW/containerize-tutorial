using Demo.API.Contexts;
using Demo.API.Dtos.Requests;
using Demo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProvincesController : ControllerBase
{
    private readonly IDemoDbContext _context;

    public ProvincesController(IDemoDbContext context)
    {
        _context = context;
    }

    [HttpGet("get_province")]
    public async Task<IActionResult> GetAllProvinceAsync()
    {
        var provinces = await _context.Set<Province>().ToListAsync();

        return Ok(provinces);
    }

    [HttpPost("create_new")]
    public async Task<IActionResult> CreateProvinceAsync(CreateProvinceRequest request)
    {
        var newRequest = new Province
        {
            ProvinceName = request.ProvinceName,
            IsActivated = true
        };

        await _context.Set<Province>().AddAsync(newRequest);

        return await _context.SaveChangesAsync() > 0
            ? Ok(newRequest)
            : BadRequest();
    }
}