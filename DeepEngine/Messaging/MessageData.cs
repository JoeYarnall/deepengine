using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public struct MessageData
    {
        public MessageData(Vector3 v1)
        {
            i1 = 0;
            i2 = 0;
            i3 = 0;
            f1 = v1.X;
            f2 = v1.Y;
            f3 = v1.Z;
            handled = false;
        }

        public MessageData(int i1)
        {
            this.i1 = i1;
            i2 = 0;
            i3 = 0;
            f1 = 0;
            f2 = 0;
            f3 = 0;
            handled = false;
        }

        public MessageData(Point p1)
        {
            i1 = p1.X;
            i2 = p1.Y;
            i3 = 0;
            f1 = 0;
            f2 = 0;
            f3 = 0;
            handled = false;
        }

        public MessageData(int i1, Vector3 v1)
        {
            this.i1 = i1;
            i2 = 0;
            i3 = 0;
            f1 = v1.X;
            f2 = v1.Y;
            f3 = v1.Z;
            handled = false;
        }

        private int i1;
        private int i2;
        private int i3;
        private float f1;
        private float f2;
        private float f3;

        public int I1 { get { return i1; } }
        public int I2 { get { return i2; } }
        public int I3 { get { return i3; } }
        public Vector3 V1 { get { return new Vector3(f1, f2, f3); } }
        public Point P1 { get { return new Point(i1, i2); } }

        private bool handled;
        public bool Handled
        {
            get { return handled; }
            set
            {
                if (value) // You can only set it to true
                {
                    handled = value;
                }
            }
        }

        public void SetInt1Response(int i)
        {
            i1 = i;
        }

        public void SetInt2Response(int i)
        {
            i2 = i;
        }

        public void SetInt3Response(int i)
        {
            i3 = i;
        }

        public void SetPoint1Response(Point p)
        {
            i1 = p.X;
            i2 = p.Y;
        }
    }
}
