using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public delegate Entity PrefabCallback();
    public delegate void InputCallback(Entity ent, GameTime gameTime, List<int> activeActions, List<int> activeStates, List<int> activeRanges, Dictionary<int, float> rangeValues);
    public delegate void ScriptCallback(Entity ent, GameTime gameTime);

    public static class GameRegistry
    {
        private static Dictionary<int, PrefabCallback> PrefabEntityDatabase { get; set; }
        private static Dictionary<int, InputCallback> InputCallbackDatabase { get; set; }
        private static Dictionary<int, ScriptCallback> ScriptCallbackDatabase { get; set; }

        public static void Initialize()
        {
            PrefabEntityDatabase = new Dictionary<int, PrefabCallback>();
            InputCallbackDatabase = new Dictionary<int, InputCallback>();
            ScriptCallbackDatabase = new Dictionary<int, ScriptCallback>();
        }

        public static Entity CreatePrefab(int code)
        {
            Entity result;
            PrefabCallback handler;

            if (PrefabEntityDatabase.TryGetValue(code, out handler))
            {
                result = handler();
                return result;
            }
            else
            {
                throw new EntityNotFoundException("The Prefab Entity of Type: " + code + " is not registered with the Registry and can't be created.");
            }
        }

        public static void RegisterPrefab(int code, PrefabCallback handler)
        {
            if (!PrefabEntityDatabase.ContainsKey(code))
            {
                PrefabEntityDatabase.Add(code, handler);
            }
            else
            {
                throw new EntityAlreadyRegisteredException("The Prefab Entity of Type: " + code + " is already registered.");
            }
        }

        public static InputCallback FetchInputCallback(int callbackID)
        {
            InputCallback callback;

            if (InputCallbackDatabase.TryGetValue(callbackID, out callback))
            {
                return callback;
            }
            else
            {
                throw new CallbackNotFoundException("The Input Callback of Type: " + callback + " is not registered with the Callback Registry and can't be fetched.");
            }
        }

        public static void RegisterInputCallback(int callbackID, InputCallback callback)
        {
            if (!InputCallbackDatabase.ContainsKey(callbackID))
            {
                InputCallbackDatabase.Add(callbackID, callback);
            }
            else
            {
                throw new CallbackAlreadyRegisteredException("The Input Callback of Type" + callbackID + "is already registered.");
            }
        }

        public static ScriptCallback FetchScriptCallback(int callbackID)
        {
            ScriptCallback callback;

            if (ScriptCallbackDatabase.TryGetValue(callbackID, out callback))
            {
                return callback;
            }
            else
            {
                throw new CallbackNotFoundException("The Script Callback of Type: " + callback + " is not registered with the Callback Registry and can't be fetched.");
            }
        }

        public static void RegisterScriptCallback(int callbackID, ScriptCallback callback)
        {
            if (!ScriptCallbackDatabase.ContainsKey(callbackID))
            {
                ScriptCallbackDatabase.Add(callbackID, callback);
            }
            else
            {
                throw new CallbackAlreadyRegisteredException("The Script Callback of Type" + callbackID + "is already registered.");
            }
        }
    }
}
