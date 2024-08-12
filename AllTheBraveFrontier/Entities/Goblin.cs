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
            MAG = Utility.RandomRange(8,16);
            RES = Utility.RandomRange(5,20);
            ATK = Utility.RandomRange(2,8);
            CON = Utility.RandomRange(12,20);
            TotalHP = CON * 500D;
            Gambit.Enqueue(1);
            Gambit.Enqueue(1);
            Gambit.Enqueue(3);
            Gambit.Enqueue(2);
            Gambit.Enqueue(3);
            // 1 1 3 2 3
        }

        public override void MainAbility(Character target, List<Character> party)
        {
            // Goblin Swipes – Deal damage to the target equal to the target’s ATK * 25 + this character’s ATK * 25
            double damageValue = target.ATK * 25D + ATK * 25D;
            target.CurrentHP -= damageValue;

            Console.WriteLine($"{Name} used {Ability} on {target.Name}.");
        }
    }
}