using UnityEngine;

namespace Startup
{
    public class IdentifierHolder : MonoBehaviour
    {
        [SerializeField] private string _assetId;
        public string AssetId => _assetId;

        public string GameplayId { get; set; }
    }
}