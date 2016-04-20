
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicom;
using Dicom.Log;
using Dicom.Network;
using ScpModel;
using MCHDAL;
namespace Scpmch
{
    public class QRMessages
    {
        Methods m = new Methods();
       public int savePath(DicomDataset ds,string Path)
        {
            Pathmodel pathmodel = new Pathmodel();
            pathmodel.Path = Path;
            int result = m.Save<Pathmodel>(pathmodel,"tb_path");

            List<Pathmodel> pList =  m.GetList<Pathmodel>(pathmodel, "tb_path"); 
            string qrlevel = ds.Get<string>(DicomTag.QueryRetrieveLevel);
            switch (qrlevel)
            {
                case "Patient":
                    Patient patient = new Patient();
                    patient.PatientBirthDate = ds.Get<string>(DicomTag.PatientBirthDate);
                    patient.PatientID = ds.Get<string>(DicomTag.PatientID);
                    patient.PatientName = ds.Get<string>(DicomTag.PatientName);
                    patient.PatientSex = ds.Get<string>(DicomTag.PatientSex);
                    patient.PID = pList.First<Pathmodel>().ID;
                    m.Save<Patient>(patient, "tb_patient");
                    return 1;
                case "Study":
                    Study study = new Study();
                    study.PID = pList.First<Pathmodel>().ID;
                    study.ReferringPhysiciansName = ds.Get<string>(DicomTag.ReferringPhysicianName);
                    study.StudyDate = ds.Get<string>(DicomTag.StudyDate);
                    study.StudyID = ds.Get<string>(DicomTag.StudyID);
                    study.StudyInstanceUID = ds.Get<string>(DicomTag.StudyInstanceUID);
                    study.StudyTime = ds.Get<string>(DicomTag.StudyTime);
                    m.Save<Study>(study, "tb_study");
                    return 1;
                case "Series":
                    Series series = new Series();
                    series.Modality = ds.Get<string>(DicomTag.Modality);
                    series.SeriesInstanceUID = ds.Get<string>(DicomTag.SeriesInstanceUID);
                    series.SeriesNumber = ds.Get<string>(DicomTag.SeriesNumber);
                    series.PID = pList.First<Pathmodel>().ID;
                    m.Save<Series>(series, "tb_series");
                    return 1;
                case "Image":
                    Image image = new Image();
                    image.InstanceNumber = ds.Get<string>(DicomTag.InstanceNumber);
                    image.PatientOrientation = ds.Get<string>(DicomTag.PatientOrientation);
                    image.PID = pList.First<Pathmodel>().ID;
                    m.Save<Image>(image, "tb_image");
                    return 1;
                default:
                    return 0;
            }

            
        }
    }
}
