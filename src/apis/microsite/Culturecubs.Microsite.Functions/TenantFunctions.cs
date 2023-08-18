//using System;
//using System.IO;
//using System.Net;
//using System.Threading.Tasks;
//using Culturecubs.Microsite.DataAccess.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
//using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;

//namespace Culturecubs.Microsite.Functions
//{
//    public class TenantFunctions
//    {
//        private readonly ILogger<TenantFunctions> _logger;

//        public TenantFunctions(ILogger<TenantFunctions> log)
//        {
//            _logger = log;
//        }

//        [FunctionName("validatetenant")]
//        [OpenApiOperation(operationId: "validatetenant", tags: new[] { "Tenant" })]
//        [OpenApiParameter(name: "code", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "TenantId code")]
//        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ThemeConfiguration), Description = "Returns Theme based on passed TenantId code if valid")]
//        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tenant/{code}")] HttpRequest req, Guid guid)
//        {

//            try
//            {
//                _logger.LogInformation("Get GetThemeByTenant code  request Started");
//                var result = await _themeConfigurationRepository.GetThemeByTenantId(guid);
//                return new OkObjectResult(result);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, $"Error while processing tenant/{guid} request.");
//                return new BadRequestObjectResult(Constants.ErrorMsg);
//            }
//        }
//    }
//}

