namespace Business.Models.Project;

public class ProjectRegistrationForm
{
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CustomerId { get; set; }
    public int StatusTypeId { get; set; }
    public int ProjectManagerId { get; set; }
}
