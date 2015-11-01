/**
 * FlattenMountains Plugin for KerbolPlus
 * All Rights Reserved - Thomas P. and KillAshley
 */

using UnityEngine;

namespace Kopernicus
{
    namespace Configuration
    {
        namespace ModLoader
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
            public class FlattenMountains : ModLoader, IParserEventSubscriber
            {
                // Actual PQS mod we are loading
                private PQSMod_FlattenMountains _mod;

                // The altitude for the flatten
                [ParserTarget("altitude", optional = true)]
                private NumericParser<double> altitude
                {
                    set { _mod.altitude = value.value; }
                }

                void IParserEventSubscriber.Apply(ConfigNode node) { }

                void IParserEventSubscriber.PostApply(ConfigNode node) { }

                public FlattenMountains()
                {
                    // Create the base mod
                    GameObject modObject = new GameObject("FlattenMountains");
                    modObject.transform.parent = Utility.Deactivator;
                    _mod = modObject.AddComponent<PQSMod_FlattenMountains>();
                    base.mod = _mod;
                }

                public FlattenMountains(PQSMod template)
                {
                    _mod = template as PQSMod_FlattenMountains;
                    _mod.transform.parent = Utility.Deactivator;
                    base.mod = _mod;
                }
            }
        }
    }
}