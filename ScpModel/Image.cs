using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicom;
using Dicom.Network;

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

        public DicomDataset ToDicomDataset()
        {
            DicomDataset dataSet = new DicomDataset();
            dataSet.Add(DicomTag.QueryRetrieveLevel, "IMAGE");
            dataSet.Add(DicomTag.InstanceNumber, InstanceNumber);
            dataSet.Add(DicomTag.ContentDate, ContentDate);
            dataSet.Add(DicomTag.ContentTime, ContentTime);
            dataSet.Add(DicomTag.PatientOrientation, PatientOrientation);

            return dataSet;
        }
    }
}
