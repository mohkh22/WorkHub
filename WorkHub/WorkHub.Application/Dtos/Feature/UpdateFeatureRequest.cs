using System.ComponentModel.DataAnnotations;

namespace WorkHub.Application.Dtos.Feature
{
    public class UpdateFeatureRequest
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Name must be less than or equal 50")]

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

}
