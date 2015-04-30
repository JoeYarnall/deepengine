using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TRead = System.Collections.Generic.List<DeepEngine.Entity>;

namespace DeepEngine
{
    public class EntityReader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            var output = new TRead();

            int entityCount = input.ReadInt32();

            for (int i = 0; i < entityCount; i++)
            {
                var e = new Entity();
                e.Name = input.ReadString();
                e.Active = input.ReadBoolean();
                e.Persist = input.ReadBoolean();

                int componentCount = input.ReadInt32();

                for (int j = 0; j < componentCount; j++)
                {
                    var c = (CTest)Activator.CreateInstance(Type.GetType("DeepEngine." + input.ReadString() + ", DeepEngine"));

                    c.Active = input.ReadBoolean();
                    c.Persist = input.ReadBoolean();
                    c.Value1 = input.ReadInt32();
                    c.Value2 = input.ReadInt32();

                    e.ComponentList.Add(c);
                }

                output.Add(e);
            }

            return output;
        }
    }
}
