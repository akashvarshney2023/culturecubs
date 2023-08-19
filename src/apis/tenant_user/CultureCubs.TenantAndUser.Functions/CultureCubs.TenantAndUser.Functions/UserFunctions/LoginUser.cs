using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Culturecubs.TenantAndUser.DataAccess.Repository;
using CultureCubs.TenantAndUser.Functions.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CultureCubs.TenantAndUser.Functions.UserFunctions
{
    public class LoginUser
    {
        private readonly ILogger<LoginUser> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginUser(ILogger<LoginUser> log,IUserRepository userRepository,ITokenService tokenService)
        {
            _logger = log;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [FunctionName("login")]
        [OpenApiOperation(operationId: "login", tags: new[] { "User" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Userlogin), Required = true, Description = "User login details")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Token response")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string), Description = "Bad request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Description = "Unauthorized")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login")] HttpRequest req)
        {
            try
            {
                // Read the request body
                var requestBodyStream = req.Body;
                var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

                // Parse the request body
                var requestBody = JsonConvert.DeserializeObject<Userlogin>(requestBodyText);

                if (string.IsNullOrEmpty(requestBody.UserName) || string.IsNullOrEmpty(requestBody.Password))
                {
                    return new BadRequestObjectResult("UserName or Password cannot be empty");
                }

                // Replace this with your actual user retrieval logic
                var user = await _userRepository.GetUsersByUserName(requestBody.UserName);
                if (user == null || !Helper.VerifyUserPassword(requestBody.Password, user.Password))
                {
                    return new UnauthorizedResult();
                }

                var token = _tokenService.GenerateToken(user.UserName);

                return new OkObjectResult(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the login function.");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        public class Userlogin
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}

