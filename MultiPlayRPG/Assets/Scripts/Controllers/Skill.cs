using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class Skill :NetworkBehaviour
    {

        public Sprite Icon;

        [SerializeField] float castTime = 1f;
        [SerializeField] float cooldown = 1f;

    }
}