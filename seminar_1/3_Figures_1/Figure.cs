using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    /// <summary>
    /// Класс фигура
    /// </summary>
    abstract class Figure
    {
        /// <summary>
        /// Тип фигуры
        /// </summary>
        public string Type
        {
            get
            {
                return this._Type;
            }
            protected set
            {
                this._Type = value;
            }
        }
        string _Type;

        /// <summary>
        /// Вычисление площади
        /// </summary>
        public abstract double Area();

        /// <summary>
        /// Приведение к строке, переопределение метода Object
        /// </summary>
        public override string ToString()
        {
            return this.Type + " площадью " + this.Area().ToString();
        }
    }
}
