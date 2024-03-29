﻿using UnityEngine;


namespace BehaviorSim.BehaviorTree {
    public class SquirrelTree : Tree
    {
        public SquirrelTree() : base()
        {
            SelectorNode subroot = new SelectorNode("Subroot");
            DecoratorNode subrootDecorator = new TickUntilPredatorNearbyNode(subroot);

            SequenceNode hungerRoot = new SequenceNode("Hunger Subtree");
            hungerRoot.AddChild(new FoodLowNode(0.6f));
            hungerRoot.AddChild(new IsFoodNearbyNode());
            hungerRoot.AddChild(new GoToTargetFoodNode(3.0f));
            hungerRoot.AddChild(new EatFoodNode());

            ControlNode root = new SelectorNode("Root");
            subroot.AddChild(hungerRoot);

            ActionNode wander = new WanderNode();
            subroot.AddChild(wander);

            ActionNode runFromAnimal = new RunFromAnimalNode();
            DecoratorNode runFromAnimalDecorator = new TickUntilPredatorNotNearbyNode(runFromAnimal);

            root.AddChild(runFromAnimalDecorator);
            root.AddChild(subrootDecorator);
            _root = root;

            GameObject canvas = GameObject.Find("Canvas"); 
            UIManager uiManager = canvas.GetComponent<UIManager>();

            GameObject uiTree = uiManager.GetUITree(AnimalType.SQUIRREL);
            SetUIPointers(uiTree);
        }
    }
}