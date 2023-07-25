namespace gallery.data.Model
{
    public class ItemModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = "";
        public string PictureUrl { get; set; } = "";
    }
}
