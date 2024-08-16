namespace AllTheBraveFrontier.Entities
{
    public abstract class Character : ICharacterActions
    {

        public string Name { get; set; } = string.Empty;
        public string Ability { get; set; } = string.Empty;
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
            Console.WriteLine($"\n{Name} used BASIC ATTACK on {target.Name}.");

            double damageValue = 0D;

            if (AttackType.Equals("Physical"))
                damageValue += ATK * 50D;
            else
                damageValue += MAG * 25D;

            Console.Write($"{target.Name}'s HP: {target.CurrentHP}/{target.TotalHP} -> ");
            if ((target.CurrentHP - damageValue) < 0D)
                target.CurrentHP = 0D;
            else
                target.CurrentHP -= damageValue;
            Console.Write($"{target.CurrentHP}/{target.TotalHP}.\n");
        }

        public abstract void MainAbility(params object[] targets);

        public bool IsDead()
        {
            return CurrentHP <= 0D;
        }

    }
}