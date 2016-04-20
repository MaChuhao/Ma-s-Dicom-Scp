using Dicom;
using Dicom.Log;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScpModel;
using MCHDAL;

namespace Scpmch

{
    class PacsServer : DicomService, IDicomServiceProvider, IDicomCStoreProvider, IDicomCEchoProvider, IDicomCFindProvider, IDicomCMoveProvider, IDicomNServiceProvider
    {
        private Methods m = new Methods();
        
        public String StorageParh { get; set; }
        public PacsServer(Stream stream, Logger log) : base(stream, log) { }

        public void OnReceiveAssociationRequest(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.SetResult(DicomPresentationContextResult.Accept);
            }
            SendAssociationAccept(association);

        }

        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }

        /*
        此处为CStore的实现，Dicomfile为原有一个类
        */
        public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
        {

            DicomFile dicomfile = new DicomFile(request.Dataset);
            
            int hash = dicomfile.GetHashCode();
            String path = "G:/Temp/ScpDataPath/" + hash.ToString()+".dcm";
            dicomfile.Save(path);

            
            QRMessages qrm = new QRMessages();
            qrm.savePath(request.Dataset, path);
            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public IEnumerable<DicomCFindResponse> OnCFindRequest(DicomCFindRequest request)
        {
            List<DicomCFindResponse> responses = new List<DicomCFindResponse>();
            IList<DicomDataset> queryResults = HandleCFindQuery(request);

            foreach (var item in queryResults)
            {
                DicomCFindResponse response = new DicomCFindResponse(request, DicomStatus.Pending);
                response.Dataset = item;
                responses.Add(response);
            }
            responses.Add(new DicomCFindResponse(request, DicomStatus.Success));
            return responses;
        }

        private IList<DicomDataset> HandleCFindQuery(DicomCFindRequest request)
        {

            IList<DicomDataset> queryResults = new List<DicomDataset>();

            
            List<Pathmodel> pathmodel = m.GetList<Pathmodel>(new Pathmodel(),"tb_path");

            foreach( Pathmodel p in pathmodel)
            {
                DicomFile dcmFile = DicomFile.Open(p.Path);
                DicomDataset dcmDataSet = dcmFile.Dataset;
                

                //此段
                DicomDataset dataSet = new DicomDataset();
                dataSet.Add(DicomTag.QueryRetrieveLevel, dcmDataSet.Get<String>(DicomTag.QueryRetrieveLevel));
                dataSet.Add(DicomTag.StudyInstanceUID, dcmDataSet.Get<String>(DicomTag.StudyInstanceUID));
                dataSet.Add(DicomTag.StudyDate, dcmDataSet.Get<String>(DicomTag.StudyDate));
                dataSet.Add(DicomTag.PatientID, dcmDataSet.Get<String>(DicomTag.PatientID));
                dataSet.Add(DicomTag.PatientName, dcmDataSet.Get<String>(DicomTag.PatientName));
                queryResults.Add(dataSet);
                //此段
            }
            

            return queryResults;
        }

        public IEnumerable<DicomCMoveResponse> OnCMoveRequest(DicomCMoveRequest request)
        {
            List<DicomCMoveResponse> response = new List<DicomCMoveResponse>();
            List<DicomDataset> queryResults = HandleCMoveRequest(request);
            DicomClient cstoreClient = new DicomClient();

            var studyUid = request.Dataset.Get<string>(DicomTag.StudyInstanceUID);
            var instUid = request.Dataset.Get<string>(DicomTag.SOPInstanceUID);

            //try
            //{
            //    List<PathModel> pathmodel =  mysqlhealper.GetDataByStudyInstanceUid(studyUid);
            //    if(pathmodel.Count != 0)
            //    {
            //        String path = pathmodel[0].Path;
                    
                   
            //        DicomCStoreRequest cstorerq = new DicomCStoreRequest(path);
            //        cstorerq.OnResponseReceived = (rq, rs) =>
            //        {
            //            if (rs.Status == DicomStatus.Success)
            //            {
            //                DicomCMoveResponse rsp = new DicomCMoveResponse(request, DicomStatus.Pending);
            //                SendResponse(rsp);
            //            }

            //        };
            //        cstoreClient.AddRequest(cstorerq);
            //        cstoreClient.Send("127.0.0.1", 5104, false, this.Association.CalledAE, request.DestinationAE);
                   
            //    }
                

            //}
            //catch 
            //{
            //    DicomCMoveResponse rs = new DicomCMoveResponse(request, DicomStatus.StorageStorageOutOfResources);
            //    response.Add(rs);
            //    return response;
            //}

            response.Add(new DicomCMoveResponse(request, DicomStatus.Success));
            return response;
        }

        private List<DicomDataset> HandleCMoveRequest(DicomCMoveRequest request)
        {
            foreach (var item in request.Dataset)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(request.Dataset);
            Console.WriteLine(request.Dataset.Get<string>(DicomTag.StudyInstanceUID));
            Console.WriteLine(request.Dataset.Get<string>(DicomTag.QueryRetrieveLevel));

            var queryResults = new List<DicomDataset>();
            return queryResults;
        }

        public void OnCStoreRequestException(string tempFileName, Exception e)
        {
            
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            
        }

        public void OnReceiveAssociationReleaseRequest()
        {
            SendAssociationReleaseResponse();
        }

        public void OnConnectionClosed(Exception exception)
        {

        }


        public DicomNActionResponse OnNActionRequest(DicomNActionRequest request)
        {
            return new DicomNActionResponse(request, DicomStatus.Success);
        }

        public DicomNCreateResponse OnNCreateRequest(DicomNCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public DicomNDeleteResponse OnNDeleteRequest(DicomNDeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public DicomNEventReportResponse OnNEventReportRequest(DicomNEventReportRequest request)
        {
            return new DicomNEventReportResponse(request, DicomStatus.Success);
        }

        public DicomNGetResponse OnNGetRequest(DicomNGetRequest request)
        {
            throw new NotImplementedException();
        }

        public DicomNSetResponse OnNSetRequest(DicomNSetRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
