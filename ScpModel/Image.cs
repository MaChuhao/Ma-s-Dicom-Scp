using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpModel
{
    public class Image
    {
        public Int32 ID { get; set; }

        public Int32 PID { get; set; }

        public String InstanceNumber { get; set; }

        public String ContentDate { get; set; }

        public String ContentTime { get; set; }

        public String PatientOrientation { get; set; }
    }
}
