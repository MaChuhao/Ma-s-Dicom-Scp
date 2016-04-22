using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicom;
using Dicom.Network;

namespace ScpModel
{
    public class Series
    {
        public Int32 ID { get; set; }

        public Int32 PID { get; set; }

        public String SeriesInstanceUID { get; set; }

        public String Modality { get; set; }

        public String SeriesNumber { get; set; }

        public DicomDataset ToDicomDataset()
        {
            DicomDataset dataSet = new DicomDataset();
            dataSet.Add(DicomTag.QueryRetrieveLevel, DicomQueryRetrieveLevel.Series);
            dataSet.Add(DicomTag.SeriesInstanceUID, SeriesInstanceUID);
            dataSet.Add(DicomTag.Modality, Modality);
            dataSet.Add(DicomTag.SeriesNumber, SeriesNumber);

            return dataSet;
        }
    }
}
