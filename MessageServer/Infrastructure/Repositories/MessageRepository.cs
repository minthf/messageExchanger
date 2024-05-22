using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Infrastructure.Repositories;

public class MessageRepository(ILogger<MessageRepository> logger, IConfiguration configuration) : IMessageRepository
{
    private readonly ILogger<MessageRepository> _logger = logger;
    private readonly string _connectionString = configuration.GetConnectionString("Postgres");

    public async Task<int> AddMessageAsync(Message message)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand("INSERT INTO messages (content, date, serialnumber) VALUES (@content, @date, @serialnumber) RETURNING id", connection);
            command.Parameters.AddWithValue("content", message.Content);
            command.Parameters.AddWithValue("date", message.Date);
            command.Parameters.AddWithValue("serialnumber", message.SerialNumber);

            var id = Convert.ToInt32(await command.ExecuteScalarAsync());
            return id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding message to the database.");
            throw;
        }
    }

    public async Task<List<Message>> GetMessagesAsync(DateTime start, DateTime end)
    {
        var messages = new List<Message>();
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand("SELECT id, content, date, serialnumber FROM messages WHERE date BETWEEN @start AND @end", connection);
            command.Parameters.AddWithValue("start", start);
            command.Parameters.AddWithValue("end", end);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var message = new Message
                {
                    Id = reader.GetInt32(0),
                    Content = reader.GetString(1),
                    Date = reader.GetDateTime(2),
                    SerialNumber = reader.GetInt32(3),
                };
                messages.Add(message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving messages from the database.");
            throw;
        }
        return messages;
    }
}
