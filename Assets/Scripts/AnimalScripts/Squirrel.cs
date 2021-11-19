using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorSim.BehaviorTree;

namespace BehaviorSim
{
    public class Squirrel : Animal
    {
        public override AnimalType Type => AnimalType.SQUIRREL;
        public override AnimalStats Stats => AnimalStatsTypes.SquirrelStats;
        public override AnimalFoodChainData FoodChainData => AnimalFoodChainDataTypes.SquirrelFoodChainData;

        protected void Start()
        {
            _direction = gameObject.transform.forward;

            _food = Random.Range(0.33f * Stats.MaxFood, 0.66f * Stats.MaxFood);
            _water = Random.Range(0.5f * Stats.MaxFood, 0.8f * Stats.MaxFood);
            _health = Stats.MaxHealth;

            _behaviorTree = new SquirrelTree();
            _behaviorTree.SetOwner(gameObject);

            _debugArrowObject = transform.GetChild(1).gameObject;
            _debugArrowObject.SetActive(false);
        }
    }

}

