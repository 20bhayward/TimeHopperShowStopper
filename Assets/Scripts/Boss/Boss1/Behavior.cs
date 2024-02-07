using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Node rootNode;
    void Start()
    {
        // Create the nodes
        Node moveNode = new MoveNode(gameObject);
        Node attackNode = new AttackNode(gameObject);
        Node fleeNode = new FleeNode(gameObject);

        // Create the tree
        // Behaviors are priortized in the order they are listed, logic as to whether they should *do* a leaf node behavior is should be in the leaf node itself
        // For example, if the parameters for *Attack* aren't meant, attack returns false and it tries to flee, if false it tries to move
        rootNode = new SelectorNode(
            new List<Node>
            {
                attackNode,
                fleeNode,
                moveNode
            }
        );
    }

    // Update is called once per frame
    void Update()
    {
        // Execute the behavior tree, it does 1 of the leaf nodes on update
        rootNode.Execute();
    }


    // Base Node class
    public abstract class Node
    {
        public abstract bool Execute();
    }

    // Composite Nodes
    public class SelectorNode : Node
    {
        private List<Node> childNodes;

        public SelectorNode(List<Node> nodes)
        {
            childNodes = nodes;
        }

        public override bool Execute()
        {
            foreach (Node node in childNodes)
            {
                if (node.Execute())
                {
                    return true; // Succeeds if any child node succeeds
                }
            }
            return false; // Fails if all child nodes fail
        }
    }

    // Leaf Nodes
    public class MoveNode : Node
    {

        private GameObject gameObject;

        public MoveNode(GameObject obj)
        {
            gameObject = obj;
        }

        public override bool Execute()
        {
            Debug.Log("Moving...");
            // Implement movement logic here


            gameObject.GetComponent<Renderer>().material.color = Color.green;
            // Animations would replace the above line 


            return true; // Return true for successful execution
        }
    }

    public class AttackNode : Node
    {


        private GameObject gameObject;

        public AttackNode(GameObject obj)
        {
            gameObject = obj;
        }

        public override bool Execute()
        {
            Debug.Log("Attacking...");
            // Implement attack logic here
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            // Animations would replace the above line
            // 
            return true; // Return true for successful execution
        }
    }

    public class FleeNode : Node
    {

        private GameObject gameObject;

        public FleeNode(GameObject obj)
        {
            gameObject = obj;
        }

        public override bool Execute()
        {
            Debug.Log("Fleeing...");
            // Implement flee logic here



            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            // Animations would replace the above line 

            return true; // Return true for successful execution
        }
    }
}
