using UnityEngine;
using UnityEngine.Networking;

namespace MultiPlayRPG
{
    public class StatsManager : NetworkBehaviour
    {
        #region Fields

        [SyncVar] public float Expa;
        [SyncVar] public float NextLevelExp;
        [SyncVar] public int Damage;
        [SyncVar] public int Armor;
        [SyncVar] public int MoveSpeed;
        [SyncVar] public int Level;
        [SyncVar] public int StatPoints;
        [SyncVar] public int SkillPoints;
        public PlayerScriptsConnector Player;

        #endregion


        #region Methods

        [Command]
        public void CmdUpgradeStat(int stat)
        {
            if (Player.Progress.RemoveStatPoint())
            {
                switch (stat)
                {
                    case (int)StatType.Damage:
                        Player.Character.Stats.Damag.BaseValue++;
                        break;
                    case (int)StatType.Armor:
                        Player.Character.Stats.Armor.BaseValue++;
                        break;
                    case (int)StatType.MoveSpeed:
                        Player.Character.Stats.MoveSpeed.BaseValue++;
                        break;
                }
            }
        }

        [Command]
        public void CmdUpgradeSkill(int index)
        {
            if (Player.Progress.RemoveSkillPoint())
            {
                UpgradableSkill skill = Player.Character.UnitSkills[index] as UpgradableSkill;
                if (skill != null)
                {
                    skill.Level++;
                }
            }
        }

        #endregion
    }
}