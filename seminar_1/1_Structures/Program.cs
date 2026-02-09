using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // явное объявление строки
            string str = "строка1";

            // компилятор выводит тип
            // по значению правой части выражения
            var str2 = "тоже строка";

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Строковые интерполяции
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
             
            string City = "Moscow";
            string Country = "Russia";

            //Старый способ вывода с помощью конкатенации строк
            Console.WriteLine(City + ", " + Country);
            //Старый способ вывода с помощью string.Format
            Console.WriteLine(string.Format("{0}, {1}", City, Country));
            //Новый способ вывода с помощью строковой интерполяции
            Console.WriteLine($"{City}, {Country}");


            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Условия
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Условный оператор (в отличие от С++ в условии
            //используется логический тип)
            if (str == "строка1")
            {
                Console.WriteLine("if: str == \"строка1\"");
            }
            else
            {
                Console.WriteLine("if: str != \"строка1\"");
            }

            //Условная операция
            string result = (str == "строка1" ? "Да" : "Нет");
            Console.WriteLine($"?: Равна ли строка {str} строке 'строка1' - {result}");

            //Оператор switch
            string result2 = "";
            switch (str)
            {
                case "строка1":
                    result2 = "строка1";
                    break;

                case "строка2":
                case "строка3":
                    result2 = "строка2 или строка3";
                    break;

                default:
                    result2 = "другая строка";
                    break;
            }
            Console.WriteLine($"switch: {result2}");

            // Современный стиль (switch expression)
            // появился в C# 8
            string result21 = str switch
            {
                "строка1" => "строка1",
                "строка2" or "строка3" => "строка2 или строка3",  // логический or
                _ => "другая строка"  // _ = default
            };

            // Более сложный пример с типами
            int? obj = 3;
            string description = obj switch
            {
                int i => $"Целое число: {i}",
                null => "Null",
            };

            // С условиями (guards)
            var number = 5;
            string category = number switch
            {
                < 0 => "Отрицательное",
                0 => "Ноль",
                > 0 and <= 10 => "Малое положительное",
                > 10 => "Большое положительное"
            };
            Console.WriteLine($"{result21} {obj} {number}");

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Циклы
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Цикл for
            Console.Write("\nЦикл for: ");
            for (int i = 0; i < 3; i++)
                Console.Write(i);

            //Цикл foreach
            Console.Write("\nЦикл foreach: ");
            int[] array1 = { 1, 2, 3 };
            foreach (int i2 in array1)
                Console.Write(i2);

            //Цикл while
            Console.Write("\nЦикл while: ");
            int i3 = 0;
            while (i3 < 3)
            {
                Console.Write(i3);
                i3++;
            }

            //Цикл do while
            Console.Write("\nЦикл do while: ");
            int i4 = 0;
            do
            {
                Console.Write(i4);
                i4++;
            } while (i4 < 3);

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Обработка исключений
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            Console.WriteLine("\n\nДеление на 0:");
            try
            {
                int num1 = 1;
                int num2 = 1;

                string zero = "0";
                int.TryParse(zero, out num2);

                int num3 = num1 / num2;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Попытка деления на 0");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Собственное исключение");
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Это сообщение выводится в блоке finally");
            }


            Console.WriteLine("\nСобственное исключение:");
            try
            {
                throw new Exception("!!! Новое исключение !!!");
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Попытка деления на 0");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Собственное исключение");
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Это сообщение выводится в блоке finally");
            }

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Константы
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            const int int_const = 333;

            //Ошибка
            //int_const = 1;

            Console.WriteLine("Константа {0}", int_const);

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Параметры функций
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //В С# по умолчанию аргументы обычных типов передаются по значению, а объектов по ссылке
            //Аргументы ref всегда передаются по ссылке
            //Аргументы out являются только выходными параметрами 

            string RefTest = "Значение до вызова функций";

            ParamByVal(RefTest);
            Console.WriteLine("\nВызов функции ParamByVal. Значение переменной: " + RefTest);

            ParamByRef(ref RefTest);
            Console.WriteLine("Вызов функции ParamByRef. Значение переменной: " + RefTest);

            int x = 2, x2, x3;
            ParamOut(x, out x2, out x3);
            Console.WriteLine("Вызов функции ParamOut. x={0}, x^2={1}, x^3={2}", x, x2, x3);

            //Объявление параметра x22 прямо в методе,
            //третий параметр не используется
            ParamOut(x, out int x22, out int _);

            //Переменное количество параметров
            ParamArray("Вывод параметров: ", 1, 2, 333);

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Работа с null
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Ошибка, типу int нельзя присвоить null
            //int nl = null;

            //Nullable value types
            //используют структуру Nullable<T>
            int? nl_number = 42;
            int? nl_nullNumber = null;

            if (nl_number.HasValue)
            {
                //присваивается значение int (42)
                int nl_value = nl_number.Value;
                Console.WriteLine(nl_value);
            }

            // Оператор ?? присваивает правое значение если левое null
            // Null-coalescing оператор ?? появился в C# 2
            int nl_result1 = nl_number ?? -1; // 42
            int nl_result2 = nl_nullNumber ?? -1; // -1
            Console.WriteLine($"{nl_number} {nl_nullNumber} {nl_result1} {nl_result2}");

            //Nullable reference types — это ссылочные типы
            //(например, string), которые явно помечены как допускающие null
            string? nl_str1 = "Строка1"; 
            string? nl_str2 = null;    // может быть null
            if(nl_str2 == null) Console.WriteLine(nl_str1);

            //Null-conditional оператор ?. появился в C# 6
            // Если nl_str2 == null, то возвращается null, ошибка не возникает
            int ? nl_length1 = nl_str1?.Length;
            int? nl_length2 = nl_str2?.Length;
            Console.WriteLine($"{nl_length1} {nl_length2}");

            // Null-coalescing assignment (присваивание) ??=
            // появилось начиная с C# 8
            // Присваивание сработает, только если nl_str2 == null
            nl_str2 ??= "!!!";
            Console.WriteLine(nl_str2);

            //Для проверки на null можно использовать выражения
            // старый стиль
            if (obj == null) { }
            // современный стиль начиная с C# 7
            if (obj is null) { }
            if (obj is not null) { }
        }

        /// <summary>
        /// Передача параметра по значению
        /// </summary>
        /// <param name="param"></param>
        static void ParamByVal(string param)
        {
            param = "Это значение НЕ будет передано в вызывающую функцию";
        }

        /// <summary>
        /// Передача параметра по ссылке
        /// </summary>
        /// <param name="param"></param>
        static void ParamByRef(ref string param)
        {
            param = "Это значение будет передано в вызывающую функцию";
        }

        /// <summary>
        /// Выходные параметры объявляются с помощью out
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x2"></param>
        /// <param name="x3"></param>
        static void ParamOut(int x, out int x2, out int x3)
        {
            x2 = x * x;
            x3 = x * x * x;
        }

        /// <summary>
        /// Переменное количество параметров задается с помощью params
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ArrayParams"></param>
        static void ParamArray(string str, params int[] ArrayParams)
        {
            Console.Write(str);
            foreach (int i in ArrayParams)
                Console.Write(" {0} ", i);
        }
    }
}
