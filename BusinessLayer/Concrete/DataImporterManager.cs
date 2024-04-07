using BusinessLayer.Abstract;
using Core.Constatnts;
using Core.Entities;
using Core.Enums;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class DataImporterManager:IDataImporterService
    {
        private readonly IDataImporterDal _dataImporterDal;

        public DataImporterManager(IDataImporterDal dataImporter)
        {
            _dataImporterDal = dataImporter;
        }

        public async Task<List<TradeData>> ImportDataFromFileAsync(string filePath, FileType fileType)
        {
            List<TradeData> tradeDataList = new List<TradeData>();

            switch (fileType)
            {
                case FileType.Csv:
                    tradeDataList = await Task.Run(() => _dataImporterDal.ImportCsvData(filePath));
                    break;

                case FileType.Txt:
                    tradeDataList = await Task.Run(() => _dataImporterDal.ImportTxtData(filePath));
                    break;

                case FileType.Xml:
                    tradeDataList = await Task.Run(() => _dataImporterDal.ImportXmlData(filePath));
                    break;

                default:
                    throw new ArgumentException(Messages.UnsupportedFyleType);
            }


            return tradeDataList;
        }
        public FileType DetermineFileType(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            switch (extension.ToLower())
            {
                case ".csv":
                    return FileType.Csv;

                case ".txt":
                    return FileType.Txt;

                case ".xml":
                    return FileType.Xml;



                default:
                    throw new ArgumentException(Messages.UnsupportedFyleType);
            }
        }
    }
}
