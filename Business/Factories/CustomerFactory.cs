using Business.Models.Customer;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{

    public static Customer Create(CustomerEntity entity)
    {
        var customer = new Customer
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
        };

        return customer;
    }
}
