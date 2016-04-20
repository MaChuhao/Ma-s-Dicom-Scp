using ClearCanvas.Dicom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scpmch
{
    public class QRMessages
    {
        #region Private Classes and Structures
        struct FileToSend
        {
            public String filename;
            public SopClass sopClass;
            public TransferSyntax transferSyntax;
        }
        #endregion

        private List<FileToSend> _fileList = new List<FileToSend>();

        public bool AddFileToSend(String file)
        {

            try
            {
                DicomFile dicomFile = new DicomFile(file);

                // Only load to sopy instance uid to reduce amount of data read from file
                dicomFile.Load(DicomTags.SopInstanceUid, DicomReadOptions.Default | DicomReadOptions.DoNotStorePixelDataInDataSet);

                FileToSend fileStruct = new FileToSend();

                fileStruct.filename = file;
                string sopClassInFile = dicomFile.DataSet[DicomTags.SopClassUid].ToString();
                if (sopClassInFile.Length == 0)
                    return false;

                if (!sopClassInFile.Equals(dicomFile.SopClass.Uid))
                {
                    //Logger.LogError("SOP Class in Meta Info does not match SOP Class in DataSet");
                    fileStruct.sopClass = SopClass.GetSopClass(sopClassInFile);
                }
                else
                    fileStruct.sopClass = dicomFile.SopClass;

                fileStruct.transferSyntax = dicomFile.TransferSyntax;

                _fileList.Add(fileStruct);
            }
            catch (DicomException e)
            {
                //Logger.LogErrorException(e, "Unexpected exception when loading file for sending: {0}", file);
                return false;
            }
            return true;
        }
    }
}
