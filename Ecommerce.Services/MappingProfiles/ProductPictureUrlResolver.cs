using AutoMapper;
using Ecommerce.Domain.Entities.ProductModule;
using Ecommerce.Shared.ProductDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.MappingProfiles
{
    internal class ProductPictureUrlResolver : IValueResolver<Products, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Products source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
             if(source.PictureUrl.StartsWith("http") || source.PictureUrl.StartsWith("https"))
                return source.PictureUrl;

            var baseUrl = _configuration.GetSection("URLs")["BaseUrl"];
            var pictureUrl = $"{baseUrl}{source.PictureUrl}";
            return pictureUrl;
        }
    }
}
