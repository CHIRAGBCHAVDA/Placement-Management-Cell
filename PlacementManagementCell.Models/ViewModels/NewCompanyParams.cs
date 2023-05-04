using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PlacementManagementCell.Models.ViewModels
{
    public class NewCompanyParams
    {
        public long CompanyId { get; set; }
        public string avatar { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string technology { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string title { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public int BID { get; set; }
        
        [Required(ErrorMessage = "This Field is required")]
        public long package { get; set; }
        
        [Required(ErrorMessage = "This Field is required")]
        public string briefdesc { get; set; }
        
        [Required(ErrorMessage = "This Field is required")]
        public string longdesc { get; set; }
        
        public int maxBacklog { get; set; }
        
        public long minCgpa { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public DateTime fromdate { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public DateTime todate { get; set; }
        public long vacancy { get; set; }
        
        
        [Required(ErrorMessage = "This Field is required")]
        public DateTime deadline { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string address { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        public string traininginfo { get; set; }
        [Required(ErrorMessage = "This Field is required")]
        public string benefits { get; set; }

        public string driveLink { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string city { get; set; }
    }
}
