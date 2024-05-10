using Kolokwium01.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

/*
public async Task<int> AddAnimal(NewAnimalDto newAnimalDto)
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES (@Name, @Type, @AdmissionDate, @OwnerId);" +
                              "SELECT @@IDENTITY AS ID;";
        command.Parameters.AddWithValue("@Name", newAnimalDto.Name);
        command.Parameters.AddWithValue("@Type", newAnimalDto.Type);
        command.Parameters.AddWithValue("@AdmissionDate", newAnimalDto.AdmissionDate);
        command.Parameters.AddWithValue("@OwnerId", newAnimalDto.OwnerId);

        await connection.OpenAsync();
        var transaction = await connection.BeginTransactionAsync();
        command.Transaction = transaction as SqlTransaction;

        object? id;

        try
        {
            id = Convert.ToInt32(await command.ExecuteScalarAsync());

            foreach (var procedure in newAnimalDto.Procedures)
            {
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO Procedure_Animal VALUES (@ProcedureId, @AnimalId, @Date)";
                command.Parameters.AddWithValue("@ProcedureId", procedure.ProcedureId);
                command.Parameters.AddWithValue("@AnimalId", id);
                command.Parameters.AddWithValue("@Date", procedure.Date);

                await command.ExecuteScalarAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }

        return (int)id;
    }
}
*/