using System;
using UnityEngine;


namespace BehaviorSim
{
    public partial class AnimalFoodChainData
    {
        private static int numberFoodSources = Enum.GetNames(typeof(FoodType)).Length;
        private static int numberAnimals = Enum.GetNames(typeof(AnimalType)).Length;
        public AnimalFoodChainData(AnimalType animal) {
            FoodSources = new bool[numberFoodSources];
            Predators = new bool[numberAnimals];

            switch (animal) {
                case AnimalType.SQUIRREL:
                    FoodSources[(int)FoodType.ACORN] = true;
                    Predators[(int)AnimalType.FOX] = true;
                    break;
                case AnimalType.FOX:
                    FoodSources[(int)FoodType.SQUIRREL] = true;
                    break;
            }
        }
    }

    public static class AnimalFoodChainDataTypes
    {
        public static AnimalFoodChainData SquirrelFoodChainData = new AnimalFoodChainData(AnimalType.SQUIRREL);
    }
}