using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Dragonsire : Enemy
    {

        public Dragonsire() : base()
        {
            Name = "Dragonsire";
            Ability = "Breath of Life";
            AttackType = "Magical";
            MAG = 30D;
            RES = 30D;
            ATK = 30D;
            CON = 70D;
            TotalHP = CON * 500D;
            CurrentHP = TotalHP;
            Gambit.Enqueue(1);
            Gambit.Enqueue(1);
            Gambit.Enqueue(1);
            Gambit.Enqueue(1);
            Gambit.Enqueue(2);
            InitialGambit = new Queue<int>(Gambit);
        }

        public override void MainAbility(params object[] heroes)
        {
            // Breath of Life – Deal damage to its target equal to this character’s ATK * 50
            // and heals this character equal to this character’s MAG * 100. Also increases
            // this character’s individual stats by 10% respectively.

            foreach (var a in heroes)
            {
                if (a is Hero h)
                {
                    double damageValue = ATK * 50D;
                    double healValue = MAG * 100D;

                    Console.WriteLine($"\n{Name} used {Ability} on {h.Name}.");

                    // Hero's HP
                    Console.Write($"{h.Name}'s HP: {h.CurrentHP}/{h.TotalHP} -> ");
                    if ((h.CurrentHP - damageValue) < 0D)
                        h.CurrentHP = 0;
                    else
                        h.CurrentHP -= damageValue;
                    Console.Write($"{h.CurrentHP}/{h.TotalHP}.\n");

                    // Dragonsire's HP
                    Console.Write($"{Name}'s HP: {CurrentHP}/{TotalHP} -> ");
                    if (CurrentHP + healValue > TotalHP)
                        CurrentHP = TotalHP;
                    else
                        CurrentHP += healValue;
                    Console.Write($"{CurrentHP}/{TotalHP}.\n");

                    MAG += MAG * 0.10D;
                    RES += RES * 0.10D;
                    ATK += ATK * 0.10D;
                    CON += CON * 0.10D;

                    Console.WriteLine($"\n{ShowDetails()}");
                }
            }
        }
    }
}