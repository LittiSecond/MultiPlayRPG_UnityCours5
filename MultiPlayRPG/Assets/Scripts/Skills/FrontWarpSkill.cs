using UnityEngine;


namespace MultiPlayRPG
{
    public class FrontWarpSkill : Skill
    {
        #region Fields

        [SerializeField] private float _warpDistance = 7.0f;

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
