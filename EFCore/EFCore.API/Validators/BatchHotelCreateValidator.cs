using EFCore.API.Dtos.Hotels;
using FluentValidation;

namespace EFCore.API.Validators
{
    public class BatchHotelCreateValidator : AbstractValidator<BatchHotelCreateDto>
    {
        public BatchHotelCreateValidator(HotelCreateValidator hotelValidator)
        {
            RuleForEach(x => x.Hotels)
                .SetValidator(hotelValidator);
        }
    }
}
