using System;

using Microsoft.Extensions.Options;

using Payroll2.Api.Configuration;

namespace Payroll2.Api.Framework
{
    internal sealed class AppSettingsService : IAppSettingsService
    {
        private readonly IOptions<JwtSecurityKey> _serviceSettings;

        public AppSettingsService(IOptions<JwtSecurityKey> serviceSettings)
        {
            _serviceSettings = serviceSettings;
        }

        public string GetJwtSecurityKey()
        {
            return _serviceSettings.Value.KeyValue;
        }
    }
}