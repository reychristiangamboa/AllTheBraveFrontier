using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Constable : Hero
    {

        public Constable() : base()
        {
            Class = "Constable";
            Ability = "Take and Receive";
            AttackType = "Physical";
            MAG = Utility.RandomRange(2, 4);
            RES = Utility.RandomRange(4, 8);
            ATK = Utility.RandomRange(5, 9);
            CON = Utility.RandomRange(8, 13);
            TotalHP = CON * 250D;
            CurrentHP = TotalHP;
            EvolutionLine = new Dictionary<int, string>() {
                { 1, "Squire" },
                { 2, "Soldier" },
                { 3, "Knight"}
            };
        }

        public override Hero Clone()
        {
            return new Constable
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
            // Take and Receive – attacks the enemy and deals damage equal to the higher
            // of 100 or this character’s ATK * 100 - target’s CON * 20 up to a maximum of
            // 1000, then takes damage equal to the higher of 100 and the enemy’s ATK * 50
            // - this character’s CON * 50 up to a maximum of 500.

            foreach (var t in targets)
            {
                if (t is Enemy e)
                {
                    Console.WriteLine($"\n{Name} used {Ability} on {e.Name}.");
                    
                    // Calculate damage dealt to the enemy
                    double baseDamageToEnemy = ATK * 100D - e.CON * 20D;
                    double damageToEnemy = Math.Max(100D, baseDamageToEnemy);
                    damageToEnemy = Math.Min(damageToEnemy, 1000D);

                    // Enemy's HP
                    Console.Write($"{e.Name}'s HP: {e.CurrentHP}/{e.TotalHP} -> ");
                    if ((e.CurrentHP - damageToEnemy) < 0D)
                        e.CurrentHP = 0D;
                    else
                        e.CurrentHP -= damageToEnemy;
                    Console.Write($"{e.CurrentHP}/{e.TotalHP}.\n");

                    // Calculate damage received from the enemy
                    double baseDamageToSelf = e.ATK * 50D - CON * 50D;
                    double damageToSelf = Math.Max(100D, baseDamageToSelf);
                    damageToSelf = Math.Min(damageToSelf, 500D);

                    // Constable's HP
                    Console.Write($"{Name}'s HP: {CurrentHP}/{TotalHP} -> ");
                    if ((CurrentHP - damageToSelf * 0.25D) < 0D)
                        CurrentHP = 0D;
                    else
                        CurrentHP -= damageToSelf * 0.25D;
                    Console.Write($"{CurrentHP}/{TotalHP}.\n");
                }
            }
        }

    }
}