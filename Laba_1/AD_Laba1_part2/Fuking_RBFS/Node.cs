using System;
using System.Collections.Generic;
using System.Text;

namespace Fuking_RBFS
{
    public class Node
    {
        public int[,] Map { get; set; } 
        public Node parentNode { get; set; } 

        public List<Node> children = new List<Node>();

        public int Depth { get; set; } 

        public Node(int[,] Map, Node parentNode, int Depth)
        {
            this.Map = Map;
            this.parentNode = parentNode;
            this.Depth = Depth;
        }
    }
}
