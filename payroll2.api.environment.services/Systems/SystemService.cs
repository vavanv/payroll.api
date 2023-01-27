using System;
using System.Threading.Tasks;

using Payroll2.Api.Environment.DataAccess.Repository;
using Payroll2.Api.Framework.Exception;

namespace Payroll2.Api.Environment.Services.Systems
{
    internal sealed class SystemService : ISystemService
    {
        private readonly IRepository<DataAccess.EnvironmentEntities.Systems> _systems;

        public SystemService(IRepository<DataAccess.EnvironmentEntities.Systems> systems)
        {
            _systems = systems;
        }

        public async Task<DataAccess.EnvironmentEntities.Systems> GetSystemByName(string name)
        {
            var system = await _systems.FindOne(s => s.Name == name);
            if (system == null) throw new BusinessException($"System [{name}] does not exist");

            return system;
        }

        public DataAccess.EnvironmentEntities.Systems GetSystemByNameSync(string name)
        {
            var system = _systems.FindOneSync(s => s.Name == name);
            if (system == null || !system.Name.Equals(name, StringComparison.Ordinal))
                throw new BusinessException($"System [{name}] does not exist");

            return system;
        }

        public async Task<DataAccess.EnvironmentEntities.Systems> GetSystemById(int id)
        {
            var system = await _systems.FindOne(s => s.Id == id);
            if (system == null) throw new BusinessException($"System with [{id}] does not exist");

            return system;
        }

        public DataAccess.EnvironmentEntities.Systems GetSystemByIdSync(int id)
        {
            var system = _systems.FindOneSync(s => s.Id == id);
            if (system == null) throw new BusinessException($"System with [{id}] does not exist");

            return system;
        }
    }
}