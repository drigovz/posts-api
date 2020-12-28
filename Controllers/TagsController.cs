using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PostsApi.DTOs;
using PostsApi.Models;
using PostsApi.Repository;

namespace PostsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TagsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public TagsController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TagDTO>> GetAllTags()
        {
            try
            {
                var tags = _uof.TagsRepository.Get();
                return _mapper.Map<List<TagDTO>>(tags);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagAsync([BindRequired] int id)
        {
            try
            {
                var tag = await _uof.TagsRepository.GetByIdAsync(c => c.Id == id);
                if (tag == null)
                    return NotFound($"The tag index {id} not found!");
                else
                {
                    var tagDTO = _mapper.Map<TagDTO>(tag);
                    return new ObjectResult(tagDTO);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect to server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TagDTO tagDTO)
        {
            try
            {
                if (tagDTO == null)
                    return BadRequest();

                var tag = _mapper.Map<Tag>(tagDTO);
                _uof.TagsRepository.Add(tag);
                await _uof.Commit();

                var result = _mapper.Map<TagDTO>(tag);
                return new ObjectResult(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to create a new tag");
            }
        }

        [HttpPost("posts")]
        public async Task<IActionResult> CreateAsync([FromBody] PostTagDTO postTagDTO)
        {
            try
            {
                if (postTagDTO == null)
                    return BadRequest();

                var postTag = _mapper.Map<PostTag>(postTagDTO);
                _uof.PostsTagsRepository.Add(postTag);
                await _uof.Commit();

                var result = _mapper.Map<PostTagDTO>(postTag);
                return new ObjectResult(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to create a new tag");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAsync([BindRequired] int id, [FromBody] TagDTO tagDTO)
        {
            try
            {
                if (tagDTO == null || tagDTO.Id != id)
                    return BadRequest($"Tag with id {id} not found");

                var tag = _mapper.Map<Tag>(tagDTO);
                tag.UpdatedAt = DateTime.UtcNow;
                tag.CreatedAt = tag.CreatedAt;
                _uof.TagsRepository.Update(tag);
                await _uof.Commit();

                return StatusCode(StatusCodes.Status200OK, $"Tag id {id} update succesfull");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when try to update tag id {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([BindRequired] int id)
        {
            try
            {
                var tag = await _uof.TagsRepository.GetByIdAsync(c => c.Id == id);
                if (tag == null)
                    return NotFound($"Tag with id {id} not found");

                _uof.TagsRepository.Delete(tag);
                await _uof.Commit();

                return StatusCode(StatusCodes.Status200OK, "Tag deleted succesfull");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when try to delete tag id {id}");
            }
        }
    }
}