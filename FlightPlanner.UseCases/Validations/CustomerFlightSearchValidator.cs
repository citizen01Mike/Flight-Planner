using FlightPlanner.Models;
using FluentValidation;

namespace FlightPlanner.UseCases.Validations
{
    public class CustomerFlightSearchValidator : AbstractValidator<SearchFlights>
    {
        public CustomerFlightSearchValidator()
        {
            RuleFor(request => request.From).NotEmpty();
            RuleFor(request => request.To).NotEmpty();
            RuleFor(request => request.DepartureDate).NotEmpty();
        }
    }
}
