using System;
using System.Collections.Generic;
using System.Text;

namespace seminar_4;

/// <summary>
/// Интерфейс для взаимодействия с пустыми элементами
/// </summary>
/// <typeparam name="T">Класс, объекты котрого предполагается помещать в ячейку матрицы</typeparam>
public interface IMatrixCheckEmpty<T>
{
    /// <summary>
    /// Возвращает пустой элемент
    /// </summary>
    T GetEmptyElement();

    /// <summary>
    /// Проверка что элемент является пустым
    /// </summary>
    bool CheckEmptyElement(T element);
}

/// <summary>
/// Реализация проверки на пустоту для геометрической фигуры
/// </summary>
internal class FigureMatrixCheckEmpty : IMatrixCheckEmpty<Figure>
{
    /// <summary>
    /// В качестве пустого элемента возвращается null
    /// </summary>
    public Figure GetEmptyElement() => null!;

    /// <summary>
    /// Проверка что переданный параметр равен null
    /// </summary>
    public bool CheckEmptyElement(Figure element) => element is null;
}

/// <summary>
/// Класс разреженной матрицы
/// с первичным конструктором
/// </summary>
/// <typeparam name="T">Класс, объекты котрого предполагается помещать в ячейку матрицы</typeparam>
/// <param name="maxX">Максимальное количество колонок матрицы</param>
/// <param name="maxY">Максимальное количество строк матрицы</param>
/// <param name="checkEmpty">Объект класса реализующего проверку на пустоту</param>
public class Matrix<T>(int maxX, int maxY, IMatrixCheckEmpty<T> checkEmpty)
{
    /// <summary>
    /// Словарь для хранения значений
    /// </summary>
    readonly Dictionary<(int x, int y), T> _matrix = [];

    /// <summary>
    /// Индексатор для доступа к данным
    /// </summary>
    public T this[int x, int y]
    {
        set
        {
            CheckBounds(x, y);
            _matrix[(x, y)] = value;
        }
        get
        {
            CheckBounds(x, y);
            return _matrix.TryGetValue((x, y), out var element)
                ? element
                : checkEmpty.GetEmptyElement();
        }
    }

    /// <summary>
    /// Проверка границ
    /// </summary>
    void CheckBounds(int x, int y)
    {
        if (x < 0 || x >= maxX)
            throw new ArgumentOutOfRangeException
                (nameof(x), $"x={x} выходит за границы");
        if (y < 0 || y >= maxY)
            throw new ArgumentOutOfRangeException
                (nameof(y), $"y={y} выходит за границы");
    }

    /// <summary>
    /// Ширина столбца при выводе матрицы (символов)
    /// </summary>
    public int ColumnWidth { get; set; } = 32;

    /// <summary>
    /// Приведение к строке в табличном виде
    /// </summary>
    public override string ToString()
    {
        var b = new StringBuilder();
        for (int j = 0; j < maxY; j++)
        {
            b.Append('|');
            for (int i = 0; i < maxX; i++)
            {
                string cell = !checkEmpty.CheckEmptyElement(this[i, j])
                    ? $"{this[i, j]}"
                    : "-";
                b.Append(cell.PadRight(ColumnWidth));
                b.Append('|');
            }
            b.AppendLine();
        }
        return b.ToString();
    }
}

