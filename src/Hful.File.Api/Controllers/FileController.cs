using Hful.Core.Application;
using Hful.File.Dto;
using Hful.File.Service;
using Hful.File.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hful.File.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("file")]
    public class FileController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public FileController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpPost]
        [Route("upload")]
        public Task<AttachmentUploadDto> UploadAsync([FromForm] IFormFile file)
        {
            // todo tenant
            return _attachmentService.UploadFileAsync(file.OpenReadStream(), file.Name, null);
        }

        [HttpGet]
        [Route("/download/{id}")]
        public async Task<ActionResult> DownloadAsync([FromRoute] Guid id)
        {
            var data = await _attachmentService.DownloadFileAsync(id);
            if (data == null || data.Stream == null)
            {
                return NotFound();
            }

            return File(data.Stream, MimeMapping.GetMimeType(data.Extension), data.Name);
        }
    }
}
