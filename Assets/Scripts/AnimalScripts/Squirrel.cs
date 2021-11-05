using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorSim.BehaviorTree;

namespace BehaviorSim
{
    public class Squirrel : Animal
    {
        public Squirrel() : base(100, 100, 100)
        {
        }

        protected void Start()
        {
            _speed = 2.5f;
            _angularSpeed = 45.0f;
            _direction = gameObject.transform.forward;

            _food = maxFood / 3.0f;
            _water = maxWater / 2.0f;
            _health = maxHealth;

            _sightFOV = 360.0f;
            _sightRadius = 50.0f;

            _type = AnimalType.SQUIRREL;

            _behaviorTree = new SquirrelTree();
            _behaviorTree.SetOwner(gameObject);
        }
    }

}

