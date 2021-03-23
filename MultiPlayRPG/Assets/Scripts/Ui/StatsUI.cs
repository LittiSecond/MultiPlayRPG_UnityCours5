using UnityEngine;
using UnityEngine.UI;


namespace MultiPlayRPG
{
    public class StatsUI : MonoBehaviour
    {
        #region Fields
        
        public static StatsUI Instance;

        [SerializeField] private GameObject _statsUI;
        [SerializeField] private StatItemUI _damagStat;
        [SerializeField] private StatItemUI _armorStat;
        [SerializeField] private StatItemUI _moveSpeedStat;
        [SerializeField] private Text _levelText;
        [SerializeField] private Text _statPointsText;

        private StatsManager _manager;

        private float _currentExpa = -0.1f;
        private float _nextLevelExp = -1.0f;
        private int _currentDamag = -1;
        private int _currentArmor = -1;
        private int _currentMoveSpeed = -1;
        private int _currentLevel = -1;
        private int _currentStatPoints = -1;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one instance of StatsUI found!");
                return;
            }
            Instance = this;
            _statsUI.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Stats"))
            {
                _statsUI.SetActive(!_statsUI.activeSelf);
            }

            CheckManagerChanges();
        }

        #endregion


        #region Methods

        public void SetManager(StatsManager statsManager)
        {
            _manager = statsManager;
            CheckManagerChanges();
        }

        private void CheckManagerChanges()
        {
            if (_manager != null)
            {
                if (_currentDamag != _manager.Damage)
                {
                    _currentDamag = _manager.Damage;
                    _damagStat.ChangeStat(_currentDamag);
                }
                if (_currentArmor != _manager.Armor)
                {
                    _currentArmor = _manager.Armor;
                    _armorStat.ChangeStat(_currentArmor);
                }
                if (_currentMoveSpeed != _manager.MoveSpeed)
                {
                    _currentMoveSpeed = _manager.MoveSpeed;
                    _moveSpeedStat.ChangeStat(_currentMoveSpeed);
                }

                if (_currentLevel != _manager.Level)
                {
                    _currentLevel = _manager.Level;
                    _levelText.text = _currentLevel.ToString();
                }
                if (_currentExpa != _manager.Expa)
                {
                    _currentExpa = _manager.Expa;
                }
                if (_nextLevelExp != _manager.NextLevelExp)
                {
                    _nextLevelExp = _manager.NextLevelExp;
                }
                if (_currentStatPoints != _manager.StatPoints)
                {
                    _currentStatPoints = _manager.StatPoints;
                    _statPointsText.text = _currentStatPoints.ToString();
                    if (_currentStatPoints > 0) SetUpgradableStats(true);
                    else SetUpgradableStats(false);
                }
            }
        }

        private void SetUpgradableStats(bool isActive)
        {
            _damagStat.SetUpgradable(isActive);
            _armorStat.SetUpgradable(isActive);
            _moveSpeedStat.SetUpgradable(isActive);
        }

        public void UpgradeStat(StatItemUI stat)
        {
            if (stat == _damagStat)
            {
                _manager.CmdUpgradeStat((int)StatType.Damage);
            }
            else if (stat == _armorStat)
            {
                _manager.CmdUpgradeStat((int)StatType.Armor);
            }
            else if (stat == _moveSpeedStat)
            {
                _manager.CmdUpgradeStat((int)StatType.MoveSpeed);
            }
        }

        #endregion
    }
}