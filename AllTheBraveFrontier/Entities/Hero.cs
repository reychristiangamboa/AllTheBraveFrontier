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
            return $"{Name} ({Class} - {EvolutionLine[CurrentEvolution]})\nHP {CurrentHP}/{TotalHP}\nMAG\t{MAG}\tRES\t{RES}\nATK\t{ATK}\tCON\t{CON}\n";
        }

        public abstract Hero Clone();
        
    }
}