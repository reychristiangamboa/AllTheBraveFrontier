using AllTheBraveFrontier.Entities;

public interface ICharacterActions {
    void BasicAttack(Character target);
    void MainAbility(Character? target = default, List<Hero>? party = default);
    bool IsDead();

}