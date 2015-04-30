using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepEngine;

namespace DeepSpace
{
    public class CPlanet : Component
    {
        public PlanetClass Class { get; set; }
        public PlanetAtmosphere Atmosphere { get; set; }
        public float MaxPop { get; set; }
        public float CurPop { get; set; }
        public float PopGrowthRate { get; set; }

        public CPlanet()
            : base(true, false)
        {
        }
    }

    public enum PlanetClass
    {
        Dead,
        Harsh,
        Habitable,
        Verdant,
        Lush
    }

    public enum PlanetAtmosphere
    {
        None,
        Oxygen,
        Nitrogen
    }
}
