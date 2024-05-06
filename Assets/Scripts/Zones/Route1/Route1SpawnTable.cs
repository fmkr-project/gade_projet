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
                {0.4f, 001},
                {0.3f, 008},
                {0.12f, 006},
                {0.07f, 013},
                {0.06f, 012},
                {0.05f, 002}
            };
    }
}