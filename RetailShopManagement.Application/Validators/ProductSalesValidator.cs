using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RetailShopManagement.Application.Common.Models;

namespace RetailShopManagement.Application.Validators
{
    public class ProductSalesValidator : AbstractValidator<InvoiceItemBaseModel>
    {
        public ProductSalesValidator()
        {
            //RuleFor(x => x.Id)
            //    .NotEmpty().WithMessage("ProductSale Id is required.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product Name is required.")
                .MaximumLength(200).WithMessage("Product Name cannot exceed 200 characters.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Unit is required.")
                .MaximumLength(50).WithMessage("Unit cannot exceed 50 characters.")
                .NotEqual("None").WithMessage("Unit cannot be 'None'.");

            //RuleFor(x => x.InvoiceId)
            //    .NotEmpty().WithMessage("Invoice ID is required.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit Price must be greater than zero.");
            
            RuleFor(x => x.SubTotal)
                .GreaterThan(0).WithMessage("SubTotal must be greater than zero.");
        }
    }
}
