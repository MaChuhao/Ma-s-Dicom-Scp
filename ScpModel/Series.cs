using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpModel
{
    public class Series
    {
        public Int32 ID { get; set; }

        public Int32 PID { get; set; }

        public String SeriesInstanceUID { get; set; }

        public String Modality { get; set; }

        public String SeriesNumber { get; set; }

    }
}
