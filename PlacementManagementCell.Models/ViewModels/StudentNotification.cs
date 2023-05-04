using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.Models.ViewModels
{
    public class StudentNotification
    {
        public long NotificationId { get; set; }
        public long CompanyId { get; set; }
        public string CompanyLogo { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string LinkToRedirect { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }

    }
}


//https://localhost:44357/Student/CompanyDetail?companyId=1002