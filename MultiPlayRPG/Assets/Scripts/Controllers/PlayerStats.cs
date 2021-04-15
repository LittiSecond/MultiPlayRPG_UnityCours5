namespace MultiPlayRPG
{
    public class PlayerStats : UnitStats
    {
        #region Fields

        private StatsManager _manager;
        private UserData _data;

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

        public override int CurrentHealth 
        {
            get 
            { 
                return base.CurrentHealth; 
            }
            protected set
            {
                base.CurrentHealth = value;
                _data.CurrentHealth = CurrentHealth;
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
            if (Damag.BaseValue != _data.StatDamage)
            {
                _data.StatDamage = Damag.BaseValue;
            }

            if (_manager != null)
            {
                _manager.Damage = value;
            }
        }

        private void ArmorChanged(int value)
        {
            if (Armor.BaseValue != _data.StatArmor)
            {
                _data.StatArmor = Armor.BaseValue;
            }

            if (_manager != null)
            {
                _manager.Armor = value;
            }
        }

        private void MoveSpeedChanged(int value)
        {
            if (MoveSpeed.BaseValue != _data.StatMoveSpeed)
            {
                _data.StatMoveSpeed = MoveSpeed.BaseValue;
            }

            if (_manager != null)
            {
                _manager.MoveSpeed = value;
            }
        }

        public void Load(UserData data)
        {
            _data = data;
            CurrentHealth = data.CurrentHealth;
            if (data.StatDamage > 0)
            { 
                Damag.BaseValue = data.StatDamage;
            }
            if (data.StatArmor > 0)
            {
                Armor.BaseValue = data.StatArmor;
            }
            if (data.StatMoveSpeed > 0)
            {
                MoveSpeed.BaseValue = data.StatMoveSpeed;
            }
        }

        #endregion

    }
}
