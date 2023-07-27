namespace gallery.data.Queries
{
    internal class ItemQueries
    {
        public static string GetAllItemsQuery = "select * from item";
        public static string GetItemFormatQuery = "select * from item where \"Id\" = {0}";
        public static string SearchItemsByNameFormatQuery =
            "select * from item where \"Name\" like '%{0}%'";

        public static string AddItemFormatQuery =
            "insert into item (\"Name\", \"Description\", \"PictureUrl\") values ('{0}', '{1}', '{2}')";

        public static string UpdateItemFormatQuery =
            "update item set \"Name\" = '{0}', \"Description\" = '{1}', \"PictureUrl\" = '{2}' where \"Id\" = {3}";

        public static string RemoveItemFormatQuery = "delete from item where \"Id\" = {0}";
    }
}
