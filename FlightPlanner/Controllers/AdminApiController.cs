using FlightPlanner.Core.Services;
using FlightPlanner.Extensions;
using FlightPlanner.UseCases.Flights.AddFlight;
using FlightPlanner.UseCases.Flights.DeleteFlight;
using FlightPlanner.UseCases.Flights.GetFlight;
using FlightPlanner.UseCases.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);
        private readonly IMediator _mediator;
        private readonly IFlightService _flightService;

        public AdminApiController(IMediator mediator, IFlightService flightService)
        {
            _mediator = mediator;
            _flightService = flightService;
        }
        
        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> GetFlight(int id)
        {
            return (await _mediator.Send(new GetFlightQuery(id))).ToActionResult();
        }
        
        [HttpPut]
        [Route("flights")]
        public async Task<IActionResult> AddFlight(AddFlightRequest request)
        {
            Semaphore.Wait();
            try
            {
                return (await _mediator.Send(new AddFlightCommand{ AddFlightRequest = request })).ToActionResult();
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]

        public async Task<IActionResult> DeleteFlightById(int id)
        {
            return (await _mediator.Send(new DeleteFlightByIdCommand(id))).ToActionResult();
        }
    }
}
