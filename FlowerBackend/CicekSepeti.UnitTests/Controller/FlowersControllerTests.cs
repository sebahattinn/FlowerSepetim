using CicekSepeti.API.Controllers;
using CicekSepeti.Application.DTOs;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CicekSepeti.UnitTests.Controller
{
    public class FlowersControllerTests
    {
        private readonly Mock<IFlowerRepository> _mockRepo;
        private readonly FlowersController _controller;

        public FlowersControllerTests()
        {
            _mockRepo = new Mock<IFlowerRepository>();
            _controller = new FlowersController(_mockRepo.Object);
        }

        // ==========================================
        // 🟢 1. GET (OKUMA) SENARYOLARI
        // ==========================================

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenFlowersExist()
        {
            // ARRANGE
            var flowerList = new List<Flower>
            {
                Flower.Load(1, "Gül", "Desc", "Red", "img.jpg", 100, 50, 1, true, false),
                Flower.Load(2, "Lale", "Desc", "Yellow", "img.jpg", 50, 100, 1, true, false)
            };

            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(flowerList);

            // ACT
            var result = await _controller.GetAll();

            // ASSERT
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnFlowers = okResult.Value.Should().BeAssignableTo<List<Flower>>().Subject;

            returnFlowers.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithEmptyList_WhenRepoIsEmpty()
        {
            // ARRANGE
            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Flower>());

            // ACT
            var result = await _controller.GetAll();

            // ASSERT
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnFlowers = okResult.Value.Should().BeAssignableTo<List<Flower>>().Subject;
            returnFlowers.Should().BeEmpty();
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenIdExists()
        {
            // ARRANGE
            var flower = Flower.Load(1, "Orkide", "Desc", "White", "img.jpg", 200, 20, 2, true, true);
            _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(flower);

            // ACT
            var result = await _controller.GetById(1);

            // ASSERT
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnFlower = okResult.Value.Should().BeAssignableTo<Flower>().Subject;
            returnFlower.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // ARRANGE
            _mockRepo.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((Flower?)null);

            // ACT
            var result = await _controller.GetById(99);

            // ASSERT
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        // 🔥 YENİ: VİTRİN TESTİ
        [Fact]
        public async Task GetFeatured_ShouldReturnOnlyFeaturedFlowers()
        {
            // ARRANGE
            var featuredList = new List<Flower>
            {
                Flower.Load(1, "Vip Gül", "Desc", "Red", "img.jpg", 500, 10, 1, true, true) // IsFeatured = true
            };

            _mockRepo.Setup(x => x.GetFeaturedFlowersAsync()).ReturnsAsync(featuredList);

            // ACT
            var result = await _controller.GetFeatured();

            // ASSERT
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnFlowers = okResult.Value.Should().BeAssignableTo<List<Flower>>().Subject;

            returnFlowers.Should().ContainSingle();
            returnFlowers[0].IsFeatured.Should().BeTrue();
        }

        // ==========================================
        // 🟡 2. CREATE (EKLEME) SENARYOLARI
        // ==========================================

        [Fact]
        public async Task Create_ShouldReturnCreated_WhenDtoIsValid()
        {
            // ARRANGE
            var validDto = new CreateFlowerDto("Papatya", "Desc", "White", "url", 50, 100, 1, false);
            _mockRepo.Setup(x => x.AddAsync(It.IsAny<Flower>())).ReturnsAsync(10); // ID 10 döndü varsayalım

            // ACT
            var result = await _controller.Create(validDto);

            // ASSERT
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.ActionName.Should().Be(nameof(FlowersController.GetById));
            createdResult.RouteValues!["id"].Should().Be(10);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenDomainRuleViolated_NegativePrice()
        {
            // ARRANGE: Fiyat -50 (Domain Kuralı İhlali)
            var invalidDto = new CreateFlowerDto("Hatalı", "Desc", "Color", "url", -50, 10, 1, false);

            // ACT
            var result = await _controller.Create(invalidDto);

            // ASSERT
            // Controller try-catch içinde Domain Exception'ı yakalayıp BadRequest dönmeli
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenDomainRuleViolated_EmptyName()
        {
            // ARRANGE: İsim Boş (Domain Kuralı İhlali)
            var invalidDto = new CreateFlowerDto("", "Desc", "Color", "url", 50, 10, 1, false);

            // ACT
            var result = await _controller.Create(invalidDto);

            // ASSERT
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenRepositoryThrowsException()
        {
            // ARRANGE: DB Bağlantı Hatası
            var validDto = new CreateFlowerDto("Test", "Desc", "Color", "url", 50, 10, 1, false);
            _mockRepo.Setup(x => x.AddAsync(It.IsAny<Flower>())).ThrowsAsync(new Exception("DB Error"));

            // ACT
            var result = await _controller.Create(validDto);

            // ASSERT
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // ==========================================
        // 🔴 3. UPDATE (GÜNCELLEME) SENARYOLARI
        // ==========================================

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // ARRANGE: Güncellenecek çiçek yok
            var updateDto = new CreateFlowerDto("Gül", "Desc", "Red", "url", 100, 50, 1, false);
            _mockRepo.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((Flower?)null);

            // ACT
            var result = await _controller.Update(99, updateDto);

            // ASSERT
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenUpdateIsSuccessful()
        {
            // ARRANGE: Var olan bir çiçeği güncelle
            var existingFlower = Flower.Load(1, "Eski İsim", "Desc", "Red", "url", 50, 10, 1, true, false);
            var updateDto = new CreateFlowerDto("Yeni İsim", "New Desc", "Red", "url", 150, 20, 1, true);

            _mockRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(existingFlower);
            _mockRepo.Setup(x => x.UpdateAsync(It.IsAny<Flower>())).Returns(Task.CompletedTask);

            // ACT
            var result = await _controller.Update(1, updateDto);

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();
            // Repo'nun UpdateAsync metodunun çağrıldığını doğrulayalım (Verify)
            _mockRepo.Verify(x => x.UpdateAsync(It.Is<Flower>(f => f.Name == "Yeni İsim" && f.Price == 150)), Times.Once);
        }

        // ==========================================
        // 🗑️ 4. DELETE (SİLME) & KATEGORİ SENARYOLARI
        // ==========================================

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenCalled()
        {
            // ARRANGE
            _mockRepo.Setup(x => x.DeleteAsync(1)).Returns(Task.CompletedTask);

            // ACT
            var result = await _controller.Delete(1);

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();
            _mockRepo.Verify(x => x.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task AddCategory_ShouldReturnBadRequest_WhenNameIsEmpty()
        {
            // ARRANGE: Boş kategori ismi
            string emptyCategoryName = "   ";

            // ACT
            var result = await _controller.AddCategory(emptyCategoryName);

            // ASSERT
            result.Should().BeOfType<BadRequestObjectResult>();
            // Repo'ya hiç gidilmediğini doğrula
            _mockRepo.Verify(x => x.AddCategoryAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task AddCategory_ShouldReturnOk_WhenNameIsValid()
        {
            // ARRANGE
            string categoryName = "Orkideler";
            _mockRepo.Setup(x => x.AddCategoryAsync(categoryName)).ReturnsAsync(5);

            // ACT
            var result = await _controller.AddCategory(categoryName);

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}