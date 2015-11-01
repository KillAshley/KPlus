/**
 * Cryovulcano Plugin for KerbolPlus
 * All Rights Reserved - Thomas P. and KillAshley
 */
 
using System.Collections.Generic;
using Kopernicus.Configuration;
using PlanetaryParticle = Kopernicus.Configuration.ParticleLoader.PlanetaryParticle;
using UnityEngine;

namespace Vulcano
{
    /// <summary>
    /// External Parser target that creates a Vulcano
    /// </summary>
    [ExternalParserTarget(configNodeName = "Vulcano", parentNodeName = "Body")]
    public class Vulcano : ExternalParserTargetLoader, IParserEventSubscriber
    {
        // Set-up our parental objects
        PlanetaryParticle particle;
        GameObject body;

        // Target of our particles
        [ParserTarget("target", optional = true, allowMerge = false)]
        public string targetLoader
        {
            set { particle.target = value; }
        }

        // minEmission of particles
        [ParserTarget("minEmission", optional = true, allowMerge = false)]
        public NumericParser<float> minEmission
        {
            set { particle.minEmission = value.value; }
        }

        // maxEmission of particles
        [ParserTarget("maxEmission", optional = true, allowMerge = false)]
        public NumericParser<float> maxEmission
        {
            set { particle.maxEmission = value.value; }
        }

        // minimum lifespan of particles
        [ParserTarget("lifespanMin", optional = true, allowMerge = false)]
        public NumericParser<float> lifespanMin
        {
            set { particle.minEnergy = value.value; }
        }

        // maximum lifespan of particles
        [ParserTarget("lifespanMax", optional = true, allowMerge = false)]
        public NumericParser<float> lifespanMax
        {
            set { particle.maxEnergy = value.value; }

        }

        // minimum size of particles
        [ParserTarget("sizeMin", optional = true, allowMerge = false)]
        public NumericParser<float> sizeMin
        {
            set { particle.emitter.minSize = value.value; }
        }

        // maximum size of particles
        [ParserTarget("sizeMax", optional = true, allowMerge = false)]
        public NumericParser<float> sizeMax
        {
            set { particle.emitter.maxSize = value.value; }
        }

        // speedScale of particles
        [ParserTarget("speedScale", optional = true, allowMerge = false)]
        public NumericParser<float> speedScaleLoader
        {
            set { particle.speedScale = value.value; }
        }

        // grow rate of particles
        [ParserTarget("rate", optional = true, allowMerge = false)]
        public NumericParser<float> rate
        {
            set { particle.animator.sizeGrow = value.value; }
        }

        // rand Velocity of particles
        [ParserTarget("randVelocity", optional = true, allowMerge = false)]
        public Vector3Parser randVelocity
        {
            set { particle.randomVelocity = value.value; }
        }

        // Texture of particles
        [ParserTarget("texture", optional = true, allowMerge = false)]
        public Texture2DParser texture
        {
            set { particle.Renderer.material.mainTexture = value.value; }
        }

        // Apply event
        void IParserEventSubscriber.Apply(ConfigNode node)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.transform.parent = generatedBody.scaledVersion.transform;
            gameObject.transform.position = generatedBody.scaledVersion.transform.position;
            gameObject.transform.localPosition = Vector3.zero;
            Object.DestroyImmediate(gameObject.rigidbody);
            particle = PlanetaryParticle.CreateInstance(gameObject);
        }

        // Post-Apply event
        void IParserEventSubscriber.PostApply(ConfigNode node)
        {
            List<Color> colors = new List<Color>();
            foreach (string color in node.GetNode("Colors").GetValuesStartsWith("color"))
            {
                Vector4 c = ConfigNode.ParseVector4(color);
                colors.Add(new Color(c.x, c.y, c.z, c.w));

            }
            particle.animator.colorAnimation = colors.ToArray();
        }
    }
}
