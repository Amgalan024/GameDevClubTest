using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Startup
{
    public class LevelContainer : MonoBehaviour
    {
        [SerializeField] private List<BaseSaver> _objectToSave;

        public IEnumerable<BaseSaver> ObjectToSave => _objectToSave.AsEnumerable();

        private readonly Dictionary<BaseSaver, Action> _savableSubscriptions = new Dictionary<BaseSaver, Action>();

        public void AddSavableObject(BaseSaver baseSaver)
        {
            _objectToSave.Add(baseSaver);

            Action subscription = () => RemoveSavable(baseSaver);

            _savableSubscriptions.Add(baseSaver, subscription);

            baseSaver.OnDeleted += subscription;
        }

        private void RemoveSavable(BaseSaver baseSaver)
        {
            _objectToSave.Remove(baseSaver);

            baseSaver.OnDeleted -= _savableSubscriptions[baseSaver];
        }
    }
}