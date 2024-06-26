﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Repositories.Interfaces;
using ProductManagementAPI.Entities.ViewModels;
using ProductManagementAPI.Entity.Models;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<City>> GetCities()
        {
            try
            {
                List<City> city = _productsRepository.GetCites();
                return Ok(city);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetProductsCountWithFilter(ProductListParams listParams)
        {
            try
            {
                int count = _productsRepository.GetAllProductsCount(listParams);
                return Ok(count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Product>> GetAllProducts()
        {
            try
            {
                List<Product> products = _productsRepository.GetAllProducts();
                return Ok(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<ProductDetails>> GetProductByFilter(ProductListParams listParams)
        {
            try
            {
                if (listParams == null)
                {
                    return BadRequest();
                }
                List<ProductDetails> products = _productsRepository.GetAllProducts(listParams);
                return Ok(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ProductDetails> GetProductById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                ProductDetails productDetails = _productsRepository.GetProductDetail(id);
                return Ok(productDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> CreateProduct(ProductDetails product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                Product newProduct = _productsRepository.AddEditProduct(product);

                return Ok(newProduct);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> EditProduct(int id, ProductDetails product)
        {
            try
            {
                if (product == null || product.ProductId != id)
                {
                    return BadRequest();
                }
                Product newProduct = _productsRepository.AddEditProduct(product);

                return Ok(newProduct);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                _productsRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> JsonPatchWithModelState(int id, JsonPatchDocument<ProductDetails> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    return BadRequest();
                }
                ProductDetails product = _productsRepository.GetProductDetail(id);
                if (product == null)
                {
                    return BadRequest();
                }
                patchDoc.ApplyTo(product, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _productsRepository.AddEditProduct(product);

                return Ok(product);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
