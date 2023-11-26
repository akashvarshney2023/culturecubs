using ContentManagement.DataContext;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContentManagement.Model;
using ContentManagement.DTO;
using Microsoft.EntityFrameworkCore;

namespace ContentManagement.Controllers
{
    [Route("api/[controller]")]
    public class ContentManagementController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ContentManagementController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("addContent")]
        public IActionResult AddContent([FromBody] ContentMnagementDTO contentDTO)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    _dbContext.Content.Add(new ContentManagement.Model.ContentManagement
                    {
                        Key = contentDTO.Key,
                        TenantId = contentDTO.TenantId,
                        DateCreated = DateTime.Now

                    });
                    _dbContext.SaveChanges();
                    var contentEntity = _dbContext.Content
                    .OrderByDescending(e => e.DateCreated) // Order by the timestamp in descending order
                     .FirstOrDefault();

                    _dbContext.Microsite.Add(new Microsite
                    {
                        ContentId = contentEntity.ContentId,//contentEntity.ContentId,
                        DateCreated = DateTime.Now,
                        Value = contentDTO.microsite.Value,
                        Key = contentDTO.microsite.Key,
                        description = contentDTO.microsite.description,
                    });
                    _dbContext.SaveChanges();

                    var microSiteEntity = _dbContext.Microsite
                    .OrderByDescending(e => e.DateCreated) // Order by the timestamp in descending order
                     .FirstOrDefault();
                    ;
                    if (contentDTO.microsite.Pages != null && contentDTO.microsite.Pages.Count > 0)
                    {
                        foreach (var page in contentDTO.microsite.Pages)
                        {

                            _dbContext.Page.Add(new Page
                            {
                                MicrositeId = microSiteEntity.MicrositeId,// microSiteEntity.MicrositeId,
                                DateCreated = DateTime.Now,
                                Description = page.Description,
                                Key = page.Key,
                                Value = page.Content
                            });
                            _dbContext.SaveChanges();
                        }
                    }


                    transaction.Commit();
                    // return CreatedAtAction( new { id =  }, );
                    return Ok(contentEntity.ContentId);


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest($"An error occurred: {ex.Message}");

                }
            }

        }

        [HttpGet("getContentByContentKey")]
        public async Task<ActionResult<ContentManagement.Model.ContentManagement>> GetTenant(string key)
        {
            var contentManagement = await _dbContext.Content.FindAsync(key);
            if (contentManagement == null)
            {
                return NotFound();
            }
            return Ok(contentManagement);
        }

        [HttpGet("getContentByTenantId")]
        public async Task<ActionResult<ContentMnagementDTO>> getContentByTenantId(int tenantId)
        {
            var contentDto = new ContentMnagementDTO();

            try
            {
                var tenantIdLong = Convert.ToInt64(tenantId);
                var contentManagement = _dbContext.Content.FirstOrDefault(c => c.TenantId == tenantId);
                if (contentManagement == null)
                {
                    return NotFound("There is no matching conetent for given key");
                }
                contentDto.TenantId = tenantId;
                contentDto.DateCreated = contentManagement.DateCreated;
                contentDto.Key = contentManagement.Key;
                var micrositeEntity = _dbContext.Microsite.FirstOrDefault(m => m.ContentId == contentManagement.ContentId);
                if (micrositeEntity == null)
                {
                    return contentDto;
                }
                contentDto.microsite = new MicrositeDTO()
                {
                    DateCreated = micrositeEntity.DateCreated,
                    description = micrositeEntity.description,
                    Value = micrositeEntity.Value,

                };

                var pageEntity = _dbContext.Page.Where(p => p.MicrositeId == micrositeEntity.MicrositeId).ToList();
                if (pageEntity == null)
                {
                    return contentDto;
                }
                contentDto.microsite.Pages = new List<PageDTO>();

                foreach (var item in pageEntity)
                {
                    contentDto.microsite.Pages.Add(new PageDTO()
                    {
                        DateCreated = item.DateCreated,
                        Key = item.Key, 
                        Content = item.Value,
                        Description = item.Description

                    });
                }
            }
            catch (Exception ex)
            {

                return contentDto;
            }

            return Ok(contentDto);
        }

    }
}
