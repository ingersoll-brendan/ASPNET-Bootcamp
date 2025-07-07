namespace Bootcamp.Web.Api.Models
{
    public class CustomerDto
    {
        public required int Id { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required long PhoneNumber { get; set; }

        public required ICollection<AddressDto> Addresses { get; set; } = new List<AddressDto>();
        public required ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
