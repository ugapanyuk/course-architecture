// Top-level statements (C# 9.0) - позволяют писать код без явного класса Program и метода Main
// Упрощает структуру для простых приложений
using Figures_2;

// Использование var (C# 3.0) - неявная типизация локальных переменных для улучшения читаемости
// Компилятор автоматически определяет тип на основе правой части выражения
var rect = new Rectangle(5, 4);
var square = new Square(5);
var circle = new Circle(5);

rect.Print();
square.Print();
circle.Print();

