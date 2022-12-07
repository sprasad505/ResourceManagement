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

        public Story AddStory(Story st)
        {
            try
            {
                return this.genericRepository.AddStory(st);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Story> DeleteStory(string Id)
        {
            try
            {
                return await this.genericRepository.DeleteStory(Id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Story>> GetStories()
        {
            try
            {
                return await this.genericRepository.GetStories();
            }
            catch
            {
                throw;
            }
        }

        public string PatchStory(string Id, Story st)
        {
            try
            {
                return this.genericRepository.PatchStory(Id, st);
            }
            catch
            {
                throw;
            }
        }
    }
}
