using Microsoft.Data.Sqlite;

// === Точка входа (операторы верхнего уровня) ===

const string dbFile = "developers.db";
const string devCsv = "dev.csv";
const string depCsv = "dep.csv";

CreateDatabase(dbFile);
LoadData(dbFile, devCsv, depCsv);

PrintData(dbFile, "dep");
PrintData(dbFile, "dev");

// Проекция — получаем список значений одной колонки
List<string> names = Projection(dbFile, "dev", "dev_name");
Console.WriteLine("\n=== Результат Projection(dev, dev_name) ===");
foreach (var name in names)
    Console.WriteLine(name);

// Выборка — получаем строки, подходящие под условие
List<string[]> rows = Where(dbFile, "dev", "dep_id", "2");
Console.WriteLine("\n=== Результат Where(dev, dep_id, 2) ===");
foreach (var row in rows)
    Console.WriteLine(string.Join(" | ", row));

// Соединение
var (columns, joinRows) = Join(dbFile, "dev", "dep", "dep_id", "dep_id");
Console.WriteLine("\n=== Результат Join(dev, dep, dep_id, dep_id) ===");
Console.WriteLine(string.Join(" | ", columns));
Console.WriteLine(new string('-', 80));
foreach (var row in joinRows)
    Console.WriteLine(string.Join(" | ", row));

// Среднее количество коммитов по отделам
var (gavgCols, gavgRows) = GroupAvg(dbFile, "dev", "dep_id", "dev_commits");
Console.WriteLine("\n=== Результат GroupAvg(dev, dep_id, dev_commits) ===");
Console.WriteLine(string.Join(" | ", gavgCols));
Console.WriteLine(new string('-', 40));
foreach (var row in gavgRows)
    Console.WriteLine(string.Join(" | ", row));


// ================================================
// === Статические функции ========================
// ================================================

static void CreateDatabase(string dbPath)
{
    if (File.Exists(dbPath))
        File.Delete(dbPath);

    using var connection = new SqliteConnection($"Data Source={dbPath}");
    connection.Open();

    var command = connection.CreateCommand();

    command.CommandText = @"
        CREATE TABLE dep (
            dep_id   INTEGER PRIMARY KEY,
            dep_name TEXT NOT NULL
        );";
    command.ExecuteNonQuery();

    command.CommandText = @"
        CREATE TABLE dev (
            dev_id      INTEGER PRIMARY KEY,
            dep_id      INTEGER NOT NULL,
            dev_name    TEXT    NOT NULL,
            dev_commits INTEGER NOT NULL,
            FOREIGN KEY (dep_id) REFERENCES dep(dep_id)
        );";
    command.ExecuteNonQuery();

    Console.WriteLine($"[OK] База данных «{dbPath}» создана, таблицы dep и dev готовы.");
}

static void LoadData(string dbPath, string devCsvPath, string depCsvPath)
{
    using var connection = new SqliteConnection($"Data Source={dbPath}");
    connection.Open();

    using (var transaction = connection.BeginTransaction())
    {
        var lines = File.ReadAllLines(depCsvPath);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(';');
            if (parts.Length < 2) continue;

            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO dep (dep_id, dep_name) VALUES (@id, @name);";
            cmd.Parameters.AddWithValue("@id", int.Parse(parts[0]));
            cmd.Parameters.AddWithValue("@name", parts[1]);
            cmd.ExecuteNonQuery();
        }
        transaction.Commit();
        Console.WriteLine($"[OK] Загружено строк из «{depCsvPath}»: {lines.Length - 1}");
    }

    using (var transaction = connection.BeginTransaction())
    {
        var lines = File.ReadAllLines(devCsvPath);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(';');
            if (parts.Length < 4) continue;

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO dev (dev_id, dep_id, dev_name, dev_commits)
                                VALUES (@devId, @depId, @name, @commits);";
            cmd.Parameters.AddWithValue("@devId", int.Parse(parts[0]));
            cmd.Parameters.AddWithValue("@depId", int.Parse(parts[1]));
            cmd.Parameters.AddWithValue("@name", parts[2]);
            cmd.Parameters.AddWithValue("@commits", int.Parse(parts[3]));
            cmd.ExecuteNonQuery();
        }
        transaction.Commit();
        Console.WriteLine($"[OK] Загружено строк из «{devCsvPath}»: {lines.Length - 1}");
    }
}

