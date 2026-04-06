using System;
using System.Collections.Generic;
using System.Text;

namespace seminar_4;


/// <summary>
/// Контейнерный элемент списка
/// </summary>
/// <typeparam name="T">Тип элемента списка</typeparam>
/// <param name="data">Параметр, соответствующий типу элемента списка</param>
public class SimpleListItem<T>(T data)
{
    /// <summary>
    /// Данные
    /// </summary>
    public T Data { get; set; } = data;

    /// <summary>
    /// Следующий элемент
    /// </summary>
    public SimpleListItem<T>? Next { get; set; }
}


/// <summary>
/// Список
/// </summary>
public class SimpleList<T> : IEnumerable<T>
    where T : IComparable
{
    /// <summary>
    /// Первый элемент списка
    /// </summary>
    protected SimpleListItem<T>? first;
    //В C# поля ссылочных типов всегда
    //инициализируются значением null по умолчанию
    //Следующее объявление корректно но избыточно:
    //protected SimpleListItem<T>? first = null;

    /// <summary>
    /// Последний элемент списка
    /// </summary>
    protected SimpleListItem<T>? last;

    /// <summary>
    /// Количество элементов
    /// </summary>
    public int Count { get; protected set; }

    /// <summary>
    /// Добавление элемента
    /// </summary>
    public void Add(T element)
    {
        var newItem = new SimpleListItem<T>(element);
        Count++;

        // Добавление первого элемента
        if (last is null)
        //if (first is null) - также корректно
        //но тогда для last.Next = newItem;
        //будет выдаваться предупреждение: 
        //"Разыменование вероятной пустой ссылки."
        {
            first = newItem;
            last = newItem;
        }
        // Добавление следующих элементов
        else
        {
            last.Next = newItem;
            last = newItem;
        }
    }

    /// <summary>
    /// Чтение контейнера с заданным номером
    /// </summary>
    public SimpleListItem<T> GetItem(int number)
    {
        if (number < 0 || number >= Count)
            throw new IndexOutOfRangeException(
                $"Индекс {number} выходит за границы списка");

        var current = first;
        for (int i = 0; i < number; i++)
            current = current!.Next;

        return current!;
    }

    /// <summary>
    /// Чтение элемента с заданным номером
    /// </summary>
    public T Get(int number) => GetItem(number).Data;

    /// <summary>
    /// Для перебора коллекции
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        var current = first;
        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    // Реализация обобщённого IEnumerator<T> требует реализации
    // необобщённого интерфейса IEnumerable
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary>
    /// Сортировка
    /// </summary>
    public void Sort() => Sort(0, Count - 1);

    /// <summary>
    /// Алгоритм быстрой сортировки
    /// </summary>
    private void Sort(int low, int high)
    {
        int i = low;
        int j = high;
        T x = Get((low + high) / 2);
        do
        {
            while (Get(i).CompareTo(x) < 0) ++i;
            while (Get(j).CompareTo(x) > 0) --j;
            if (i <= j)
            {
                Swap(i, j);
                i++; j--;
            }
        } while (i <= j);

        if (low < j) Sort(low, j);
        if (i < high) Sort(i, high);
    }

    /// <summary>
    /// Вспомогательный метод для обмена элементов при сортировке
    /// </summary>
    private void Swap(int i, int j)
    {
        var ci = GetItem(i);
        var cj = GetItem(j);
        (ci.Data, cj.Data) = (cj.Data, ci.Data);
    }
}


/// <summary>
/// Класс стек
/// </summary>
class SimpleStack<T> : SimpleList<T> where T : IComparable
{
    /// <summary>
    /// Добавление в стек
    /// </summary>
    public void Push(T element) => Add(element);

    /// <summary>
    /// Удаление и чтение из стека
    /// </summary>
    public T Pop()
    {
        T result;

        // Если стек пуст, возвращается значение по умолчанию для типа
        if (Count == 0)
            return default!;
        else if (Count == 1)
        {
            // Единственный элемент — читаем и обнуляем список
            result = first!.Data;
            first = null;
            last = null;
        }
        else
        {
            // Поиск предпоследнего элемента
            var newLast = GetItem(Count - 2);
            // Чтение значения из последнего элемента
            result = newLast.Next!.Data;
            // Предпоследний элемент становится последним
            last = newLast;
            newLast.Next = null;
        }

        Count--;
        return result;
    }
}
