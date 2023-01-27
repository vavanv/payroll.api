using System;
using System.Collections.Generic;

namespace Payroll2.Api.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public List<MasterListModel> MasterLists { get; set; }
    }
}