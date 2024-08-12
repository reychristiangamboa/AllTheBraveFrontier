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
            MAG = Utility.RandomRange(1, 4);
            RES = Utility.RandomRange(1, 8);
            ATK = Utility.RandomRange(7, 11);
            CON = Utility.RandomRange(1, 4);
            TotalHP = CON * 250D;
            EvolutionLine = new Dictionary<int, string>() {
                { 1, "Sewer Rat" },
                { 2, "Cutpurse" },
                { 3, "Cutthroat"}
            };
        }

        public override void MainAbility(Character target, List<Character> party)
        {
            // Blood-soaked Blades – attacks a target enemy and deals damage equal to this character’s ATK * 100, 
            // and deals damage to itself equal to 25% of the damage dealt.
            double damageValue = ATK * 100;
            target.CurrentHP -= damageValue;
            CurrentHP -= damageValue * 0.25;
        }
        
    }
}