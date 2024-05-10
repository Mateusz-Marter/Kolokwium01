using System.Data.SqlClient;
using Kolokwium01.Models;

namespace Kolokwium01.Repository;

public class OrdersRepository : IOrdersRepository
{
    private readonly IConfiguration _configuration;
    
    public OrdersRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<bool> DoesOrderExist(int id)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.Parameters.AddWithValue("@Id", id);
        command.CommandText = "SELECT * FROM [Order] WHERE IdOrder = @Id";

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return result is not null;
    }

    public async Task<Order> GetOrder(int id)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.Parameters.AddWithValue("@Id", id);
        command.CommandText = "SELECT * FROM [Order] WHERE IdOrder = @Id";

        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();
        
        reader.Read();
        
        command.Parameters.Clear();
        command.Parameters.AddWithValue("@Id", id);
        
        var idOrder = reader.GetOrdinal("IdOrder");
        var name = reader.GetOrdinal("Name");
        var description = reader.GetOrdinal("Description");
        var creationDate = reader.GetOrdinal("CreationDate");
        var idClient = reader.GetOrdinal("IdClient");
        
        await reader.CloseAsync();

        command.CommandText = "SELECT * FROM PRODUCT P" +
                              " JOIN Order_Product OP ON P.IdProduct = OP.IdProduct" +
                              " JOIN [Order] O ON OP.IdOrder = O.IdOrder" +
                              " WHERE O.IdOrder = @Id";
        
        reader = await command.ExecuteReaderAsync();

        List < Product > products = new();

        while (reader.Read())
        {
            products.Add(new Product()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetFloat(2),
                IdProductType = reader.GetInt32(3)
            });
        }
        
        var order = new Order()
        {
            Id = reader.GetInt32(idOrder),
            Name = reader.GetString(name),
            Description = reader.GetString(description),
            CreationDate = reader.GetDateTime(creationDate),
            IdClient = reader.GetInt32(idClient),
            Products = products
        };

        return order;
    }

    public async Task<bool> DoesClientExist(int id)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.Parameters.AddWithValue("@Id", id);
        command.CommandText = "SELECT * FROM Client WHERE IdClient = @Id";

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return result is not null;
    }

    public Task DeleteClient(int id)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.Parameters.AddWithValue("@Id", id);
        command.CommandText"DELETE FROM Order  " +
    }
}

