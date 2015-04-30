using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DeepEngine
{
    public sealed class Entity : DeepObject, IEquatable<Entity>
    {
        public int InstanceID { get; set; }
        public String Name { get; set; }

        private HashSet<Type> compListHash;
        private Boolean Dirty { get; set; }

        public event EventHandler<EntityChangedEvent> EntityChangedEvent;

        public List<Component> ComponentList { get; set; }

        public Entity()
        {
            ComponentList = new List<Component>();
            compListHash = new HashSet<Type>();
            Dirty = false;
        }

        public bool Equals(Entity obj)
        {
            if (this.InstanceID == obj.InstanceID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Entity)
            {
                return Equals(obj);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.InstanceID;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Name);

            foreach (Component c in ComponentList)
            {
                sb.AppendLine(c.ToString());
            }

            return sb.ToString();
        }

        public T AddComponent<T>() where T : Component
        {
            try
            {
                var c = (T)Activator.CreateInstance(typeof(T));
                ComponentList.Add(c);

                if(EntityChangedEvent != null)
                    EntityChangedEvent(this, new EntityChangedEvent(this, typeof(T), ACTION.ADD));
                
                Dirty = true;

                return c;
            }
            catch (ComponentNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Trys to get a Component of the given type and returns the first instance found. 
        /// If the Component type doesn't exist it throws ComponentNotFoundException.
        /// </summary>
        /// <typeparam name="T">The Type of Component.</typeparam>
        /// <returns>The Component.</returns>
        public T GetComponent<T>() where T : Component
        {
            T comp = (T)ComponentList.Find(delegate(Component c)
            {
                if (c.GetType().Equals(typeof(T)))
                    return true;
                else
                    return false;
            });

            if (comp != null)
                return comp;
            else
                throw new ComponentNotFoundException("The Component of Type: " + typeof(T).ToString() + " could not be found.");
        }
        
        /// <summary>
        /// Trys to get all Components of the given type and returns them in a List.
        /// If the Component type doesn't exist it throws ComponentNotFoundException.
        /// </summary>
        /// <typeparam name="T">The Type of Component.</typeparam>
        /// <returns>A List of the Components found.</returns>
        public List<T> GetComponents<T>() where T : Component
        {
            var result = (List<T>)ComponentList.FindAll(delegate(Component c)
                {
                    if (c.GetType().Equals(typeof(T)))
                        return true;
                    else
                        return false;
                }).Cast<T>();

            if (result != null)
                return result;
            else
                throw new ComponentNotFoundException("The Component of Type: " + typeof(T).ToString() + " could not be found.");
        }

        /// <summary>
        /// Checks if the Entity has a Component of the desired Type. Returns True if it does and False if it doesn't.
        /// </summary>
        /// <typeparam name="T">The Type of Component.</typeparam>
        /// <returns>Returns True if it does and False if it doesn't.</returns>
        public bool HasComponent<T>() where T : Component
        {
            return ComponentList.Exists(delegate(Component c)
                {
                    if (c.GetType().Equals(typeof(T)))
                        return true;
                    else
                        return false;
                });
        }

        /// <summary>
        /// Trys to Remove a Component of the given type from this Entity. If the Component doesn't exist it throws ComponentNotFoundException. 
        /// </summary>
        /// <param name="component">The Component to remove.</param>
        public void RemoveComponent(Component component)
        {
            if (ComponentList.Remove(component))
            {                   
                EntityChangedEvent(this, new EntityChangedEvent(this, component.GetType(), ACTION.REMOVE));
                Dirty = true;
            }
            else
            {
                throw new ComponentNotFoundException("The Component of Type: " + component.GetType().ToString() + " could not be found.");
            }
        }
        
        /// <summary>
        /// Removes all Components from this Entity.
        /// </summary>
        /// <param name="persist">If true then Components that have their Persist values set to true will not be removed.</param>
        public void RemoveAllComponents(bool persist)
        {
            foreach (Component c in ComponentList)
            {
                if (!(persist && c.Persist))
                {
                    EntityChangedEvent(this, new EntityChangedEvent(this, c.GetType(), ACTION.REMOVE));
                    Dirty = true;
                }
            }

            if (persist)
            {
                ComponentList.RemoveAll(delegate(Component c)
                    {
                        return !c.Persist;
                    });
            }
            else
            {
                ComponentList.Clear();
            }
        }

        /// <summary>
        /// Gets a HashSet that represents the Component Types attached to this Entity.
        /// </summary>
        /// <returns>The HashSet of Types.</returns>
        public HashSet<Type> GetComponentListHashSet()
        {
            if (Dirty)
            {
                Dirty = false;
                compListHash.Clear();

                foreach (Component c in ComponentList)
                {
                    compListHash.Add(c.GetType());
                }
            }
            
            return compListHash;
        }
    }
}
