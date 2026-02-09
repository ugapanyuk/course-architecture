using System;
using System.Collections.Generic;
using System.Text;

namespace Figures_2; // File-scoped namespace (C# 10.0) - уменьшает уровень вложенности на один, делая код более плоским

/// <summary>
/// Базовый абстрактный класс для всех фигур
/// </summary>
// abstract - класс не может быть создан напрямую, только через наследников
abstract class Figure
{
    /// <summary>
    /// Тип фигуры (только для чтения извне)
    /// </summary>
    // Auto-property с init accessor (C# 9.0) - свойство можно установить только в конструкторе или инициализаторе
    // Это обеспечивает неизменяемость после создания объекта
    // Заменяет старый паттерн с backing field (_Type)
    public string Type { get; protected init; } = string.Empty;

    // Альтернатива с required (C# 11.0) - требует обязательной установки свойства при создании:
    // public required string Type { get; init; }

    /// <summary>
    /// Абстрактный метод вычисления площади
    /// </summary>
    /// <returns>Площадь фигуры</returns>
    // abstract - метод должен быть реализован в производных классах
    public abstract double Area();

    /// <summary>
    /// Строковое представление фигуры
    /// </summary>
    /// <returns>Строка с типом и площадью фигуры</returns>
    // override - переопределяет метод базового класса Object
    // Expression-bodied member (C# 6.0) - лаконичный синтаксис для методов, возвращающих одно выражение
    // String interpolation (C# 6.0) - $"" позволяет встраивать выражения прямо в строку вместо конкатенации
    public override string ToString() => $"{Type} площадью {Area()}";
}
