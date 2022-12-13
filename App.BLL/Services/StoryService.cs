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
    public class StoryService : IStoryService
    {
        private readonly IGenericRepository<Story> genericRepository;
        public StoryService(IGenericRepository<Story> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public string AddStory(Story st)
        {
            var data = this.genericRepository.AddStory(st);
            return data;
        }

        public async Task<Story> DeleteStory(string Id)
        {
            return await this.genericRepository.DeleteStory(Id);
        }

        public async Task<List<Story>> GetStories()
        {
            return await this.genericRepository.GetStories();
        }
        public async Task<List<Story>> SearchStory(string Id)
        {
            try
            {
                return await this.genericRepository.SearchStory(Id);
            }
            catch
            {
                throw;
            }
        }

        public string PatchStory(string Id, Story st)
        {
            var data = this.genericRepository.PatchStory(Id, st);
            return data;
        }
    }
}
