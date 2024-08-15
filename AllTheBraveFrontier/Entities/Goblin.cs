using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Goblin : Enemy
    {

        public Goblin() : base()
        {
            Name = "Goblin";
            Ability = "Goblin Swipes";
            AttackType = "Physical";
            MAG = Utility.RandomRange(8,17);
            RES = Utility.RandomRange(5,21);
            ATK = Utility.RandomRange(2,9);
            CON = Utility.RandomRange(12,21);
            TotalHP = CON * 500D;
            CurrentHP = TotalHP;
            Gambit.Enqueue(1);
            Gambit.Enqueue(1);
            Gambit.Enqueue(3);
            Gambit.Enqueue(2);
            Gambit.Enqueue(3);
            InitialGambit = new Queue<int>(Gambit);
        }

        public override void MainAbility(params object[] heroes)
        {
            // Goblin Swipes – Deal damage to the target equal to the target’s ATK * 25 + this character’s ATK * 25
            foreach (var a in heroes)
            {
                if (a is Hero h)
                {
                    double damageValue = h.ATK * 25D + ATK * 25D;

                    Console.WriteLine($"\n{Name} used {Ability} on {h.Name}.");

                    // Hero's HP
                    Console.Write($"{h.Name}'s HP: {h.CurrentHP}/{h.TotalHP} -> ");
                    if ((h.CurrentHP - damageValue) < 0)
                        h.CurrentHP = 0;
                    else
                        h.CurrentHP -= damageValue;
                    Console.Write($"{h.CurrentHP}/{h.TotalHP}.\n");
                }
            }
        }
    }
}