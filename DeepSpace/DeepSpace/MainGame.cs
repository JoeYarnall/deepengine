using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DeepSpace
{
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Texture2D backgroundTexture;
        Texture2D planetTexture;
        Texture2D mouseTexture;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            EntityEngine.Initialize();

            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            this.IsMouseVisible = false;
            graphics.IsFullScreen = false;

            graphics.ApplyChanges();
            Window.Title = "Deep Engine v0.1 - " + graphics.PreferredBackBufferWidth + " x " + graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("testFont2");
            backgroundTexture = Content.Load<Texture2D>("background");
            planetTexture = Content.Load<Texture2D>("Planets/planet1");
            mouseTexture = Content.Load<Texture2D>("Mouse");

            #region Prefab Declarations
            GameRegistry.RegisterPrefab(PrefabIds.UI, () =>
            {
                var e = new Entity();
                e.Active = true;
                e.Persist = false;
                e.Name = "UI";

                EntityEngine.AttachChildEntity(e, EntityEngine.SpawnPrefab(PrefabIds.Mouse));
                EntityEngine.AttachChildEntity(e, EntityEngine.SpawnPrefab(PrefabIds.DebugWindow));
                
                return e;
            });

            GameRegistry.RegisterPrefab(PrefabIds.Player, () =>
            {
                var e = new Entity();
                e.Active = true;
                e.Persist = false;
                e.Name = "Player";

                EntityEngine.AttachChildEntity(e, EntityEngine.SpawnPrefab(PrefabIds.Camera));

                e.AddComponent<CInputMap>();
                e.GetComponent<CInputMap>().Priority = 1;
                e.GetComponent<CInputMap>().Add(MouseButtons.LeftButton, MappedInputIds.LeftClick, InputType.Action);
                e.GetComponent<CInputMap>().Add(Keys.Enter, MappedInputIds.EndTurn, InputType.Action);

                e.AddComponent<CInputHandlers>();
                e.GetComponent<CInputHandlers>().Add(MappedInputIds.LeftClick);
                e.GetComponent<CInputHandlers>().Add(MappedInputIds.EndTurn);

                e.AddComponent<CPlayerState>();
                e.GetComponent<CPlayerState>().PickedEntity = null;
                e.GetComponent<CPlayerState>().PlayerNumber = 1;

                return e;
            });
            
            GameRegistry.RegisterPrefab(PrefabIds.Camera, () =>
            {
                var e = new Entity();
                e.Active = true;
                e.Persist = false;
                e.Name = "Main Camera";

                e.AddComponent<CCameraPlacement>();
                e.GetComponent<CCameraPlacement>().StayWithinWorldBounds = true;
                e.GetComponent<CCameraPlacement>().Position = new Vector2(0, 0);
                e.GetComponent<CCameraPlacement>().Rotation = 0f;
                e.GetComponent<CCameraPlacement>().Zoom = 1f;
                
                e.AddComponent<CViewport>();
                e.GetComponent<CViewport>().IsMouseVisible = this.IsMouseVisible;
                e.GetComponent<CViewport>().AntiAliasing = graphics.PreferMultiSampling;
                e.GetComponent<CViewport>().VSync = graphics.SynchronizeWithVerticalRetrace;
                e.GetComponent<CViewport>().IsFullscreen = graphics.IsFullScreen;
                e.GetComponent<CViewport>().Width = graphics.PreferredBackBufferWidth;
                e.GetComponent<CViewport>().Height = graphics.PreferredBackBufferHeight;
                e.GetComponent<CViewport>().WorldUnitPixelWidth = 50;
                e.GetComponent<CViewport>().WorldUnitPixelHeight = 50;

                e.AddComponent<CInputMap>();
                e.GetComponent<CInputMap>().Priority = 1;
                e.GetComponent<CInputMap>().Add(Keys.W, MappedInputIds.CameraUp, InputType.State);
                e.GetComponent<CInputMap>().Add(Keys.A, MappedInputIds.CameraLeft, InputType.State);
                e.GetComponent<CInputMap>().Add(Keys.S, MappedInputIds.CameraDown, InputType.State);
                e.GetComponent<CInputMap>().Add(Keys.D, MappedInputIds.CameraRight, InputType.State);

                e.AddComponent<CInputHandlers>();
                e.GetComponent<CInputHandlers>().Add(MappedInputIds.CameraUp);
                e.GetComponent<CInputHandlers>().Add(MappedInputIds.CameraLeft);
                e.GetComponent<CInputHandlers>().Add(MappedInputIds.CameraDown);
                e.GetComponent<CInputHandlers>().Add(MappedInputIds.CameraRight);

                return e;
            });

            GameRegistry.RegisterPrefab(PrefabIds.Mouse, () =>
            {
                var e = new Entity();
                e.Active = true;
                e.Persist = false;
                e.Name = "Mouse";
                
                e.AddComponent<CSprite>();
                e.GetComponent<CSprite>().Alpha = 1f;
                e.GetComponent<CSprite>().FrameIndex = 0;
                e.GetComponent<CSprite>().VarietyIndex = 0;
                e.GetComponent<CSprite>().Tint = Color.White;
                e.GetComponent<CSprite>().Sprite = Sprite.CreateHorizontalFramesVerticalVarieties(new SpriteSheet(mouseTexture, 1, 1, 0), 0, 1, 0, 1);

                e.AddComponent<CScreenPlacement>();
                e.GetComponent<CScreenPlacement>().Rotation = 0f;
                e.GetComponent<CScreenPlacement>().Visible = true;
                e.GetComponent<CScreenPlacement>().Depth = 0;
                e.GetComponent<CScreenPlacement>().Bounds = new Rectangle(
                    (int)(graphics.PreferredBackBufferWidth * 0.5), 
                    (int)(graphics.PreferredBackBufferHeight * 0.5), 
                    mouseTexture.Width + 1, 
                    mouseTexture.Height + 1);

                e.AddComponent<CScriptHandlers>();
                e.GetComponent<CScriptHandlers>().Add(ScriptIds.SetMouseLocation);
                
                return e;
            });

            GameRegistry.RegisterPrefab(PrefabIds.DebugWindow, () =>
            {
                var e = new Entity();
                e.Active = true;
                e.Persist = true;
                e.Name = "Debug Window";

                e.AddComponent<CText>();
                e.GetComponent<CText>().Font = font;
                e.GetComponent<CText>().TextAlpha = 1f;
                e.GetComponent<CText>().TextColor = Color.White;

                e.AddComponent<CInputMap>();
                e.GetComponent<CInputMap>().Priority = 2;
                e.GetComponent<CInputMap>().Add(Keys.F3, MappedInputIds.ToggleDebugMode, InputType.Action);

                e.AddComponent<CInputHandlers>();
                e.GetComponent<CInputHandlers>().Add(MappedInputIds.ToggleDebugMode);

                e.AddComponent<CScriptHandlers>();
                e.GetComponent<CScriptHandlers>().Add(ScriptIds.SetDebugWindowText);
                
                return e;
            });

            GameRegistry.RegisterPrefab(PrefabIds.MetaData, () =>
            {
                var e = new Entity();
                e.Active = true;
                e.Persist = true;
                e.Name = "Meta Data";

                e.AddComponent<CFrameRate>();

                e.AddComponent<CEntityEngineMetrics>();
                e.GetComponent<CEntityEngineMetrics>().TotalEntities = 0;
                e.GetComponent<CEntityEngineMetrics>().ActiveEntities = 0;
                e.GetComponent<CEntityEngineMetrics>().TotalSystems = 0;
                e.GetComponent<CEntityEngineMetrics>().ActiveSystems = 0;

                e.AddComponent<CScriptHandlers>();
                e.GetComponent<CScriptHandlers>().Add(ScriptIds.SetEntityEngineMetrics);
                e.GetComponent<CScriptHandlers>().Add(ScriptIds.SetFrameRate);

                return e;
            });

            GameRegistry.RegisterPrefab(PrefabIds.World, () =>
            {
                var e = new Entity();
                e.Active = true;
                e.Persist = false;
                e.Name = "World";

                e.AddComponent<CGameState>();
                e.GetComponent<CGameState>().CurrentTurnNumber = 1;
                e.GetComponent<CGameState>().MaxNumberOfTurns = 100;
                e.GetComponent<CGameState>().MaxPlayers = 4;
                e.GetComponent<CGameState>().ActivePlayer = 1;

                return e;
            });

            GameRegistry.RegisterPrefab(PrefabIds.Planet, () =>
            {
                var e = new Entity();
                e.Active = true;
                e.Persist = false;
                e.Name = "Planet";

                e.AddComponent<CWorldPlacement>();
                e.GetComponent<CWorldPlacement>().Layer = 0;
                e.GetComponent<CWorldPlacement>().Position = new Vector3(0f, 0f, 1f);
                e.GetComponent<CWorldPlacement>().Rotation = 0f;
                e.GetComponent<CWorldPlacement>().Size = new Vector2(6, 6);
                e.GetComponent<CWorldPlacement>().Visible = true;

                e.AddComponent<CSprite>();
                e.GetComponent<CSprite>().Alpha = 1f;
                e.GetComponent<CSprite>().FrameIndex = 0;
                e.GetComponent<CSprite>().VarietyIndex = 0;
                e.GetComponent<CSprite>().Tint = Color.White;
                e.GetComponent<CSprite>().Sprite = Sprite.CreateHorizontalFramesVerticalVarieties(new SpriteSheet(planetTexture, 1, 1, 0), 0, 1, 0, 1);

                e.AddComponent<CCollider>();
                e.GetComponent<CCollider>().Collidable = true;
                e.GetComponent<CCollider>().Pickable = true;

                e.AddComponent<COwner>();
                e.GetComponent<COwner>().PlayerNumber = 0;

                e.AddComponent<CPlanet>();
                e.GetComponent<CPlanet>().Class = PlanetClass.Habitable;
                e.GetComponent<CPlanet>().Atmosphere = PlanetAtmosphere.Oxygen;
                e.GetComponent<CPlanet>().MaxPop = 10f;
                e.GetComponent<CPlanet>().CurPop = 0f;
                e.GetComponent<CPlanet>().PopGrowthRate = 0.25f;
                
                return e;
            });
            #endregion Prefab Declarations

            #region Callback Declarations
            GameRegistry.RegisterInputCallback(MappedInputIds.CameraUp, (Entity ent, GameTime gameTime, List<int> activeActions, List<int> activeStates, List<int> activeRanges, Dictionary<int, float> rangeValues) =>
            {
                if (activeStates.Contains(MappedInputIds.CameraUp))
                {
                    ent.GetComponent<CCameraPlacement>().Position =
                        Vector2.Lerp(ent.GetComponent<CCameraPlacement>().Position, ent.GetComponent<CCameraPlacement>().Position + new Vector2(0, -3f), (float)gameTime.ElapsedGameTime.TotalSeconds);
                    activeStates.Remove(MappedInputIds.CameraUp);
                }
            });

            GameRegistry.RegisterInputCallback(MappedInputIds.CameraLeft, (Entity ent, GameTime gameTime, List<int> activeActions, List<int> activeStates, List<int> activeRanges, Dictionary<int, float> rangeValues) =>
            {
                if (activeStates.Contains(MappedInputIds.CameraLeft))
                {
                    ent.GetComponent<CCameraPlacement>().Position =
                        Vector2.Lerp(ent.GetComponent<CCameraPlacement>().Position, ent.GetComponent<CCameraPlacement>().Position + new Vector2(-3f, 0), (float)gameTime.ElapsedGameTime.TotalSeconds);
                    activeStates.Remove(MappedInputIds.CameraLeft);
                }
            });

            GameRegistry.RegisterInputCallback(MappedInputIds.CameraDown, (Entity ent, GameTime gameTime, List<int> activeActions, List<int> activeStates, List<int> activeRanges, Dictionary<int, float> rangeValues) =>
            {
                if (activeStates.Contains(MappedInputIds.CameraDown))
                {
                    ent.GetComponent<CCameraPlacement>().Position =
                        Vector2.Lerp(ent.GetComponent<CCameraPlacement>().Position, ent.GetComponent<CCameraPlacement>().Position + new Vector2(0, 3f), (float)gameTime.ElapsedGameTime.TotalSeconds);
                    activeStates.Remove(MappedInputIds.CameraDown);
                }
            });

            GameRegistry.RegisterInputCallback(MappedInputIds.CameraRight, (Entity ent, GameTime gameTime, List<int> activeActions, List<int> activeStates, List<int> activeRanges, Dictionary<int, float> rangeValues) =>
            {
                if (activeStates.Contains(MappedInputIds.CameraRight))
                {
                    ent.GetComponent<CCameraPlacement>().Position =
                        Vector2.Lerp(ent.GetComponent<CCameraPlacement>().Position, ent.GetComponent<CCameraPlacement>().Position + new Vector2(3f, 0), (float)gameTime.ElapsedGameTime.TotalSeconds);
                    activeStates.Remove(MappedInputIds.CameraRight);
                }
            });

            GameRegistry.RegisterInputCallback(MappedInputIds.CameraZoom, (Entity ent, GameTime gameTime, List<int> activeActions, List<int> activeStates, List<int> activeRanges, Dictionary<int, float> rangeValues) =>
            {
                if (activeRanges.Contains(MappedInputIds.CameraZoom))
                {
                    float delta;
                    if (rangeValues.TryGetValue(MappedInputIds.CameraZoom, out delta))
                    {
                        ent.GetComponent<CCameraPlacement>().Zoom += 0.005f * ((float)gameTime.ElapsedGameTime.TotalSeconds * delta);
                        rangeValues.Remove(MappedInputIds.CameraZoom);
                        activeRanges.Remove(MappedInputIds.CameraZoom);
                    }
                }
            });

            GameRegistry.RegisterInputCallback(MappedInputIds.LeftClick, (Entity ent, GameTime gameTime, List<int> activeActions, List<int> activeStates, List<int> activeRanges, Dictionary<int, float> rangeValues) =>
            {
                if (activeActions.Contains(MappedInputIds.LeftClick))
                {
                    //get mouse location from local player
                    MessageData mouseData = new MessageData();
                    EntityEngine.SendMessage(EngineMessageIds.GetMouseLocation, ref mouseData, ent, ent);
                    //send raycast world message with mouse screen location
                    MessageData raycastData = new MessageData(mouseData.P1);
                    EntityEngine.SendMessage(EngineMessageIds.RaycastWorld, ref raycastData, ent, ent);
                    //Find Entity that has been picked
                    Entity pickedEntity = EntityEngine.GetEntity(raycastData.I3);
                    //Send Entity Picked Message?
                    ent.GetComponent<CPlayerState>().PickedEntity = pickedEntity;
                    activeActions.Remove(MappedInputIds.LeftClick);
                }
            });

            GameRegistry.RegisterInputCallback(MappedInputIds.ToggleDebugMode, (Entity ent, GameTime gameTime, List<int> activeActions, List<int> activeStates, List<int> activeRanges, Dictionary<int, float> rangeValues) =>
            {
                if (activeActions.Contains(MappedInputIds.ToggleDebugMode))
                {
                    ent.Active = !ent.Active;
                    activeActions.Remove(MappedInputIds.ToggleDebugMode);
                }
            });

            GameRegistry.RegisterInputCallback(MappedInputIds.EndTurn, (Entity ent, GameTime gameTime, List<int> activeActions, List<int> activeStates, List<int> activeRanges, Dictionary<int, float> rangeValues) =>
            {
                if (activeActions.Contains(MappedInputIds.EndTurn))
                {
                    var gameState = ent.GetComponent<CGameState>();

                    if (gameState.ActivePlayer >= gameState.MaxPlayers)
                    {
                        if (gameState.CurrentTurnNumber >= gameState.MaxNumberOfTurns)
                        {
                            //TODO: End Game
                        }
                        else
                        {
                            gameState.CurrentTurnNumber++;
                            gameState.ActivePlayer = 1;
                        }
                    }
                    else
                    {
                        gameState.ActivePlayer++;
                    }

                    activeActions.Remove(MappedInputIds.EndTurn);
                }
            });

            GameRegistry.RegisterScriptCallback(ScriptIds.SetMouseLocation, (Entity ent, GameTime gameTime) =>
            {
                if (ent.HasComponent<CScreenPlacement>())
                {
                    MessageData data = new MessageData();
                    EntityEngine.SendMessage(EngineMessageIds.GetMouseLocation, ref data, ent, ent);

                    if (data.Handled)
                    {
                        var tmp = ent.GetComponent<CScreenPlacement>().Bounds;
                        tmp.Location = data.P1;
                        ent.GetComponent<CScreenPlacement>().Bounds = tmp;
                    }
                }
            });

            GameRegistry.RegisterScriptCallback(ScriptIds.SetDebugWindowText, (Entity ent, GameTime gameTime) =>
            {
                if (ent.HasComponent<CText>())
                {
                    var metaData = EntityEngine.GetEntity("Meta Data").GetComponent<CEntityEngineMetrics>();
                    var frameRate = EntityEngine.GetEntity("Meta Data").GetComponent<CFrameRate>();
                    var playerState = EntityEngine.GetEntity("Player").GetComponent<CPlayerState>();

                    var text = ent.GetComponent<CText>();

                    StringBuilder build = new StringBuilder();

                    build.AppendFormat("Frame Rate       : {0}", frameRate.FrameRate).AppendLine();
                    build.AppendFormat("Active Entities  : {0}", metaData.ActiveEntities.ToString()).AppendLine();
                    build.AppendFormat("Total Entities   : {0}", metaData.TotalEntities.ToString()).AppendLine();
                    build.AppendFormat("Active Systems   : {0}", metaData.ActiveSystems.ToString()).AppendLine();
                    build.AppendFormat("Total Systems    : {0}", metaData.TotalSystems.ToString()).AppendLine();
                    if (playerState.PickedEntity != null)
                        build.AppendFormat("Picked Entity    : {0}-{1}", playerState.PickedEntity.Name.ToString(), playerState.PickedEntity.InstanceID.ToString()).AppendLine();
                    else
                        build.Append("Picked Entity    : None").AppendLine();

                    text.Text = build.ToString();
                }
            });

            GameRegistry.RegisterScriptCallback(ScriptIds.SetEntityEngineMetrics, (Entity ent, GameTime gameTime) =>
            {
                if (ent.HasComponent<CEntityEngineMetrics>())
                {
                    var metaComp = ent.GetComponent<CEntityEngineMetrics>();

                    metaComp.TotalEntities = EntityEngine.GetAllEntities().Count;
                    metaComp.ActiveEntities = EntityEngine.GetAllActiveEntities().Count;
                    metaComp.TotalSystems = EntityEngine.GetAllSystems().Count;
                    metaComp.ActiveSystems = EntityEngine.GetAllActiveSystems().Count;
                }
            });

            GameRegistry.RegisterScriptCallback(ScriptIds.SetFrameRate, (Entity ent, GameTime gameTime) =>
            {
                if (ent.HasComponent<CFrameRate>())
                {
                    var fpsComp = ent.GetComponent<CFrameRate>();

                    fpsComp.FrameCounter++; //TODO: Needs to be moved to the draw call some how

                    fpsComp.ElapsedTime += gameTime.ElapsedGameTime;

                    if (fpsComp.ElapsedTime > TimeSpan.FromSeconds(1))
                    {
                        fpsComp.ElapsedTime -= TimeSpan.FromSeconds(1);
                        fpsComp.FrameRate = fpsComp.FrameCounter;
                        fpsComp.FrameCounter = 0;
                    }
                }
            });

            #endregion Callback Declarations

            EntityEngine.AddSystem<InputSystem>();
            EntityEngine.AddSystem<ScriptSystem>();
            EntityEngine.AddSystem<CameraSystem>();
            EntityEngine.AddSystem<ViewPortSystem>();
            EntityEngine.AddSystem<CollisionSystem>();
            EntityEngine.AddSystem<WorldSpriteRenderSystem>();
            EntityEngine.AddSystem<ScreenTextRenderSystem>();
            EntityEngine.AddSystem<ScreenSpriteRenderSystem>();

            EntityEngine.SpawnPrefab(PrefabIds.MetaData);
            EntityEngine.SpawnPrefab(PrefabIds.World);
            EntityEngine.SpawnPrefab(PrefabIds.Player);
            EntityEngine.SpawnPrefab(PrefabIds.UI);
            EntityEngine.SpawnPrefab(PrefabIds.Planet);
        }

        protected override void Update(GameTime gameTime)
        {
            EntityEngine.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            EntityEngine.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}