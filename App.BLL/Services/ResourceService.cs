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
            return await genericRepository.GetResources();
        }
        public Resource AddResources(Resource r)
        {
            return genericRepository.AddResources(r);
        }

        public string PatchResource(string Id, Resource r)
        {
            var data = genericRepository.PatchResource(Id, r);
            return data;
        }

        public string DeleteResource(string Id)
        {
            var data = genericRepository.DeleteResource(Id);
            return data;
        }
        public async Task<List<Resource>> SearchResource(string Id)
        {
            return await genericRepository.SearchResource(Id);
        }
    }

}