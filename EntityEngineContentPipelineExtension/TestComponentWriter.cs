using System;
using DeepEngine;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TWrite = DeepEngine.CCameraPlacement;

namespace EntityEngineContentPipelineExtension
{
    [ContentTypeWriter]
    public class TestComponentWriter : ContentTypeWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            // TODO: write the specified value to the output ContentWriter.
            throw new NotImplementedException();
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "DeepEngine.TestComponentReader, DeepEngine";
        }
    }
}
