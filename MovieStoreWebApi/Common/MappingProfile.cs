using AutoMapper;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebApi.Application.ActorOperations.Queries.GetActors;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomers;
using MovieStoreWebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStoreWebApi.Application.DirectorOperations.Queries.GetDirectors;
using MovieStoreWebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebApi.Application.MovieOperations.Queries.GetMovies;
using MovieStoreWebApi.Application.OrderOperations.Commands.CreateOrder;
using MovieStoreWebApi.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStoreWebApi.Application.OrderOperations.Queries.GetOrders;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Common
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<int,Actor>().ForMember(c => c.Id , c=> c.MapFrom(c=> c));
            CreateMap<int,Movie>().ForMember(c => c.Id , c=> c.MapFrom(c=> c));
            
            CreateMap<CreateActorModel, Actor>();
            CreateMap<Actor, GetActorDetailModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Actor, GetActorsModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));

            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<Customer, GetCustomerDetailModel>();
            CreateMap<Customer, GetCustomersModel>();

            CreateMap<CreateDirectorModel, Director>();
            CreateMap<Director, GetDirectorDetailModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Director, GetDirectorsModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));

            CreateMap<CreateMovieModel, Movie>();
            CreateMap<Movie, GetMovieDetailModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name + " " + c.Director.LastName)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " " + c.LastName).ToList()));
            CreateMap<Movie, GetMoviesModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name +" "+ c.Director.LastName)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " "+c.LastName).ToList()));

            CreateMap<CreateOrderModel, Order>();
            CreateMap<Order, GetOrderDetailModel>().ForMember(c=> c.CustomerName, c=> c.MapFrom(c=> c.Customer.Name + " " + c.Customer.LastName)).ForMember(c => c.PurchasedMovie, c => c.MapFrom(c => c.PurchasedMovie.Name));
            CreateMap<Order, GetOrdersModel>().ForMember(c=> c.CustomerName, c=> c.MapFrom(c=> c.Customer.Name + " " + c.Customer.LastName)).ForMember(c => c.PurchasedMovie, c => c.MapFrom(c => c.PurchasedMovie.Name));
        }
    }
}

