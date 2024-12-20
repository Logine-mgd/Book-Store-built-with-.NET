using AutoMapper;
using Project1.DTOs;
using Project1.Models;

namespace Project1.Mappings
{
    public class Mapconfig:Profile
    {
        public Mapconfig()
        {
            CreateMap<Catalog, CatalogDTO>().ReverseMap();
            CreateMap<Admin, AdminDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
        }

    }
}
