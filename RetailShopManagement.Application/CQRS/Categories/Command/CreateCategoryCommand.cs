using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.CQRS.Categories.Command
{
    public class CreateCategoryCommand : IRequest<Unit>
    {
        public string Name { get; set; }
    }

    public class CreateCategoryCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<CreateCategoryCommand, Unit>
    {
        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
           
            var existingCategory = await context.Categories
                .Where(x => x.Name.ToLower() == request.Name.ToLower()).
                FirstOrDefaultAsync(cancellationToken);

            if (existingCategory != null)
            {
                throw new Exception("Category already exists.");
            }

            var category = new Category()
            {
                Name = request.Name,
                CreatedBy = "Sushil Rai",
                CreatedOn = DateTime.UtcNow
            };
            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
