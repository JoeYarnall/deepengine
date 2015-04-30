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

using TInput = System.String;
using TOutput = System.Collections.Generic.List<DeepEngine.Entity>;
using System.Reflection;

namespace EntityEngineContentPipelineExtension
{
    [ContentProcessor(DisplayName = "Entity Processor")]
    public class EntityProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            TOutput output = new TOutput();

            XDocument doc = XDocument.Parse(input);

            foreach (XElement entity in doc.Descendants("Entity"))
            {
                Entity e = new DeepEngine.Entity();
                e.Name = Convert.ToString(entity.Element("Name").Value);
                e.Active = Convert.ToBoolean(entity.Element("Active").Value);
                e.Persist = Convert.ToBoolean(entity.Element("Persist").Value);

                foreach (XElement component in entity.Descendants("Component"))
                {
                    String s = Convert.ToString(component.Element("Type").Value);
                    Component c = null;

                    if (s.Equals("CTest"))
                    {
                        TestComponentProcessor p = new TestComponentProcessor();
                        c = p.Process(component, context);
                    }

                    c.Active = Convert.ToBoolean(component.Element("Active").Value);
                    c.Persist = Convert.ToBoolean(component.Element("Persist").Value);

                    e.ComponentList.Add(c);
                }

                output.Add(e);
            }

            return output;
        }
    }
}