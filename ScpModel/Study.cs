using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpModel
{
    public class Study
    {
        public Int32 ID { get; set; }

        public Int32 PID { get; set; }

        public String StudyInstanceUID { get; set; }

        public String StudyID { get; set; }

        public String StudyDate { get; set; }

        public String StudyTime { get; set; }

        public String AccessionNumber { get; set; }

        public String ReferringPhysiciansName { get; set; }
    }
}
