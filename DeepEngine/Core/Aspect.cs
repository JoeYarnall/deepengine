using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public sealed class Aspect : IEquatable<Aspect>
    {
        public HashSet<Type> AllSet { get; set; }
        public HashSet<Type> OneSet { get; set; }
        public HashSet<Type> ExclusionSet { get; set; }

        public static Aspect GetListForAll(Type type, params Type[] types)
        {
            var result = new Aspect();

            if (typeof(Component).IsAssignableFrom(type))
            {
                result.AllSet.Add(type);
            }

            foreach (Type t in types)
            {
                if (typeof(Component).IsAssignableFrom(t))
                {
                    result.AllSet.Add(t);
                }
            }

            return result;
        }

        public static Aspect GetListForOne(Type type, params Type[] types)
        {
            var result = new Aspect();

            if (typeof(Component).IsAssignableFrom(type))
            {
                result.OneSet.Add(type);
            }

            foreach (Type t in types)
            {
                if (typeof(Component).IsAssignableFrom(t))
                {
                    result.OneSet.Add(t);
                }
            }

            return result;
        }

        public Aspect()
        {
            AllSet = new HashSet<Type>();
            OneSet = new HashSet<Type>();
            ExclusionSet = new HashSet<Type>();
        }

        public Aspect Exclude(Type type, params Type[] types)
        {
            if (typeof(Component).IsAssignableFrom(type))
            {
                this.ExclusionSet.Add(type);
            }

            foreach (Type t in types)
            {
                if (typeof(Component).IsAssignableFrom(t))
                {
                    this.ExclusionSet.Add(t);
                }
            }

            return this;
        }

        public Aspect One(Type type, params Type[] types)
        {
            if (typeof(Component).IsAssignableFrom(type))
            {
                this.OneSet.Add(type);
            }

            foreach (Type t in types)
            {
                if (typeof(Component).IsAssignableFrom(t))
                {
                    this.OneSet.Add(t);
                }
            }

            return this;
        }

        public Aspect All(Type type, params Type[] types)
        {
            if (typeof(Component).IsAssignableFrom(type))
            {
                this.AllSet.Add(type);
            }

            foreach (Type t in types)
            {
                if (typeof(Component).IsAssignableFrom(t))
                {
                    this.AllSet.Add(t);
                }
            }

            return this;
        }

        public bool Equals(Aspect other)
        {
            // First two lines are just optimizations
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (this.AllSet.Equals(other.AllSet) && this.OneSet.Equals(other.OneSet) && this.ExclusionSet.Equals(other.ExclusionSet)) 
                return true;
            else 
                return false;
        }

        public override bool Equals(object obj)
        {
            // First two lines are just optimizations
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            // Actually check the type, should not throw exception from Equals override
            if (obj.GetType() != this.GetType()) return false;

            // Call the implementation from IEquatable
            return Equals((Aspect)obj);
        }

        public override int GetHashCode()
        {
            int hash = 13;

            hash = (hash * 7) + AllSet.GetHashCode();
            hash = (hash * 7) + OneSet.GetHashCode();
            hash = (hash * 7) + ExclusionSet.GetHashCode();

            return hash;
        }
    }
}
