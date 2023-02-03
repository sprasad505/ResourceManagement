using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services.Contracts
{
    public interface IPointService
    {
            public string AddPoint(List<Point> po);
            Task<List<Point>> GetPoints();
            Task<List<Point>> SearchPoint(string Id);
            public string PatchPoint(string Id,Point po);
            public string DeletePoint(string Id);
     
    }
}
