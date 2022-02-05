namespace Lodgify.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        //nav prop
        public int CookieTypeId { get; set; }
        public CookieType CookieType { get; set; }


        


    }
}