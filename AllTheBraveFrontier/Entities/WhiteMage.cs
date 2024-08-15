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
            MAG = Utility.RandomRange(2, 9);
            RES = Utility.RandomRange(2, 6);
            ATK = Utility.RandomRange(0, 3);
            CON = Utility.RandomRange(3, 6);
            TotalHP = CON * 250D;
            CurrentHP = TotalHP;
            EvolutionLine = new Dictionary<int, string>() {
                { 1, "Acolyte" },
                { 2, "Cleric" },
                { 3, "Holy Mage"}
            };
        }

        public override Hero Clone()
        {
            return new WhiteMage 
            {
                Name = Name,
                Class = Class,
                Level = Level,
                Ability = Ability,
                AttackType = AttackType,
                MAG = MAG,
                RES = RES,
                ATK = ATK,
                CON = CON,
                TotalHP = TotalHP,
                CurrentHP = CurrentHP,
                CurrentEvolution = CurrentEvolution,
                EvolutionLine = EvolutionLine,
            };
        }

        public override void MainAbility(params object[] allies)
        {
            // Divine Renewal - Heals all allies to this characters's MAG + the ally's MAG * 50

            Console.WriteLine($"\n{Name} used {Ability}.");

            foreach(var ally in allies)
            {
                if (ally is Hero c && c != this)
                {
                    double healValue = MAG + c.MAG * 50D;

                    Console.Write($"{c.Name}'s HP: {c.CurrentHP}/{c.TotalHP} -> ");
                    if(c.CurrentHP + healValue > c.TotalHP)
                        c.CurrentHP = c.TotalHP;
                    else
                        c.CurrentHP += healValue;
                    Console.Write($"{c.CurrentHP}/{c.TotalHP}\n");
                }
            }

        }
        
    }
}