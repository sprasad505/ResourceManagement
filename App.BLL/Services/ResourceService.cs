using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using App.BLL.Services.Contracts;

namespace App.BLL.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IGenericRepository<Resource> genericRepository;
        public ResourceService(IGenericRepository<Resource> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task<List<Resource>> GetResources()
        {
            try
            {
                return await genericRepository.GetResources();
            }
            catch
            {
                throw;
            }
        }
        public Resource AddResources(Resource r)
        {
            try
            {
                return genericRepository.AddResources(r);
            }
            catch
            {
                throw;
            }
        }

        public string PatchResource(string Id, Resource r)
        {
            try
            {
              var data= genericRepository.PatchResource(Id, r);
                return data;

            }
            catch
            {
                throw;
            }
        }

        public string DeleteResource(string Id)
        {
            try
            {
                var data= genericRepository.DeleteResource(Id);
                return data;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Resource> SearchResource(string Id)
        {
            try
            {
                var result = await genericRepository.SearchResource(Id);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }

}