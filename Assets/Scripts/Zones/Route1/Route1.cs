using System.Collections.Generic;

namespace Zones.Route1
{
    public class Route1 : Zone
    {
        public override string ZoneName => "Route 1" ;

        public override Dictionary<string, CreatureSpawnTable> ZoneSpawnTables =>
            new()
            {
                {"Grass", new Route1SpawnTable()}
            };
    }
}