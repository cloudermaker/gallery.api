using Dapper;
using gallery.data.Model;
using gallery.data.Queries;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace gallery.data.Repository;

public class GalleryDbRepository : IGalleryDbRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public GalleryDbRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("gallery") ?? "";
    }

    public bool CreateItem(ItemModel item)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var query = string.Format(
            ItemQueries.AddItemFormatQuery,
            item.Name,
            item.Description,
            item.PictureUrl
        );
        var updatedItemCount = connection.Execute(query);

        return updatedItemCount > 0;
    }

    public bool DeleteItem(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var query = string.Format(ItemQueries.RemoveItemFormatQuery, id);
        var updatedItemCount = connection.Execute(query);

        return updatedItemCount > 0;
    }

    public List<ItemModel> GetAllItems()
    {
        using var connection = new NpgsqlConnection(_connectionString);

        return connection.Query<ItemModel>(ItemQueries.GetAllItemsQuery).ToList();
    }

    public List<ItemModel> GetFakeItems()
    {
        return Enumerable
            .Range(1, 10)
            .Select(idx => new ItemModel { Id = idx, Name = $"item_{idx}" })
            .ToList();
    }

    public ItemModel? GetItem(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        return connection.QuerySingle<ItemModel?>(
            string.Format(ItemQueries.GetItemFormatQuery, id)
        );
    }

    public bool UpdateItem(ItemModel item)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        var query = string.Format(
            ItemQueries.UpdateItemFormatQuery,
            item.Name,
            item.Description,
            item.PictureUrl,
            item.Id
        );
        var updatedItemCount = connection.Execute(query);

        return updatedItemCount > 0;
    }
}
