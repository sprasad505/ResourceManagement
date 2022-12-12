using App.DAL.DataContext;
using App.DAL.Middlewares;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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
        public string AddAlloc(Allocation a)
        {
            try
            {
                this.resourcedbContext.Add(a);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(a);
                return json;
                
            }
            catch
            {
                throw;
            }
        }
        public string AddSprint(Sprint s)
        {
            try
            {
                this.resourcedbContext.Add(s);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(s);
                return json;
            }
            catch
            {
                throw;
            }
        }

        public string AddProjects(Project p)
        {
            try
            {
                this.resourcedbContext.Add(p);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(p);
                return json;
            }
            catch
            {
                throw;
            }
        }

        public string AddResources(Resource r)
        {
            try
            {
                this.resourcedbContext.Add(r);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(r);
                return json;
            }
            catch
            {
                throw;
            }
        }

        public string AddTeams(Team t)
        {
            try
            {
                this.resourcedbContext.Add(t);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(t);
                return json;
            }
            catch
            {
                throw;
            }
        }
        public string AddStory(Story st)
        {
            try
            {
                st.CreatedOn = DateTime.Now;
                st.ModifiedOn = DateTime.Now;
                this.resourcedbContext.Add(st);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(st);
                return json;
            }
            catch
            {
                throw;
            }
        }

        public string AddPoint(Point po)
        {
            try
            {
                var data = this.resourcedbContext.Stories.Find(po.StoryId);
                data.ModifiedOn = DateTime.Now;
                this.resourcedbContext.SaveChanges();
                this.resourcedbContext.Add(po);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(po);
                return json;
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
                c1.Date = Convert.ToDateTime(DateTime.ParseExact(c.Date , "yyyy-MM-dd", CultureInfo.InvariantCulture));
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

        public async Task<List<Sprint>> GetSprints()
        {
            try
            {
                var output = await this.resourcedbContext.Set<Sprint>().ToListAsync();
                if (output == null)
                {
                    throw new APIException(404, "Table is empty");
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<Calendar22>> GetHolidays()
        {
            try
            {
                var output = await this.resourcedbContext.Set<Calendar22>().ToListAsync();
                if (output == null)
                {
                    throw new APIException(404, "Table is empty");
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Allocation>> GetAllocations()
        {
            try
            {
                var output = await this.resourcedbContext.Set<Allocation>().ToListAsync();
                if (output == null)
                {
                    throw new APIException(404, "Table is empty");
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Project>> GetProjects()
        {
            try
            {
                var output = await this.resourcedbContext.Set<Project>().ToListAsync();
                if (output == null)
                {
                    throw new APIException(404, "Table is empty");
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Resource>> GetResources()
        {
            try
            {
                var output = await this.resourcedbContext.Set<Resource>().ToListAsync();
                if (output == null)
                {
                    throw new APIException(404, "Table is empty");
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Team>> GetTeams()
        {
            try
            {
                var output = await this.resourcedbContext.Set<Team>().ToListAsync();
                if (output == null)
                {
                    throw new APIException(404, "Table is empty");
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Story>> GetStories()
        {
            try
            {
                var output = await this.resourcedbContext.Set<Story>().ToListAsync();
                if (output == null)
                {
                    throw new APIException(404, "Table is empty");
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Point>> GetPoints()
        {
            try
            {
                var output =  await this.resourcedbContext.Set<Point>().ToListAsync();
                if(output == null)
                {
                    throw new APIException(404, "Table is empty");
                }
                return output;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string PatchAllocation(string Id, Allocation alloc)
        {
            try
            {

                var data = this.resourcedbContext.Allocations.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                data.EmployeeId = alloc.EmployeeId;
                data.TeamId = alloc.TeamId;
                data.ProjectId = alloc.ProjectId;
                data.Role = alloc.Role;
                data.HoursPerDay = alloc.HoursPerDay;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string PatchProject(string Id, Project proj)
        {
            try
            {
                var data = this.resourcedbContext.Projects.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                data.Name = proj.Name;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string PatchTeam(string Id, Team team)
        {
            try
            {

                var data = this.resourcedbContext.Teams.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                data.Name = team.Name;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PatchSprint(long Id, Sprint sprint)
        {
            try
            {
                var data = this.resourcedbContext.Sprints.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                data.Name = sprint.Name;
                data.Duration = sprint.Duration;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PatchResource(string Id, Resource res)
        {
            try
            {
                var data = this.resourcedbContext.Resources.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                data.EmployeeId = res.EmployeeId;
                data.Email = res.Email;
                data.Name = res.Name;
                data.Designation = res.Designation;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string PatchStory(string Id, Story st)
        {
            try
            {
                var data = this.resourcedbContext.Stories.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                return "Empty";
                data.Name = st.Name;
                data.ModifiedOn = DateTime.Now;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PatchPoint(string Id, Point po)
        {
            try
            {
                var data = this.resourcedbContext.Points.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                var data1 = this.resourcedbContext.Stories.Find(data.StoryId);
                data1.ModifiedOn = DateTime.Now;
                this.resourcedbContext.SaveChanges();

                data.UserId = po.UserId;
                data.Points = po.Points;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteProject(string ProjectId)
        {
            try
            {
                var data = this.resourcedbContext.Projects.Find(Convert.ToInt64(ProjectId));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                this.resourcedbContext.Projects.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteAllocation(string AllocationId)
        {
            try
            {
                var data = this.resourcedbContext.Allocations.Find(Convert.ToInt64(AllocationId));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                this.resourcedbContext.Allocations.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteTeam(string TeamId)
        {
            try
            {
                var data = this.resourcedbContext.Teams.Find(Convert.ToInt64(TeamId));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                this.resourcedbContext.Teams.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteSprint(string Id)
        {
            try
            {
                var data = this.resourcedbContext.Sprints.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                this.resourcedbContext.Sprints.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteResource(string EmployeeId)
        {
            try
            {
                var data = this.resourcedbContext.Resources.Find(Convert.ToInt64(EmployeeId));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                this.resourcedbContext.Resources.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Story> DeleteStory(string Id)
        {
            try
            {
                var data = this.resourcedbContext.Stories.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                this.resourcedbContext.Stories.Remove(data);
                this.resourcedbContext.SaveChanges();
                var result = await this.resourcedbContext.Set<Point>().ToListAsync();
                foreach (var item in result)
                {
                    if(item.StoryId.Equals(Id))
                    {
                        this.resourcedbContext.Points.Remove(item);
                        this.resourcedbContext.SaveChanges();
                    }
                }
                var json = JsonConvert.SerializeObject(data);
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string DeletePoint(string Id)
        {
            try
            {
                var data = this.resourcedbContext.Points.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(404, "No content with matching Id");
                }
                var data1 = this.resourcedbContext.Stories.Find(data.StoryId);
                data1.ModifiedOn = DateTime.Now;
                this.resourcedbContext.SaveChanges();
                this.resourcedbContext.Points.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data);
                return json;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Allocation>> SearchAllocation(string Id)
        {
            try
            {
                List<Allocation> a = new List<Allocation>();
                var result = await this.resourcedbContext.Set<Allocation>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.ProjectId == Convert.ToInt64(Id))
                    {
                        a.Add(item);
                    }
                }
                if (a == null)
                {
                    throw new APIException(409, "Not found");
                }
                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Resource>> SearchResource(string Id)
        {
            try
            {
                List<Resource> r = new List<Resource>();
                var result = await this.resourcedbContext.Set<Resource>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.ProjectId == Convert.ToInt64(Id))
                    {
                        r.Add(item);
                    }
                }
                if (r == null)
                {
                    throw new APIException(409, "Not found");
                }
                return r;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Team>> SearchTeam(string Id)
        {
            try
            {
                List<Team> t = new List<Team>();
                var result = await this.resourcedbContext.Set<Team>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.ProjectId == Convert.ToInt64(Id))
                    {
                        t.Add(item);
                    }
                }
                if (t == null)
                {
                    throw new APIException(409, "Not found");
                }
                return t;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Sprint>> SearchSprint(string Id)
        {
            try
            {
                List<Sprint> s = new List<Sprint>();    
                var result = await this.resourcedbContext.Set<Sprint>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.ProjectId == Convert.ToInt64(Id))
                    {
                        s.Add(item);
                    }
                }
                return s;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Story>> SearchStory(string Id)
        {
            try
            {
                List<Story> s = new List<Story>();
                var result = await this.resourcedbContext.Set<Story>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.ProjectId == Convert.ToInt64(Id))
                    {
                        s.Add(item);
                    }
                }
                if (s == null)
                {
                    throw new APIException(409, "Not found");
                }
                return s;
            }
            catch(Exception ex)
            {
                throw ex;
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
                if (p.Id == null)
                {
                    throw new APIException(409, "Not found");
                }
                return p;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Adduser(string email, byte[] passwordHash, byte[] passwordSalt)
        {
            try
            {
                User user = new User();
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
