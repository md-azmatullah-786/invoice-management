using IM_API.Models;

namespace IM_API.Repository
{
    public interface IAccountRepository
    {
        public Customer ValidateUserDetails(LoginRequest loginRequest);
        public Customer GetCustomerDetails(int id);
    }
}
