using Enitities;

namespace API_LinkMob.Dto.Dtos
{
    public record CustomerDto
    {
        private string? fullAddress;
        private List<Invoice>? invoice;

        public CustomerDto(Guid id, string? companyName, string? address, string? state, string? country, string? fullAddress, string? subscriptionState, List<Invoice>? invoice, int numberOfInvoices)
        {
            Id = id;
            CompanyName = companyName;
            Address = address;
            State = state;
            Country = country;
            this.fullAddress = fullAddress;
            SubscriptionState = subscriptionState;
            this.invoice = invoice;
            NumberOfInvoices = numberOfInvoices;
        }

        public Guid Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? FullAddress
        {
            get
            {
                return $"{Address} {State} {Country}";
            }
        }
        public string? SubscriptionState { get; set; }
        public List<InvoiceDto>? Invoice { get; set; }
        public int NumberOfInvoices { get; set; }
    }

    public record InvoiceDto
    {
        public Guid Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }


}
