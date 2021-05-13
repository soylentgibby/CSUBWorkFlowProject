using CSUBWorkFlowProject.Data.Repositories;
using CSUBWorkFlowProject.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSUBWorkFlowProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _LoginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _LoginRepository = loginRepository;
        }

        [HttpPost]
        public async Task<ActionResult<User>> UserLogin([FromBody] User userlogin)
        {
            try
            {
                var login = _LoginRepository.GetLogin(userlogin.Username, userlogin.Password);

                if (login == null)
                    return BadRequest();

                return Ok(login);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
