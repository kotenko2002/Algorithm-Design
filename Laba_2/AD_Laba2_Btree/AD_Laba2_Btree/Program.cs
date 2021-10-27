using AD_Laba2_Btree.Interfaces_and_T;
using System;

namespace AD_Laba2_Btree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BTree(Degree.T);
            for (var i = 0; i < 1000; i++)
            {
                var random = new Random();
                while (true)
                {
                    var input = random.Next(1, 10000);
                    tree.Insert(input);
                    break;
                }
            }

            tree.Traverse();
        }
    }
}
