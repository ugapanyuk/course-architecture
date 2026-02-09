using System;
using System.Collections.Generic;
using System.Text;

namespace Figures_2; // File-scoped namespace (C# 10.0)

/// <summary>
/// Класс прямоугольника
/// </summary>
class Rectangle : Figure, IPrint
{
    /// <summary>
    /// Высота прямоугольника
    /// </summary>
    // Auto-property с init accessor (C# 9.0) - свойство доступно только для чтения извне
    // и может быть установлено только при инициализации
    // Заменяет старый паттерн с private полем
    public double Height { get; init; }

    /// <summary>
    /// Ширина прямоугольника
    /// </summary>
    public double Width { get; init; }

    /// <summary>
    /// Конструктор прямоугольника
    /// </summary>
    /// <param name="height">Высота прямоугольника</param>
    /// <param name="width">Ширина прямоугольника</param>
    // Object initializer можно использовать с init свойствами
    public Rectangle(double height, double width)
    {
        Height = height;
        Width = width;
        Type = "Прямоугольник";
    }

    /// <summary>
    /// Вычисление площади прямоугольника
    /// </summary>
    /// <returns>Площадь (высота × ширина)</returns>
    // override - переопределяет абстрактный метод базового класса
    // Expression-bodied member (C# 6.0) - компактная запись для методов с одним выражением
    public override double Area() => Height * Width;

    /// <summary>
    /// Вывод информации о прямоугольнике в консоль
    /// </summary>
    // Expression-bodied member (C# 6.0)
    public void Print() => Console.WriteLine(ToString());
}

// ═══════════════════════════════════════════════════════════════
// АЛЬТЕРНАТИВНЫЙ ВАРИАНТ С RECORD (C# 9.0/10.0)
// ═══════════════════════════════════════════════════════════════
// Record (C# 9.0) - специальный ссылочный тип для неизменяемых данных
// Автоматически генерирует: конструктор, свойства, Equals, GetHashCode, ToString, Deconstruct
// Positional record (C# 9.0) - компактный синтаксис с параметрами в объявлении

/*
/// <summary>
/// Прямоугольник (альтернативная реализация через record)
/// </summary>
/// <param name="Height">Высота прямоугольника</param>
/// <param name="Width">Ширина прямоугольника</param>
record Rectangle(double Height, double Width) : Figure, IPrint
{
    // Инициализация Type через property initializer
    public string Type { get; init; } = "Прямоугольник";
    
    // Primary constructor (C# 12.0) позволяет использовать параметры конструктора напрямую
    public override double Area() => Height * Width;
    public void Print() => Console.WriteLine(ToString());
    
    // with expression (C# 9.0) позволяет создавать копии с изменёнными свойствами:
    // var newRect = rect with { Width = 10 };
}
*/
