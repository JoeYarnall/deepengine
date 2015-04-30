using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public delegate List<Entity> RequestEntityListHandler();

    public static class EntityEngine
    {
        private static NodeListManager manager;

        private static List<Entity> entityList;
        private static List<System> systemList;

        private static List<Entity> chngEntsQueue;
        private static List<Entity> addEntsQueue;
        private static List<Entity> rmvEntsQueue;

        private static int NextInstanceID { get; set; }
        public const int InvalidInstanceID = 0x00000000;
        
        #region Core Methods
        public static void Initialize()
        {
            GameRegistry.Initialize();

            entityList = new List<Entity>();
            systemList = new List<System>();

            chngEntsQueue = new List<Entity>();
            addEntsQueue = new List<Entity>();
            rmvEntsQueue = new List<Entity>();

            manager = new NodeListManager(GetAllEntities);

            NextInstanceID = 0x00000001;
        }

        public static void Update(GameTime gameTime)
        {
            foreach (System system in systemList)
            {
                if (system.Active)
                {
                    system.Update(gameTime);
                }
            }
            
            //Multi-Threading
            //var handles = new ManualResetEvent[systemList.Count];
            //int i = 0;
            //foreach (System system in systemList)
            //{
            //    if (system.Active)
            //    {
            //        handles[i] = new ManualResetEvent(false);
            //        var currentHandle = handles[i];
            //        Action wrappedAction = () => { try { system.Update(gameTime); } finally { currentHandle.Set(); } };
            //        ThreadPool.QueueUserWorkItem(x => wrappedAction());
            //        i++;
            //    }
            //}
            //WaitHandle.WaitAll(handles);


            //Update the EntityList
            ManageEntityList();
        }

        public static void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (System system in systemList)
            {
                if (system.Active)
                {
                    system.Draw(gameTime, sb);
                }
            }
        }
        #endregion Core Methods

        #region Entity Methods
        public static Entity SpawnEntity(string name, bool active, bool persist)
        {
            Entity temp = new Entity();

            temp.Name = name;
            temp.Active = active;
            temp.Persist = persist;

            return AddEntity(temp);
        }

        public static Entity SpawnEntity()
        {
            return AddEntity(new Entity());
        }

        public static Entity SpawnPrefab(int code)
        {
            return AddEntity(GameRegistry.CreatePrefab(code));
        }

        private static Entity AddEntity(Entity e)
        {
            e.EntityChangedEvent += EntityChanged;
            e.InstanceID = NextInstanceID++;

            var data = new MessageData();
            SendMessage(EngineMessageIds.EntityCreated, ref data, e, e);

            //Add the entities to the addedEntities buffer to be processed.
            addEntsQueue.Add(e);
            return e;
        }

        public static Entity GetEntity(string name)
        {
            return entityList.Find(e => e.Name.Equals(name));
        }

        public static Entity GetEntity(int instanceID)
        {
            return entityList.Find(e => e.InstanceID == instanceID);
        }

        public static List<Entity> GetAllEntities()
        {
            return entityList;
        }

        public static List<Entity> GetAllActiveEntities()
        {
            return entityList.FindAll( (Entity e) =>
            {
                return e.Active;
            });
        }

        public static void RemoveEntity(Entity entity)
        {
            rmvEntsQueue.Add(entity);
        }

        public static void RemoveAllEntities(bool persist)
        {
            List<Entity> temp = new List<Entity>();

            if (persist)
            {
                foreach (Entity e in entityList)
                {
                    if (!e.Persist)
                    {
                        //Add the entity to the removedEntities buffer to be processed.
                        rmvEntsQueue.Add(e);
                        temp.Add(e);
                    }
                }

                foreach (Entity e in temp)
                {
                    //Remove the entity from the entity list.
                    entityList.Remove(e);
                }
            }
            else
            {
                //Add all the entities to the removedEntities buffer to be processed.
                rmvEntsQueue.AddRange(entityList);

                //Clear the entityList.
                entityList.Clear();
            }
        }

        public static void EntityChanged(object sender, EntityChangedEvent args)
        {
            if (!chngEntsQueue.Contains(args.Entity))
            {
                chngEntsQueue.Add(args.Entity);
            }
        }

        private static void ManageEntityList()
        {
            //Add the entities to the EntityList
            entityList.AddRange(addEntsQueue);

            //Remove entities from the EntityList
            foreach (Entity e in rmvEntsQueue)
            {
                entityList.Remove(e);
            }

            //Update the NodeLists
            manager.ManageNodeLists(rmvEntsQueue, addEntsQueue, chngEntsQueue);

            addEntsQueue.Clear();
            rmvEntsQueue.Clear();
            chngEntsQueue.Clear();
        }
        #endregion Entity Methods

        #region System Methods
        public static System AddSystem<T>() where T : System
        {
            T result = (T)Activator.CreateInstance(typeof(T));

            //Give the System callbacks to request and release NodeLists.
            result.RequestHandler = manager.RequestNodeList;
            result.ReleaseHandler = manager.ReleaseNodeList;

            //Run the initialize method.
            result.Initialize();

            systemList.Add(result);

            return result;
        }

        public static System GetSystem<T>() where T : System
        {
            return systemList.Find(delegate(System s)
            {
                if (s.GetType().Equals(typeof(T)))
                    return true;
                else
                    return false;
            });
        }

        public static System GetSystem(int systemId)
        {
            return systemList.Find((s) =>
            {
                return s.SystemId == systemId; 
            });
        }

        public static List<System> GetAllSystems()
        {
            return systemList;
        }

        public static List<System> GetAllActiveSystems()
        {
            return systemList.FindAll(delegate(System s)
            {
                return s.Active;
            });
        }

        public static void RemoveSystem(System system)
        {
            system.Destroy();
            systemList.Remove(system);
        }

        public static void RemoveAllSystems(bool persist)
        {
            foreach (System s in systemList)
            {
                if (!s.Persist || !persist)
                {
                    s.Destroy();
                }
            }

            if (persist)
            {
                systemList.RemoveAll(
                delegate(System s)
                {
                    return !s.Persist;
                });
            }
            else
            {
                systemList = new List<System>();
            }
        }
        #endregion System Methods

        #region Messaging Methods

        public static void SendMessage(int messageId, ref MessageData data, Entity target, object sender)
        {
            for (int i = 0; i < systemList.Count; i++)
            {
                systemList[i].DeliverMessage(messageId, ref data, target, sender);
            }
        }

        #endregion Messaging Methods

        #region Parent Child Entities

        public static bool AttachChildEntity(Entity parent, Entity child)
        {
            //First check to see if child doesn't already have a parent.
            if (child.HasComponent<CParent>())
            {
                if (child.GetComponent<CParent>().Parent != null)
                {
                    //TODO: BAIL OUT - CHILD ALREADY HAS A PARENT
                    return false;
                }
            }
            else
            {
                child.AddComponent<CParent>();
                child.GetComponent<CParent>().Parent = parent;
            }

            //Next check to see if parent already has children.
            if (!parent.HasComponent<CChildren>())
            {
                parent.AddComponent<CChildren>();
            }

            parent.GetComponent<CChildren>().Children.Add(child);

            return true;
        }

        public static bool RemoveChildEntity(Entity parent, Entity child)
        {
            //TODO: REMOVE PARENT FROM CHILD AND VICE VERSA
            return true;
        }

        #endregion Parent Child Entities
    }
}