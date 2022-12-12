using App.BLL.Services.Contracts;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class PointService : IPointService
    {
        private readonly IGenericRepository<Point> genericRepository;
        public PointService(IGenericRepository<Point> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public string AddPoint(Point po)
        {
            try
            {
                return this.genericRepository.AddPoint(po);
            }
            catch
            {
                throw;
            }
        }

        public string DeletePoint(string Id)
        {
            try
            {
                var data=genericRepository.DeletePoint(Id);
                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Point>> GetPoints()
        {
            try
            {
                return await this.genericRepository.GetPoints();
            }
            catch
            {
                throw;
            }
        }

        public string PatchPoint(string Id, Point po)
        {
            try
            {
                var data=genericRepository.PatchPoint(Id, po);
                return data;
            }
            catch
            {
                throw;
            }
        }
    }
}
