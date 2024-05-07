using System.Collections.Generic;

namespace Zones.Route1
{
    public class Route1SpawnTable : CreatureSpawnTable
    {
        public override Dictionary<float, int> Data =>
            new()
            {
                {0.62f, 001},
                {0.61f, 014},
                {0.6f, 008},
                {0.45f, 006},
                {0.4f, 013},
                {0.2f, 011},
                {0.17f, 015},
                {0.11f, 019},
                {0.09f, 017},
                {0.0899999f, 018},
                {0.08f, 009},
                {0.07f, 007},
                {0.065f, 016},
                {0.06f, 002},
                {0.05f, 010},
                {0.02f, 003},
                {0.01f, 004},
                {0.0001f, 012}
            };
    }
}