using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class SkeletalDragon : Enemy
    {

        public SkeletalDragon() : base()
        {
            Name = "Skeletal Dragon";
            Ability = "Boneshard Barrage";
            AttackType = "Magical";
            MAG = Utility.RandomRange(23,28);
            RES = Utility.RandomRange(20,28);
            ATK = Utility.RandomRange(16,24);
            CON = Utility.RandomRange(30,51);
            TotalHP = CON * 500D;
            CurrentHP = TotalHP;
            Gambit.Enqueue(1);
            Gambit.Enqueue(1);
            Gambit.Enqueue(2);
            Gambit.Enqueue(2);
            Gambit.Enqueue(3);
            InitialGambit = new Queue<int>(Gambit);
        }

        public override void MainAbility(params object[] heroes)
        {
            // Boneshard Barrage – Deal damage to its target equal to ATK * 100
            foreach (var a in heroes)
            {
                if (a is Hero h)
                {
                    double damageValue = ATK * 100D;

                    Console.WriteLine($"\n{Name} used {Ability} on {h.Name}.");

                    // Hero's HP
                    Console.Write($"{h.Name}'s HP: {h.CurrentHP}/{h.TotalHP} -> ");
                    if ((h.CurrentHP - damageValue) < 0D)
                        h.CurrentHP = 0;
                    else
                        h.CurrentHP -= damageValue;
                    Console.Write($"{h.CurrentHP}/{h.TotalHP}.\n");
                }
            }
        }
    }
}