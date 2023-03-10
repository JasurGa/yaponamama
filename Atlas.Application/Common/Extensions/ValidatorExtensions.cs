using FluentValidation;

namespace Atlas.Application.Common.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(x => x.StartsWith("+998"))
                    .WithMessage("Phone number must start with +998")
                .Length(13)
                    .WithMessage("Phone number digits count must be 13");
        }
    }
}
