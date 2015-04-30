using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class InputSystem : System
    {
        private CViewport ViewPort { get; set; }

        private Dictionary<int, float> RangeValues { get; set; }

        private MouseState PreviousMouseState { get; set; }
        private MouseState CurrentMouseState { get; set; }
        private List<MouseButtons> MappedMouseButtons { get; set; }
        private List<MouseRanges> MappedMouseRanges { get; set; }

        private KeyboardState PreviousKeyboardState { get; set; }
        private KeyboardState CurrentKeyboardState { get; set; }
        private List<Keys> MappedKeyboardKeys { get; set; }

        private List<int> ActiveActions { get; set; }
        private List<int> ActiveStates { get; set; }
        private List<int> ActiveRanges { get; set; }

        private Point MouseScreenLocation { get; set; }

        public InputSystem()
            : base(true, true, EngineSystemIds.Input, Aspect.GetListForAll(typeof(CInputMap), typeof(CInputHandlers)), new Dictionary<int, MessageHandler>())
        {
            SupportedMessages.Add(EngineMessageIds.EntityCreated, OnEntitySpawnedMessage);
            SupportedMessages.Add(EngineMessageIds.GetMouseLocation, OnGetMouseLocationMessage);
        }

        public void OnEntitySpawnedMessage(ref MessageData data, Entity target, object sender)
        {
            if (ViewPort == null && target.HasComponent<CViewport>())
                ViewPort = target.GetComponent<CViewport>();
        }

        public void OnGetMouseLocationMessage(ref MessageData data, Entity target, object sender)
        {
            if (!data.Handled) //If the message has already been handled don't respond
            {
                data.SetPoint1Response(MouseScreenLocation);
                data.Handled = true;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            RangeValues = new Dictionary<int, float>();

            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();

            MappedKeyboardKeys = new List<Keys>();
            MappedMouseButtons = new List<MouseButtons>();
            MappedMouseRanges = new List<MouseRanges>();

            ActiveActions = new List<int>();
            ActiveStates = new List<int>();
            ActiveRanges = new List<int>();

            ViewPort = null;
        }

        public override void Update(GameTime gameTime)
        {
            //Update Keyboard and Mouse States
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            //Clear Data Structures
            ActiveActions.Clear();
            ActiveStates.Clear();
            ActiveRanges.Clear();
            MappedKeyboardKeys.Clear();
            MappedMouseButtons.Clear();
            MappedMouseRanges.Clear();
            RangeValues.Clear();

            MouseScreenLocation = new Point(CurrentMouseState.X, CurrentMouseState.Y);

            //Sort EntityList by InputMap Priority
            EntityList.Sort((Entity x, Entity y) =>
            {
                if (x.GetComponent<CInputMap>().Priority < y.GetComponent<CInputMap>().Priority)
                {
                    return -1;
                }
                else if (x.GetComponent<CInputMap>().Priority > y.GetComponent<CInputMap>().Priority)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            });

            //Map Raw Inputs to Actions, States and Ranges
            foreach (Entity e in EntityList)
            {
                var inputMap = e.GetComponent<CInputMap>();

                //KEYBOARD KEY TO ACTION
                foreach (KeyValuePair<Keys, int> pair in inputMap.KeyboardKeyToAction)
                {
                    if (!MappedKeyboardKeys.Contains(pair.Key))
                    {
                        if (PreviousKeyboardState.IsKeyUp(pair.Key) && CurrentKeyboardState.IsKeyDown(pair.Key))
                        {
                            ActiveActions.Add(pair.Value);
                            MappedKeyboardKeys.Add(pair.Key);
                        }
                    }
                }

                //KEYBOARD KEY TO STATE
                foreach (KeyValuePair<Keys, int> pair in inputMap.KeyboardKeyToState)
                {
                    if (!MappedKeyboardKeys.Contains(pair.Key))
                    {
                        if (CurrentKeyboardState.IsKeyDown(pair.Key))
                        {
                            ActiveStates.Add(pair.Value);
                            MappedKeyboardKeys.Add(pair.Key);
                        }
                    }
                }

                //MOUSE BUTTON TO ACTION
                foreach (KeyValuePair<MouseButtons, int> pair in inputMap.MouseButtonToAction)
                {
                    if (!MappedMouseButtons.Contains(pair.Key))
                    {
                        if (pair.Key == MouseButtons.LeftButton)
                        {
                            if (PreviousMouseState.LeftButton == ButtonState.Released && CurrentMouseState.LeftButton == ButtonState.Pressed)
                            {
                                ActiveActions.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                        if (pair.Key == MouseButtons.RightButton)
                        {
                            if (PreviousMouseState.RightButton == ButtonState.Released && CurrentMouseState.RightButton == ButtonState.Pressed)
                            {
                                ActiveActions.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                        if (pair.Key == MouseButtons.MiddleButton)
                        {
                            if (PreviousMouseState.MiddleButton == ButtonState.Released && CurrentMouseState.MiddleButton == ButtonState.Pressed)
                            {
                                ActiveActions.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                        if (pair.Key == MouseButtons.XButton1)
                        {
                            if (PreviousMouseState.XButton1 == ButtonState.Released && CurrentMouseState.XButton1 == ButtonState.Pressed)
                            {
                                ActiveActions.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                        if (pair.Key == MouseButtons.XButton2)
                        {
                            if (PreviousMouseState.XButton2 == ButtonState.Released && CurrentMouseState.XButton2 == ButtonState.Pressed)
                            {
                                ActiveActions.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                    }
                }

                //MOUSE BUTTON TO STATE
                foreach (KeyValuePair<MouseButtons, int> pair in inputMap.MouseButtonToState)
                {
                    if (!MappedMouseButtons.Contains(pair.Key))
                    {
                        if (pair.Key == MouseButtons.LeftButton)
                        {
                            if (CurrentMouseState.LeftButton == ButtonState.Pressed)
                            {
                                ActiveStates.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                        if (pair.Key == MouseButtons.RightButton)
                        {
                            if (CurrentMouseState.RightButton == ButtonState.Pressed)
                            {
                                ActiveStates.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                        if (pair.Key == MouseButtons.MiddleButton)
                        {
                            if (CurrentMouseState.MiddleButton == ButtonState.Pressed)
                            {
                                ActiveStates.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                        if (pair.Key == MouseButtons.XButton1)
                        {
                            if (CurrentMouseState.XButton1 == ButtonState.Pressed)
                            {
                                ActiveStates.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                        if (pair.Key == MouseButtons.XButton2)
                        {
                            if (CurrentMouseState.XButton2 == ButtonState.Pressed)
                            {
                                ActiveStates.Add(pair.Value);
                                MappedMouseButtons.Add(pair.Key);
                            }
                        }
                    }
                }

                //MOUSE RANGE TO RANGE
                foreach (KeyValuePair<MouseRanges, int> pair in inputMap.MouseRangeToRange)
                {
                    if (!MappedMouseRanges.Contains(pair.Key))
                    {
                        if (pair.Key == MouseRanges.ScrollDelta)
                        {
                            ActiveRanges.Add(pair.Value);
                            RangeValues.Add(pair.Value, CurrentMouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue);
                            MappedMouseRanges.Add(pair.Key);
                        }
                        if (pair.Key == MouseRanges.MouseHorizontalDelta)
                        {
                            float hCenter = ViewPort.Width / 2;
                            ActiveRanges.Add(pair.Value);
                            RangeValues.Add(pair.Value, CurrentMouseState.X - hCenter);
                            MappedMouseRanges.Add(pair.Key);
                        }
                        if (pair.Key == MouseRanges.MouseVerticalDelta)
                        {
                            float vCenter = ViewPort.Height / 2;
                            ActiveRanges.Add(pair.Value);
                            RangeValues.Add(pair.Value, CurrentMouseState.Y - vCenter);
                            MappedMouseRanges.Add(pair.Key);
                        }
                    }
                }
            }

            //Given the Mapped Inputs, Call all Callbacks and see if they can use the Mapped Input
            foreach (Entity e in EntityList)
            {
                var inputHandlers = e.GetComponent<CInputHandlers>();

                foreach (int callbackID in inputHandlers.InputHandlerIDs)
                {
                    GameRegistry.FetchInputCallback(callbackID)(e, gameTime, ActiveActions, ActiveStates, ActiveRanges, RangeValues);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //Do Nothing
        }
    }
}
