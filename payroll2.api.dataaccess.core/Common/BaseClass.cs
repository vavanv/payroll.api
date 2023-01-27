using System;

namespace Payroll2.Api.DataAccess.Core.Common
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}