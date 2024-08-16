using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class RedMage : Hero
    {

        public RedMage() : base()
        {
            Class = "Red Mage";
            Ability = "Spirit Bind";
            AttackType = "Magical";
            MAG = Utility.RandomRange(4, 7);
            RES = Utility.RandomRange(3, 9);
            ATK = Utility.RandomRange(0, 3);
            CON = Utility.RandomRange(3, 6);
            TotalHP = CON * 250D;
            CurrentHP = TotalHP;
            EvolutionLine = new Dictionary<int, string>() {
                { 1, "Stage Hand" },
                { 2, "Assistant" },
                { 3, "Magician"}
            };
        }

        public override Hero Clone()
        {
            return new RedMage
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
            // Spirit Bind – Increases the individual stats of all the party members by 10% of
            // this character’s individual stats respectively until the end of the battle.
            // This cannot let the individual stat go beyond 100.

            Console.WriteLine($"\n{Name} used {Ability}.");

            foreach (var ally in allies)
            {
                if (ally is Hero c && c != this)
                {
                    c.MAG = Math.Min(100D, c.MAG + MAG * 0.10D);
                    c.RES = Math.Min(100D, c.RES + RES * 0.10D);
                    c.ATK = Math.Min(100D, c.ATK + ATK * 0.10D);
                    c.CON = Math.Min(100D, c.CON + CON * 0.10D);

                    Console.WriteLine($"{c.ShowDetails()}\n");
                }
            }

        }

    }
}