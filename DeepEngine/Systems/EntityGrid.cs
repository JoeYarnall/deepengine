using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DeepEngine
{
    public class EntityGrid
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Layers { get; private set; }
        public Rectangle Bounds { get; private set; }

        private List<int>[] Grid { get; set; }
        
        public void Clear()
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                Grid[i].Clear();
            }
        }

        public void Assign(int instanceID, int x, int y, int layer)
        {
            Debug.Assert(layer < Layers && layer >= 0);
            Debug.Assert(x < Width && x >= 0);
            Debug.Assert(y < Height && y >= 0);

            int index = x + Width * y;
            Grid[index][layer] = instanceID;
        }

        public int QueryInstanceIDAt(int x, int y, int layer)
        {
            Debug.Assert(layer < Layers && layer >= 0);
            
            var list = QueryInstanceIDsAt(x, y);
            int result = list[layer];
            return result;
        }

        public List<int> QueryInstanceIDsAt(int x, int y)
        {
            List<int> result = new List<int>();

            if (x >= Width || x < 0 || y >= Height || y < 0)
            {
                for (int i = 0; i < Layers; i++)
                {
                    result.Add(EntityEngine.InvalidInstanceID);
                }
            }
            else
                result.AddRange(Grid[x + Width * y]);

            return result;
        }

        public void SetSize(int width, int height, int layers)
        {
            Width = width;
            Height = height;
            Layers = layers;
            RebuildGrid();
        }

        private void RebuildGrid()
        {
            int size = Width * Height;
            Bounds = new Rectangle(0, 0, Width, Height);

            Grid = new List<int>[size];

            for (int i = 0; i < Grid.Length; i++)
            {
                Grid[i] = new List<int>(Layers);

                for(int j = 0; j < Layers; j++)
                {
                    Grid[i].Add(0);
                }
            }
        }
    }
}
