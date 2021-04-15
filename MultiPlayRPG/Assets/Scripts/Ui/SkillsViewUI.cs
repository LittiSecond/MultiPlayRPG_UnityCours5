using UnityEngine;
using UnityEngine.UI;


namespace MultiPlayRPG
{
    public class SkillsViewUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SkillViewItemUI[] _items;
        [SerializeField] private Text _skillPointsText;

        public static SkillsViewUI Instance;

        private StatsManager _manager;
        private int _currentSkillPoints;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("SkillsViewUI::Awake: More than one instance of SkillTree found!");
                return;
            }
            Instance = this;
        }

        void Update()
        {
            if (_manager != null)
            {
                CheckManagerChanges();
            }
        }

        #endregion


        #region Methods

        private void CheckManagerChanges()
        {
            if (_currentSkillPoints != _manager.SkillPoints)
            {
                _currentSkillPoints = _manager.SkillPoints;
                _skillPointsText.text = _currentSkillPoints.ToString();
                if (_currentSkillPoints > 0)
                {
                    SetUpgradableSkills(true);
                }
                else
                {
                    SetUpgradableSkills(false);
                }
            }
        }

        public void SetManager(StatsManager statsManager)
        {
            _manager = statsManager;
            CheckManagerChanges();
        }

        void SetUpgradableSkills(bool active)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].SetUpgradable(active);
            }
        }

        public void SetCharacter(CharacterOfPlr character)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].SetSkill(i < character.UnitSkills.Count ? 
                    character.UnitSkills[i] as UpgradableSkill : null);
            }
            if (_manager != null)
            {
                CheckManagerChanges();
            }
        }

        public void UpgradeSkill(SkillViewItemUI skillItem)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == skillItem)
                {
                    _manager.CmdUpgradeSkill(i);
                    break;
                }
            }
        }

        #endregion



    }
}