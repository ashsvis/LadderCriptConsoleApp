using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderCriptConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sourceText = "meet me after the toga party";
            Console.WriteLine(sourceText);
            // переводим в верхний регистр, преобразуем в массив символов
            // и избавляемся от пробелов, преобразуем в массив из перечисления
            var sourceArray = sourceText.ToUpper().ToCharArray().Where(x => x != ' ').ToArray();
            Console.WriteLine(string.Join("", sourceArray));
            // создаем массив списков, размерность массива является ключом шифра
            var list = new List<char>[2];
            // создаем внутренние списки символов в массиве списков
            for (var i = 0; i < list.Length; i++)
                list[i] = new List<char>();
            // добавляем в списки символы из исходного массива,
            // по очереди, то в один, то в другой (то в третий, и так далее)
            for (var i = 0; i < sourceArray.Length; i++)
                list[i % list.Length].Add(sourceArray[i]);
            // для сцепления полученных массивов понадобится StringBuilder
            var sb = new StringBuilder();
            // добавляем в него полученные цепочки символов
            for (var i = 0; i < list.Length; i++)
                sb.Append(string.Join("", list[i]));
            Console.WriteLine(sb);
            var encodeText = sb.ToString();
            var encodeArray = encodeText.ToCharArray();
            // создаем массив списков, размерность массива является ключом шифра
            list = new List<char>[2];
            // создаем внутренние списки символов в массиве списков
            for (var i = 0; i < list.Length; i++)
            {
                // создаем внутренние списки символов в массиве списков
                list[i] = new List<char>();
                // получаем длину подмассива на основе размерности (ключа шифра)
                // округление в бОльшую сторону
                var len = (int)Math.Ceiling(encodeArray.Length * 1.0 / list.Length);
                // заполнение списков частями закодированного сообщения
                list[i].AddRange(encodeArray.Skip(i % list.Length * len).Take(len));
            }
            // для сцепления полученных массивов понадобится StringBuilder
            sb = new StringBuilder();
            // добавляем из всех списков ("параллельно") первые символы, потом вторые и т.д.
            for (var n = 0; n < list[0].Count; n++)
            {
                for (var i = 0; i < list.Length; i++)
                    // контроль длины списка, последний список может быть короче
                    if (n < list[i].Count)
                        sb.Append(list[i][n]);
            }
            // вывод результата декодировки
            Console.WriteLine(sb);
            Console.ReadKey();
        }
    }
}
