using System;
using System.Collections.Generic;
using System.Text;

namespace Figures_2; // File-scoped namespace (C# 10.0)

/// <summary>
/// Класс квадрата (частный случай прямоугольника)
/// </summary>
// sealed (C# 1.0) - запрещает дальнейшее наследование от этого класса
// Это логично, так как квадрат - конечная форма в иерархии
sealed class Square : Rectangle
{
    /// <summary>
    /// Конструктор квадрата
    /// </summary>
    /// <param name="size">Размер стороны квадрата</param>
    // base(size, size) - вызов конструктора базового класса Rectangle
    // : this (C# 1.0) / : base (C# 1.0) - делегирование конструкторов
    public Square(double size) : base(size, size)
    {
        // После вызова base, можем переопределить Type
        Type = "Квадрат";
    }

    // Примечание: IPrint уже реализован в Rectangle, повторное указание не требуется
}

// ═══════════════════════════════════════════════════════════════
// АЛЬТЕРНАТИВНЫЙ ВАРИАНТ С PRIMARY CONSTRUCTOR (C# 12.0)
// ═══════════════════════════════════════════════════════════════
// Primary constructor (C# 12.0) - параметры конструктора объявляются в заголовке класса

/*
/// <summary>
/// Квадрат (альтернативная реализация с primary constructor)
/// </summary>
/// <param name="size">Размер стороны квадрата</param>
sealed class Square(double size) : Rectangle(size, size)
{
    // Можем переопределить Type через инициализатор свойства
    public new string Type { get; init; } = "Квадрат";
}
*/