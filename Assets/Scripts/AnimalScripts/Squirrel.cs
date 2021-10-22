using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorSim.BehaviorTree;

namespace BehaviorSim
{
    public class Squirrel : Animal
    {
        protected void Start()
        {
            _speed = 2.5f;
            _angularSpeed = 45.0f;
            _direction = gameObject.transform.forward;

            _hunger = MaxHunger / 3.0f;
            _thirst = MaxThirst / 2.0f;
            _health = MaxHealth;

            _sightFOV = 360.0f;
            _sightRadius = 50.0f;

            _type = AnimalType.SQUIRREL;
            _foodValue = 0.4f * MaxHunger;

            SequenceNode hungerRoot = new SequenceNode("Hunger Subtree");
            hungerRoot.AddChild(new HungerLowNode());
            hungerRoot.AddChild(new FindNearbyFoodNode());
            hungerRoot.AddChild(new GoToFoodNode());
            hungerRoot.AddChild(new EatFoodNode());

            SelectorNode root = new SelectorNode("root");
            root.AddChild(hungerRoot);
            root.AddChild(new WanderNode());
            _behaviorTree = new BehaviorTree.Tree(root);
            _behaviorTree.SetOwner(gameObject);
        }
    }

}

