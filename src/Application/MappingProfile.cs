using AutoMapper;
using Domain.Entities;
using Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application
{


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Genre, GenreModel>().ReverseMap();
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<OrderModel, Order>();
            CreateMap<OrderItemModel, OrderItem>();
            CreateMap<Order, OrderModel>();
            CreateMap<OrderItem, OrderItemModel>();

            CreateMap<Cart, CartModel>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<CartItem, CartItemModel>()
                .ForMember(dest => dest.Book, opt => opt.Ignore());

        }
    }
}
