using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Excel;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Infrastructure.Excel
{
    public class ExcelAccessor : IExcelAccessor
    {
        public async Task<List<DataApartmentResult>> GetDataApartment(IFormFile file)
        {
            List<DataApartmentResult> listData = new List<DataApartmentResult>();
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                ISheet sheet = xssWorkbook.GetSheetAt(0);

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                    listData.Add(new DataApartmentResult
                    {
                        Name = row.GetCell(0).ToString(),
                        Floor = row.GetCell(1) == null ? 0 : Convert.ToInt32(row.GetCell(1).ToString()),
                        Percentage = row.GetCell(2) == null ? 0 : Convert.ToDouble(row.GetCell(2).ToString()),
                    });

                }
            }
            return listData;
        }

        public Task<DataPeriodResult> GetDataClient()
        {
            throw new NotImplementedException();
        }
    }
}