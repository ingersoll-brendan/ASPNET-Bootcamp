namespace Bootcamp.Web.Api.Models
{
    public class AddressDto
    {
        public required int Id { get; set; }

        public required string Street1 { get; set; }
        public string? Street2 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required int Zip { get; set; }

        public required AddressTypeDto AddressType { get; set; }
    }
}
