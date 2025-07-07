namespace Bootcamp.Web.Api.Models
{
    public class OrderDto
    {
        public required int Id { get; set; }
        public required int CustomerId { get; set; }
        public required int BillingAddressId { get; set; }
        public required int ShippingAddressId { get; set; }

        public int OrderNumber { get; set; }

        public required CustomerDto Customer { get; set; }
        public required AddressDto BillingAddress { get; set; }
        public required AddressDto ShippingAddress { get; set; }
    }
}
