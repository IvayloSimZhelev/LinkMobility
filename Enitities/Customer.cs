﻿namespace Enitities
{
    public class Customer
    {
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
        public List<Invoice>? Invoice { get; set; }
        public int NumberOfInvoices { get; set; }
    }
}
