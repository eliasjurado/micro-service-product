﻿using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.Dto;
using ProductAPI.Models.Dtos;
using ProductAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/processproducts")]
    public class ProcessProductAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRepository;

        public ProcessProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._response = new ResponseDto();
        }

        [HttpPost]
        //[Authorize]
        [Route("RegisterBreadProduction")]
        public async Task<object> RegisterBreadProductionAsync([FromBody] ProcessProductDto processProduct)
        {
            try
            {
                ProcessProductDto model = await _productRepository.RegisterBreadProductionAsync(processProduct);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        //[Authorize]
        [Route("ProductAvailable")]
        public async Task<object> GetProductAvailableAsync(int idProduct)
        {
            try
            {
                _response.Result = await _productRepository.GetProductAvailableAsync(idProduct);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        //[Authorize]
        [Route("UpdateProductStock")]
        public async Task<object> UpdateProductStockAsync(double amount, int idProduct)
        {
            try
            {
                _response.Result = await _productRepository.UpdateProductStockAsync(amount, idProduct);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}