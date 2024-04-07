using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IDataImporterService
    {
        Task<List<TradeData>> ImportDataFromFileAsync(string filePath, FileType fileType);
        FileType DetermineFileType(string filePath);
    }
}
