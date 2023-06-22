using API_LinkMob.Dto.Dtos;
using Enitities;

namespace API_LinkMob.Extentions
{
    public static class Extentions
    {
        public static CustomerDto asDto(this Customer customer)
        {
            return new CustomerDto(customer.Id, customer.CompanyName, customer.Address, customer.State, customer.Country, customer.FullAddress, customer.SubscriptionState, customer.Invoice, customer.NumberOfInvoices);

        }
    }
}
