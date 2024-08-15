namespace AllTheBraveFrontier.Entities
{
    public abstract class Enemy : Character
    {

        public Queue<int> Gambit { get; set; }
        public Queue<int> InitialGambit { get; set; }

        public Enemy() : base() => Gambit = new Queue<int>();

        public void ResetGambit()
        {
            Gambit.Clear();

            foreach(int action in InitialGambit)
                Gambit.Enqueue(action);
        }

        public string ShowDetails()
        {
            return $"{Name}\nHP{CurrentHP}/{TotalHP}\nMAG\t{MAG}\tRES\t{RES}\nATK\t{ATK}\tCON\t{CON}\n";
        }

        public string ShowGambit()
        {
            string gambitStr = string.Empty;
            
            foreach(int action in Gambit)
            {
                if (action == 1)
                    gambitStr += "BASIC ATTACK\n";
                else if (action == 2)
                    gambitStr += $"{Ability}\n";
                else
                    gambitStr += "SKIP\n";
            }

            return gambitStr;
        }
    }
}