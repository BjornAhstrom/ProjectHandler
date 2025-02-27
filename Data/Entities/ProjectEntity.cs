namespace Data.Entities;

public class ProjectEntity
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int StatusTypeId { get; set; }
    public StatusTypeEntity? StatusType { get; set; }

    public int CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }

    public int ProjectManagerId { get; set; }
    public UserEntity? ProjectManager { get; set; }

}
