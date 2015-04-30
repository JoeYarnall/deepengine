using DeepEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TWrite = System.Collections.Generic.List<DeepEngine.Entity>;

namespace EntityEngineContentPipelineExtension
{
    [ContentTypeWriter]
    public class EntityWriter : ContentTypeWriter<TWrite>
    {
        protected override void Initialize(ContentCompiler compiler)
        {
            base.Initialize(compiler);
        }

        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write(value.Count);

            foreach (Entity e in value)
            {
                output.Write(e.Name);
                output.Write(e.Active);
                output.Write(e.Persist);

                output.Write(e.ComponentList.Count);

                foreach (Component c in e.ComponentList)
                {
                    if (c is CCameraPlacement)
                    {
                        var tc = c as CCameraPlacement;
                        output.Write(tc.GetType().Name);
                        output.Write(tc.Active);
                        output.Write(tc.Persist);
                        //output.Write(tc.Value1);
                        //output.Write(tc.Value2);
                    }
                }
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "DeepEngine.EntityReader, DeepEngine";
        }
    }
}