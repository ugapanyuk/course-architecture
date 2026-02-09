using System;
using System.Collections.Generic;
using System.Text;

namespace Figures_2; // File-scoped namespace (C# 10.0)

/// <summary>
/// Интерфейс для объектов, которые могут быть напечатаны в консоль
/// </summary>
interface IPrint
{
    /// <summary>
    /// Вывод информации об объекте в консоль
    /// </summary>
    void Print();

    // Альтернатива с default implementation (C# 8.0) - интерфейсы могут содержать реализацию по умолчанию:
    // void Print() => Console.WriteLine(ToString());
}
