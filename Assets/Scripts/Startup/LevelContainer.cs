using System.Collections.Generic;
using Enemy;

namespace Startup
{
    public class LevelContainer
    {
        public PlayerModel PlayerModel { get; set; }
        public List<EnemyModel> EnemyModels { get; } = new List<EnemyModel>();
    }
}