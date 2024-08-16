using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Rogue : Hero
    {

        public Rogue() : base()
        {
            Class = "Rogue";
            Ability = "Blood-soaked Blades";
            AttackType = "Physical";
            MAG = Utility.RandomRange(1, 5);
            RES = Utility.RandomRange(1, 9);
            ATK = Utility.RandomRange(7, 12);
            CON = Utility.RandomRange(1, 5);
            TotalHP = CON * 250D;
            CurrentHP = TotalHP;
            EvolutionLine = new Dictionary<int, string>() {
                { 1, "Sewer Rat" },
                { 2, "Cutpurse" },
                { 3, "Cutthroat"}
            };
        }

        public override Hero Clone()
        {
            return new Rogue 
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
            // Blood-soaked Blades – attacks a target enemy and deals damage equal to this character’s ATK * 100, 
            // and deals damage to itself equal to 25% of the damage dealt.

            foreach (var t in targets)
            {
                if (t is Enemy e)
                {
                    Console.WriteLine($"\n{Name} used {Ability} on {e.Name}.");
                    
                    double damageValue = ATK * 100D;
                    
                    // Enemy's HP
                    Console.Write($"{e.Name}'s HP: {e.CurrentHP}/{e.TotalHP} -> ");
                    if ((e.CurrentHP - damageValue) < 0D)
                        e.CurrentHP = 0D;
                    else
                        e.CurrentHP -= damageValue;
                    Console.Write($"{e.CurrentHP}/{e.TotalHP}.\n");
                    
                    // Rogue's HP
                    Console.Write($"{Name}'s HP: {CurrentHP}/{TotalHP} -> ");
                    if ((CurrentHP - damageValue * 0.25D) < 0D)
                        CurrentHP = 0D;
                    else
                        CurrentHP -= damageValue * 0.25D;
                    Console.Write($"{CurrentHP}/{TotalHP}.\n");
                }
            }
        }
        
    }
}