using UnityEngine;


namespace MultiPlayRPG
{
    public class FrontWarpSkill : UpgradableSkill
    {
        #region Fields

        private const float BASE_DISTANCE = 7.0f;


        [SerializeField] private float _warpDistance = 7.0f;

        #endregion


        #region Properties

        public override int Level
        {
            set
            {
                base.Level = value;
                _warpDistance = BASE_DISTANCE + 0.5f * Level;
            }
        }

        #endregion


        #region Methods

        protected override void OnUse()
        {
            if (isServer)
            {
                _unit.Motor.StopFollowingTarget();
            }
            base.OnUse();
        }

        protected override void OnCastComplete()
        {
            if (isServer)
            {
                _unit.transform.Translate(Vector3.forward * _warpDistance);
                _unit.Motor.StopFollowingTarget();
            }
            base.OnCastComplete();
        }

        #endregion
    }
}
