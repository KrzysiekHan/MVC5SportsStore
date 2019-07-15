using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;

namespace Domain.DbModel
{
   public class MapperConfig
    {
        public IMapper Mapper { get; set; }
        public void configMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users, User>();
                cfg.CreateMap<User, Users>();
                cfg.CreateMap<Products, Product>();
                cfg.CreateMap<Product, Products>();
            });

            this.Mapper = configuration.CreateMapper();
        }
    }
}
