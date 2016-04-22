
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
            int pid = pList.First<Pathmodel>().ID;

            return saveModel(ds, pid);
        }
        private int saveModel(DicomDataset ds,int pid)
        {
            try
            {
                Patient patient = new Patient();
                patient.PatientBirthDate = ds.Get<string>(DicomTag.PatientBirthDate);
                patient.PatientID = ds.Get<string>(DicomTag.PatientID);
                patient.PatientName = ds.Get<string>(DicomTag.PatientName);
                patient.PatientSex = ds.Get<string>(DicomTag.PatientSex);
                patient.PID = pid;



                Study study = new Study();
                study.PID = pid;
                study.ReferringPhysiciansName = ds.Get<string>(DicomTag.ReferringPhysicianName);
                study.StudyDate = ds.Get<string>(DicomTag.StudyDate);
                study.StudyID = ds.Get<string>(DicomTag.StudyID);
                study.StudyInstanceUID = ds.Get<string>(DicomTag.StudyInstanceUID);
                study.StudyTime = ds.Get<string>(DicomTag.StudyTime);


                Series series = new Series();
                series.Modality = ds.Get<string>(DicomTag.Modality);
                series.SeriesInstanceUID = ds.Get<string>(DicomTag.SeriesInstanceUID);
                series.SeriesNumber = ds.Get<string>(DicomTag.SeriesNumber);
                series.PID = pid;


                Image image = new Image();
                image.InstanceNumber = ds.Get<string>(DicomTag.InstanceNumber);
                image.PatientOrientation = ds.Get<string>(DicomTag.PatientOrientation);
                image.ContentDate = ds.Get<string>(DicomTag.ContentDate);
                image.ContentTime = ds.Get<string>(DicomTag.ContentTime);
                image.PID = pid;


                m.Save<Patient>(patient, "tb_patient");
                m.Save<Study>(study, "tb_study");
                m.Save<Series>(series, "tb_series");
                m.Save<Image>(image, "tb_image");

                return 1;
            }
            catch
            {
                return 0;
            }
           
        }

        public bool CheckRecive(DicomDataset ds)
        {
            Patient patient = new Patient();
            patient.PatientBirthDate = ds.Get<string>(DicomTag.PatientBirthDate);
            patient.PatientID = ds.Get<string>(DicomTag.PatientID);
            patient.PatientName = ds.Get<string>(DicomTag.PatientName);
            patient.PatientSex = ds.Get<string>(DicomTag.PatientSex);
            


            Study study = new Study();
            study.ReferringPhysiciansName = ds.Get<string>(DicomTag.ReferringPhysicianName);
            study.StudyDate = ds.Get<string>(DicomTag.StudyDate);
            study.StudyID = ds.Get<string>(DicomTag.StudyID);
            study.StudyInstanceUID = ds.Get<string>(DicomTag.StudyInstanceUID);
            study.StudyTime = ds.Get<string>(DicomTag.StudyTime);


            Series series = new Series();
            series.Modality = ds.Get<string>(DicomTag.Modality);
            series.SeriesInstanceUID = ds.Get<string>(DicomTag.SeriesInstanceUID);
            series.SeriesNumber = ds.Get<string>(DicomTag.SeriesNumber);
            


            Image image = new Image();
            image.InstanceNumber = ds.Get<string>(DicomTag.InstanceNumber);
            image.ContentDate = ds.Get<string>(DicomTag.ContentDate);
            image.ContentTime = ds.Get<string>(DicomTag.ContentTime);
            image.PatientOrientation = ds.Get<string>(DicomTag.PatientOrientation);
            
            if( m.GetList<Patient>(patient,"tb_patient").Count != 0&&
                m.GetList<Study>(study,"tb_study").Count != 0&&
                m.GetList<Series>(series,"tb_series").Count != 0&&
                m.GetList<Image>(image,"tb_image").Count != 0)
            {
                return false;
            }
            return true;
        }

        public IList<DicomDataset> CFind(DicomDataset ds, DicomQueryRetrieveLevel level)
        {
            IList<DicomDataset> queryResults = new List<DicomDataset>();

            switch (level)
            {
                case DicomQueryRetrieveLevel.Patient:
                    Patient patient = new Patient();
                    patient.PatientBirthDate = ds.Get<string>(DicomTag.PatientBirthDate);
                    patient.PatientID = ds.Get<string>(DicomTag.PatientID);
                    patient.PatientName = ds.Get<string>(DicomTag.PatientName);
                    patient.PatientSex = ds.Get<string>(DicomTag.PatientSex);

                    List<Patient> pList = m.GetList<Patient>(patient, "tb_patient");
                    foreach(Patient p in pList)
                    {
                        DicomDataset dds = p.ToDicomDataset();
                        queryResults.Add(dds);
                    }
                    break;
                case DicomQueryRetrieveLevel.Study:
                    Study study = new Study();
                    study.ReferringPhysiciansName = ds.Get<string>(DicomTag.ReferringPhysicianName);
                    study.StudyDate = ds.Get<string>(DicomTag.StudyDate);
                    study.StudyID = ds.Get<string>(DicomTag.StudyID);
                    study.StudyInstanceUID = ds.Get<string>(DicomTag.StudyInstanceUID);
                    study.StudyTime = ds.Get<string>(DicomTag.StudyTime);
                    List<Study> studyList = m.GetList<Study>(study, "tb_study");
                    foreach (Study s in studyList)
                    {
                        DicomDataset dds = s.ToDicomDataset();
                        queryResults.Add(dds);
                    }
                   
                    break;
                case DicomQueryRetrieveLevel.Series:
                    Series series = new Series();
                    series.Modality = ds.Get<string>(DicomTag.Modality);
                    series.SeriesInstanceUID = ds.Get<string>(DicomTag.SeriesInstanceUID);
                    series.SeriesNumber = ds.Get<string>(DicomTag.SeriesNumber);
                   
                    List<Series> seriesList = m.GetList<Series>(series, "tb_series");
                    foreach (Series s in seriesList)
                    {
                        DicomDataset dds = s.ToDicomDataset();
                        queryResults.Add(dds);
                    }
                    break;
                case DicomQueryRetrieveLevel.Image:
                    Image image = new Image();
                    image.InstanceNumber = ds.Get<string>(DicomTag.InstanceNumber);
                    image.ContentDate = ds.Get<string>(DicomTag.ContentDate);
                    image.ContentTime = ds.Get<string>(DicomTag.ContentTime);
                    image.PatientOrientation = ds.Get<string>(DicomTag.PatientOrientation);
                    
                    List<Image> ImageList = m.GetList<Image>(image, "tb_image");
                    foreach (Image i in ImageList)
                    {
                        DicomDataset dds = i.ToDicomDataset();
                        queryResults.Add(dds);
                    }
                    break;
                default:
                    break;
            }
        
            return queryResults;
        }

        public List<Pathmodel> CMove(DicomDataset ds, DicomQueryRetrieveLevel level)
        {
            List<Pathmodel> pathList = new List<Pathmodel>();

            switch (level)
            {
                case DicomQueryRetrieveLevel.Patient:
                    Patient patient = new Patient();
                    patient.PatientBirthDate = ds.Get<string>(DicomTag.PatientBirthDate);
                    patient.PatientID = ds.Get<string>(DicomTag.PatientID);
                    patient.PatientName = ds.Get<string>(DicomTag.PatientName);
                    patient.PatientSex = ds.Get<string>(DicomTag.PatientSex);

                    List<Patient> pList = m.GetList<Patient>(patient, "tb_patient");
                    foreach (Patient p in pList)
                    {
                        Pathmodel pathmodel = new Pathmodel();
                        pathmodel.ID = p.PID;
                        pathList.AddRange(m.GetList<Pathmodel>(pathmodel,"tb_path"));
                    }
                    break;
                case DicomQueryRetrieveLevel.Study:
                    Study study = new Study();
                    study.ReferringPhysiciansName = ds.Get<string>(DicomTag.ReferringPhysicianName);
                    study.StudyDate = ds.Get<string>(DicomTag.StudyDate);
                    study.StudyID = ds.Get<string>(DicomTag.StudyID);
                    study.StudyInstanceUID = ds.Get<string>(DicomTag.StudyInstanceUID);
                    study.StudyTime = ds.Get<string>(DicomTag.StudyTime);
                    List<Study> studyList = m.GetList<Study>(study, "tb_study");
                    foreach (Study s in studyList)
                    {
                        Pathmodel pathmodel = new Pathmodel();
                        pathmodel.ID = s.PID;
                        pathList.AddRange(m.GetList<Pathmodel>(pathmodel, "tb_path"));
                    }

                    break;
                case DicomQueryRetrieveLevel.Series:
                    Series series = new Series();
                    series.Modality = ds.Get<string>(DicomTag.Modality);
                    series.SeriesInstanceUID = ds.Get<string>(DicomTag.SeriesInstanceUID);
                    series.SeriesNumber = ds.Get<string>(DicomTag.SeriesNumber);

                    List<Series> seriesList = m.GetList<Series>(series, "tb_series");
                    foreach (Series s in seriesList)
                    {
                        Pathmodel pathmodel = new Pathmodel();
                        pathmodel.ID = s.PID;
                        pathList.AddRange(m.GetList<Pathmodel>(pathmodel, "tb_path"));
                    }
                    break;
                case DicomQueryRetrieveLevel.Image:
                    Image image = new Image();
                    image.InstanceNumber = ds.Get<string>(DicomTag.InstanceNumber);
                    image.ContentDate = ds.Get<string>(DicomTag.ContentDate);
                    image.ContentTime = ds.Get<string>(DicomTag.ContentTime);
                    image.PatientOrientation = ds.Get<string>(DicomTag.PatientOrientation);

                    List<Image> ImageList = m.GetList<Image>(image, "tb_image");
                    foreach (Image i in ImageList)
                    {
                        Pathmodel pathmodel = new Pathmodel();
                        pathmodel.ID = i.PID;
                        pathList.AddRange(m.GetList<Pathmodel>(pathmodel, "tb_path"));
                    }
                    break;
                default:
                    break;
            }

            return pathList;
        }
    }
}
