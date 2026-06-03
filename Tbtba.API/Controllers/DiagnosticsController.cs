using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Tbtba.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticsController : ControllerBase
    {
        // شيك على اسم الـ DbContext بتاعك لو اسمه ApplicationDbContext أو TbtbaContext غيره هنا
        private readonly DbContext _context;

        public DiagnosticsController(DbContext context)
        {
            _context = context;
        }

        [HttpGet("database")]
        public async Task<IActionResult> CheckDatabase()
        {
            try
            {
                // 1. التشييك على الاتصال بالداتا بيز
                var canConnect = await _context.Database.CanConnectAsync();

                // 2. التشييك على وجود جداول الـ Identity عافية
                var hasAspNetUsers = false;
                var hasAspNetRoles = false;

                try
                {
                    await _context.Database.ExecuteSqlRawAsync("SELECT 1 FROM \"AspNetUsers\" LIMIT 1;");
                    hasAspNetUsers = true;
                }
                catch { hasAspNetUsers = false; }

                try
                {
                    await _context.Database.ExecuteSqlRawAsync("SELECT 1 FROM \"AspNetRoles\" LIMIT 1;");
                    hasAspNetRoles = true;
                }
                catch { hasAspNetRoles = false; }

                return Ok(new
                {
                    success = true,
                    canConnect = canConnect,
                    hasAspNetUsers = hasAspNetUsers,
                    hasAspNetRoles = hasAspNetRoles,
                    timeChecked = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }
    }
}