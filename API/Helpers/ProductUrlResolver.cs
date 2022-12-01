using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, Product, string>
    {
        private IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, Product destination, string destMember, 
                                ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}