// === Точка входа (операторы верхнего уровня) ===

string mode = args[0].ToLower();

switch (mode)
{
    case "projection":
        {
            // CSV поступает из стандартного ввода
            var table = ReadCsv(Console.In, ';');
            string columnName = args[1];
            var result = Projection(table, columnName);
            WriteCsv(Console.Out, result, ';');
            break;
        }

    case "where":
        {
            if (args.Length < 3)
            {
                Console.Error.WriteLine(
                    "Ошибка: режим 'where' требует два параметра: where <колонка> <значение>");
                return 1;
            }
            // CSV поступает из стандартного ввода
            var table = ReadCsv(Console.In, ';');
            string columnName = args[1];
            string value = args[2];
            var result = Where(table, columnName, value);
            WriteCsv(Console.Out, result, ';');
            break;
        }

    case "join":
        {
            if (args.Length < 5)
            {
                Console.Error.WriteLine(
                    "Использование: program join <таблица1> <таблица2> <ключ1> <ключ2>");
                return 1;
            }

            using var reader1 = File.OpenText(args[1] + ".csv");
            using var reader2 = File.OpenText(args[2] + ".csv");

            var left = ReadCsv(reader1, ';');
            var right = ReadCsv(reader2, ';');

            var result = Join(left, right, args[3], args[4]);
            WriteCsv(Console.Out, result, ';');
            break;
        }

    case "group_avg":
        {
            if (args.Length < 3)
            {
                Console.Error.WriteLine(
                    "Использование: program group_avg <колонка_группировки> <колонка_значений>");
                return 1;
            }
            // CSV поступает из стандартного ввода
            var table = ReadCsv(Console.In, ';');

            string groupColumn = args[1];
            string valueColumn = args[2];

            var result = GroupAvg(table, groupColumn, valueColumn);
            WriteCsv(Console.Out, result, ';');
            break;
        }

}

return 0;

// ================================================
// === Статические функции ========================
// ================================================

// ---------- Чтение / запись CSV ----------

static CsvTable ReadCsv(TextReader reader, char separator)
{
    string? headerLine = reader.ReadLine();
    if (headerLine is null)
        throw new InvalidOperationException("Входной поток пуст — нет строки заголовков.");

    string[] headers = headerLine.Split(separator);
    var rows = new List<CsvRow>();

    string? line;
    while ((line = reader.ReadLine()) is not null)
    {
        if (string.IsNullOrWhiteSpace(line))
            continue;

        string[] parts = line.Split(separator);
        rows.Add(new CsvRow(parts));
    }

    return new CsvTable(headers, rows);
}

static void WriteCsv(TextWriter writer, CsvTable table, char separator)
{
    writer.WriteLine(string.Join(separator, table.Headers));

    foreach (var row in table.Rows)
        writer.WriteLine(string.Join(separator, row.Fields));
}

// ---------- Вспомогательные ----------

/// <summary>
/// Ищет индекс столбца по имени. Бросает исключение, если столбец не найден.
/// </summary>
static int FindColumnIndex(CsvTable table, string columnName)
{
    int index = Array.IndexOf(table.Headers, columnName);
    if (index < 0)
        throw new ArgumentException(
            $"Колонка «{columnName}» не найдена. " +
            $"Доступные колонки: {string.Join(", ", table.Headers)}");
    return index;
}

// ---------- Операции ----------

/// <summary>
/// Проекция: SELECT columnName FROM table.
/// Возвращает таблицу с одним столбцом.
/// </summary>
static CsvTable Projection(CsvTable table, string columnName)
{
    int colIndex = FindColumnIndex(table, columnName);

    string[] newHeaders = [columnName];
    var newRows = new List<CsvRow>();

    foreach (var row in table.Rows)
    {
        string[] fields = [row.Fields[colIndex]];
        newRows.Add(new CsvRow(fields));
    }

    return new CsvTable(newHeaders, newRows);
}

