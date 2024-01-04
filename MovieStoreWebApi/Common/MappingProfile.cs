using AutoMapper;
using MovieStoreWebApi.Application.ActorOperations.Commands;
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
            // CreateMap<CreateMovieModel, Movie>().ForMember(c=> c.Actors, c=>c.MapFrom(c=> c.Actors));
            // CreateMap<CreateDirectorModel, Director>();
            // CreateMap<CreateOrderModel, Order>();
            // CreateMap<Order, OrderViewModel>().ForMember(c=> c.CustomerName, c=> c.MapFrom(c=> c.Customer.Name + " " + c.Customer.LastName)).ForMember(c => c.PurchasedMovie, c => c.MapFrom(c => c.PurchasedMovie.Name));
            // CreateMap<CreateCustomerModel, Customer>();
            // CreateMap<Movie, MovieViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name + "" + c.Director.LastName)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " " + c.LastName).ToList()));
            // CreateMap<Director, DirectorViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            // CreateMap<Actor, ActorViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            // CreateMap<Actor, ActorsViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            // CreateMap<Director, DirectorsViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            // CreateMap<Movie, MoviesViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name +""+ c.Director.LastName)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " "+c.LastName).ToList()));

        }
    }
}

