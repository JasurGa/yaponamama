using System;
using Atlas.Payme.MerchantApi.Extensions;
using System.Text.Json;

namespace Atlas.Payme.MerchantApi.Helpers.Policies
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToSnakeCase();
    }
}

