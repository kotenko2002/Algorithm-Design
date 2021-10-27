using System;
using System.Collections.Generic;
using System.Text;

namespace AD_Laba2_Btree.Interfaces
{
    public interface IBNode
    {
        public void SplitChild(int i, BNode y);
        public void Insert(int k);
        public BNode Search(int k);
        public void Traverse();
    }
}
