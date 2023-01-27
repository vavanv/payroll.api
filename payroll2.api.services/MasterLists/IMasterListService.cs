using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Payroll2.Api.DataAccess.PayrollEntities;

namespace Payroll2.Api.Services.MasterLists
{
    public interface IMasterListService
    {
        Task<ICollection<MasterList>> GetMasterListByTypeId(int typeId);
        Task<ICollection<MasterList>> GetMasterLists();
    }
}