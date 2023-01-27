using System;

namespace Payroll2.Api.Framework
{
    public interface IAppSettingsService
    {
        string GetJwtSecurityKey();
    }
}