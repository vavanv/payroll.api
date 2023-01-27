using System;
using System.Collections.Generic;

using Payroll2.Api.DataAccess.PayrollEntities;
using Payroll2.Api.Models;

namespace Payroll2.Api.Mappers
{
    public interface IMasterListMapper
    {
        MasterListModel MapFrom(MasterList itemEntity);
        IEnumerable<MasterListModel> MapFrom(IEnumerable<MasterList> entities);
    }
}