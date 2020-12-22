using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PostsApi.DTOs;
using PostsApi.Models;
using PostsApi.Repository;

namespace PostsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetAll()
        {
            try
            {
                var category = _uof.CategoriesRepository.Get().Include(c => c.Posts).ToList();
                return _mapper.Map<List<CategoryDTO>>(category);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync([BindRequired] int id)
        {
            try
            {
                var category = await _uof.CategoriesRepository.GetByIdAsync(c => c.Id == id);
                if (category == null)
                    return NotFound($"Category with id {id} not found");
                else
                {
                    var categoryDTO = _mapper.Map<CategoryDTO>(category);
                    return new ObjectResult(categoryDTO);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect to server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                if (categoryDTO == null)
                    return BadRequest();

                var category = _mapper.Map<Category>(categoryDTO);
                _uof.CategoriesRepository.Add(category);
                await _uof.Commit();

                var result = _mapper.Map<CategoryDTO>(category);
                return new ObjectResult(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to add category");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync([BindRequired] int id, [FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                if (categoryDTO == null || id != categoryDTO.Id)
                    return BadRequest($"Category with id {id} not found");
                else
                {
                    var category = _mapper.Map<Category>(categoryDTO);
                    category.UpdatedAt = DateTime.UtcNow;
                    _uof.CategoriesRepository.Update(category);
                    await _uof.Commit();

                    return StatusCode(StatusCodes.Status200OK, $"Category id {id} update succesfull");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when try to update category id {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([BindRequired] int id)
        {
            try
            {
                var category = await _uof.CategoriesRepository.GetByIdAsync(c => c.Id == id);
                if (category == null)
                    return NotFound($"Category id {id} not found");

                _uof.CategoriesRepository.Delete(category);
                await _uof.Commit();

                return StatusCode(StatusCodes.Status200OK, "Category deleted succesfull");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when try to delete category id {id}");
            }
        }
    }
}