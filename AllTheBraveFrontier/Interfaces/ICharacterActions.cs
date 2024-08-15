using AllTheBraveFrontier.Entities;

public interface ICharacterActions {
    void BasicAttack(Character target);
    void MainAbility(params object[] targets);
    bool IsDead();

}