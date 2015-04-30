using System;
using DeepEngine;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

using TInput = System.Xml.Linq.XElement;
using TOutput = DeepEngine.Component;
using System.Reflection;

namespace EntityEngineContentPipelineExtension
{
    [ContentProcessor(DisplayName = "EntityEngineContentPipelineExtension.TestComponentProcessor")]
    public class TestComponentProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            Assembly assembly = Assembly.Load("DeepEngine");
            String s = "DeepEngine." + Convert.ToString(input.Element("Type").Value);
            Type t = assembly.GetType(s);

            CCameraPlacement c = (CCameraPlacement)Activator.CreateInstance(t);

            //c.Position = Convert.ToSingle(input.Element("Value1").Value);
            //c.Value2 = Convert.ToInt32(input.Element("Value2").Value);

            return c as Component;
        }
    }
}