using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.Models.ViewModels
{
    public class BaseResponseViewModel
    {
        public long StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
