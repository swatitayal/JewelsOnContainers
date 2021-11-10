using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public PicController(IWebHostEnvironment env)
        {
            _env = env;  //env will store the location of where the solution is stored on VM (docker implementation)
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetImage(int id)
        {
            var webRoot = _env.WebRootPath; //1
            var path = Path.Combine($"{webRoot}/Pics/", $"Ring{id}.jpg"); //2
            var buffer = System.IO.File.ReadAllBytes(path); //3
            return File(buffer, "image/jpeg"); 
        }
    }
}

//1. WebRootPath will have the loaction of wwwroot folder.
//2. Path will have the value as the path to access a particular file as C:\ProductCatalogAPI\wwwroot\Pics\Ring3.jpg. Instead of
//   sending the path to the user, we need to send the file(image) present at that path.
//3. Since image is not a text file and instead is a binary file, hence we are using File.ReadAllBytes function 
//   So buffer will have the byte format of that image.
//4. File belongs to ControllerBase
//   Will tell Client that the file sent (Buffer) is a binary file of type "jpeg" Thus, the corresponding image will be formed.