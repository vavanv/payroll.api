using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.DataAccess.Repository;

namespace Payroll2.Api.Services.MasterLists
{
    internal sealed class MasterListService : IMasterListService
    {
        private readonly IRepository<MasterList> _masterList;

        public MasterListService(IRepository<MasterList> masterList)
        {
            _masterList = masterList;
        }

        public Task<ICollection<MasterList>> GetMasterListByTypeId(int typeId)
        {
            var masterList = _masterList.FindAll(p => p.MasterListTypeId == typeId);

            return masterList;
        }

        public async Task<ICollection<MasterList>> GetMasterLists()
        {
            var masterList = await _masterList.All();

            return masterList;
        }
    }
}