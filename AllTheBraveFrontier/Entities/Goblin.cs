using AllTheBraveFrontier.Utilities;

namespace AllTheBraveFrontier.Entities
{
    public class Goblin : Enemy
    {

        public Goblin() : base()
        {
            Name = "Goblin";
            AttackType = "Physical";
            MAG = Utility.RandomRange(8,16);
            RES = Utility.RandomRange(5,20);
            ATK = Utility.RandomRange(2,8);
            CON = Utility.RandomRange(12,20);
            TotalHP = CON * 500;
            Gambit.Enqueue(1);
            Gambit.Enqueue(1);
            Gambit.Enqueue(3);
            Gambit.Enqueue(2);
            Gambit.Enqueue(3);
            // 1 1 3 2 3
        }

        public void GoblinSwipes(Hero hero)
        {
            double damage = (hero.ATK * 25) + (ATK * 25);

            hero.CurrentHP -= damage;

            Console.WriteLine($"{Name} used GOBLIN SWIPES on {hero.Name}.");
            Console.WriteLine($"{hero.Name}'s HP is now {hero.CurrentHP}/{hero.TotalHP}");
        }

        public override void MainAbility(Character target, List<Character> party)
        {
            double damageValue = target.ATK * 25 + ATK * 25;
            target.CurrentHP -= damageValue;
        }
    }
}