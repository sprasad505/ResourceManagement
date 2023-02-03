using App.DAL.DataContext;
using App.DAL.Middlewares;
using App.DAL.Models;
using App.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

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
                var dataProj = this.resourcedbContext.Set<Project>().ToList();
                var dataTeam = this.resourcedbContext.Set<Team>().ToList();
                var dataRes = this.resourcedbContext.Set<Resource>().ToList();
                var dataalloc = this.resourcedbContext.Set<Allocation>().ToList();
                bool testProj = false;
                bool testTeam = false;
                bool testRes = false;
                bool testalloc = false;
                foreach(var alloc in dataalloc)
                {
                    if(a.ResourceId == alloc.ResourceId && a.ProjectId == alloc.ProjectId)
                    {
                        testalloc = true;
                        break;
                    }
                }
                foreach (var item in dataProj)
                {
                    if (a.ProjectId == item.Id)
                    {
                        testProj = true;
                        break;
                    }
                }
                if (!testProj)
                {
                    throw new APIException(409, "Retry with a valid ProjectId");
                }
                foreach (var item in dataTeam)
                {
                    if (a.TeamId == item.Id)
                    {
                        testTeam = true;
                        break;
                    }
                }
                if (!testTeam)
                {
                    throw new APIException(409, "Retry with a valid TeamId");
                }
                foreach (var item in dataRes)
                {
                    if (a.ResourceId == item.Id)
                    {
                        testRes = true;
                        break;
                    }
                }
                if (!testRes)
                {
                    throw new APIException(409, "Retry with a valid ResourceId");
                }
                if(testalloc)
                {
                    throw new APIException(409, "Resource Already Allocated to the same project");
                }
                this.resourcedbContext.Add(a);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(a, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string AddScrumMaster(Resource r)
        {
            try
            {
                var dataRes = this.resourcedbContext.Set<Resource>().ToList();
                bool testRes = false;
                foreach (var item in dataRes)
                {
                    if (item.EmployeeId == r.EmployeeId )
                    {
                        testRes = true;   
                    }
                }
                if(testRes)
                {
                    throw new APIException(409, "Employee already exists");
                }
                r.Role = Resource.Designation.ScrumMaster;
                this.resourcedbContext.Add(r);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(r, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string AddSprint(Sprint s)
        {
            try
            {
                var dataProj = this.resourcedbContext.Set<Project>().ToList();
                bool testProj = false;
                foreach (var item in dataProj)
                {
                    if (s.ProjectId == item.Id)
                    {
                        testProj = true;
                        break;
                    }
                }
                if (!testProj)
                {
                    throw new APIException(409, "Retry with a valid ProjectId");
                }
                s.PlanningSprint = false;
                this.resourcedbContext.Add(s);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(s, Formatting.Indented,
                 new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                 });
                return json;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string AddProjects(Project p, string id)
        {
            try
            {
                this.resourcedbContext.Add(p);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(p, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                var r = this.resourcedbContext.Resources.Find(Convert.ToInt64(id));
                r.ProjectId = p.Id;
                this.resourcedbContext.SaveChanges();
                return json;
            }
            catch
            {
                throw;
            }
        }
        public string AddLeave(Leave l)
        {
            try
            {
                var emp = resourcedbContext.Set<Resource>().ToList();
                var holiday = resourcedbContext.Set<Calendar22>().ToList();
                bool checkemp = false;
                bool checkholiday = false;
                string name = null;
                foreach (var item in emp)
                {
                    if(l.EmployeeId == item.EmployeeId)
                    {
                        checkemp = true;
                        name = item.Name;
                        break;
                    }
                }
                if(!checkemp)
                {
                    throw new APIException(409, "Retry with a valid EmployeeId");
                }
                foreach(var item in holiday)
                {
                    if(l.LeaveDate == item.Date)
                    {
                        checkholiday = true;
                        break;
                    }
                }
                if(checkholiday)
                {
                    throw new APIException(409, "Date is already a holiday");
                }
                l.Name = name;
                l.CreatedDate = DateTime.Now;
                l.ModifiedDate = DateTime.Now;
                if(l.hours == 0)
                {
                    l.hours = 8;
                }
                this.resourcedbContext.Add(l);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(l, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch(Exception ex)
            {
                throw(ex);
            }
        }

        public string AddResources(Resource r)
        {
            try
            {
                var dataProj = this.resourcedbContext.Set<Project>().ToList();
                var dataRes = this.resourcedbContext.Set<Resource>().ToList();
                bool testProj = false;
                bool testRes = false;
                foreach (var item in dataProj)
                {
                    if (r.ProjectId == item.Id)
                    {
                        testProj = true;
                        break;
                    }
                }
                if (!testProj)
                {
                    throw new APIException(409, "Retry with a valid ProjectId");
                }
                foreach (var item in dataRes)
                {
                    if (r.EmployeeId == item.EmployeeId)
                    {
                        testRes = true;
                        break;
                    }
                }
                if (testRes)
                {
                    throw new APIException(409, "Employee Already exists! Retry with a new EmployeeId");
                }
                this.resourcedbContext.Add(r);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(r, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string AddTeams(Team t)
        {
            try
            {
                var dataProj = this.resourcedbContext.Set<Project>().ToList();
                bool testProj = false;
                foreach (var item in dataProj)
                {
                    if (t.ProjectId == item.Id)
                    {
                        testProj = true;
                        break;
                    }
                }
                if (!testProj)
                {
                    throw new APIException(409, "Retry with a valid ProjectId");
                }
                this.resourcedbContext.Add(t);
                this.resourcedbContext.SaveChanges();
                var json=JsonConvert.SerializeObject(t, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string AddStory(Story st)
        {
            try
            {
                var dataProj = this.resourcedbContext.Set<Project>().ToList();
                bool checkproj = false;
                foreach (var item in dataProj)
                {
                    if (st.ProjectId == item.Id)
                    {
                        checkproj = true;
                        break;
                    }
                }
                if (!checkproj)
                {
                    throw new APIException(409, "Retry with a valid ProjectId");
                }
                st.CreatedOn = DateTime.Now;
                st.ModifiedOn = DateTime.Now;
                this.resourcedbContext.Add(st);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(st, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public string AddPoint(List<Point> po)
        {
            try
            {
                var datapoint = resourcedbContext.Set<Point>().ToList();
                foreach (var item in po)
                {
                    bool checkpoint = true;
                    var data = this.resourcedbContext.Stories.Find(item.StoryId);
                    if (data == null)
                    {
                        throw new APIException(409, "Story Doesn't exist!");
                    }
                    foreach (var p in datapoint)
                    {
                        if (item.StoryId == p.StoryId && item.TeamId == p.TeamId)
                        {
                            checkpoint = false;
                            p.Points = item.Points;
                            data.ModifiedOn = DateTime.Now;
                            this.resourcedbContext.SaveChanges();
                            break;
                        }
                    }
                    if (checkpoint)
                    {
                        data.ModifiedOn = DateTime.Now;
                        this.resourcedbContext.SaveChanges();
                        this.resourcedbContext.Add(item);
                        this.resourcedbContext.SaveChanges();
                    }
                }
                var json = JsonConvert.SerializeObject(po, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch
            {
                throw;
            }
        }

        public string AddHolidays(InterCalender c)
        {
            try
            {
                Calendar22 c1 = new Calendar22();
                c1.Date = Convert.ToDateTime(DateTime.ParseExact(c.Date , "yyyy-MM-dd", CultureInfo.InvariantCulture));
                c1.Name = c.Name;
                this.resourcedbContext.Calender22s.Add(c1);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(c, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            } 
            catch
            {
                throw;
            }
        }
        public async Task<List<Leave>> GetallLeaves()
        {
            try
            {
                var result = await this.resourcedbContext.Set<Leave>().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Sprint>> GetSprints()
        {
            try
            {
                var output = await this.resourcedbContext.Set<Sprint>().ToListAsync();
                if (output == null)
                {
                    throw new APIException(409, "Sprints are empty");
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Leave>> GetLeaves(string EmployeeId)
        {
            try
            {
                List<Leave> leave = new List<Leave>();
                var data = await this.resourcedbContext.Set<Leave>().ToListAsync();
                foreach(var item in data)
                {
                    if(item.EmployeeId == EmployeeId)
                    {
                        leave.Add(item);
                    }
                }
                if (leave == null)
                {
                    throw new APIException(409, "No Leaves for this employee");
                }
                return leave;
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
                    throw new APIException(409, "Holidays are empty");
                }
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Alloc>> GetAllocations()
        {
            try
            {
                List<Alloc> a = new List<Alloc>();
                var test2=resourcedbContext.Allocations.Join
                                (resourcedbContext.Resources,
                                a => a.ResourceId,
                                b => b.Id,
                                (a, b) => new
                                {
                                    Id = a.Id,
                                    EmployeeId = b.EmployeeId,
                                    Name = b.Name,
                                    TeamId = a.TeamId,
                                    ProjectId = a.ProjectId,
                                    HoursPerDay = a.HoursPerDay
                                }).Join(resourcedbContext.Teams,
                                a=>a.TeamId,
                                b=>b.Id,
                                (a, b) => new
                                {
                                    Id = a.Id,
                                    EmployeeId = a.EmployeeId,
                                    Name = a.Name,
                                    TeamId = a.TeamId,
                                    TeamName = b.Name,
                                    ProjectId = a.ProjectId,
                                    HoursPerDay = a.HoursPerDay
                                }).ToList();
                foreach(var item in test2)
                {
                    a.Add(new Alloc
                    {
                        Id = item.Id,
                        EmployeeId = item.EmployeeId,
                        Name = item.Name,
                        TeamId = item.TeamId,
                        TeamName = item.TeamName,
                        ProjectId = item.ProjectId,
                        HoursPerDay = item.HoursPerDay
                    });
                }
                //var output = await this.resourcedbContext.Set<Allocation>().ToListAsync();
                if (test2.Count == 0)
                {
                    throw new APIException(409, "Allocations are empty");
                }
                return a;
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
                    throw new APIException(409, "Projects are empty");
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
                    throw new APIException(409, "Resources are empty");
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
                    throw new APIException(409, "Teams are empty");
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
                    throw new APIException(409, "Stories are empty");
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
                    throw new APIException(409, "Points are empty");
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
                bool testalloc = false;
                var dataalloc = this.resourcedbContext.Set<Allocation>().ToList();
                foreach (var a in dataalloc)
                {
                    if (alloc.ResourceId == a.ResourceId && alloc.ProjectId == a.ProjectId)
                    {
                        testalloc = true;
                        break;
                    }
                }
                var data = this.resourcedbContext.Allocations.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(409, "No content with matching Id");
                }
                if(testalloc)
                {
                    throw new APIException(409, "Resource already allocated to the same project");
                }
            
                data.ResourceId = alloc.ResourceId;
                data.TeamId = alloc.TeamId;
                data.ProjectId = alloc.ProjectId;
                data.HoursPerDay = alloc.HoursPerDay;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
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
                    throw new APIException(409, "No content with matching Id");
                }
                data.Name = proj.Name;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string PatchLeave(string Id, Leave l)
        {
            try
            {   
                var data = this.resourcedbContext.Leaves.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(409, "No content with matching Id");
                }
                data.LeaveDate = l.LeaveDate;
                data.ModifiedDate = DateTime.Now;
                if(l.hours == null)
                {
                    data.hours = 8;
                }
                else
                {
                    data.hours = l.hours;
                }
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
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
                    throw new APIException(409, "No content with matching Id");
                }
                data.Name = team.Name;
                data.ProjectId = team.ProjectId;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
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
                    throw new APIException(409, "No content with matching Id");
                }
                data.name = sprint.name;
                data.Duration = sprint.Duration;
                data.PlanningSprint = false;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string PlanningSprint(String Id)
        {
            try
            {
                var sprintdata = this.resourcedbContext.Set<Sprint>().ToList();
                var data = this.resourcedbContext.Sprints.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(409, "No content with matching Id");
                }
                if (data.PlanningSprint == true)
                {
                    throw new APIException(409, "Selected Sprint is already a Planning Sprint");
                }
                foreach (var sprint in sprintdata)
                {
                    if(sprint.ProjectId == data.ProjectId && sprint.Id != Convert.ToInt64(Id))
                    {
                        sprint.PlanningSprint = false;
                    }
                }
                data.PlanningSprint = true;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public string PatchResource(string Id, Resource res)
        {
            try
            {
                var data = this.resourcedbContext.Resources.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(409, "No content with matching Id");
                }
                data.EmployeeId = res.EmployeeId;
                data.Email = res.Email;
                data.Name = res.Name;
                data.Role = res.Role;
                data.ProjectId = res.ProjectId;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
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
                    throw new APIException(409, "No content with matching Id");
                }
                data.Name = st.Name;
                data.ModifiedOn = DateTime.Now;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Story> AddStorytoSprint(List<Story> st)
        {
            try
            {
                var datasprint = this.resourcedbContext.Set<Sprint>().ToList();
                List<Story> storyadded = new List<Story>();
                bool checksp = false;
                foreach (var stItem in st)
                {
                    var data = this.resourcedbContext.Stories.Find(Convert.ToInt64(stItem.Id));
                    if (data == null)
                    {
                        throw new APIException(409, "No content with matching Id");
                    }
                    if (stItem.SprintId == null)
                    {
                        throw new APIException(409, "Add a Sprint Id");
                    }
                    foreach (var item in datasprint)
                    {
                        if (stItem.SprintId == item.Id)
                        {
                            checksp = true;
                            break;
                        }
                    }
                    if (!checksp)
                    {
                        throw new APIException(409, "Retry with a valid SprintId");
                    }
                    data.ModifiedOn = DateTime.Now;
                    data.SprintId = stItem.SprintId;
                    this.resourcedbContext.SaveChanges();
                    storyadded.Add(data);    
                }
                return storyadded;
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
                    throw new APIException(409, "No content with matching Id");
                }
                var data1 = this.resourcedbContext.Stories.Find(data.StoryId);
                data1.ModifiedOn = DateTime.Now;
                this.resourcedbContext.SaveChanges();

                data.TeamId = po.TeamId;
                data.Points = po.Points;
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
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
                var sprintdata = this.resourcedbContext.Set<Sprint>().ToList();
                var allocdata = this.resourcedbContext.Set<Allocation>().ToList();
                var teamdata = this.resourcedbContext.Set<Team>().ToList();
                var resdata = this.resourcedbContext.Set<Resource>().ToList();
                if (data == null)
                {
                    throw new APIException(409, "No content with matching Id");
                }
                foreach (var alloc in allocdata)
                {
                    if (alloc.ProjectId == Convert.ToInt64(ProjectId))
                    {
                        DeleteAllocation((alloc.Id).ToString());
                    }
                }
                foreach (var sprint in sprintdata)
                {
                    if (sprint.ProjectId == Convert.ToInt64(ProjectId))
                    {
                        DeleteSprint((sprint.Id).ToString());
                    }
                }
                foreach (var team in teamdata)
                {
                    if (team.ProjectId == Convert.ToInt64(ProjectId))
                    {
                        DeleteTeam((team.Id).ToString());
                    }
                }
                foreach (var resource in resdata )
                {
                    if(resource.ProjectId == Convert.ToInt64(ProjectId))
                    {
                        DeleteResource((resource.Id).ToString());
                    }
                }
                this.resourcedbContext.Projects.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteLeave(string LeaveId)
        {
            try
            {
                var data = this.resourcedbContext.Leaves.Find(Convert.ToInt64(LeaveId));
                if (data == null)
                {
                    throw new APIException(409, "No content with matching Id");
                }
                this.resourcedbContext.Leaves.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
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
                    throw new APIException(409, "No content with matching Id");
                }
                this.resourcedbContext.Allocations.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
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
                    throw new APIException(409, "No content with matching Id");
                }
                this.resourcedbContext.Teams.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
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
                var storydata = this.resourcedbContext.Set<Story>().ToList();
                if (data == null)
                {
                    throw new APIException(409, "No content with matching Id");
                }
                foreach (var story in storydata)
                {
                    if (story.SprintId == Convert.ToInt64(Id))
                    {
                        DeleteStory((story.Id).ToString());
                    }
                }
                this.resourcedbContext.Sprints.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
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
                    throw new APIException(409, "No content with matching Id");
                }
                var leavedata = this.resourcedbContext.Set<Leave>().ToList();
                foreach(var leave in leavedata)
                {
                    if(leave.EmployeeId == data.EmployeeId)
                    {
                        DeleteLeave((leave.Id).ToString());
                    }
                }
                this.resourcedbContext.Resources.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  string DeleteStory(string Id)
        {
            try
            {
                var data = this.resourcedbContext.Stories.Find(Convert.ToInt64(Id));
                if (data == null)
                {
                    throw new APIException(409, "No content with matching Id");
                }
                var result = this.resourcedbContext.Set<Point>().ToList();
                foreach (var item in result)
                {
                    if(item.StoryId == Convert.ToInt64(Id))
                    {
                        this.resourcedbContext.Points.Remove(item);
                        this.resourcedbContext.SaveChanges();
                    }
                }
                this.resourcedbContext.Stories.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
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
                    throw new APIException(409, "No content with matching Id");
                }
                var data1 = this.resourcedbContext.Stories.Find(data.StoryId);
                data1.ModifiedOn = DateTime.Now;
                this.resourcedbContext.SaveChanges();
                this.resourcedbContext.Points.Remove(data);
                this.resourcedbContext.SaveChanges();
                var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return json;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Alloc>> SearchAllocation(string Id)
        {
            try
            {
                List<Allocation> a = new List<Allocation>();
                long id=0;
                var result = await this.resourcedbContext.Set<Allocation>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.ProjectId == Convert.ToInt64(Id))
                    {
                        a.Add(item);
                    }
                }
                if (a.Count == 0)
                {
                    throw new APIException(409, "No Allocations found");
                }
                List<Alloc> allocs = new List<Alloc>();
                foreach(var item in a)
                {
                    var team = this.resourcedbContext.Teams.Find(item.TeamId);
                    string teamname = team.Name;
                    var resource = this.resourcedbContext.Resources.Find(item.ResourceId);
                    string employeename = resource.Name;
                    string employeeId = resource.EmployeeId;
                    allocs.Add(new Alloc { Id = item.Id, EmployeeId = employeeId, 
                                      Name = employeename, TeamId = item.TeamId,
                                      TeamName = teamname, ProjectId = item.ProjectId,
                                      HoursPerDay = item.HoursPerDay});
                }

                return allocs;
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
                    throw new APIException(409, "No Resources found");
                }
                return r;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Resource>> GetScrumMaster()
        {
            try
            {
                List<Resource> r = new List<Resource>();
                var result = await this.resourcedbContext.Set<Resource>().ToListAsync();
                foreach(var item in result)
                {
                    if(item.Role == Resource.Designation.ScrumMaster)
                    {
                        r.Add(item);
                    }

                }
                if (r == null)
                {
                    throw new APIException(409, "No Scrum Masters found");
                }
                return r;
            }
            catch (Exception ex)
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
                    throw new APIException(409, "No Teams found");
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
                if (s == null)
                {
                    throw new APIException(409, "No Sprints found");
                }
                return s;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<Story>> SearchStory(string Id)
        {
            try
            {
                List<Story> s = new List<Story>();
                var datastory = await this.resourcedbContext.Set<Story>().ToListAsync();
                var datapoint = await this.resourcedbContext.Set<Point>().ToListAsync();
                foreach (var story in datastory)
                {
                    if (story.ProjectId == Convert.ToInt64(Id))
                    {
                        foreach(var point in datapoint)
                        {
                            if(point.StoryId == story.Id)
                            {
                                story.Points.Add(point);
                            }
                        }
                        s.Add(story);
                    }
                }
                
                if (s == null)
                {
                    throw new APIException(409, "No Stories found");
                }
                return s;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Point>> SearchPoint(string Id)
        {
            try
            {
                List<Point> p = new List<Point>();
                var result = await this.resourcedbContext.Set<Point>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.StoryId == Convert.ToInt64(Id))
                    {
                        p.Add(item);
                    }
                }
                if (p == null)
                {
                    throw new APIException(409, "No Story Points found");
                }
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Story>> StoriesLeft(string Id)
        {
            try
            {
                List<Story> s = new List<Story>();
                var result = await this.resourcedbContext.Set<Story>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.ProjectId == Convert.ToInt64(Id) && item.SprintId == null)
                    {
                        s.Add(item);
                    }
                }
                if (s == null)
                {
                    throw new APIException(409, "No Stories left");
                }
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Story>> GetAddedStories(string Id)
        {
            try
            {
                List<Story> s = new List<Story>();
                var result = await this.resourcedbContext.Set<Story>().ToListAsync();
                foreach (var item in result)
                {
                    if (item.SprintId == Convert.ToInt64(Id))
                    {
                        s.Add(item);
                    }
                }
                if (s == null)
                {
                    throw new APIException(409, "No Added Sories found");
                }
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Project>> SearchProject(string id)
        {
            try
            {
                List<Project> p = new List<Project>();
                var projdata = await this.resourcedbContext.Set<Project>().ToListAsync();
                var allocdata = await this.resourcedbContext.Set<Allocation>().ToListAsync();
                foreach (var item in allocdata)
                {
                    if (item.ResourceId == Convert.ToInt64(id))
                    {
                        foreach(var project in projdata)
                        {
                            if(project.Id == item.ProjectId)
                            {
                                p.Add(project);
                                break;
                            }
                        }
                    }
                }
                if (p == null)
                {
                    throw new APIException(409, "No Projects Found");
                }
                return p;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string GetTeams(string projId, string resId)
        {
            TeamIdModel team = new TeamIdModel();
            var allocdata = this.resourcedbContext.Set<Allocation>().ToList();
            foreach(var item in allocdata)
            {
                if(item.ProjectId == Convert.ToInt64(projId) && item.ResourceId == Convert.ToInt64(resId))
                {
                    team.teamId = item.TeamId.ToString();
                }
            }
            if(team == null)
            {
                throw new APIException(409, "Invalid projectId or ResourceId");
            }
            var json = JsonConvert.SerializeObject(team, Formatting.Indented,
                 new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                 });
            return json;
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
