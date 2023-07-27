using gallery.data.Model;

namespace gallery.data.Repository
{
    public interface IGalleryDbRepository
    {
        List<ItemModel> GetAllItems();
        ItemModel? GetItem(int id);

        bool CreateItem(ItemModel item);
        bool DeleteItem(int id);
        bool UpdateItem(ItemModel item);

        List<ItemModel> GetFakeItems();
    }
}
