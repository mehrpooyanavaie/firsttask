using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using firsttask.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions;
using firsttask.Data;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using AutoMapper;
using firsttask.Repository;

namespace firsttask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyFirstController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public MyFirstController(IProductRepository productRepository, Data.MyFirstContext mydb, ICategoryRepository categoryRepository ,IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        [HttpGet("itemswithpagination")]
        public async Task<ActionResult<List<ProductViewModel>>> GetAllProductsAsync(int page = 1, int pagesize = 10)
        {
            var productsperpage = await _productRepository.GetAllProductsAsync(page, pagesize);
            return Ok(productsperpage);
        }
        [HttpGet("categorieswithpagination")]
        public async Task<ActionResult<List<CategoryViewModel>>> GetAllCategoriesAsync(int page = 1, int pagesize = 10)
        {
            var categoriesperpage = await _categoryRepository.GetAllCategoriesAsync(page, pagesize);
            return Ok(categoriesperpage);
        }
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductViewModel>> GetByIdProductAsync(int id)
        {
            var myreturndata = await _productRepository.GetByIdProductAsync(id);
            if (myreturndata == null)
                return NotFound();
            return Ok(myreturndata);
        }
        [HttpGet("category/{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetByIdCategoryAsync(int id)
        {
            var myreturndata = await _categoryRepository.GetByIdCategoryAsync(id);
            if (myreturndata == null)
                return NotFound();
            return Ok(myreturndata);
        }
        [HttpPost("addproduct")]
        public async Task<ActionResult<int>> AddProductAsync([FromForm] PostProductModel product)
        {
            if (!ModelState.IsValid)
                return BadRequest(-1);//return -1 if an error accures
            var tosaveid = await _productRepository.AddProductAsync(product);
            if (tosaveid == -1)
                return BadRequest(-1);
            return Ok(tosaveid);
        }
        [HttpPost("addcategory")]
        public async Task<ActionResult<int>> AddCategoryAsync(PostCategoryModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest(-1);//return -1 if an error accures
            var tosaveid = await _categoryRepository.AddCategoryAsync(category);
            if (tosaveid == -1)
                return BadRequest(-1);//return -1 if an error accures
            return Ok(tosaveid);
        }
        [HttpDelete("deleteproduct/{id}")]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            bool existornot = await _productRepository.DeleteProductAsync(id);
            if (!existornot)
                return NotFound();
            return NoContent();
        }
        [HttpDelete("deletecategory/{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            var mycategory = await _categoryRepository.DeleteCategoryAsync(id);
            if (!mycategory)
                return NotFound();
            return NoContent();
        }
        [HttpPut("editproduct/{id}")]
        public async Task<ActionResult<int>> EditProductAsync(int id, PostProductModel product)
        {
            if (ModelState.IsValid)
            {
                int tosaveid = await _productRepository.UpdateProductAsync(id, product);
                if (tosaveid == -1)
                    return BadRequest(-1);//return -1 if an error accures
                return Ok(id);
            }
            return BadRequest(-1);//return -1 if an error accures
        }
        [HttpPut("editcategory/{id}")]
        public async Task<ActionResult<int>> EditCategoryAsync(int id, PostCategoryModel category)
        {
            if (ModelState.IsValid)
            {
                var tosaveid = await _categoryRepository.UpdateCategoryAsync(id, category);
                if (tosaveid == -1)
                    return BadRequest(-1);//return -1 if an error accures
                return Ok(id);
            }
            return BadRequest(-1);//return -1 if an error accures
        }
    }
}
