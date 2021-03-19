namespace MultiPlayRPG
{
    public class PlayerStats : UnitStats
    {
        #region Fields

        private StatsManager _manager;

        #endregion


        #region Properties

        public StatsManager Manager
        { 
            set
            {
                _manager = value;
                _manager.Damage = Damag.GetValue();
                _manager.Armor = Armor.GetValue();
                _manager.MoveSpeed = MoveSpeed.GetValue();
            }
        }

        #endregion


        #region UnityMethods

        public override void OnStartServer()
        {
            base.OnStartServer();
            Damag.OnStatChanged += DamagChanged;
            Armor.OnStatChanged += ArmorChanged;
            MoveSpeed.OnStatChanged += MoveSpeedChanged;
        }

        #endregion


        #region Methods

        private void DamagChanged(int value)
        {
            if (_manager != null)
            {
                _manager.Damage = value;
            }
        }

        private void ArmorChanged(int value)
        {
            if (_manager != null)
            {
                _manager.Armor = value;
            }
        }

        private void MoveSpeedChanged(int value)
        {
            if (_manager != null)
            {
                _manager.MoveSpeed = value;
            }
        }

        #endregion

    }
}
