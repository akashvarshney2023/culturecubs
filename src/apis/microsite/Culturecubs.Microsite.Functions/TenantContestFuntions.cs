using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Culturecubs.Microsite.DataAccess.Models;
using Culturecubs.Microsite.DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Culturecubs.Microsite.Functions
{
    public class TenantContestFuntions
    {
        private readonly ILogger<TenantContestFuntions> _logger;
        private readonly ITenantContestRepository _tenantContestRepository;

        public TenantContestFuntions(ILogger<TenantContestFuntions> log, ITenantContestRepository tenantContestRepository)
        {
            _logger = log;
            _tenantContestRepository = tenantContestRepository;
        }

        [FunctionName("GetContestsByTenantId")]
        [OpenApiOperation(operationId: "getcontestsbytenantid", tags: new[] { "Contest" })]
        [OpenApiParameter(name: "guid", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "TenantId.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Contest>), Description = "Returns contests based on passed TenantId")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contest/{guid}")] HttpRequest req, Guid guid)
        {

            try
            {
                _logger.LogInformation("Get contest by Id request Started");
                var result = await _tenantContestRepository.GetContestsByTenantId(guid);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing tenant/{guid} request.");
                return new BadRequestObjectResult(Constants.ErrorMsg);
            }
        }


        [FunctionName("CreateOrUpdateContest")]
        [OpenApiOperation(operationId: "createorupdatecontest", tags: new[] { "Contest" })]
        [OpenApiParameter(name: "guid", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "TenantId.")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Contest), Required = true, Description = "Contest object to create or update.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Contest), Description = "Returns the created or updated contest.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Returns an error message if the request is invalid.")]
        public async Task<IActionResult> CreateOrUpdateContest([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "contest/{guid}")] HttpRequest req, Guid guid)
        {
            try
            {
                _logger.LogInformation("Create or Update contest request Started");

                // Read the request body and deserialize the Contest object
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var contest = JsonConvert.DeserializeObject<Contest>(requestBody);
                contest.TenantId = guid; // Set the TenantId from the route parameter

                // Call the create or update method
                var result = await _tenantContestRepository.CreateOrUpdateContest(contest);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while processing contest/{guid} request.");
                return new BadRequestObjectResult(Constants.ErrorMsg);
            }
        }


    }
}

