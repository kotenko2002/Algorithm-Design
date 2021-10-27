using System.Collections.Generic;
using AD_Laba2_Btree.Interfaces;
using System.Text;
using System;
using AD_Laba2_Btree.Interfaces_and_T;

namespace AD_Laba2_Btree
{
    public class BNode : IBNode
    {
        public int KeyCount;
        public int MinDegree;
        public readonly int[] Keys = new int[Degree.T * 2 - 1];
        public readonly BNode[] Children = new BNode[Degree.T * 2]; 
        public bool IsLeaf;

        public void SplitChild(int i, BNode y)
        {
            var z = new BNode()
            {
                MinDegree = y.MinDegree,
                IsLeaf = y.IsLeaf,
                KeyCount = this.MinDegree - 1
            };

            for (var j = 0; j < this.MinDegree - 1; j++)
                z.Keys[j] = y.Keys[j + this.MinDegree];

            if (!y.IsLeaf)
            {
                for (var j = 0; j < this.MinDegree; j++)
                    z.Children[j] = y.Children[j + this.MinDegree];
            }
            y.KeyCount = this.MinDegree - 1;

            for (var j = this.KeyCount; j >= i; j--)
                this.Children[j + 1] = this.Children[j];
            this.Children[i + 1] = z;

            for (var j = this.KeyCount - 1; j >= i; j--)
                this.Keys[j + 1] = this.Keys[j];
            this.Keys[i] = y.Keys[this.MinDegree - 1];
            this.KeyCount++;
        }

        public void Insert(int k)
        {
            var i = this.KeyCount - 1;
            if (this.IsLeaf)
            {
                while (i >= 0 && this.Keys[i] > k)
                {
                    this.Keys[i + 1] = this.Keys[i];
                    i--;
                }

                this.Keys[i + 1] = k;
                this.KeyCount++;
            }
            else
            {
                while (i >= 0 && this.Keys[i] > k)
                    i--;
                if (this.Children[i + 1].KeyCount == 2 * this.MinDegree - 1)
                {
                    this.SplitChild(i + 1, this.Children[i + 1]);
                    if (Keys[i + 1] < k)
                        i++;
                }
                Children[i + 1].Insert(k);
            }
        }

        public BNode Search(int k)
        {
            var i = 0;
            while (i < this.KeyCount && k > this.Keys[i])
                i++;

            if (Keys[i] == k)
                return this;

            return IsLeaf ? null : this.Children[i].Search(k);
        }

        public void Traverse()
        {
            var i = 0;
            for (i = 0; i < this.KeyCount; i++)
            {
                if (!this.IsLeaf)
                    this.Children[i].Traverse();
                Console.Write(this.Keys[i] + "    ");
            }
            if (!this.IsLeaf)
                this.Children[i].Traverse();
        }
    }
}
