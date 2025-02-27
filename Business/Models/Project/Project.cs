using Business.Models.StatusType;

namespace Business.Models.Project;

public class Project
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusTypeId { get; set; }
    public int CustomerId { get; set; }
    public int ProjectManagerId { get; set; }
    public string StatusTypeName { get; set; } = null!;
    public string ProjectManager { get; set; } = null!;
}
