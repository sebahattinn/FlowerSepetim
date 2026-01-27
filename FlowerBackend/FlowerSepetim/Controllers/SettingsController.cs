using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CicekSepeti.Domain.Interfaces;

namespace CicekSepeti.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IFlowerRepository _repo; 

        public SettingsController(IFlowerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var status = await _repo.GetMaintenanceStateAsync();
            return Ok(new { isMaintenance = status });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleMaintenance([FromBody] bool status)
        {
            await _repo.UpdateMaintenanceStateAsync(status);
            return Ok(new { message = status ? "Site bakıma alındı." : "Site yayına açıldı." });
        }
    }
}