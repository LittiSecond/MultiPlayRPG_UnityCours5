using UnityEngine;
using UnityEngine.UI;


namespace MultiPlayRPG
{
    public class StatItemUI : MonoBehaviour
    {
        [SerializeField] private Text _value;

        public void ChangeStat(int dtat)
        {
            _value.text = dtat.ToString();
        }

    }
}