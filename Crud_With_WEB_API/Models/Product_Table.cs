namespace Crud_With_WEB_API.Models
{
    public class Product_Table
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Qty { get; set; }    

    }
}
