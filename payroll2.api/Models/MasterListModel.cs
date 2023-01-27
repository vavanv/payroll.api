using System;

namespace Payroll2.Api.Models
{
    public class MasterListModel
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}