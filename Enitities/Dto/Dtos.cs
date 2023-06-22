using Enitities;
using System.ComponentModel.DataAnnotations;

namespace API_LinkMob.Dto.Dtos
{
    public record CustomerDto(Guid id, [Required] string? companyName, [Required] string? address, [Required] string? state, [Required] string? country, string? fullAddress, string? subscriptionState, List<Invoice>? invoice, int numberOfInvoices);

    public record UserDto(Guid id, string? email, [Required] string? name, [Required] string? password);

    public record InvoiceDto(Guid id, string? invoiceNumber, DateTime? date, decimal total);
}