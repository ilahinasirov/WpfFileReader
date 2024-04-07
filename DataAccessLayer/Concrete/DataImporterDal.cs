using Core.Entities;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataAccessLayer.Concrete
{
    public class DataImporterDal:IDataImporterDal
    {
        public List<TradeData> ImportCsvData(string filePath)
        {
            List<TradeData> tradeDataList = new List<TradeData>();

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',');

                TradeData tradeData = new TradeData
                {
                    Date = DateTime.Parse(columns[0]),
                    Open = decimal.Parse(columns[1]),
                    High = decimal.Parse(columns[2]),
                    Low = decimal.Parse(columns[3]),
                    Close = decimal.Parse(columns[4]),
                    Volume = int.Parse(columns[5])
                };

                tradeDataList.Add(tradeData);
            }

            return tradeDataList;
        }

        public List<TradeData> ImportTxtData(string filePath)
        {
            List<TradeData> tradeDataList = new List<TradeData>();

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 2; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(';');

                TradeData tradeData = new TradeData
                {
                    Date = DateTime.Parse(columns[0]),
                    Open = decimal.Parse(columns[1]),
                    High = decimal.Parse(columns[2]),
                    Low = decimal.Parse(columns[3]),
                    Close = decimal.Parse(columns[4]),
                    Volume = int.Parse(columns[5])
                };

                tradeDataList.Add(tradeData);
            }

            return tradeDataList;
        }

        public List<TradeData> ImportXmlData(string filePath)
        {
            List<TradeData> tradeDataList = new List<TradeData>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNodeList valueNodes = xmlDoc.SelectNodes("//value");

            foreach (XmlNode node in valueNodes)
            {
                TradeData tradeData = new TradeData
                {
                    Date = DateTime.Parse(node.Attributes["date"].Value),
                    Open = decimal.Parse(node.Attributes["open"].Value),
                    High = decimal.Parse(node.Attributes["high"].Value),
                    Low = decimal.Parse(node.Attributes["low"].Value),
                    Close = decimal.Parse(node.Attributes["close"].Value),
                    Volume = int.Parse(node.Attributes["volume"].Value)
                };

                tradeDataList.Add(tradeData);
            }

            return tradeDataList;
        }
    }
}
