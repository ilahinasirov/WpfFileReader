using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IDataImporterDal
    {
        List<TradeData> ImportCsvData(string filePath);
        List<TradeData> ImportTxtData(string filePath);
        List<TradeData> ImportXmlData(string filePath);
    }
}
