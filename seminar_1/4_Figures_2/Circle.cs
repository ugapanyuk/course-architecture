using System;
using System.Collections.Generic;
using System.Text;

namespace Figures_2; // File-scoped namespace (C# 10.0)

/// <summary>
/// Класс круга
/// </summary>
// sealed (C# 1.0) - класс не может быть унаследован (опционально, но рекомендуется для финальных классов)
sealed class Circle : Figure, IPrint
{
    /// <summary>
    /// Радиус круга
    /// </summary>
    // Auto-property с init accessor (C# 9.0)
    public double Radius { get; init; }

    /// <summary>
    /// Конструктор круга
    /// </summary>
    /// <param name="radius">Радиус круга</param>
    public Circle(double radius)
    {
        Radius = radius;
        Type = "Круг";
    }

    /// <summary>
    /// Вычисление площади круга
    /// </summary>
    /// <returns>Площадь (π × радиус²)</returns>
    // override - переопределяет абстрактный метод
    // Expression-bodied member (C# 6.0)
    // Math.PI - константа π
    public override double Area() => Math.PI * Radius * Radius;

    /// <summary>
    /// Вывод информации о круге в консоль
    /// </summary>
    // Expression-bodied member (C# 6.0)
    public void Print() => Console.WriteLine(ToString());
}

// ═══════════════════════════════════════════════════════════════
// ДОПОЛНИТЕЛЬНЫЕ СОВРЕМЕННЫЕ ВОЗМОЖНОСТИ
// ═══════════════════════════════════════════════════════════════

/*
// 1. PATTERN MATCHING (C# 7.0+)
// Пример использования в методе расширения:
public static class FigureExtensions
{
    public static string GetDescription(this Figure figure) => figure switch
    {
        Circle c when c.Radius > 10 => "Большой круг",
        Circle => "Круг",
        Square s when s.Height == s.Width => "Квадрат",
        Rectangle => "Прямоугольник",
        _ => "Неизвестная фигура"
    };
}

// 2. NULLABLE REFERENCE TYPES (C# 8.0)
// Включается в .csproj: <Nullable>enable</Nullable>
// Помогает избежать NullReferenceException

// 3. RECORDS (C# 9.0) - для неизменяемых данных
record CircleRecord(double Radius) : Figure, IPrint
{
    public string Type { get; init; } = "Круг";
    public override double Area() => Math.PI * Radius * Radius;
    public void Print() => Console.WriteLine(ToString());
}

// 4. INIT-ONLY SETTERS с object initializer
var circle = new Circle(5) { Type = "Круг" }; // Type можно установить при создании

// 5. TARGET-TYPED NEW (C# 9.0)
Circle circle = new(5); // Тип выводится из левой части
*/
