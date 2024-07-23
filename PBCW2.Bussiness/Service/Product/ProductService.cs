using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PBCW2.Bussiness.Exceptions;
using PBCW2.Data.UnitOfWork;
using PBCW2.Schema;

namespace PBCW2.Bussiness.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductRequest> _productValidator;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ProductRequest> productValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productValidator = productValidator;
        }

        public async Task<ApiResponse<List<ProductResponse>>> GetAll(string? name)
        {
            List<Product> products;
            if (string.IsNullOrEmpty(name))
            {
                products = await _unitOfWork.ProductRepository.GetAll();
            }
            else
            {
                products = await _unitOfWork.ProductRepository.GetAll(p => p.Name.Contains(name));
            }

            var mappedList = _mapper.Map<List<ProductResponse>>(products);
            return ApiResponse<List<ProductResponse>>.SuccessResult(mappedList);
        }

        public async Task<ApiResponse<ProductResponse>> GetById(long id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            var mapped = _mapper.Map<ProductResponse>(product);

            return ApiResponse<ProductResponse>.SuccessResult(mapped);
        }

        public async Task<ApiResponse<ProductResponse>> Add(ProductRequest product)
        {
            // Validate the product using FluentValidation
            _productValidator.ValidateAndThrow(product);

            var mapped = _mapper.Map<Product>(product);
            await _unitOfWork.ProductRepository.Insert(mapped);
            await _unitOfWork.Complete();

            return ApiResponse<ProductResponse>.SuccessResult(_mapper.Map<ProductResponse>(mapped), "Product created successfully");
        }

        public async Task<ApiResponse<bool>> Update(long id,ProductRequest updatedProduct)
        {
            // Validate the updated product using FluentValidation
            _productValidator.ValidateAndThrow(updatedProduct);

            // Find the product by ID
            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            // Update the product properties
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Category = updatedProduct.Category;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;

            // Save the changes to the context
            await _unitOfWork.Complete();

            return ApiResponse<bool>.SuccessResult(true, "Product updated successfully");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            // Remove the product from the context and save changes
            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.Complete();
            return ApiResponse<bool>.SuccessResult(true, "Product deleted successfully");
        }

        public async Task<ApiResponse<List<Product>>> SortByPrice(bool ascending)
        {
            List<Product> sortedProducts;
            if (ascending)
            {
                sortedProducts = await _unitOfWork.ProductRepository.GetAll(null, orderBy: q => q.OrderBy(d => d.Price));
            }
            else
            {
                sortedProducts = await _unitOfWork.ProductRepository.GetAll(null, orderBy: q => q.OrderByDescending(d => d.Price));
            }

            return ApiResponse<List<Product>>.SuccessResult(sortedProducts);
        }
    }
}
