using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services.Contracts
{
    public interface IStoryService
    {
        public string AddStory(Story st);
        Task<List<Story>> GetStories();
        Task<List<Story>> SearchStory(string Id);
        Task<List<Story>> GetAddedStories(string Id);
        public string PatchStory(string Id, Story st);
        Task<Story> DeleteStory(string Id);
        public string AddStorytoSprint(List<Story> st);
        
    }
}
