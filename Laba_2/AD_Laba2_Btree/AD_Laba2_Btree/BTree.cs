using System.Collections.Generic;
using AD_Laba2_Btree.Interfaces;
using System.Text;
using System;

namespace AD_Laba2_Btree
{
    public class BTree : IBTree
    {
        public BTree(int degree)
        {
            _root = null;
            _degree = degree;
        }

        private BNode _root;
        private readonly int _degree;

        public void Insert(int k)
        {
            if (_root == null)
                _root = new BNode { MinDegree = _degree, Keys = { [0] = k }, KeyCount = 1, IsLeaf = true };
            else
            {
                if (_root.Search(k) != null)
                    return;
                if (_root.KeyCount == 2 * _degree - 1)
                {
                    var s = new BNode { MinDegree = _degree, IsLeaf = false, Children = { [0] = _root } };
                    s.SplitChild(0, _root);
                    var i = 0;
                    if (s.Keys[0] < k)
                        i++;
                    s.Children[i].Insert(k);
                    _root = s;
                }
                else
                    _root.Insert(k);
            }
        }

        public void Traverse()
        {
            Console.WriteLine("Traversal has started!\n");
            _root?.Traverse();
            Console.WriteLine("\n\nTraversal has ended!");
        }
    }
}
