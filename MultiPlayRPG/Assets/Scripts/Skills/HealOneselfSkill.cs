using UnityEngine;


namespace MultiPlayRPG
{
    public class HealOneselfSkill : UpgradableSkill
    {
        #region Fields

        private const int BASE_HEALING = 5;

        [SerializeField] private int _healAmount = 10;
        [SerializeField] private ParticleSystem _particle;

        #endregion


        #region Properties

        public override int Level
        {
            set
            {
                base.Level = value;
                _healAmount = BASE_HEALING + Level;
            }
        }

        #endregion


        #region Methods

        protected override void OnCastComplete()
        {
            if (isServer)
            {
                _unit.Stats.TakeHealing(_healAmount);
            }
            else 
            {
                _particle.Play();
            }
            base.OnCastComplete();
        }

        #endregion
    }
}
