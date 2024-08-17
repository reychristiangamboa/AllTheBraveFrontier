using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public abstract class Hero : Character
    {
        public string Class { get; set; }
        public int Level { get; set; }
        public int CurrentEvolution { get; set; }
        public Dictionary<int, string> EvolutionLine { get; set; }

        public Hero() : base()
        {
            Name = Utility.GenerateName();
            Level = 1;
            CurrentEvolution = 1;
            EvolutionLine = [];
        }

        public string ShowDetails()
        {
            return $"{Name} ({Class} - {EvolutionLine[CurrentEvolution]})\nLVL {Level}\nHP {CurrentHP}/{TotalHP}\nMAG\t{MAG}\tRES\t{RES}\nATK\t{ATK}\tCON\t{CON}\n";
        }

        public Hero Fusion(Hero fusee)
        {
            double averageFuser = GetAverageStats();
            double averageFusee = fusee.GetAverageStats();

            Console.WriteLine($"\nA SUCCESSFUL FUSION OF HEROES!");

            if (averageFuser > averageFusee)
            {
                Console.WriteLine($"\n{fusee.ShowDetails()}");
                Console.WriteLine("has been merged to");
                Console.WriteLine($"\n{ShowDetails()}");

                FuseAttributes(fusee);

                Console.WriteLine($"\n{ShowDetails()}");

                return this;
            }
            else
            {
                Console.WriteLine($"\n{ShowDetails()}");
                Console.WriteLine("has been merged to");
                Console.WriteLine($"\n{fusee.ShowDetails()}");

                fusee.FuseAttributes(this);

                Console.WriteLine($"\n{fusee.ShowDetails()}");

                return fusee;
            }
        }

        public abstract Hero Clone();

        private double GetAverageStats()
        {
            return (MAG + RES + ATK + CON) / 4;
        }

        private void FuseAttributes(Hero other)
        {
            if (Level < 50)
                LevelUp();

            MAG += other.MAG * 0.10D;
            RES += other.RES * 0.10D;
            ATK += other.ATK * 0.10D;
            CON += other.CON * 0.10D;

            if (Level == 10)
            {
                CurrentEvolution++;
                MAG += MAG * 0.20D;
                RES += RES * 0.20D;
                ATK += ATK * 0.20D;
                CON += CON * 0.20D;
            }
            else if (Level == 30)
            {
                CurrentEvolution++;
                MAG += MAG * 0.30D;
                RES += RES * 0.30D;
                ATK += ATK * 0.30D;
                CON += CON * 0.30D;
            }
        }

        private void LevelUp()
        {
            Level++;
        }

    }
}