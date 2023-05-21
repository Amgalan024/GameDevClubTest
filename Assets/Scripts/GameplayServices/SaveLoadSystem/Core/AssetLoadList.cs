using System.Collections.Generic;
using UnityEngine;

namespace Startup
{
    [CreateAssetMenu(fileName = nameof(AssetLoadList), menuName = "SO/" + nameof(AssetLoadList))]
    public class AssetLoadList : ScriptableObject
    {
        [SerializeField] private List<MonoBehaviour> _assets;

        public List<MonoBehaviour> Assets => _assets;
    }
}