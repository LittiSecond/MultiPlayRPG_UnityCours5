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

        private StatsManager _manager;

        private int _currentDamag = -1;
        private int _currentArmor = -1;
        private int _currentMoveSpeed = -1;

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
            }
        }


        #endregion
    }
}