using System;

// ================= ОСНОВНОЙ КОД ПРОГРАММЫ =================

double a, b, c;
double[] roots = new double[4];
double[] roots_bubble = new double[4];
double[] roots_selection = new double[4];
int n;
string[] input;

Console.WriteLine("Решение биквадратного уравнения: ax^4 + bx^2 + c = 0");
Console.WriteLine();

// Ввод коэффициентов
Console.Write("Введите коэффициенты a, b, c через пробел: ");
input = Console.ReadLine().Split(' ');
a = double.Parse(input[0]);
b = double.Parse(input[1]);
c = double.Parse(input[2]);

Console.WriteLine();
Console.WriteLine("-----------------------------------------------");
Console.WriteLine("Уравнение: {0:F2}x^4 + {1:F2}x^2 + {2:F2} = 0", a, b, c);
Console.WriteLine("-----------------------------------------------");

// Решение уравнения
n = SolveBiquadratic(a, b, c, roots);

Console.WriteLine();
Console.WriteLine("КОРНИ ДО СОРТИРОВКИ:");
PrintArray(roots, n);

// Копируем массив для двух разных сортировок
CopyArray(roots, roots_bubble, n);
CopyArray(roots, roots_selection, n);

// Применяем обе сортировки
BubbleSort(roots_bubble, n);
SelectionSort(roots_selection, n);

// Выводим результаты
Console.WriteLine();
Console.WriteLine("===============================================");
Console.WriteLine("РЕЗУЛЬТАТЫ СОРТИРОВКИ:");
Console.WriteLine("===============================================");

Console.WriteLine();
Console.WriteLine("1. ПУЗЫРЬКОВАЯ СОРТИРОВКА (Bubble Sort):");
PrintArray(roots_bubble, n);

Console.WriteLine();
Console.WriteLine("2. СОРТИРОВКА ВЫБОРОМ (Selection Sort):");
PrintArray(roots_selection, n);

// Проверка идентичности результатов
Console.WriteLine();
Console.WriteLine("-----------------------------------------------");
if (ArraysEqual(roots_bubble, roots_selection, n))
{
    Console.WriteLine("Оба метода дают ОДИНАКОВЫЙ результат");
}
else
{
    Console.WriteLine("ВНИМАНИЕ: Результаты РАЗЛИЧАЮТСЯ!");
}

Console.WriteLine();
Console.WriteLine("===============================================");
Console.WriteLine("ТЕСТОВЫЕ ПРИМЕРЫ:");
Console.WriteLine("===============================================");
Console.WriteLine("1) a=1, b=-5,  c=4  → корни: -2, -1, 1, 2");
Console.WriteLine("2) a=1, b=-10, c=9  → корни: -3, -1, 1, 3");
Console.WriteLine("3) a=1, b=-13, c=36 → корни: -3, -2, 2, 3");
Console.WriteLine("4) a=1, b=-17, c=16 → корни: -4, -1, 1, 4");
Console.WriteLine("===============================================");

Console.WriteLine();
Console.Write("Нажмите любую клавишу для выхода...");
Console.ReadKey();


// ================= ФУНКЦИИ =================

/* Решение биквадратного уравнения ax^4 + bx^2 + c = 0
   Возвращает количество корней */
static int SolveBiquadratic(double a, double b, double c, double[] roots)
{
    // Замена y = x^2, получаем ay^2 + by + c = 0
    double D = b * b - 4 * a * c;
    double y1 = (-b + Math.Sqrt(D)) / (2 * a);
    double y2 = (-b - Math.Sqrt(D)) / (2 * a);

    // Находим x = ±√y для каждого y
    roots[0] = Math.Sqrt(y1);
    roots[1] = -Math.Sqrt(y1);
    roots[2] = Math.Sqrt(y2);
    roots[3] = -Math.Sqrt(y2);

    return 4;
}

/* Пузырьковая сортировка массива по возрастанию */
static void BubbleSort(double[] arr, int n)
{
    int i, j;
    double temp;

    for (i = 0; i < n - 1; i++)
    {
        for (j = 0; j < n - i - 1; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

/* Сортировка выбором - находим минимум и ставим на своё место */
static void SelectionSort(double[] arr, int n)
{
    int i, j, min_idx;
    double temp;

    // Для каждой позиции находим минимум в оставшейся части
    for (i = 0; i < n - 1; i++)
    {
        min_idx = i;  // предполагаем, что минимум на позиции i

        // Ищем минимум среди оставшихся элементов
        for (j = i + 1; j < n; j++)
        {
            if (arr[j] < arr[min_idx])
            {
                min_idx = j;
            }
        }

        // Меняем местами найденный минимум с элементом на позиции i
        temp = arr[i];
        arr[i] = arr[min_idx];
        arr[min_idx] = temp;
    }
}

/* Вывод массива */
static void PrintArray(double[] arr, int n)
{
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine("  x{0} = {1,7:F3}", i + 1, arr[i]);
    }
}

/* Копирование массива */
static void CopyArray(double[] src, double[] dst, int n)
{
    for (int i = 0; i < n; i++)
    {
        dst[i] = src[i];
    }
}

/* Проверка идентичности массивов */
static bool ArraysEqual(double[] arr1, double[] arr2, int n)
{
    for (int i = 0; i < n; i++)
    {
        if (Math.Abs(arr1[i] - arr2[i]) > 0.0001)
        {
            return false;
        }
    }
    return true;
}
