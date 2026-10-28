using WorkHub.Domain.Enums;

namespace WorkHub.Domain.Entities
{
    public class Space:ISoftDeletable
    {

        public Guid Id { get; private set; }
        public string SpaceNumber { get; private set; } = null!;
        public string? Description { get; private set; }

        public SpaceStatus Status { get; private set; }

        public SpaceType Type { get; private set; }

        public int MinCapacity { get; private set; }
        public int MaxCapacity { get; private set; }
        public decimal HourlyPrice { get; private set; }
        public decimal DailyPrice { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }

        public bool IsDeleted { get; private set; }


        private Space()
        {

        }

        public static Space Create(string spaceNumber, string? description, SpaceType type, int minCapacity, int maxCapacity, decimal hourlyPrice, decimal dailyPrice)
        {
            if (string.IsNullOrWhiteSpace(spaceNumber))
            {
                throw new ArgumentException("Space number cannot be null or empty.", nameof(spaceNumber));
            }

            if (minCapacity <= 0)
            {
                throw new ArgumentException("Minimum capacity cannot be less than or equal to zero.", nameof(minCapacity));
            }

            if (maxCapacity < minCapacity)
            {
                throw new ArgumentException("Maximum capacity cannot be less than minimum capacity.", nameof(maxCapacity));
            }

            if (hourlyPrice <= 0)
            {
                throw new ArgumentException("Hourly price cannot be less than or equal to zero.", nameof(hourlyPrice));
            }

            if (dailyPrice <= 0)
            {
                throw new ArgumentException("Daily price cannot be less than or equal to zero.", nameof(dailyPrice));
            }

            if (Enum.IsDefined(typeof(SpaceType), type) == false)
            {
                throw new ArgumentException("Invalid space type.", nameof(type));
            }

            return new Space
            {
                Id = Guid.NewGuid(),
                SpaceNumber = spaceNumber,
                Description = description,
                Status = SpaceStatus.Available,
                Type = type,
                MinCapacity = minCapacity,
                MaxCapacity = maxCapacity,
                HourlyPrice = hourlyPrice,
                DailyPrice = dailyPrice,
                CreatedAtUtc = DateTime.UtcNow,
                IsDeleted = false
            };
        }

        public void UpdateCapacity(int minCapacity, int maxCapacity)
        {
            if (minCapacity <= 0)
            {
                throw new ArgumentException("Minimum capacity cannot be less than or equal to zero.", nameof(minCapacity));
            }
            if (maxCapacity < minCapacity)
            {
                throw new ArgumentException("Maximum capacity cannot be less than minimum capacity.", nameof(maxCapacity));
            }
           this.MinCapacity = minCapacity;
           this.MaxCapacity = maxCapacity;
        }

        public void UpdatePricing(decimal hourlyPrice, decimal dailyPrice)
        {
            if (hourlyPrice <= 0)
            {
                throw new ArgumentException("Hourly price cannot be less than or equal to zero.", nameof(hourlyPrice));
            }
            if (dailyPrice <= 0)
            {
                throw new ArgumentException("Daily price cannot be less than or equal to zero.", nameof(dailyPrice));
            }
            this.HourlyPrice = hourlyPrice;
            this.DailyPrice = dailyPrice;
        }

        public void UpdateDetails(string spaceNumber,string? description)
        {
            if (string.IsNullOrWhiteSpace(spaceNumber))
            {
                throw new ArgumentException("Space number cannot be null or empty.", nameof(spaceNumber));
            }
            this.Description = description;
            this.SpaceNumber = spaceNumber;
        }

        public void SoftDelete()
        {
            if (!this.IsDeleted)
            {
                this.IsDeleted = true; 
            }
        }

        public void Restore()
        {
            if (this.IsDeleted)
            {
                this.IsDeleted = false;
            }
        }

    }
}
