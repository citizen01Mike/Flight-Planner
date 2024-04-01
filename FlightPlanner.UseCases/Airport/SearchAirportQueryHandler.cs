using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;
using System.Net;

namespace FlightPlanner.UseCases.Airport
{
    public class SearchAirportQueryHandler :IRequestHandler<SearchAirportQuery, ServiceResult>
    {
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;

        public SearchAirportQueryHandler(IMapper mapper, IAirportService airportService)
        {
            _mapper = mapper;
            _airportService = airportService;
        }

        public async Task<ServiceResult> Handle(SearchAirportQuery request, CancellationToken cancellationToken)
        {
            var airport = _airportService.SearchForAirport(request.AirportName);
            
            return new ServiceResult
            {
                ResultObject = _mapper.Map<List<AirportViewModel>>(airport),
                Status = HttpStatusCode.OK
            };
        }
    }
}
