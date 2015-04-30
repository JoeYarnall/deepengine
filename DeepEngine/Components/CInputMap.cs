using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DeepEngine
{
    public enum InputType
    {
        Action,
        State,
        Range
    }

    public class CInputMap : Component
    {
        public int Priority { get; set; }

        public Dictionary<Keys, int> KeyboardKeyToAction { get; set; }
        public Dictionary<Keys, int> KeyboardKeyToState { get; set; }

        public Dictionary<MouseButtons, int> MouseButtonToAction { get; set; }
        public Dictionary<MouseButtons, int> MouseButtonToState { get; set; }

        public Dictionary<MouseRanges, int> MouseRangeToRange { get; set; }

        public CInputMap()
            : base(true, false)
        {
            Priority = 0;

            KeyboardKeyToAction = new Dictionary<Keys, int>();
            KeyboardKeyToState = new Dictionary<Keys, int>();

            MouseButtonToAction = new Dictionary<MouseButtons, int>();
            MouseButtonToState = new Dictionary<MouseButtons, int>();

            MouseRangeToRange = new Dictionary<MouseRanges, int>();
        }

        public void Add(Keys key, int inputID, InputType type)
        {
            if (type == InputType.Action)
            {
                KeyboardKeyToAction.Add(key, inputID);
            }
            else if (type == InputType.State)
            {
                KeyboardKeyToState.Add(key, inputID);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void Add(MouseButtons button, int inputID, InputType type)
        {
            if (type == InputType.Action)
            {
                MouseButtonToAction.Add(button, inputID);
            }
            else if (type == InputType.State)
            {
                MouseButtonToState.Add(button, inputID);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void Add(MouseRanges range, int inputID, InputType type)
        {
            if (type == InputType.Range)
            {
                MouseRangeToRange.Add(range, inputID);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
