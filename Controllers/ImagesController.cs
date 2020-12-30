using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
    public class ImagesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public ImagesController(IUnitOfWork uof, IMapper mapper, IWebHostEnvironment environment)
        {
            _uof = uof;
            _mapper = mapper;
            _environment = environment;
        }

        [HttpPost("upload/{postId:int}")]
        public async Task<IActionResult> SendFile([FromForm] List<IFormFile> fileUpload, [BindRequired] int postId)
        {
            if (fileUpload.Count > 0)
            {
                try
                {
                    string directoryPath = "/uploads/posts-images/";
                    List<ImageDTO> listImages = new List<ImageDTO>();

                    if (!Directory.Exists(_environment.WebRootPath + directoryPath))
                        Directory.CreateDirectory(_environment.WebRootPath + directoryPath);

                    foreach (var file in fileUpload)
                    {
                        string extension = Path.GetExtension(file.FileName.ToString().Trim()),
                               guid = Guid.NewGuid().ToString("n"),
                               guidFormat = String.Format("{0}-{1}-{2}", guid.Substring(0, 4), guid.Substring(5, 4), guid.Substring(8, 4)),
                               originalPath = $"{directoryPath}img_{guidFormat}{extension}",
                               image = $"img_{guidFormat}{extension}";

                        using (var stream = System.IO.File.Create(_environment.WebRootPath + directoryPath + image))
                        {
                            await file.CopyToAsync(stream);
                        }

                        ImageDTO obj = new ImageDTO() { Description = image, Url = originalPath, PostId = postId };
                        var resultImage = _mapper.Map<Image>(obj);
                        _uof.ImagesRepository.Add(resultImage);
                        await _uof.Commit();

                        var resultImageDTO = _mapper.Map<ImageDTO>(resultImage);
                        listImages.Add(resultImageDTO);
                    }

                    return Ok(listImages);
                }
                catch (Exception ex)
                {
                    return BadRequest("An error ocurred: \n" + ex.Message.ToString());
                }
            }
            else
            {
                return Ok("Request does not contain images");
            }
        }
    }
}