using System;
using UnityEngine;

namespace Gameplay
{
    public class ApplicationQuitHandler : MonoBehaviour
    {
        public event Action OnQuit;

        private void OnApplicationQuit()
        {
            OnQuit?.Invoke();
        }
    }
}