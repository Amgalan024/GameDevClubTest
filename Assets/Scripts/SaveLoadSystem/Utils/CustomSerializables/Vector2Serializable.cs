using System;
using UnityEngine;

namespace Utils.CustomSerializables
{
    [Serializable]
    public class Vector2Serializable
    {
        public float X;
        public float Y;

        public Vector2Serializable(Vector2 vector2)
        {
            X = vector2.x;
            Y = vector2.y;
        }

        public Vector2 ToVector()
        {
            return new Vector2(X, Y);
        }
    }
}