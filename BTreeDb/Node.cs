using System;
using System.Linq;

namespace BTreeDb
{
    public class Node
    {
        public Data[] Elements { get; set; } 
        public Node[] Children { get; set; } 
        public int KeyCount; 
        public int MinDegree;
        public bool IsLeaf;

        

        public void Insert(int key, string value)
        {
            if (Elements.Any(element => element?.Key == key))
            {
                foreach (var element in Elements)
                {
                    if (element.Key != key) continue;
                    Console.WriteLine("У данного ключа уже есть значение, желаете заменить?(Нажмите пробел)");
                    Console.ReadLine();
                    element.Value = value;
                    Console.WriteLine("Значение заменино успешно:)");
                    break;
                }

                return;
            }

            var i = KeyCount - 1;
            if (IsLeaf)
            {
                while (i >= 0 && Elements[i].Key > key)
                {
                    Elements[i + 1] = Elements[i];
                    i--;
                }

                Elements[i + 1] = new Data()
                {
                    Key = key,
                    Value = value
                };
                KeyCount++;
            }
            else
            {
                while (i >= 0 && Elements[i].Key > key)
                    i--;
                if (Children[i + 1].KeyCount == 2 * MinDegree - 1)
                {
                    SplitChild(i + 1, Children[i + 1]);
                    if (Elements[i + 1].Key < key)
                        i++;
                }

                Children[i + 1].Insert(key, value);
            }
        }
        public void SplitChild(int i, Node y)
        {
            var z = new Node()
            {
                MinDegree = y.MinDegree,
                IsLeaf = y.IsLeaf,
                KeyCount = MinDegree - 1,
                Elements = new Data[Degree.T * 2 - 1],
                Children = new Node[Degree.T * 2]
            };

            for (var j = 0; j < MinDegree - 1; j++)
                z.Elements[j] = y.Elements[j + MinDegree];

            if (!y.IsLeaf)
            {
                for (var j = 0; j < MinDegree; j++)
                    z.Children[j] = y.Children[j + MinDegree];
            }

            y.KeyCount = MinDegree - 1;

            for (var j = KeyCount; j >= i; j--)
                Children[j + 1] = Children[j];
            Children[i + 1] = z;

            for (var j = KeyCount - 1; j >= i; j--)
                Elements[j + 1] = Elements[j];
            Elements[i] = y.Elements[MinDegree - 1];
            KeyCount++;
        }
        public void Remove(int k)
        {
            var index = FindIndexOfKey(k);
            if (index < KeyCount && Elements[index].Key == k)
            {
                if (IsLeaf)
                    RemoveFromLeaf(index);
                else
                    RemoveFromNonLeaf(index);
                Console.WriteLine("Ключ " + k + " вместе со значением улетели в помойку :)");
            }
            else
            {
                if (IsLeaf)
                {
                    Console.WriteLine("Шо ты мне втираешь? Такого ключа не существует и точка!");
                    return;
                }

                var flag = index == KeyCount;

                if (flag && index > KeyCount)
                    Children[index - 1].Remove(k);
                else
                    Children[index].Remove(k);
            }
        }
        private void RemoveFromNonLeaf(int index)
        {
            var k = Elements[index];
            if (Children[index].KeyCount >= MinDegree)
            {
                var predecessors = GetPredecessors(index);
                Elements[index] = predecessors;
                Children[index].Remove(predecessors.Key);
            }
            else if (Children[index + 1].KeyCount >= MinDegree)
            {
                var successor = GetSuccessor(index);
                Elements[index] = successor;
                Children[index + 1].Remove(successor.Key);
            }
            else
            {
                Merge(index);
                Children[index].Remove(k.Key);
            }
        }
        private Data GetPredecessors(int index)
        {
            var current = Children[index];
            while (!current.IsLeaf)
                current = current.Children[current.KeyCount];
            return current.Elements[current.KeyCount];
        }
        private void RemoveFromLeaf(int index)
        {
            for (var i = index + 1; i < KeyCount; i++)
                Elements[i - 1] = Elements[i];
            KeyCount--;
        }
        private Data GetSuccessor(int index)
        {
            var current = Children[index + 1];
            while (!current.IsLeaf)
                current = current.Children[0];
            return current.Elements[0];
        }
        private void Merge(int index)
        {
            Children[index].Elements[MinDegree - 1] = Elements[index];

            for (var i = 0; i < Children[index + 1].KeyCount; i++)
                Children[index].Elements[i + MinDegree] = Children[index + 1].Elements[i];

            if (!Children[index].IsLeaf)
                for (var i = 0; i < Children[index + 1].KeyCount; i++)
                    Children[index].Children[i + MinDegree] = Children[index + 1].Children[i];
            for (var i = index + 1; i < KeyCount; i++)
                Elements[i - 1] = Elements[i];
            for (var i = index + 2; i <= KeyCount; i++)
                Children[i - 1] = Children[i];
            Children[index].KeyCount += Children[index + 1].KeyCount + 1;
            KeyCount--;
        }
        public void Traverse()
        {
            var i = 0;
            for (i = 0; i < KeyCount; i++)
            {
                if (!IsLeaf)
                    Children[i].Traverse();
                Console.Write($"{Elements[i].Key}|{Elements[i].Value}\t");
            }

            if (!IsLeaf)
                Children[i].Traverse();
        }
        public Node Search(int k)
        {
            var i = 0;
            while (i < KeyCount - 1 && k > Elements[i].Key)
                i++;

            if (Elements[i].Key == k)
                return this;

            return IsLeaf ? null : Children[i].Search(k);
        }
        private int FindIndexOfKey(int k)
        {
            var index = 0;
            while (index < KeyCount && Elements[index].Key < k)
                index++;
            return index;
        } 
    }
}