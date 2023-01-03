using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.DAL.Models;

namespace App.BLL.Services.Contracts
{
    public interface IResourceService
    {
        public string AddResources(Resource r);
        public string AddScrumMaster(Resource r);
        Task<List<Resource>> GetScrumMaster();
        Task<List<Resource>> GetResources();
        Task<List<Resource>> SearchResource(string Id);
        public string PatchResource(string Id, Resource r);
        public string DeleteResource(string Id);

    }
}
