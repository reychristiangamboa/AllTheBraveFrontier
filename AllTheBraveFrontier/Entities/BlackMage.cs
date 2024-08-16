using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class BlackMage : Hero
    {

        public BlackMage() : base()
        {
            Class = "Black Mage";
            Ability = "Astral Hurricane";
            AttackType = "Magical";
            MAG = Utility.RandomRange(4, 11);
            RES = Utility.RandomRange(3, 9);
            ATK = Utility.RandomRange(0, 2);
            CON = Utility.RandomRange(1, 4);
            TotalHP = CON * 250D;
            CurrentHP = TotalHP;
            EvolutionLine = new Dictionary<int, string>() {
                { 1, "Apprentice" },
                { 2, "Scholar" },
                { 3, "Wizard"}
            };
        }

        public override Hero Clone()
        {
            return new BlackMage
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

        public override void MainAbility(params object[] targets)
        {
            // Astral Hurricane – Deals damage the enemy equal to the higher of 200 or
            // this character’s MAG * 100 – enemy’s RES * 80, up to a maximum of 1000.
            // This character’s MAG is then reduced by 20%.

            Console.WriteLine($"\n{Name} used {Ability}.");

            foreach (var t in targets)
            {
                if (t is Enemy e)
                {
                    // Calculate damage
                    double baseDamage = MAG * 100D - e.RES * 80D;
                    double damage = Math.Max(200D, baseDamage);
                    damage = Math.Min(damage, 1000D);

                    // Enemy's HP
                    Console.Write($"{e.Name}'s HP: {e.CurrentHP}/{e.TotalHP} -> ");
                    if ((e.CurrentHP - damage) < 0D)
                        e.CurrentHP = 0D;
                    else
                        e.CurrentHP -= damage;
                    Console.Write($"{e.CurrentHP}/{e.TotalHP}.\n");

                    // Reduce this character's MAG by 20%
                    Console.Write($"{Name}'s MAG: {MAG} -> ");
                    MAG = MAG * 0.80D;
                    Console.Write($"{MAG}.\n");
                }
            }

        }

    }
}