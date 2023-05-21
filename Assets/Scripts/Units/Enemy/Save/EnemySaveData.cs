using System;
using Startup;
using UnityEngine;
using Utils.CustomSerializables;

namespace Enemy.Save
{
    [Serializable]
    public class EnemySaveData : BaseSaveData
    {
        public int HealthPoints;
        public Vector2Serializable Position;
        public Vector2Serializable Scale;

        public EnemySaveData(string assetId, int healthPoints, Vector2 position, Vector2 scale) : base(assetId)
        {
            HealthPoints = healthPoints;
            Position = new Vector2Serializable(position);
            Scale = new Vector2Serializable(scale);
        }
    }
}