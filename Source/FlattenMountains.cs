/**
 * FlattenMountains Plugin for KerbolPlus
 * All Rights Reserved - Thomas P. and KillAshley
 */

using Kopernicus.Configuration;
using Kopernicus.Configuration.ModLoader;
using UnityEngine;

namespace KerbolPlus
{
    public class PQSMod_FlattenMountains : PQSMod
    {
        public double altitude = 0;
        public override void OnVertexBuildHeight(PQS.VertexBuildData data)
        {
            if (data.vertHeight > (altitude + sphere.radius))
                data.vertHeight = sphere.radius + altitude;
        }
    }

    [RequireConfigType(ConfigType.Node)]
    public class FlattenMountains : ModLoader<PQSMod_FlattenMountains>
    {
        // The altitude for the flatten
        [ParserTarget("altitude", optional = true)]
        private NumericParser<double> altitude
        {
            get { return mod.altitude; }
            set { mod.altitude = value; }
        }
    }
}