using BackEnd.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Service.background_checks
{
    public class DiscountExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public DiscountExpirationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MyApplicationDBContext>();

                var closedStatus = await context.Statuses
                    .FirstOrDefaultAsync(s => s.Name == "closed"
                        && s.Type == "discount", stoppingToken);

                if (closedStatus != null)
                {
                    var now = DateTime.UtcNow;
                    var expiredDiscount = await context.Discounts
                        .Where(d => d.EndDate <= now)
                        .ToListAsync(stoppingToken);

                    foreach (var discount in expiredDiscount)
                    {
                        discount.StatusId = closedStatus.Id;
                    }

                    if (expiredDiscount.Count > 0)
                    {
                        await context.SaveChangesAsync(stoppingToken);
                    }
                }

               await Task.Delay(
               TimeSpan.FromMinutes(10),
               stoppingToken);

            }
        }
    }
}
