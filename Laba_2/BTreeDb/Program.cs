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
