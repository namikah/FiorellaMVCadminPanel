namespace FirstFiorellaMVC.Models
{
    public class Expert
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }
    }
}
