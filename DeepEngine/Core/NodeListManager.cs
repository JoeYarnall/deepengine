using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public delegate List<Entity> RequestHandler(Aspect request);
    public delegate bool ReleaseHandler(Aspect request);

    public sealed class NodeListManager
    {
        Dictionary<Aspect, List<Entity>> nodeLists;
        Dictionary<Aspect, int> nodeListCounters;

        public RequestEntityListHandler RequestEntityListHandler { get; set; }

        public NodeListManager(RequestEntityListHandler handler)
        {
            nodeLists = new Dictionary<Aspect, List<Entity>>();
            nodeListCounters = new Dictionary<Aspect, int>();

            RequestEntityListHandler = handler;
        }

        public void ManageNodeLists(List<Entity> rmvEntsQueue, List<Entity> addEntsQueue, List<Entity> chngEntsQueue)
        {
            //Entities that have been REMOVED from the EntityList this frame.
            foreach (Entity e in rmvEntsQueue)
            {
                //These Elements are now removed from the system so for speed remove them from all the other lists.
                addEntsQueue.RemoveAll(delegate(Entity ent)
                {
                    return ent.Equals(e);
                });

                chngEntsQueue.RemoveAll(delegate(Entity ent)
                {
                    return ent.Equals(e);
                });

                //Now remove them from all the NodeLists
                foreach (List<Entity> nl in nodeLists.Values)
                {
                    nl.RemoveAll(delegate (Entity ent) 
                    {
                        return ent.Equals(e);    
                    });
                }
            }

            //Entities that have been ADDED to the EntityList this frame.
            foreach (Entity e in addEntsQueue)
            {
                foreach (Aspect a in nodeLists.Keys)
                {
                    if (CompareEntityToAspect(a, e))
                    {
                        nodeLists[a].Add(e);
                    }
                }
            }

            //Entities that have been CHANGED in the EntityList this frame.
            foreach (Entity e in chngEntsQueue)
            {
                foreach (Aspect a in nodeLists.Keys)
                {
                    if (CompareEntityToAspect(a, e))
                        nodeLists[a].Add(e);
                    else
                        nodeLists[a].Remove(e);
                }
            }
        }

        /// <summary>
        /// Used to request a NodeList that meets the Entity Prototypes requirements.
        /// </summary>
        /// <param name="request">The Entity Prototype that represents the contents of the NodeList.</param>
        /// <returns>The NodeList requested.</returns>
        public List<Entity> RequestNodeList(Aspect request)
        {
            List<Entity> temp;

            if (nodeLists.ContainsKey(request) && nodeListCounters.ContainsKey(request))
            {
                temp = nodeLists[request];
            }
            else
            {
                temp = GenerateNodeList(request);
            }

            RegisterNodeList(request, temp);
            return temp;
        }

        /// <summary>
        /// Release a NodeList from the Manager.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>True if the NodeList is released and false if it isn't.</returns>
        public bool ReleaseNodeList(Aspect key)
        {
            if (key == null || !nodeLists.ContainsKey(key) || !nodeListCounters.ContainsKey(key))
            {
                return false;
            }

            if (nodeListCounters[key] == 1)
            {
                nodeLists.Remove(key);
                nodeListCounters.Remove(key);
            }
            else
            {
                nodeListCounters[key]--;
            }

            return true;
        }
        
        /// <summary>
        /// Searches the EntityList for all Entities that match the Aspect provided and returns them.
        /// </summary>
        /// <param name="key">The Aspect to look for.</param>
        /// <returns>The List of Entities that match the provided Aspect.</returns>
        private List<Entity> GenerateNodeList(Aspect key)
        {
            List<Entity> result = new List<Entity>();
            List<Entity> entityList = RequestEntityListHandler();

            if (key == null) { return entityList; }
            
            foreach (Entity e in entityList)
            {
                //Check AllSet is Subset of Entity && ExclusionSet DOES NOT overlap Entity && OneSet DOES overlap Entity
                if (CompareEntityToAspect(key, e))
                {
                    result.Add(e);
                }
            }

            return result;
        }

        /// <summary>
        /// Register a NodeList with the Manager.
        /// </summary>
        /// <param name="key">A prototype of the Entity represented in the NodeList.</param>
        /// <param name="value">The NodeList to store.</param>
        /// <returns>True if the NodeList is registerd and false if it doesn't.</returns>
        private bool RegisterNodeList(Aspect key, List<Entity> value)
        {
            if (key == null || value == null) return false;

            if (nodeLists.ContainsKey(key) && nodeListCounters.ContainsKey(key))
            {
                nodeListCounters[key]++;
            }
            else
            {
                nodeLists.Add(key, value);
                nodeListCounters.Add(key, 1);
            }

            return true;
        }

        /// <summary>
        /// Checks an to see if the Entity meets the requirements of an Aspect.
        /// </summary>
        /// <param name="a">The Aspect to check.</param>
        /// <param name="e">The Entity to check.</param>
        /// <returns>True if the Entity meets the requirements of the Aspect and False if it doesn't.</returns>
        private bool CompareEntityToAspect(Aspect a, Entity e)
        {
            var set = e.GetComponentListHashSet();

            //Check AllSet is Subset of Entity && ExclusionSet DOES NOT overlap Entity && OneSet DOES overlap Entity
            bool all = a.AllSet.IsSubsetOf(set);
            
            bool exclude = !a.ExclusionSet.Overlaps(set);
            
            bool one;

            if(a.OneSet.Count == 0)
                one = true;
            else
                one = a.OneSet.Overlaps(set);

            
            if (all && exclude && one)
                return true;
            else
                return false;
        }
    }
}