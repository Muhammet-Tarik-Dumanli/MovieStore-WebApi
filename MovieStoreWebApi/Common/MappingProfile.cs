using AutoMapper;
using MovieStoreWebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebApi.Application.ActorOperations.Queries.GetActors;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomers;
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
        }
    }
}

