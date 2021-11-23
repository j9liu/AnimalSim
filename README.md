# Animal Simulation with Behavior Trees

**University of Pennsylvania, Senior Design Project (in-progress)** by Janine Liu

[LinkedIn](https://www.linkedin.com/in/liujanine/) | [Website](https://www.janineliu.com/)

## Overview

This project explores the use of behavior trees and purposes them for a simple animal simulation in Unity. It is inspired by the gameplay and aesthetic of _[SimAnimals](https://en.wikipedia.org/wiki/SimAnimals)_, which is an animal-focused version of _The Sims_ with a woodland setting. In summary, this project...
- provides generic `Node` and `Tree` definitions (see [Assets/Scripts/BehaviorTree](https://github.com/j9liu/AnimalSim/tree/main/Assets/Scripts/BehaviorTree)) as an basic framework for behavior trees.
- adapts these generic classes for animal behavior, which is done by...
  -  defining `Condition`s and `Action`s that can be reused by multiple animals, such as the `Wander` and `FoodLow` nodes.
  -  building behavior trees for each animal species based on these custom nodes.
- uses the resulting behavior trees in a simulation of squirrels and foxes, allowing for interactions between the species as well as the environment.
