using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using TeachFieldLookup.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace TeachFieldLookup.Controllers
{
    public class TeachingFieldController : ApiController
    {
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/teachingfield/{teachFieldCode}")]
        public IHttpActionResult GetTeachingField(string teachFieldCode)
        {
            string filePath = @"C:\path\to\CTE_Codes_Project.xlsx";
            ExcelData excelData = new ExcelData();
            Dictionary<string, Dictionary<string, string>> dict = excelData.GetDictionary(filePath);

            if (dict.ContainsKey(teachFieldCode))
            {
                Dictionary<string, string> subDict = dict[teachFieldCode];
                return Ok(new
                {
                    Subject = subDict["Subject"],
                    Credential = subDict["Credential"],
                    TeachField = subDict["TeachField"],
                    TeachFieldName = subDict["TeachFieldName"]
                });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
