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
            public string AddPoint(Point po);
            Task<List<Point>> GetPoints();
            public string PatchPoint(string Id,Point po);
            public string DeletePoint(string Id);
     
    }
}
