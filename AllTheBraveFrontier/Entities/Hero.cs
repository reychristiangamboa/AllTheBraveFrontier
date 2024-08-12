using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public abstract class Hero : Character
    {
        public int MyProperty { get; set; }
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
        
    }
}