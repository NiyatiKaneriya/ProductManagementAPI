using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Repositories.Interfaces;
using ProductManagementAPI.Entities.ViewModels;
using ProductManagementAPI.Entity.Models;

namespace ProductManagementAPI.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;


        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CategoryDetails>> GetCategories()
        {
            try
            {
                return Ok(_categoryRepository.GetCategories());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDetails> GetCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                CategoryDetails category = _categoryRepository.GetCategoryDetails(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Category> CreateCategory(CategoryDetails category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }
                if (category.CategoryId > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                Category category1 = _categoryRepository.AddEditCategory(category);
                return Ok(category1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                 
                if (!_categoryRepository.DeleteCategory(id))
                {
                    return StatusCode(StatusCodes.Status409Conflict);
                }
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Category> EditCategory(int id, CategoryDetails category)
        {
            try
            {
                if (category == null || id != category.CategoryId)
                {
                    return BadRequest();
                }
                if (category.CategoryId > 0)
                {
                    Category category1 = _categoryRepository.AddEditCategory(category);
                    return Ok(category1);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                throw;
            }
            

        }
    }
}
