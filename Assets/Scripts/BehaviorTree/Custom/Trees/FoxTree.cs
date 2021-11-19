using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class FoxTree : Tree
    {
        public FoxTree() : base()
        {
            SequenceNode hungerRoot = new SequenceNode("Hunger Subtree");
            hungerRoot.AddChild(new FoodLowNode(0.6f));
            hungerRoot.AddChild(new FindNearbyPreyNode());
            hungerRoot.AddChild(new GoToTargetAnimalNode());
            hungerRoot.AddChild(new AttackAnimalNode());
            hungerRoot.AddChild(new EatFoodNode());

            _root = new SelectorNode("Root");
            _root.AddChild(hungerRoot);
            _root.AddChild(new WanderNode());

            GameObject canvas = GameObject.Find("Canvas");
            UIManager uiManager = canvas.GetComponent<UIManager>();

            GameObject uiTree = uiManager.GetUITree(AnimalType.FOX);
            SetUIPointers(uiTree);
        }
    }
}