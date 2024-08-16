namespace AllTheBraveFrontier.Entities
{
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public List<Hero> HeroRoster { get; set; }
        public List<Hero> Party { get; set; }

        public Player()
        {
            HeroRoster = new List<Hero>();
            Party = new List<Hero>();
        }
    }
}