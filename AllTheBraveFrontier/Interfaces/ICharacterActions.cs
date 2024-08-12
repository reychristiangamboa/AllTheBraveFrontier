using AllTheBraveFrontier.Entities;

public interface ICharacterActions {
    void BasicAttack(Character target);
    void MainAbility(Character target, List<Character> party);
    bool IsDead();

}