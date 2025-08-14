using System;
using System.Collections.Generic;
using System.Text;

namespace CourseMarket.Shared.Messages
{
    public class CourseNameChangedEvent
    {
        public string CourseId { get; set; }
        public string UpdatedName { get; set; }
    }
}
