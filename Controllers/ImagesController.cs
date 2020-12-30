using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostsApi.DTOs;
using PostsApi.Models;
using PostsApi.Repository;

namespace PostsApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ImagesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private const string directoryPath = "\\files\\images\\posts\\";

        public ImagesController(IUnitOfWork uof, IMapper mapper, IWebHostEnvironment environment)
        {
            _uof = uof;
            _mapper = mapper;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ImageDTO imageDTO)
        {
            try
            {
                if (imageDTO == null)
                    return BadRequest();

                var image = _mapper.Map<Image>(imageDTO);
                _uof.ImagesRepository.Add(image);
                await _uof.Commit();

                var result = _mapper.Map<ImageDTO>(image);
                return new ObjectResult(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when try to create a new image");
            }
        }

        [HttpPost("upload")]
        public async Task<string> SendFile([FromForm] IFormFile fileUpload)
        {
            if (fileUpload.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + directoryPath))
                        Directory.CreateDirectory(_environment.WebRootPath + directoryPath);

                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + directoryPath + fileUpload.FileName))
                    {
                        await fileUpload.CopyToAsync(filestream);
                        filestream.Flush();
                        var directorySave = directoryPath + fileUpload.FileName;
                        return directorySave;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
            else
            {
                return "An error in upload files from server";
            }
        }
    }
}