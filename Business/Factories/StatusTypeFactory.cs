using Business.Models.StatusType;
using Data.Entities;

namespace Business.Factories;

public static class StatusTypeFactory
{
    public static StatusType Create(StatusTypeEntity entity)
    {
        var statusType = new StatusType
        {
            Id = entity.Id,
            StatusTypeName = entity.StatusType
        };

        return statusType;
    }
}
