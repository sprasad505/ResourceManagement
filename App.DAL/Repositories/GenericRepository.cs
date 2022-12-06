using App.DAL.DataContext;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly ResourcedbContext resourcedbContext;
        private readonly IAuthenticationService authenticationService;
        private readonly UsersContext usersContext;

        public GenericRepository(ResourcedbContext resourcedbContext, UsersContext usersContext, IAuthenticationService authenticationService)
        {
            this.resourcedbContext = resourcedbContext;
            this.authenticationService = authenticationService;
            this.usersContext = usersContext;
        }
        public Allocation AddAlloc(Allocation a)
        {
            try
            {
                
                this.resourcedbContext.Add(a);
                this.resourcedbContext.SaveChanges();
                return a;
            }
            catch
            {
                throw;
            }
        }
        public Sprint AddSprint(Sprint s)
        {
            try
            {
                this.resourcedbContext.Add(s);
                this.resourcedbContext.SaveChanges();
                return s;
            }
            catch
            {
                throw;
            }
        }

        public Project AddProjects(Project p)
        {
            try
            {
                this.resourcedbContext.Add(p);
                this.resourcedbContext.SaveChanges();
                return p;
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
                this.resourcedbContext.Add(r);
                this.resourcedbContext.SaveChanges();
                return r;
            }
            catch
            {
                throw;
            }
        }

        public Team AddTeams(Team t)
        {
            try
            {
                this.resourcedbContext.Add(t);
                this.resourcedbContext.SaveChanges();
                return t;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Sprint>> GetSprints()
        {
            try
            {
                return await this.resourcedbContext.Set<Sprint>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public InterCalender AddHolidays(InterCalender c)
        {
            try
            {
                Calendar22 c1 = new Calendar22();
                //c1.Date = DateTime.Parse(c.Date);
                //c1.Date = Convert.ToDateTime(c.Date);
                c1.Date = Convert.ToDateTime(DateTime.ParseExact(c.Date , "dd-MM-yyyy", CultureInfo.InvariantCulture));
                c1.Name = c.Name;
                this.resourcedbContext.Calender22s.Add(c1);
                this.resourcedbContext.SaveChanges();
                return c;
            } 

            catch
            {
                throw;
            }
        }
        public async Task<List<Calendar22>> GetHolidays()
        {
            try
            {
                return await this.resourcedbContext.Set<Calendar22>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Allocation>> GetAllocations()
        {
            try
            {
                return await this.resourcedbContext.Set<Allocation>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Project>> GetProjects()
        {
            try
            {
                return await this.resourcedbContext.Set<Project>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Resource>> GetResources()
        {
            try
            {
                return await this.resourcedbContext.Set<Resource>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Team>> GetTeams()
        {
            try
            {
                return await this.resourcedbContext.Set<Team>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public string PatchAllocation(string Id, Allocation alloc)
        {
            try
            {

                var data = this.resourcedbContext.Allocations.Find(Convert.ToInt64(Id));
                if (data == null)
                    return "Empty";

                data.EmployeeId = alloc.EmployeeId;
                data.TeamId = alloc.TeamId;
                data.ProjectId = alloc.ProjectId;
                data.Role = alloc.Role;
                data.HoursPerDay = alloc.HoursPerDay;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;

            }
            catch
            {
                throw;

            }
        }
        public string PatchProject(string Id, Project proj)
        {
            try
            {

                var data = this.resourcedbContext.Projects.Find(Convert.ToInt64(Id));
                if (data == null)
                    return "Empty";

                data.Name = proj.Name;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;


            }
            catch
            {
                throw;

            }
        }
        public string PatchTeam(string Id, Team team)
        {
            try
            {

                var data = this.resourcedbContext.Teams.Find(Convert.ToInt64(Id));
                if (data == null)
                    return "Empty";

                data.Name = team.Name;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;


            }
            catch
            {
                throw;

            }
        }

        public string PatchSprint(string Id, Sprint sprint)
        {
            try
            {
                var data = this.resourcedbContext.Sprints.Find(Convert.ToInt64(Id));
                if (data == null)
                    return "Empty";

                data.Name = sprint.Name;
                data.Duration = sprint.Duration;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;


            }
            catch
            {
                throw;

            }
        }

        public string PatchResource(string Id, Resource res)
        {
            try
            {

                var data = this.resourcedbContext.Resources.Find(Convert.ToInt64(Id));
                if (data == null)
                    return "Empty";

                data.EmployeeId = res.EmployeeId;
                data.Email = res.Email;
                data.Name = res.Name;
                data.Designation = res.Designation;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;


            }
            catch
            {
                throw;

            }
        }
        public string DeleteProject(string ProjectId)
        {
            try
            {
                var data = this.resourcedbContext.Projects.Find(Convert.ToInt64(ProjectId));
                if (data == null)
                {
                    //throw new HttpResponseException(HttpStatusCode.NotFound);
                    return "no data found";

                }
                this.resourcedbContext.Projects.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;


            }
            catch
            {

                throw;
            }
        }

        public string DeleteAllocation(string AllocationId)
        {
            try
            {


                var data = this.resourcedbContext.Allocations.Find(Convert.ToInt64(AllocationId));
                if (data == null)
                {
                    return "no data found";

                }
                this.resourcedbContext.Allocations.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;


            }

            catch
            {

                throw;
            }
        }

        public string DeleteTeam(string TeamId)
        {
            try
            {


                var data = this.resourcedbContext.Teams.Find(Convert.ToInt64(TeamId));
                if (data == null)
                {
                    return "no data found";

                }
                this.resourcedbContext.Teams.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;



            }

            catch
            {

                throw;
            }
        }

        public string DeleteSprint(string Id)
        {
            try
            {


                var data = this.resourcedbContext.Sprints.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    return "no data found";

                }
                this.resourcedbContext.Sprints.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }

            catch
            {

                throw;
            }
        }

        public string DeleteResource(string EmployeeId)
        {
            try
            {
                var data = this.resourcedbContext.Resources.Find(Convert.ToInt64(EmployeeId));
                if (data == null)
                {
                    return "no data found";

                }
                this.resourcedbContext.Resources.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;

            }

            catch
            {

                throw;
            }
        }

        public async Task<Allocation> SearchAllocation(string Id)
        {
            try
            {
                Allocation a = new Allocation();
                var result = await this.resourcedbContext.Set<Allocation>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.EmployeeId == Id)
                    {
                        a = item;
                        break;
                    }
                }
                return a;
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
                Resource r = new Resource();
                var result = await this.resourcedbContext.Set<Resource>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.EmployeeId == Id)
                    {
                        r = item;
                        break;
                    }
                }
                return r;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Team> SearchTeam(string name)
        {
            try
            {
                Team t = new Team();
                var result = await this.resourcedbContext.Set<Team>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.Name == name)
                    {
                        t = item;
                        break;
                    }
                }
                return t;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Sprint> SearchSprint(string name)
        {
            try
            {
                Sprint s = new Sprint();
                var result = await this.resourcedbContext.Set<Sprint>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.Name == name)
                    {
                        s = item;
                        break;
                    }
                }
                return s;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Project> SearchProject(string name)
        {
            try
            {
                Project p = new Project();
                var result = await this.resourcedbContext.Set<Project>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.Name == name)
                    {
                        p = item;
                        break;
                    }
                }
                return p;
            }
            catch
            {
                throw;
            }
        }

        public void Adduser(string email, byte[] passwordHash, byte[] passwordSalt)
        {
            try
            {
                User user = new User();
                //CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.Username = email;
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;
                this.usersContext.Add(user);
                this.usersContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public User Login(Userdto request)
        {
            try
            {
                return this.usersContext.Users.Find(request.Username);
            }
            catch
            {
                throw;

            }
        }
    }
}
