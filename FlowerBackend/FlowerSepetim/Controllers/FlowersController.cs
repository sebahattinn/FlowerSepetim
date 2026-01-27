using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.Interfaces;
using CicekSepeti.Application.DTOs;

namespace CicekSepeti.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private readonly IFlowerRepository _flowerRepository;

        public FlowersController(IFlowerRepository flowerRepository)
        {
            _flowerRepository = flowerRepository;
        }

        // GET api/flowers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var flowers = await _flowerRepository.GetAllAsync();
            return Ok(flowers);
        }

        //  YENİ: GET api/flowers/featured
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatured()
        {
            var flowers = await _flowerRepository.GetFeaturedFlowersAsync();
            return Ok(flowers);
        }

        // POST api/flowers (Sadece Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFlowerDto request)
        {
            try
            {
                var newFlower = new Flower(
                    request.Name,
                    request.Description,
                    request.Color,
                    request.ImageUrl,
                    request.Price,
                    request.Stock,
                    request.CategoryId,
                    request.IsFeatured //  DTO'da bu alan olmalı!
                );

                var id = await _flowerRepository.AddAsync(newFlower);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/flowers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var flower = await _flowerRepository.GetByIdAsync(id);
            if (flower == null) return NotFound("Çiçek bulunamadı");
            return Ok(flower);
        }

        // GÜNCELLEME (PUT api/Flowers/5)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateFlowerDto request)
        {
            var existingFlower = await _flowerRepository.GetByIdAsync(id);
            if (existingFlower == null) return NotFound("Çiçek bulunamadı");

            // DDD FACTORY İLE GÜNCELLEME
            var flowerToUpdate = Flower.Load(
                id,
                request.Name,
                request.Description,
                request.Color,
                request.ImageUrl,
                request.Price,
                request.Stock,
                request.CategoryId,
                existingFlower.IsActive,    // Mevcut aktifliği koru
                request.IsFeatured          // DTO'dan gelen yeni vitrin durumu
            );

            await _flowerRepository.UpdateAsync(flowerToUpdate);
            return Ok("Güncellendi");
        }

        // ... Delete, GetCategories, AddCategory aynı kalıyor ...  Burada ufak bir soft delete durumu söz konusu
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _flowerRepository.DeleteAsync(id);
            return Ok("Silindi");
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _flowerRepository.GetCategoriesAsync();
            return Ok(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("categories")]
        public async Task<IActionResult> AddCategory([FromBody] string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return BadRequest("Kategori adı boş olamaz.");

            var id = await _flowerRepository.AddCategoryAsync(categoryName);
            return Ok(new { id, name = categoryName, message = "Kategori eklendi." });
        }
    }
}