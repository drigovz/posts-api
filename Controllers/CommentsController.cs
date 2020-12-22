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
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CommentsController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommentDTO>> GetAllComments()
        {
            try
            {
                var comments = _uof.CommentsRepository.Get();
                return _mapper.Map<List<CommentDTO>>(comments);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentAsync([BindRequired] int id)
        {
            try
            {
                var comment = await _uof.CommentsRepository.GetByIdAsync(c => c.Id == id);
                if (comment == null)
                    return NotFound($"The comment index {id} not found!");
                else
                {
                    var commentDTO = _mapper.Map<CommentDTO>(comment);
                    return new ObjectResult(commentDTO);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect to server");
            }
        }

        [HttpGet("posts/{id}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsOfPostAsync([BindRequired] int id)
        {
            try
            {
                var comments = await _uof.CommentsRepository.GetCommentsOfPost(id);
                return _mapper.Map<List<CommentDTO>>(comments);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect to server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CommentDTO commentDTO)
        {
            try
            {
                if (commentDTO == null)
                    return BadRequest();

                var comment = _mapper.Map<Comment>(commentDTO);
                _uof.CommentsRepository.Add(comment);
                await _uof.Commit();

                var result = _mapper.Map<CommentDTO>(comment);
                return new ObjectResult(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to create a new post");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAsync([BindRequired] int id, [FromBody] CommentDTO commentDTO)
        {
            try
            {
                if (commentDTO == null || commentDTO.Id != id)
                    return BadRequest($"Comment with id {id} not found");

                var comment = _mapper.Map<Comment>(commentDTO);
                _uof.CommentsRepository.Update(comment);
                await _uof.Commit();

                return StatusCode(StatusCodes.Status200OK, $"Comment id {id} update succesfull");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when try to update comment id {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([BindRequired] int id)
        {
            try
            {
                var comment = await _uof.CommentsRepository.GetByIdAsync(c => c.Id == id);
                if (comment == null)
                    return NotFound($"Comment with id {id} not found");

                _uof.CommentsRepository.Delete(comment);
                await _uof.Commit();

                return StatusCode(StatusCodes.Status200OK, "Comment deleted succesfull");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when try to delete comment id {id}");
            }
        }
    }
}