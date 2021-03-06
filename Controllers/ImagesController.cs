using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    string[] extensions = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".tiff", ".bmp", ".svg", ".webp" };
                    string directory = "/uploads/posts-images/";
                    List<ImageDTO> listImages = new List<ImageDTO>();

                    if (!Directory.Exists(_environment.WebRootPath + directory))
                        Directory.CreateDirectory(_environment.WebRootPath + directory);

                    foreach (var file in fileUpload)
                    {
                        string extension = Path.GetExtension(file.FileName.ToString().Trim()),
                               guid = Guid.NewGuid().ToString("n"),
                               guidFormated = String.Format("{0}-{1}-{2}", guid.Substring(0, 4), guid.Substring(5, 4), guid.Substring(8, 4)),
                               image = $"img_{guidFormated}{extension}",
                               path = $"{directory}{image}";

                        if (!extensions.Contains(extension))
                            return BadRequest($"The extension {extension} not supported!");

                        using (var stream = System.IO.File.Create(_environment.WebRootPath + directory + image))
                            await file.CopyToAsync(stream);

                        ImageDTO obj = new ImageDTO() { Description = image, Url = path, PostId = postId };
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
                return Ok("Request does not contain images");
        }
    }
}