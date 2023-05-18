using UnityEngine;

namespace Item
{
    public abstract class BaseItem : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _amount;

        public Sprite Icon => _icon;
        public int Amount => _amount;
    }
}