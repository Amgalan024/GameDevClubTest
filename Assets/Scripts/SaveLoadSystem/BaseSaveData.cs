using System;

namespace Startup
{
    [Serializable]
    public class BaseSaveData
    {
        public BaseSaveData(string assetId)
        {
            AssetId = assetId;
        }

        public BaseSaveData(string assetId, string gameplayId)
        {
            AssetId = assetId;
            GameplayId = gameplayId;
        }

        public string AssetId;

        public string GameplayId;
    }
}