/// <summary>
/// Выборка: SELECT * FROM table WHERE columnName = value.
/// Возвращает таблицу с теми же столбцами, но отфильтрованными строками.
/// </summary>
static CsvTable Where(CsvTable table, string columnName, string value)
{
    int colIndex = FindColumnIndex(table, columnName);

    var newRows = new List<CsvRow>();

    foreach (var row in table.Rows)
    {
        if (row.Fields[colIndex] == value)
        {
            newRows.Add(row);
        }
    }

    return new CsvTable(table.Headers, newRows);
}


/// <summary>
/// Inner Join: для каждой пары строк, где left[leftKey] == right[rightKey],
/// склеивает поля обеих строк в одну.
/// </summary>
static CsvTable Join(CsvTable left, CsvTable right,
                     string leftKey, string rightKey)
{
    int leftKeyIndex = FindColumnIndex(left, leftKey);
    int rightKeyIndex = FindColumnIndex(right, rightKey);

    // Заголовки: все колонки левой + все колонки правой
    var newHeaders = new string[left.Headers.Length + right.Headers.Length];
    for (int i = 0; i < left.Headers.Length; i++)
        newHeaders[i] = left.Headers[i];
    for (int i = 0; i < right.Headers.Length; i++)
        newHeaders[left.Headers.Length + i] = right.Headers[i];

    var newRows = new List<CsvRow>();

    foreach (var leftRow in left.Rows)
    {
        foreach (var rightRow in right.Rows)
        {
            if (leftRow.Fields[leftKeyIndex] == rightRow.Fields[rightKeyIndex])
            {
                // Склеиваем поля двух строк
                var fields = new string[leftRow.Fields.Length + rightRow.Fields.Length];
                for (int i = 0; i < leftRow.Fields.Length; i++)
                    fields[i] = leftRow.Fields[i];
                for (int i = 0; i < rightRow.Fields.Length; i++)
                    fields[leftRow.Fields.Length + i] = rightRow.Fields[i];

                newRows.Add(new CsvRow(fields));
            }
        }
    }

    return new CsvTable(newHeaders, newRows);
}

/// <summary>
/// Вычисляет среднее арифметическое массива чисел.
/// </summary>
static double Average(List<double> values)
{
    double sum = 0;
    for (int i = 0; i < values.Count; i++)
        sum += values[i];
    return sum / values.Count;
}

/// <summary>
/// Группировка с вычислением среднего:
/// SELECT groupColumn, AVG(valueColumn) FROM table GROUP BY groupColumn
/// </summary>
static CsvTable GroupAvg(CsvTable table, string groupColumn, string valueColumn)
{
    int groupIndex = FindColumnIndex(table, groupColumn);
    int valueIndex = FindColumnIndex(table, valueColumn);

    // Ключ — значение колонки группировки,
    // значение — список числовых значений в этой группе
    var groups = new Dictionary<string, List<double>>();

    foreach (var row in table.Rows)
    {
        string key = row.Fields[groupIndex];
        double value = double.Parse(row.Fields[valueIndex]);

        if (!groups.ContainsKey(key))
            groups[key] = new List<double>();

        groups[key].Add(value);
    }

    // Формируем результат
    string[] newHeaders = [groupColumn, "avg_" + valueColumn];
    var newRows = new List<CsvRow>();

    foreach (var pair in groups)
    {
        string avg = Average(pair.Value).ToString("F2");
        newRows.Add(new CsvRow([pair.Key, avg]));
    }

    return new CsvTable(newHeaders, newRows);
}


// ================================================
// === Записи (records) для представления данных ===
// ================================================

/// <summary>
/// Одна строка CSV‑файла. Хранит значения полей в порядке столбцов.
/// record даёт неизменяемость и готовый ToString() для отладки.
/// </summary>
record CsvRow(string[] Fields);

/// <summary>
/// CSV-таблица целиком: заголовки (имена столбцов) + список строк.
/// </summary>
record CsvTable(string[] Headers, List<CsvRow> Rows);
