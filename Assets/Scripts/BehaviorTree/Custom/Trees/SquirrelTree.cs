using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorSim.BehaviorTree {
    public class SquirrelTree : Tree
    {
        public SquirrelTree() : base()
        {
            SequenceNode hungerRoot = new SequenceNode("Hunger Subtree");
            hungerRoot.AddChild(new FoodLowNode(0.6f));
            hungerRoot.AddChild(new FindNearbyFoodNode());
            hungerRoot.AddChild(new GoToFoodNode());
            hungerRoot.AddChild(new EatFoodNode());

            _root = new SelectorNode("Root");
            _root.AddChild(hungerRoot);
            _root.AddChild(new WanderNode());

            GameObject canvas = GameObject.Find("Canvas");
            UIManager uiManager = canvas.GetComponent<UIManager>();

            GameObject uiTree = uiManager.GetUITree(AnimalType.SQUIRREL);
            SetUIPointers(uiTree);
        }
    }
}