using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicom;
using Dicom.Network;

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

        public DicomDataset ToDicomDataset()
        {
            DicomDataset dataSet = new DicomDataset();
            dataSet.Add(DicomTag.QueryRetrieveLevel, DicomQueryRetrieveLevel.Study);
            dataSet.Add(DicomTag.StudyInstanceUID, StudyInstanceUID);
            dataSet.Add(DicomTag.StudyDate, StudyDate);
            dataSet.Add(DicomTag.StudyID, StudyID);
            dataSet.Add(DicomTag.StudyTime, StudyTime);
            dataSet.Add(DicomTag.AccessionNumber, AccessionNumber);
            dataSet.Add(DicomTag.ReferringPhysicianName, ReferringPhysiciansName);

            return dataSet;
        }
    }
}
