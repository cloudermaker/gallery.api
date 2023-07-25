using gallery.data.Model;

namespace gallery.data.Repository
{
    public interface IGalleryDbRepository
    {
        List<ItemModel> GetAllItems();
        List<ItemModel> GetItem(int id);

        List<ItemModel> GetFakeItems();
    }
}
