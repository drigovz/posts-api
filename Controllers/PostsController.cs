using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PostsApi.Repository;
using PostsApi.DTOs;
using PostsApi.Models;

namespace PostsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public PostsController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostDTO>> GetAll()
        {
            try
            {
                var posts = _uof.PostsRepository.Get().ToList();
                return _mapper.Map<List<PostDTO>>(posts);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPostAsync([BindRequired] int id)
        {
            try
            {
                var post = await _uof.PostsRepository.GetByIdAsync(p => p.Id == id);
                if (post == null)
                    return NotFound($"The post index {id} not found!");
                else
                {
                    var postDTO = _mapper.Map<PostDTO>(post);
                    return new ObjectResult(postDTO);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect to server");
            }
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostDateAsync([BindRequired] DateTime date)
        {
            try
            {
                var posts = await _uof.PostsRepository.GetPostsForDate(date);
                if (posts == null)
                    return StatusCode(StatusCodes.Status200OK, $"No post registred with date {date}");
                else
                {
                    return _mapper.Map<IEnumerable<PostDTO>>(posts).ToList();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostCategoryAsync([BindRequired] int category)
        {
            try
            {
                var posts = await _uof.PostsRepository.GetPostsForCategory(category);
                if (posts == null)
                    return StatusCode(StatusCodes.Status200OK, $"No posts in category {category}");
                else
                {
                    return _mapper.Map<IEnumerable<PostDTO>>(posts).ToList();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpGet("top/")]
        public ActionResult<IEnumerable<PostDTO>> GetTop()
        {
            try
            {
                var posts = _uof.PostsRepository.Get().OrderBy(p => p.NumLikes).ToList();
                if (posts == null)
                    return StatusCode(StatusCodes.Status200OK, $"No posts registred");
                else
                {
                    return _mapper.Map<List<PostDTO>>(posts);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpGet("comment/")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetMostCommented()
        {
            try
            {
                var posts = await _uof.PostsRepository.GetMostCommentedsPosts();
                if (posts == null)
                    return StatusCode(StatusCodes.Status200OK, $"No posts registred");
                else
                {
                    return _mapper.Map<List<PostDTO>>(posts);
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpGet("{tagname:alpha}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostTagAsync([BindRequired] string tagname)
        {
            try
            {
                var posts = await _uof.PostsRepository.GetPostsByTag(tagname);
                if (posts == null)
                    return StatusCode(StatusCodes.Status200OK, $"No post registred with tag {tagname}");
                else
                {
                    return _mapper.Map<IEnumerable<PostDTO>>(posts).ToList();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to connect on server");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PostDTO postDTO)
        {
            try
            {
                if (postDTO == null)
                    return BadRequest();

                var post = _mapper.Map<Post>(postDTO);
                _uof.PostsRepository.Add(post);
                await _uof.Commit();

                var result = _mapper.Map<PostDTO>(post);
                return new ObjectResult(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to create a new post");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAsync([BindRequired] int id, [FromBody] PostDTO postDTO)
        {
            try
            {
                if (postDTO == null || postDTO.Id != id)
                    return BadRequest($"Post with id {id} not found");

                var post = _mapper.Map<Post>(postDTO);
                post.UpdatedAt = DateTime.UtcNow;
                _uof.PostsRepository.Update(post);
                await _uof.Commit();

                return StatusCode(StatusCodes.Status200OK, $"Post id {id} update succesfull");
            }
            catch //(Exception ex)
            {
                //throw ex;
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when try to update post id {id}");
            }
        }
    }
}