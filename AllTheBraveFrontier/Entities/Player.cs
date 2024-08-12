namespace AllTheBraveFrontier.Entities
{
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public List<Hero> Party { get; set; }

        public Player()
        {
            Party = new List<Hero>();
        }
    }
}