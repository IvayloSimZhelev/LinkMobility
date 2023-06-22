using API_LinkMob.Dto.Dtos;
using Enitities;

namespace API_LinkMob.Extentions
{
    public static class Extentions
    {
        public static CustomerDto asCustomerDto(this Customer customer)
        {
            return new CustomerDto(customer.Id, customer.CompanyName, customer.Address, customer.State, customer.Country, customer.FullAddress, customer.SubscriptionState, customer.Invoice, customer.NumberOfInvoices);
        }

        public static UserDto asUserDto(this User user)
        {
            return new UserDto(user.Id, user.Email, user.Name, user.Password);
        }
    }
}
