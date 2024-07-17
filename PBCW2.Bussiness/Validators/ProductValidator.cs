using FluentValidation;
using PBCW2.Schema;

public class ProductValidator : AbstractValidator<ProductRequest>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Product Name");

        RuleFor(p => p.Description)
            .MaximumLength(200)
            .WithName("Product Description");

        RuleFor(p => p.Category)
            .MaximumLength(200)
            .WithName("Product Category");

        RuleFor(p => p.Price)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(10000)
            .WithName("Product Price");

        RuleFor(p => p.Stock)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(1000)
            .WithName("Product Stock");
    }
}