using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.UseCases.Models;

namespace FlightPlanner.UseCases.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Models.Airport, AirportViewModel>()
                .ForMember(viewModel => viewModel.Airport, 
                    options => options
                        .MapFrom(source => source.AirportCode));
            CreateMap<AirportViewModel, Core.Models.Airport>()
                .ForMember(destination => destination.AirportCode,
                    options => options
                        .MapFrom(source => source.Airport));
            CreateMap<AddFlightRequest, Flight>();
            CreateMap<Flight, AddFlightResponse>();
        }
    }
}
