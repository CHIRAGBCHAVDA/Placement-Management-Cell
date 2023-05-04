    using System;
using System.Collections.Generic;

namespace PlacementManagementCell.Models
{
    public partial class Notification
    {
        public long NotificationId { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Company? Company { get; set; }
    }
}
