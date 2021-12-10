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

            MaxSpeed = 5.0f,
            AngularSpeed = 75.0f * Mathf.Deg2Rad,
            SightFOV = 270.0f,
            SightRadius = 50.0f,
            HearingRadius = 70.0f,
            HearingThreshold = 4.5f,
            CollisionRadius = 6.0f,
        };

        public static AnimalStats FoxStats = new AnimalStats
        {
            MaxFood = 150,
            MaxWater = 120,
            MaxHealth = 100,

            MaxSpeed = 7.5f,
            AngularSpeed = 90.0f * Mathf.Deg2Rad,
            SightFOV = 270.0f,
            SightRadius = 65.0f,
            HearingRadius = 80.0f,
            HearingThreshold = 3.5f,
            CollisionRadius = 8.0f,
        };
    }
}