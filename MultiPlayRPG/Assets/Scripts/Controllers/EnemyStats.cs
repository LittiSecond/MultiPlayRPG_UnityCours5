namespace MultiPlayRPG
{
    public class EnemyStats : UnitStats
    {
        public override void OnStartServer()
        {
            CurrentHealth = MaxHealth;
        }

    }
}
