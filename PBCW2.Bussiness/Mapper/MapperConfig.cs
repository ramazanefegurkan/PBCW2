using AutoMapper;
using PBCW2.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PBCW2.Bussiness.Mapper
{
    public class MapperConfig : Profile
    {

        public MapperConfig()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();

        }
    }
}
