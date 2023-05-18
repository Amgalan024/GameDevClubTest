using UnityEngine;

namespace Tile
{
    public class TileModel : MonoBehaviour
    {
        [SerializeField] private bool _isObstacle;
        public bool IsObstacle => _isObstacle;
    }
}