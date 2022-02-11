using System.Collections.Generic;

namespace FirstFiorellaMVC.Models
{
    public class Position
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public ICollection<Expert> Experts { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
