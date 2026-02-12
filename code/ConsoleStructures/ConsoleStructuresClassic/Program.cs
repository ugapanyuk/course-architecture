namespace ConsoleStructuresClassic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Command line args:");
            foreach(string arg in args) 
            { 
                Console.WriteLine(arg);
            }

            EampleConditions();
            ExampleLoops();
            ExampleMatrices();
            ExampleNull();
        }

        static void EampleConditions()
        {
            object[] data = { 15, "Hello", -5, null, "World", 42, 0, "C#", -10 };

            Console.WriteLine("=== Анализ с помощью if ===");
            AnalyzeWithIf(data);

            Console.WriteLine("\n=== Анализ с помощью switch ===");
            AnalyzeWithSwitch(data);

            Console.WriteLine("\n=== Категории (switch expression) ===");
            foreach (var item in data)
            {
                string category = GetCategory(item);
                Console.WriteLine($"{item ?? "null"} -> {category}");
            }

            // В решении используются вложенные методы (функции)
            // Метод 1: Использование if с pattern matching
            static void AnalyzeWithIf(object[] array)
            {
                foreach (object obj in array)
                {
                    if (obj is null)
                    {
                        Console.WriteLine("Пустое значение");
                    }
                    else if (obj is int number && number > 0)
                    {
                        Console.WriteLine($"Положительное число: {number}");
                    }
                    else if (obj is int num && num == 0)
                    {
                        Console.WriteLine($"Ноль: {num}");
                    }
                    else if (obj is int negative && negative < 0)
                    {
                        Console.WriteLine($"Отрицательное число: {negative}");
                    }
                    else if (obj is string str)
                    {
                        Console.WriteLine($"Строка: {str}");
                    }
                }
            }

            // Метод 2: Использование switch с pattern matching и guards
            static void AnalyzeWithSwitch(object[] array)
            {
                foreach (object obj in array)
                {
                    switch (obj)
                    {
                        case null:
                            Console.WriteLine("Пустое значение");
                            break;

                        case int i when i > 0:
                            Console.WriteLine($"Положительное число: {i}");
                            break;

                        case int i when i == 0:
                            Console.WriteLine($"Ноль: {i}");
                            break;

                        case int i when i < 0:
                            Console.WriteLine($"Отрицательное число: {i}");
                            break;

                        case string s:
                            Console.WriteLine($"Строка: {s}");
                            break;
                    }
                }
            }

            // Метод 3: Использование switch expression
            static string GetCategory(object obj)
            {
                return obj switch
                {
                    null => "NULL",
                    int i when i > 0 => "ПОЛОЖИТЕЛЬНОЕ",
                    int i when i == 0 => "НОЛЬ",
                    int i when i < 0 => "ОТРИЦАТЕЛЬНОЕ",
                    string s => $"СТРОКА_ДЛИНЫ_{s.Length}",
                    _ => "НЕИЗВЕСТНЫЙ_ТИП"
                };
            }
        }


        static void ExampleLoops()
        {
            int[] numbers = { 5, -3, 8, 12, -7, 0, 15, -2, 4, 20 };

            Console.WriteLine("Исходный массив:");
            Console.WriteLine(string.Join(", ", numbers));
            Console.WriteLine();

            // 1. Вывод элементов на четных индексах (for)
            Console.WriteLine("=== 1. Элементы на четных индексах (цикл for) ===");
            PrintEvenIndices(numbers);
            Console.WriteLine();

            // 2. Сумма всех элементов (foreach)
            Console.WriteLine("=== 2. Сумма всех элементов (цикл foreach) ===");
            int sum = CalculateSum(numbers);
            Console.WriteLine($"Сумма: {sum}");
            Console.WriteLine();

            // 3. Первое число больше заданного (while с break)
            Console.WriteLine("=== 3. Первое число > 10 (цикл while с break) ===");
            int result = FindFirstGreaterThan(numbers, 10);
            if (result != -1)
                Console.WriteLine($"Найдено: {result}");
            else
                Console.WriteLine("Не найдено");
            Console.WriteLine();

            // 4. Вывод пока сумма не превысит значение (do-while)
            Console.WriteLine("=== 4. Вывод пока сумма <= 15 (цикл do-while) ===");
            PrintUntilSumExceeds(numbers, 15);
            Console.WriteLine();

            // 5. Только положительные числа (foreach с continue)
            Console.WriteLine("=== 5. Только положительные (foreach с continue) ===");
            PrintPositiveOnly(numbers);

            // Метод 1: Цикл for - элементы на четных индексах
            static void PrintEvenIndices(int[] array)
            {
                Console.Write("Элементы на индексах 0, 2, 4, ...: ");
                for (int i = 0; i < array.Length; i += 2)
                {
                    Console.Write(array[i] + " ");
                }
                Console.WriteLine();
            }

            // Метод 2: Цикл foreach - сумма элементов
            static int CalculateSum(int[] array)
            {
                int sum = 0;
                foreach (int num in array)
                {
                    sum += num;
                }
                return sum;
            }

            // Метод 3: Цикл while с break - поиск первого элемента
            static int FindFirstGreaterThan(int[] array, int threshold)
            {
                int index = 0;
                while (index < array.Length)
                {
                    if (array[index] > threshold)
                    {
                        return array[index];  // или можно использовать break
                    }
                    index++;
                }
                return -1;  // не найдено
            }

            // Метод 4: Цикл do-while - вывод до превышения суммы
            static void PrintUntilSumExceeds(int[] array, int limit)
            {
                int index = 0;
                int currentSum = 0;

                do
                {
                    currentSum += array[index];
                    Console.WriteLine($"array[{index}] = {array[index]}, накопленная сумма = {currentSum}");
                    index++;

                    if (currentSum > limit)
                    {
                        Console.WriteLine($"Сумма превысила {limit}, выход из цикла");
                        break;
                    }

                } while (index < array.Length);
            }

            // Метод 5: Цикл foreach с continue - только положительные
            static void PrintPositiveOnly(int[] array)
            {
                Console.Write("Положительные числа: ");
                foreach (int num in array)
                {
                    if (num <= 0)
                    {
                        continue;  // пропускаем отрицательные и ноль
                    }
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }
        }


        static void ExampleMatrices()
        {
            // 1. Симметричная матрица
            Console.WriteLine("=== Матрица 1: Симметричная ===");
            int[,] matrix1 = {
            { 1, 2, 3 },
            { 2, 5, 6 },
            { 3, 6, 9 }
            };
            PrintMatrix(matrix1);
            bool result1 = IsSymmetric(matrix1);
            Console.WriteLine($"Симметрична: {(result1 ? "Да" : "Нет")}");
            Console.WriteLine();

            // 2. Несимметричная матрица
            Console.WriteLine("=== Матрица 2: Несимметричная ===");
            int[,] matrix2 = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
            };
            PrintMatrix(matrix2);
            bool result2 = IsSymmetric(matrix2);
            Console.WriteLine($"Симметрична: {(result2 ? "Да" : "Нет")}");
            Console.WriteLine();

            // 3. Неквадратная матрица
            Console.WriteLine("=== Матрица 3: Неквадратная (2x3) ===");
            int[,] matrix3 = {
            { 1, 2, 3 },
            { 4, 5, 6 }
            };
            PrintMatrix(matrix3);
            bool result3 = IsSymmetric(matrix3);
            Console.WriteLine($"Симметрична: {(result3 ? "Да" : "Нет")}");
            Console.WriteLine();

            // 4. Единичная матрица
            Console.WriteLine("=== Матрица 4: Единичная матрица 4x4 ===");
            int[,] matrix4 = {
            { 1, 0, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 }
            };
            PrintMatrix(matrix4);
            bool result4 = IsSymmetric(matrix4);
            Console.WriteLine($"Симметрична: {(result4 ? "Да" : "Нет")}");

            // Метод проверки симметричности матрицы
            static bool IsSymmetric(int[,] matrix)
            {
                int rows = matrix.GetLength(0);  // количество строк
                int cols = matrix.GetLength(1);  // количество столбцов

                // Проверка: матрица должна быть квадратной
                if (rows != cols)
                {
                    return false;
                }

                // Проверка симметричности
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        // Проверяем, что элемент [i,j] равен элементу [j,i]
                        if (matrix[i, j] != matrix[j, i])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            // Метод вывода матрицы
            static void PrintMatrix(int[,] matrix)
            {
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Console.Write($"{matrix[i, j],4}");
                    }
                    Console.WriteLine();
                }
            }
        }


        static void ExampleNull()
        {
            // Исходные данные
            int?[] numbers = { 10, null, 25, null, 30, 15, null };

            // 1. Вывод исходного массива
            Console.WriteLine("=== 1. Исходный массив ===");
            for (int i = 0; i < numbers.Length; i++)
            {
                // Используем ?. для безопасного вызова ToString()
                string display = numbers[i]?.ToString() ?? "null";
                Console.WriteLine($"numbers[{i}] = {display}");
            }
            Console.WriteLine();

            // 2. Подсчет null-значений (оператор is null)
            Console.WriteLine("=== 2. Подсчет null-значений (is null) ===");
            int nullCount = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] is null)
                {
                    nullCount++;
                }
            }
            Console.WriteLine($"Количество null-значений: {nullCount}");
            Console.WriteLine();

            // 3. Подсчет не-null значений (оператор is not null)
            Console.WriteLine("=== 3. Подсчет не-null значений (is not null) ===");
            int notNullCount = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] is not null)
                {
                    notNullCount++;
                }
            }
            Console.WriteLine($"Количество не-null значений: {notNullCount}");
            Console.WriteLine();

            // 4. Сумма ненулевых чисел (HasValue и Value)
            Console.WriteLine("=== 4. Сумма ненулевых чисел (HasValue и Value) ===");
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].HasValue)
                {
                    sum += numbers[i].Value;
                    Console.WriteLine($"Добавляем numbers[{i}] = {numbers[i].Value}, текущая сумма = {sum}");
                }
            }
            Console.WriteLine($"Итоговая сумма: {sum}");
            Console.WriteLine();

            // 5. Замена null на -1 (оператор ??)
            Console.WriteLine("=== 5. Создание массива с заменой null на -1 (оператор ??) ===");
            int[] numbersWithDefault = new int[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbersWithDefault[i] = numbers[i] ?? -1;
                Console.WriteLine($"numbersWithDefault[{i}] = {numbersWithDefault[i]}");
            }
            Console.WriteLine();

            // 6. Длина строкового представления (оператор ?.)
            Console.WriteLine("=== 6. Длина строкового представления (оператор ?.) ===");
            for (int i = 0; i < numbers.Length; i++)
            {
                // numbers[i]?.ToString() вернет null если numbers[i] == null
                // затем ?.Length вернет null если ToString() вернул null
                // затем ?? 0 заменит null на 0
                int? length = numbers[i]?.ToString()?.Length ?? 0;
                Console.WriteLine($"Длина представления numbers[{i}] = {length}");
            }
            Console.WriteLine();

            // 7. Замена null на 0 в исходном массиве (оператор ??=)
            Console.WriteLine("=== 7. Замена null на 0 в исходном массиве (оператор ??=) ===");
            Console.WriteLine("До замены: null-значений = " + nullCount);
            for (int i = 0; i < numbers.Length; i++)
            {
                // Присваивание произойдет только если numbers[i] == null
                numbers[i] ??= 0;
            }

            // Проверка что null-значений больше нет
            int nullCountAfter = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] is null)
                {
                    nullCountAfter++;
                }
            }
            Console.WriteLine("После замены: null-значений = " + nullCountAfter);
            Console.WriteLine();

            // 8. Вывод итогового массива
            Console.WriteLine("=== 8. Итоговый массив после замены ===");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine($"numbers[{i}] = {numbers[i]}");
            }
            Console.WriteLine();

            // Дополнительная демонстрация
            Console.WriteLine("=== ДОПОЛНИТЕЛЬНО: Сравнение способов проверки ===");
            int? testValue = null;

            // Старый стиль
            if (testValue == null)
            {
                Console.WriteLine("testValue == null (старый стиль): true");
            }

            // Современный стиль
            if (testValue is null)
            {
                Console.WriteLine("testValue is null (современный стиль): true");
            }

            testValue = 42;
            if (testValue is not null)
            {
                Console.WriteLine($"testValue is not null: true, значение = {testValue}");
            }
        }
    }
}
