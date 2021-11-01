using System;
using System.Linq;

namespace BTreeDb
{
    internal static class Program
    {

        private static void Main(string[] args)
        {
            BTree tree = new BTree();
            while (true)
            {
                Console.WriteLine("Что делаем?");
                Console.WriteLine("1 - Добавить элемент");
                Console.WriteLine("2 - Удалить элемент");
                Console.WriteLine("3 - Типа увидеть B-дерево");
                Console.WriteLine("4 - Найти элемент");
                Console.Write("Действее №: ");
                string chooseS = Console.ReadLine();
                int chooseI = 999;
                if(chooseS != "" && int.TryParse(chooseS, out chooseI))
                {
                    switch (chooseI)
                    {
                        case 1:
                            Console.Write("Введите номер ключа: ");
                            int keyI = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Введите информацию: ");
                            string date = Console.ReadLine();
                            tree.Insert(keyI, date);
                            break;
                        case 2:
                            Console.Write("Введите номер ключа: ");
                            int keyD = Convert.ToInt32(Console.ReadLine());
                            tree.Remove(keyD);
                            break;
                        case 3:
                            tree.Traverse();
                            break;
                        case 4:
                            Console.Write("Введите номер ключа: ");
                            int keyS = Convert.ToInt32(Console.ReadLine());
                            var buff = tree.Search(keyS);
                            if(buff == null)
                                Console.WriteLine("С прискорбием сообщаю, что значения с таким ключём не существую:(");
                            else
                            {
                                var result = buff.Elements.First(element => element.Key == keyS);
                                Console.WriteLine(result.Value);
                            }
                            break;
                        default:
                            Console.WriteLine("Ты не способен считатьот 1 до 4???");
                            break;
                    }
                    Console.ReadKey();
                    Console.Clear();
                }
                
            }
        }
    }
}
//BTree tree = new BTree(25);
//Random random = new Random();
//string name = "someone";
//for (int i = 0; i < 10000; i++)
//{
//    int num = random.Next(0, 9);
//    switch (num)
//    {
//        case 0:
//            name = "Leo";
//            break;
//        case 1:
//            name = "Henry";
//            break;
//        case 2:
//            name = "Lucas";
//            break;
//        case 3:
//            name = "Jackson";
//            break;
//        case 4:
//            name = "Matthew";
//            break;
//        case 5:
//            name = "Owen";
//            break;
//        case 6:
//            name = "Ezra";
//            break;
//        case 7:
//            name = "Luca";
//            break;
//        case 8:
//            name = "Myles";
//            break;
//        case 9:
//            name = "Zion";
//            break;
//        default:
//            Console.WriteLine("Error4ik");
//            break;
//    }
//    tree.Insert(i, name);
//    Console.WriteLine(i);
//}