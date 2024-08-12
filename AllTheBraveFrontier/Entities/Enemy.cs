namespace AllTheBraveFrontier.Entities
{
    public abstract class Enemy : Character
    {

        public Queue<int> Gambit { get; set; }

        public Enemy() : base()
        {
            Gambit = new Queue<int>();
        }
    }
}