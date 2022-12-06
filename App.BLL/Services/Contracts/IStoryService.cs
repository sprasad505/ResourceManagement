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
        public Story AddStory(Story st);
        Task<List<Story>> GetStories();
        public string PatchStory(string Id, Story st);
        Task<Story> DeleteStory(string Id);
    }
}