static void PrintData(string dbPath, string tableName)
{
    using var connection = new SqliteConnection($"Data Source={dbPath}");
    connection.Open();

    var cmd = connection.CreateCommand();
    cmd.CommandText = $"SELECT * FROM {tableName} ORDER BY 1;";

    using var reader = cmd.ExecuteReader();

    int columnCount = reader.FieldCount;
    const int colWidth = 20;

    Console.WriteLine($"\n========== Таблица {tableName} ==========");

    for (int c = 0; c < columnCount; c++)
        Console.Write($"{reader.GetName(c),-colWidth}");
    Console.WriteLine();
    Console.WriteLine(new string('-', colWidth * columnCount));

    while (reader.Read())
    {
        for (int c = 0; c < columnCount; c++)
            Console.Write($"{reader.GetValue(c),-colWidth}");
        Console.WriteLine();
    }
}

/// <summary>
/// Проекция: возвращает список значений одной колонки.
/// SELECT columnName FROM tableName
/// </summary>
static List<string> Projection(string dbPath, string tableName, string columnName)
{
    var result = new List<string>();

    using var connection = new SqliteConnection($"Data Source={dbPath}");
    connection.Open();

    var cmd = connection.CreateCommand();
    cmd.CommandText = $"SELECT {columnName} FROM {tableName} ORDER BY 1;";

    using var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        result.Add(reader.GetValue(0).ToString()!);
    }

    return result;
}

/// <summary>
/// Выборка: возвращает строки, где значение колонки равно заданному.
/// SELECT * FROM tableName WHERE columnName = value
/// </summary>
static List<string[]> Where(string dbPath, string tableName, string columnName, string value)
{
    var result = new List<string[]>();

    using var connection = new SqliteConnection($"Data Source={dbPath}");
    connection.Open();

    var cmd = connection.CreateCommand();
    cmd.CommandText = $"SELECT * FROM {tableName} WHERE {columnName} = @val ORDER BY 1;";
    cmd.Parameters.AddWithValue("@val", value);

    using var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        var row = new string[reader.FieldCount];
        for (int c = 0; c < reader.FieldCount; c++)
            row[c] = reader.GetValue(c).ToString()!;
        result.Add(row);
    }

    return result;
}

static (string[] columns, List<string[]> rows) Join(
    string dbPath,
    string table1, string table2,
    string key1, string key2)
{
    var rows = new List<string[]>();

    using var connection = new SqliteConnection($"Data Source={dbPath}");
    connection.Open();

    var cmd = connection.CreateCommand();
    cmd.CommandText = $@"
        SELECT *
        FROM {table1}
        INNER JOIN {table2}
            ON {table1}.{key1} = {table2}.{key2}
        ORDER BY 1;";

    using var reader = cmd.ExecuteReader();

    // Имена столбцов
    var columns = new string[reader.FieldCount];
    for (int c = 0; c < reader.FieldCount; c++)
        columns[c] = reader.GetName(c);

    // Строки данных
    while (reader.Read())
    {
        var row = new string[reader.FieldCount];
        for (int c = 0; c < reader.FieldCount; c++)
            row[c] = reader.GetValue(c).ToString()!;
        rows.Add(row);
    }

    return (columns, rows);
}


static (string[] columns, List<string[]> rows) GroupAvg(
    string dbPath,
    string tableName,
    string groupColumn,
    string avgColumn)
{
    var rows = new List<string[]>();

    using var connection = new SqliteConnection($"Data Source={dbPath}");
    connection.Open();

    var cmd = connection.CreateCommand();
    cmd.CommandText = $@"
        SELECT {groupColumn}, AVG({avgColumn}) AS avg_{avgColumn}
        FROM {tableName}
        GROUP BY {groupColumn}
        ORDER BY 1;";

    using var reader = cmd.ExecuteReader();

    var columns = new string[reader.FieldCount];
    for (int c = 0; c < reader.FieldCount; c++)
        columns[c] = reader.GetName(c);

    while (reader.Read())
    {
        var row = new string[reader.FieldCount];
        for (int c = 0; c < reader.FieldCount; c++)
            row[c] = reader.GetValue(c).ToString()!;
        rows.Add(row);
    }

    return (columns, rows);
}
