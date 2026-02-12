using System;

// ================= ОСНОВНОЙ КОД ПРОГРАММЫ =================

Console.WriteLine("=== АНАЛИЗ КВАЛИФИКАЦИИ ГРАН-ПРИ ===");
Console.WriteLine();

// Ввод количества участников
Console.Write("Введите количество участников: ");
int n = int.Parse(Console.ReadLine());
Console.WriteLine();

// Создание массивов
string[] teams = new string[n];
double[] avgSpeeds = new double[n];

// Ввод данных
InputData(teams, avgSpeeds, n);

// Вычисление статистики
Console.WriteLine("--- СТАТИСТИКА КВАЛИФИКАЦИИ ---");
CalculateStatistics(teams, avgSpeeds, n);
Console.WriteLine();

// Вывод исходного порядка
Console.WriteLine("--- ИСХОДНЫЙ ПОРЯДОК ---");
PrintTable(teams, avgSpeeds, n, false);
Console.WriteLine();

// Копирование массивов для сортировки
string[] sortedTeams = new string[n];
double[] sortedSpeeds = new double[n];
CopyArrays(teams, avgSpeeds, sortedTeams, sortedSpeeds, n);

// Сортировка
BubbleSort(sortedTeams, sortedSpeeds, n);

// Вывод отсортированного протокола
Console.WriteLine("--- ИТОГОВЫЙ ПРОТОКОЛ КВАЛИФИКАЦИИ ---");
PrintTable(sortedTeams, sortedSpeeds, n, true);
Console.WriteLine();

// Дополнительно: фильтр по скорости
FilterBySpeed(sortedTeams, sortedSpeeds, n);

Console.WriteLine();
Console.Write("Нажмите любую клавишу для выхода...");
Console.ReadKey();

// ================= ФУНКЦИИ =================

/* Ввод данных о командах и скоростях */
static void InputData(string[] teams, double[] speeds, int n)
{
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine($"Участник #{i + 1}");
        Console.Write("Команда: ");
        teams[i] = Console.ReadLine();

        Console.Write("Средняя скорость (км/ч): ");
        speeds[i] = double.Parse(Console.ReadLine());

        Console.WriteLine();
    }
}

/* Вычисление и вывод статистики */
static void CalculateStatistics(string[] teams, double[] speeds, int n)
{
    // Вычисление средней скорости
    double sum = 0;
    for (int i = 0; i < n; i++)
    {
        sum += speeds[i];
    }
    double average = sum / n;

    // Поиск максимальной и минимальной скорости
    double maxSpeed = speeds[0];
    double minSpeed = speeds[0];
    string fastestTeam = teams[0];
    string slowestTeam = teams[0];

    for (int i = 1; i < n; i++)
    {
        if (speeds[i] > maxSpeed)
        {
            maxSpeed = speeds[i];
            fastestTeam = teams[i];
        }

        if (speeds[i] < minSpeed)
        {
            minSpeed = speeds[i];
            slowestTeam = teams[i];
        }
    }

    // Вывод статистики
    Console.WriteLine($"Средняя скорость: {average:F2} км/ч");
    Console.WriteLine($"Лидер: {fastestTeam} ({maxSpeed:F2} км/ч)");
    Console.WriteLine($"Самый медленный: {slowestTeam} ({minSpeed:F2} км/ч)");
    Console.WriteLine($"Разница темпа: {maxSpeed - minSpeed:F2} км/ч");
}

/* Вывод таблицы результатов */
static void PrintTable(string[] teams, double[] speeds, int n, bool showPosition)
{
    Console.WriteLine("-----------------------------------------------");

    if (showPosition)
    {
        Console.WriteLine("| Поз. | Команда              | Скорость      |");
    }
    else
    {
        Console.WriteLine("| Команда              | Скорость (км/ч)     |");
    }

    Console.WriteLine("-----------------------------------------------");

    for (int i = 0; i < n; i++)
    {
        if (showPosition)
        {
            Console.WriteLine($"| {i + 1,4} | {teams[i],-20} | {speeds[i],13:F2} |");
        }
        else
        {
            Console.WriteLine($"| {teams[i],-20} | {speeds[i],19:F2} |");
        }
    }

    Console.WriteLine("-----------------------------------------------");
}

/* Пузырьковая сортировка по убыванию скорости */
static void BubbleSort(string[] teams, double[] speeds, int n)
{
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - i - 1; j++)
        {
            // Для сортировки по убыванию используем <
            if (speeds[j] < speeds[j + 1])
            {
                // Меняем скорости местами
                double tempSpeed = speeds[j];
                speeds[j] = speeds[j + 1];
                speeds[j + 1] = tempSpeed;

                // Меняем команды местами
                string tempTeam = teams[j];
                teams[j] = teams[j + 1];
                teams[j + 1] = tempTeam;
            }
        }
    }
}

/* Копирование массивов */
static void CopyArrays(string[] srcTeams, double[] srcSpeeds,
                       string[] dstTeams, double[] dstSpeeds, int n)
{
    for (int i = 0; i < n; i++)
    {
        dstTeams[i] = srcTeams[i];
        dstSpeeds[i] = srcSpeeds[i];
    }
}

/* Фильтрация по минимальной скорости */
static void FilterBySpeed(string[] teams, double[] speeds, int n)
{
    Console.WriteLine("--- ДОПОЛНИТЕЛЬНО: ФИЛЬТР ПО СКОРОСТИ ---");
    Console.Write("Введите минимальную скорость для отбора (км/ч): ");
    double minSpeed = double.Parse(Console.ReadLine());
    Console.WriteLine();

    Console.WriteLine($"Команды со скоростью >= {minSpeed:F2} км/ч:");

    int count = 0;
    for (int i = 0; i < n; i++)
    {
        if (speeds[i] >= minSpeed)
        {
            Console.WriteLine($"- {teams[i]} ({speeds[i]:F2} км/ч)");
            count++;
        }
    }

    Console.WriteLine();
    Console.WriteLine($"Отобрано команд: {count}");
}
