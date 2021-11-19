using UnityEngine;
using BehaviorSim.BehaviorTree;

namespace BehaviorSim
{
    public class Fox : Animal
    {
        public override AnimalType Type => AnimalType.FOX;
        public override AnimalStats Stats => AnimalStatsTypes.FoxStats;
        public override AnimalFoodChainData FoodChainData => AnimalFoodChainDataTypes.FoxFoodChainData;

        protected void Start()
        {
            _direction = gameObject.transform.forward;

            _food = Random.Range(0.33f * Stats.MaxFood, 0.66f * Stats.MaxFood);
            _water = Random.Range(0.5f * Stats.MaxFood, 0.8f * Stats.MaxFood);
            _health = Stats.MaxHealth;

            _behaviorTree = new FoxTree();
            _behaviorTree.SetOwner(gameObject);

            _debugArrowObject = transform.GetChild(1).gameObject;
            _debugArrowObject.SetActive(false);
        }
    }

}

