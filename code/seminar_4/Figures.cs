using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace seminar_4;

/// <summary>
/// Абстрактная фигура
/// </summary>
/// <param name="type">Название типа фигуры</param>
internal abstract class Figure(string type) : IComparable
{
    // Параметр "string type" задается при объявлении класса
    // Это механизм "первичного конструктора"

    /// <summary>
    /// Тип фигуры
    /// Только для чтения
    /// Инициализируется значением из первичного конструктора
    /// </summary>
    public string Type { get; } = type;

    /// <summary>
    /// Вычисление площади
    /// Вычисляемой свойство только для чтения
    /// </summary>
    public abstract double Area { get; }

    /// <summary>
    /// Приведение к строке, переопределение метода Object
    /// используются:
    /// - строковая интерполяция ($"...") для удобства форматирования
    /// - expression-bodied member (=> вместо фигурных скобок) для лаконичности
    /// </summary>
    public override string ToString() => 
                    $"{Type} площадью {Area}";

    /// <summary>
    /// Сравнение фигур по площади (по возрастанию)
    /// </summary>
    public int CompareTo(object? obj) =>
        obj is Figure other
            ? Area.CompareTo(other.Area)
            : throw new ArgumentException("Объект не является фигурой");
}

/// <summary>
/// Класс Круг
/// </summary>
/// <param name="radius"></param>
internal class Circle(double radius) : Figure("Круг")
{
    /// <summary>
    /// Площадь круга
    /// Переопределяем get-аксессор абстрактного свойства Area
    /// </summary>
    public override double Area => Math.PI * radius * radius;
}

/// <summary>
/// Класс Прямоугольник
/// </summary>
/// <param name="height">Высота</param>
/// <param name="width">Ширина</param>
/// <param name="type">Тип фигуры для удобства наследования</param>
internal class Rectangle(double height, double width, string type = "Прямоугольник") : Figure(type)
{
    /// <summary>
    /// Площадь прямоугольника
    /// </summary>
    public override double Area => width * height;
}

/// <summary>
/// Класс Квадрат
/// </summary>
/// <param name="size">размер стороны</param>
internal class Square(double size) : Rectangle(size, size, "Квадрат") {}
// никакой реализации для класа не требуется, специфика реализации
// задается передачей параметров в конструктор базового класса
