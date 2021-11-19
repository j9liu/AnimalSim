using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSim
{
    public static class AnimalStatsTypes
    {
        public static AnimalStats SquirrelStats = new AnimalStats
        {
            MaxFood = 100,
            MaxWater = 100,
            MaxHealth = 100,

            Speed = 3.2f,
            AngularSpeed = 75.0f * Mathf.Deg2Rad,
            SightFOV = 300.0f,
            SightRadius = 50.0f,
            CollisionRadius = 6.0f,
        };
    }
}