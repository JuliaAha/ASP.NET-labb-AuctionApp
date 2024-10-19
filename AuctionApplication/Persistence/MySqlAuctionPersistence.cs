using AuctionApplication.Core.Interfaces.Interfaces;

namespace AuctionApplication.Persistence;

public class MySqlAuctionPersistence {
    
    /*private readonly AuctionDbContext _dbContext;
    private readonly IMapper _mapper;

    public MySqlProjectPersistence(ProjectDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<Project> GetAllByUserName(string userName)
    {
        var projectDbs = _dbContext.ProjectDbs
            .Where(p => p.UserName == userName)
            .ToList();

        List<Project> result = new List<Project>();
        foreach (ProjectDb pdb in projectDbs)
        {
            Project project = _mapper.Map<Project>(pdb);
            result.Add(project);
        }
        return result;
    }
    

    public Project GetById(int id, string userName)
    {
        ProjectDb projectDb = _dbContext.ProjectDbs
            .Where(p => p.Id == id && p.UserName.Equals(userName))
            .Include(p => p.TaskDbs)
            .FirstOrDefault(); //null if not found
        
        if (projectDb == null) throw new DataException("Project not found");
        
        Project project = _mapper.Map<Project>(projectDb);
        foreach (TaskDb taskDb in projectDb.TaskDbs)
        {
            Task task = _mapper.Map<Task>(taskDb);
            project.AddTask(task);
        }
        return project;
    }

    public void Save(Project project)
    {
        ProjectDb pdb = _mapper.Map<ProjectDb>(project);
        _dbContext.ProjectDbs.Add(pdb);
        _dbContext.SaveChanges();
    }*/
}