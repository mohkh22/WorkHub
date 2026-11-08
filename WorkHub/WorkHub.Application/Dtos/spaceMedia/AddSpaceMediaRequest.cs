using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WorkHub.Domain.Enums;

namespace WorkHub.Application.Dtos.spaceMedia
{
    public class AddSpaceMediaRequest
    {
        [Required(ErrorMessage = "Space is required")]
        public Guid SpaceId { get;  set; }
        [Required(ErrorMessage = "MediaType is required")]
        [EnumDataType(typeof(MediaType), ErrorMessage = "Invalid MediaType")]
        public MediaType MediaType { get;  set; }
        [Required(ErrorMessage = "File is required")]
        public IFormFile File { get;  set; } = null!;

        public bool IsPrimary { get;  set; }

    }
}
