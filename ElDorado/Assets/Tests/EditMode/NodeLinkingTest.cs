// using System.Collections;
// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;

// public class NodeLinkingTest
// {
//     public NodeModel node1;
//     public NodeModel node2;
//     public NodeModel node3;

//     public NodeController nodeController1;
//     public NodeController nodeController2;
//     public NodeController nodeController3;

//     public GameObject node1GameObject;
//     public GameObject node2GameObject;
//     public GameObject node3GameObject;

//     public void InitializeAttributes(){
//         node1GameObject = new GameObject();
//         node2GameObject = new GameObject();
//         node3GameObject = new GameObject();

//         node1 = node1GameObject.AddComponent<NodeModel>();
//         node2 = node2GameObject.AddComponent<NodeModel>();
//         node3 = node3GameObject.AddComponent<NodeModel>();

//         node2.adjacientNodesList.Add(node1);
//         node2.adjacientNodesList.Add(node3);

//         nodeController1 = node1GameObject.AddComponent<NodeController>();
//         nodeController2 = node2GameObject.AddComponent<NodeController>();
//         nodeController3 = node3GameObject.AddComponent<NodeController>();

//         nodeController1.thisNode = node1;
//         nodeController2.thisNode = node2;
//         nodeController3.thisNode = node3;
//     }

//     public void LinkNodes(){
//         nodeController1.LinkNodes();
//         nodeController2.LinkNodes();
//         nodeController3.LinkNodes();
//     }    

//     // A Test behaves as an ordinary method
//     [Test]
//     public void CheckNodesAreCorrecltyLinked()
//     {
//         InitializeAttributes();
//         LinkNodes();
//         // Assert.Contains(node1,nodeController3.thisNode.adjacientNodesList); //this one will return false
//         Assert.Contains(node1,nodeController2.thisNode.adjacientNodesList);

//         Assert.Contains(node2,nodeController1.thisNode.adjacientNodesList);
//         Assert.Contains(node2,nodeController3.thisNode.adjacientNodesList);

//         Assert.Contains(node3,nodeController2.thisNode.adjacientNodesList);
//     }
// }
