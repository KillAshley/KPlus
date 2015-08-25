﻿/* ============================================================================= *\
 * The MIT License (MIT) 
 * Copyright(c) 2015 Thomas P.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
\* ============================================================================= */

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