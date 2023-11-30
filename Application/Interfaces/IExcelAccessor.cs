using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Excel;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IExcelAccessor
    {
        Task<DataPeriodResult> GetDataClient();

        Task<List<DataApartmentResult>> GetDataApartment(IFormFile file);
         
    }
}