using AutoMapper;

using Hful.File.Domain;
using Hful.File.Dto;

namespace Hful.File
{
    public class AttachmentProfile : Profile
    {
        protected AttachmentProfile()
        {
            CreateMap<Attachment, AttachmentUploadDto>();
        }
    }
}
