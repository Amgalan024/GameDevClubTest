using System;
using UnityEngine;

namespace Units.Zones
{
    public class TriggerZone : MonoBehaviour
    {
        public event Action<Collider2D> OnZoneEnter;
        public event Action<Collider2D> OnZoneStay;
        public event Action<Collider2D> OnZoneExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnZoneEnter?.Invoke(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnZoneStay?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnZoneExit?.Invoke(other);
        }
    }
}