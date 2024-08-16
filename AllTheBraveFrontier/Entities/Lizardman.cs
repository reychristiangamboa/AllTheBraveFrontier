using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Lizardman : Enemy
    {

        public Lizardman() : base()
        {
            Name = "Lizardman";
            Ability = "Intelligent Strikes";
            AttackType = "Physical";
            MAG = Utility.RandomRange(8,13);
            RES = Utility.RandomRange(16,22);
            ATK = Utility.RandomRange(20,22);
            CON = Utility.RandomRange(21,24);
            TotalHP = CON * 500D;
            CurrentHP = TotalHP;
            Gambit.Enqueue(1);
            Gambit.Enqueue(1);
            Gambit.Enqueue(1);
            Gambit.Enqueue(2);
            Gambit.Enqueue(3);
            InitialGambit = new Queue<int>(Gambit);
        }

        public override void MainAbility(params object[] heroes)
        {
            // Intelligent Strikes – Deal damage to its target equal to this character’s MAG * 100
            foreach (var a in heroes)
            {
                if (a is Hero h)
                {
                    double damageValue = MAG * 100D;

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