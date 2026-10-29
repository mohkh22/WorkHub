using System.ComponentModel.DataAnnotations;
using WorkHub.Application.Validations;
using WorkHub.Domain.Enums;

namespace WorkHub.Application.Dtos.Space
{
    public class UpdateSpaceRequest
    {
        [Required(ErrorMessage = "Space number is required.")]
        [MaxLength(20, ErrorMessage = "Space number cannot exceed 20 characters.")]
        public string SpaceNumber { get; set; } = null!;
        public string? Description { get; set; }

        [EnumDataType(typeof(SpaceType))]
        public SpaceType Type { get; set; }

        [Required(ErrorMessage = "Minimum capacity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum capacity must be greater than 0.")]
        public int MinCapacity { get; set; }
        [Required(ErrorMessage = "Maximum capacity is required.")]
        [GreaterThanOrEqual(nameof(MinCapacity), "Maximum capacity must be greater than or equal to minimum capacity.")]
        public int MaxCapacity { get; set; }
        [Required(ErrorMessage = "Hourly price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Hourly price must be greater than 0.")]
        public decimal HourlyPrice { get; set; }
        [Required(ErrorMessage = "Daily price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Daily price must be greater than 0.")]
        public decimal DailyPrice { get; set; }
    }
}
