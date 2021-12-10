using UnityEngine;

namespace BehaviorSim.BehaviorTree
{
    public class FoxTree : Tree
    {
        public FoxTree() : base()
        {

            
            SequenceNode chaseAnimalRoot = new SequenceNode("Chase Animal Subtree");
            chaseAnimalRoot.AddChild(new SneakToAnimalNode());
            chaseAnimalRoot.AddChild(new ChaseAnimalNode());
            chaseAnimalRoot.AddChild(new AttackAnimalNode());
            

            SequenceNode hungerRoot = new SequenceNode("Hunger Subtree");
            hungerRoot.AddChild(new FoodLowNode(0.6f));
            hungerRoot.AddChild(new IsPreyNearbyNode());
            hungerRoot.AddChild(chaseAnimalRoot);
            hungerRoot.AddChild(new EatFoodNode());

            _root = new SelectorNode("Root");
            ControlNode root = (ControlNode)_root;
            root.AddChild(hungerRoot);
            root.AddChild(new WanderNode());

            GameObject canvas = GameObject.Find("Canvas");
            UIManager uiManager = canvas.GetComponent<UIManager>();

            GameObject uiTree = uiManager.GetUITree(AnimalType.FOX);
            SetUIPointers(uiTree);
        }
    }
}