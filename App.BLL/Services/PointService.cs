﻿using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class PointService
    {
        private readonly IGenericRepository<Point> genericRepository;
        public PointService(IGenericRepository<Point> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public Point AddPoint(Point po)
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
                return this.genericRepository.DeletePoint(Id);
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
                return this.genericRepository.PatchPoint(Id, po);
            }
            catch
            {
                throw;
            }
        }
    }
}