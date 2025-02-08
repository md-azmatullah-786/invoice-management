using IM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IM_API.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly InvoiceContext _context;

        public AccountRepository(InvoiceContext context)
        {
            _context = context;
        }

        public Customer ValidateUserDetails(LoginRequest loginRequest)
        {
            Customer customer = new Customer();
            customer = _context.Customer.FirstOrDefault(x => x.Username == loginRequest.UserName && x.Password == loginRequest.Password);
            return customer;
        }

        public Customer GetCustomerDetails(int id)
        {
            Customer customer = new Customer();
            customer = _context.Customer.Include(x => x.Invoices).FirstOrDefault(x => x.CustomerId == id) ?? new Customer();
            return customer;
        }
    }
}
