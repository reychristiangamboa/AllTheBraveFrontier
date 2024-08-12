namespace AllTheBraveFrontier.Entities
{
    public abstract class Character : ICharacterActions
    {

        public string Name { get; set; } = string.Empty;
        public string AttackType { get; set; } = string.Empty;
        public double MAG { get; set; }
        public double RES { get; set; }
        public double ATK { get; set; }
        public double CON { get; set; }
        public double TotalHP { get; set; }
        public double CurrentHP { get; set; }

        public Character() 
        {
            CurrentHP = TotalHP;
        }

        public void BasicAttack(Character target)
        {
            double damage = 0;

            if(AttackType.Equals("Physical")) {
                damage += ATK * 50D;
            } else {
                damage += MAG * 25D;
            }

            target.CurrentHP -= damage;

            Console.WriteLine($"{Name} used BASIC ATTACK on {target.Name}.");
            Console.WriteLine($"{target.Name}'s HP is now {CurrentHP}/{TotalHP}.");
        }

        public abstract void MainAbility(Character target, List<Character> party);

        public bool IsDead() 
        {
            return CurrentHP <= 0D;
        }

    }
}