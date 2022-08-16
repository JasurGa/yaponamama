using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class FileController : BaseController
    {
        private static Random random = new Random();

        private static string[] VideoExentensions = new string[]
        {
            "mp4",
            "avi",
        };

        private static string[] ImageExtensions = new string[]
        {
            "jpg",
            "png",
            "gif",
            "jpeg",
        };

        private static string GenerateRandomHash()
        {
            var alphabet = "0123456789abcdef";
            return new string(Enumerable.Repeat(alphabet, 32)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GetContentTypeFromExtension(string extension)
        {
            if (extension.StartsWith("."))
            {
                extension = extension.Substring(1);
            }

            if (VideoExentensions.Contains(extension))
            {
                return $"video/{extension}";
            }

            if (ImageExtensions.Contains(extension))
            {
                return $"image/{extension}";
            }

            if (extension == "")
                return "application/octet-stream";

            return $"application/{extension}";
        }

        /// <summary>
        /// Downloads file
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/file/download/0123456789abcdef0123456789abcdef.png
        ///     
        /// </remarks>
        /// <param name="fileName">File name</param>
        /// <returns>Returns file content</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        [HttpGet("download/{fileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DownloadFileAsync([FromRoute] string fileName)
        {
            var path = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(),
                "uploads", fileName));

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            var stream = new FileStream(path, FileMode.Open,
                FileAccess.Read, FileShare.Read);

            var extension = Path.GetExtension(fileName);
            return File(stream, GetContentTypeFromExtension(extension), true);
        }

        /// <summary>
        /// Uploads file
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/file/upload
        ///     === File ===
        ///     
        /// </remarks>
        /// <param name="formFile">File</param>
        /// <returns>Returns filename (string)</returns>
        /// <response code="200">Uploaded</response>
        /// <response code="400">BadRequest</response>
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UploadFileAsync(IFormFile formFile)
        {
            if (formFile == null)
            {
                return BadRequest();
            }

            var extension = Path.GetExtension(formFile.FileName);
            if (extension == null)
            {
                return BadRequest();
            }

            extension = extension.ToLower().Trim();

            string path;
            string filename;
            do
            {
                filename = string.Concat(GenerateRandomHash(), extension);
                path     = Path.Combine(Directory.GetCurrentDirectory(),
                    "uploads", filename);
            }
            while (System.IO.File.Exists(path));

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return Ok(filename);
        }
    }
}
