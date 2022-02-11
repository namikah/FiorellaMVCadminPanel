using System.Collections.Generic;

namespace FirstFiorellaMVC.Models
{
    public class Campaign
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Discount { get; set; }

        public List<Product> Products { get; set; }
    }
}
