using System;
using System.Threading.Tasks;

namespace Payroll2.Api.Environment.Services.Systems
{
    public interface ISystemService
    {
        Task<DataAccess.EnvironmentEntities.Systems> GetSystemByName(string name);
        DataAccess.EnvironmentEntities.Systems GetSystemByNameSync(string name);
        Task<DataAccess.EnvironmentEntities.Systems> GetSystemById(int id);
        DataAccess.EnvironmentEntities.Systems GetSystemByIdSync(int id);
    }
}