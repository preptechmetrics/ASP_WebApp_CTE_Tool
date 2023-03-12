using OfficeOpenXml;
using System.ComponentModel;

namespace TeachFieldLookup.Models
{
    public class ExcelData
    {
        public Dictionary<string, Dictionary<string, string>> GetDictionary(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();

                for (int row = 2; row <= rowCount; row++)
                {
                    string key = worksheet.Cells[row, 1].Value.ToString();
                    string subject = worksheet.Cells[row, 2].Value.ToString();
                    string credential = worksheet.Cells[row, 3].Value.ToString();
                    string teachField = worksheet.Cells[row, 4].Value.ToString();
                    string teachFieldName = worksheet.Cells[row, 5].Value.ToString();

                    if (dict.ContainsKey(teachField))
                    {
                        dict[teachField].Add(key, subject + "|" + credential + "|" + teachFieldName);
                    }
                    else
                    {
                        Dictionary<string, string> subDict = new Dictionary<string, string>();
                        subDict.Add(key, subject + "|" + credential + "|" + teachFieldName);
                        dict.Add(teachField, subDict);
                    }
                }

                return dict;
            }
        }
    }
}
