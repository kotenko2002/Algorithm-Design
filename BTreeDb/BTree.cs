using System;
using System.IO;
using Newtonsoft.Json;

namespace BTreeDb
{
    public class BTree 
    {
        private const string file = "data.txt";
        private Node _root { get; set; }
        private int _degree { get; set; }

        public BTree(int degree)
        {
            _root = null;
            _degree = degree;
        }

        public BTree()
        {
            var data = Load();
            _root = data.root;
            _degree = data.degree;
        }
        private void Save()
        {
            var json = JsonConvert.SerializeObject(new SaveData()
            {
                root = _root,
                degree = _degree
            });
            using var fileStream = File.CreateText(file);
            fileStream.Write(json);
        }

        private static SaveData Load()
        {
            var data = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(file));
            return data ?? null;
        }
        
        public void Insert(int key, string value)
        {
            if (_root == null)
            {
                _root = new Node {MinDegree = _degree, Elements = new Data[Degree.T*2 - 1],Children = new Node[Degree.T*2], KeyCount = 0, IsLeaf = true};
                _root.Elements[0] = new Data()
                {
                    Key = key,
                    Value = value
                };
                _root.KeyCount++;
            }
            else
            {
                if (_root.KeyCount == 2 * _degree - 1)
                {
                    var s = new Node {MinDegree = _degree, KeyCount = 0, IsLeaf = false, Elements = new Data[Degree.T*2 - 1],Children = new Node[Degree.T*2]};
                    s.Children[0] = _root;
                    s.SplitChild(0, _root);
                    var i = 0;
                    if (s.Elements[0].Key < key)
                        i++;
                    s.Children[i].Insert(key, value);
                    _root = s;
                }
                else
                {
                    _root.Insert(key, value);
                }
            }
            Save();
        }

        public void Remove(int k)
        {
            if (_root==null)
            {
                Console.WriteLine("Дерево пустое...");
                return;
            }
            _root.Remove(k);
            if (_root.KeyCount == 0)
            {
                _root = _root.IsLeaf ? null : _root.Children[0];
            }
            Save();
        }
            
        public void Traverse()
        {
            _root?.Traverse();
        }

        public Node Search(int k)
        {
            return _root?.Search(k);
        }
    }
}