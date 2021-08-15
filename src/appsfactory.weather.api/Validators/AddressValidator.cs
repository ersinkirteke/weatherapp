using FluentValidation;
using Appsfactory.Weather.Api.DTOs;

namespace Appsfactory.Weather.Api.Validators
{
    public class AddressValidator : AbstractValidator<AddressDto>
	{
		public AddressValidator()
		{

		}
	}
}
