using Culturecubs.Microsite.DataAccess.DataContext;
using Culturecubs.Microsite.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Culturecubs.Microsite.DataAccess.Repository
{
    public class TenantContestRepository : ITenantContestRepository
    {
        private readonly ApplicationDbContext _ctx;
        public TenantContestRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Contest> CreateOrUpdateContest(Contest contest)
        {
            // Add or update the Contest entry
            if (_ctx.Entry(contest).State == EntityState.Detached)
            {
                _ctx.Contests.Attach(contest);
            }

            _ctx.Entry(contest).State = contest.Id == 0 ? EntityState.Added : EntityState.Modified;

            // Handle ContestTab entries
            if (contest.Tabs != null)
            {
                foreach (var tab in contest.Tabs)
                {
                    if (tab.Id == 0)
                    {
                        // New ContestTab entry, add it
                        await _ctx.ContestTabs.AddAsync(tab);
                    }
                    else
                    {
                        // Existing ContestTab entry, update it
                        _ctx.Entry(tab).State = EntityState.Modified;
                    }
                }

                //TODO: Remove ContestTab entries that were deleted by the user
                //var tabsToDelete = await _ctx.ContestTabs
                //    .Where(existingTab => existingTab.ContestId == contest.Id && !contest.Tabs.Any(newTab => newTab.Id == existingTab.Id))
                //    .ToListAsync();

                //foreach (var tabToDelete in tabsToDelete)
                //{
                //    _ctx.ContestTabs.Remove(tabToDelete);
                //}
            }

            await _ctx.SaveChangesAsync();

            // Return the updated contest object
            return contest;
        }


        public async Task<List<Contest>> GetContestsByTenantId(Guid tenantId)
        {
            var contests = await _ctx.Contests
                        .Include(c => c.Tabs)
                        .Where(x => x.TenantId == tenantId)
                        .ToListAsync();
            return contests;
        }

    }
}
