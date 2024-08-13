using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class WhiteMage : Hero
    {

        public WhiteMage() : base()
        {
            Class = "White Mage";
            Ability = "Divine Renewal";
            AttackType = "Magical";
            MAG = Utility.RandomRange(2, 8);
            RES = Utility.RandomRange(2, 5);
            ATK = Utility.RandomRange(0, 2);
            CON = Utility.RandomRange(3, 5);
            TotalHP = CON * 250D;
            CurrentHP = TotalHP;
            EvolutionLine = new Dictionary<int, string>() {
                { 1, "Acolyte" },
                { 2, "Cleric" },
                { 3, "Holy Mage"}
            };
        }

        public override void MainAbility(Character? target, List<Hero>? party)
        {
            // Divine Renewal - Heals all allies to this characters's MAG + the ally's MAG * 50
            foreach(Character c in party)
            {
                if(c == this)
                    continue;

                double healValue = MAG + c.MAG * 50D;
                c.CurrentHP += healValue;

                Console.WriteLine($"{Name} used {Ability}.");
                Console.WriteLine($"All party members healed.");
            }
        }
        
    }
}