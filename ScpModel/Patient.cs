using Dicom;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpModel
{
    public class Patient
    {
        public Int32 ID { get; set; }

        public Int32 PID { get; set; }

        public String PatientID { get; set; }

        public String PatientName { get; set; }

        public String PatientBirthDate { get; set; }

        public String PatientSex { get; set; }

        public DicomDataset ToDicomDataset()
        {
            DicomDataset dataSet = new DicomDataset();
            dataSet.Add(DicomTag.QueryRetrieveLevel, DicomQueryRetrieveLevel.Patient);
            dataSet.Add(DicomTag.PatientID, PatientID);
            dataSet.Add(DicomTag.PatientName, PatientName);
            dataSet.Add(DicomTag.PatientBirthDate, PatientBirthDate);
            dataSet.Add(DicomTag.PatientSex, PatientSex);
            dataSet.Add(DicomTag.NumberOfPatientRelatedSeries, "1");
            dataSet.Add(DicomTag.NumberOfPatientRelatedStudies, "1");
            dataSet.Add(DicomTag.NumberOfPatientRelatedInstances, "1");


            return dataSet;
        }
    }
}
