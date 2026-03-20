using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Infrastructure.Helpers
{
    public class UniqueCodeService(IDbContextFactory<ApplicationDbContext> contextFactory) : IUniqueCodeService
    {

        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);

        public async Task<string> GetUniqueInvoiceNumberAsync(CancellationToken cancellationToken = default)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
          
            await Semaphore.WaitAsync(cancellationToken);

            try
            {
                string yearMonth = DateTime.Now.ToString("yyyyMM");
                string prefix = $"RS-{yearMonth}-";

                // Get the last invoice number for this month
                var lastInvoice = await context.Invoices
                    .Where(i => i.InvoiceNumber.StartsWith(prefix))
                    .OrderByDescending(i => i.InvoiceNumber)
                    .Select(i => i.InvoiceNumber)
                    .FirstOrDefaultAsync(cancellationToken);

                int nextNumber = 1;

                if (lastInvoice != null)
                {
                    // Extract the number part
                    string numberPart = lastInvoice.Substring(prefix.Length);
                    if (int.TryParse(numberPart, out int currentNumber))
                    {
                        nextNumber = currentNumber + 1;
                    }
                }

                // Format with 5 digits padding
                var invoiceNumber = $"{prefix}{nextNumber:D5}";
                return invoiceNumber;
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }
}
