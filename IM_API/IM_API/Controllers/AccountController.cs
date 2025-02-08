using IM_API.Models;
using IM_API.Repository;
using IM_API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.UserName) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest(loginRequest);
            }
            
            Customer loginDetails = _accountRepository.ValidateUserDetails(loginRequest);
            
            if (loginDetails == null)
            {
                return Ok("Invalid Username and Password");
            }
            else
            {
                var resObj = new LoginResponse();
                resObj.Customer = loginDetails;
                resObj.Token = GenerateToken.generateToken(loginDetails);
                return Ok(resObj);
            }
        }

        [HttpGet]
        [Route("GetCustomerDetails/{id}")]
        public IActionResult GetCustomerDetails(int id)
        {
            Customer resData = _accountRepository.GetCustomerDetails(id);

            if (resData.CustomerId == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(resData);
            }
        }
    }
}
