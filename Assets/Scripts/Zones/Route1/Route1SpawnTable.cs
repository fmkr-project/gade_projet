using System.Collections.Generic;

namespace Zones.Route1
{
    public class Route1SpawnTable : CreatureSpawnTable
    {
        public override Dictionary<float, int> Data =>
            new()
            {
                /*{0.6f, 001},
                {0.3f, 006},
                {0.1f, 007}*/
                {1f, 001}
            };
    }
}