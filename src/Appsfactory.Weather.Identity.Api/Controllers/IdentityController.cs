using Appsfactory.Weather.Identity.Api.Options;
using Appsfactory.Weather.Identity.Api.Utilities.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : BaseController<IdentityController>
    {
        #region PROPERTIES
        private readonly JWTOptions _jwtOptions;
        private readonly UserOptions _userOptions;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// 
        /// </summary>
        public IdentityController(IOptions<JWTOptions> jwtOptions, IOptions<UserOptions> userOptions)
        {
            _jwtOptions = jwtOptions.Value;
            _userOptions = userOptions.Value;
        }
        #endregion

        #region GET
        /// <summary>
        /// Login with user credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string userName, string password)
        {
            //TODO:implement identity service and handle login with identity
            TokenHandler._jwtOptions = _jwtOptions;
            TokenHandler._userOptions = _userOptions;
            return Ok(userName == _userOptions.UserName && password == _userOptions.Password ? TokenHandler.CreateAccessToken() : new UnauthorizedResult());
        }
        #endregion
    }
}
