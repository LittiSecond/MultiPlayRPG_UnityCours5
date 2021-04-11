using UnityEngine;


namespace MultiPlayRPG
{
    public class HealOneselfSkill : Skill
    {
        [SerializeField] private int _healAmount = 10;
        [SerializeField] private ParticleSystem _particle;

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

    }
}
