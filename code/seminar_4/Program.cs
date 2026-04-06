using System.Drawing;

namespace seminar_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////
            #region Геометрические фигуры
            Rectangle rect = new(5, 4);
            Square square = new(5);
            Circle circle = new(5);

            Console.WriteLine(rect);
            Console.WriteLine(square);
            Console.WriteLine(circle);
            #endregion

            //********************************************************
            // Работа с матрицей
            //********************************************************
            Console.WriteLine("\nМатрица");
            Matrix<Figure> matrix = new(3, 3, new FigureMatrixCheckEmpty());
            matrix[0, 0] = rect;
            matrix[1, 1] = square;
            matrix[2, 2] = circle;
            Console.WriteLine(matrix);


            //********************************************************
            // Список
            //********************************************************
            SimpleList<Figure> list = new();
            list.Add(circle);
            list.Add(rect);
            list.Add(square);

            Console.WriteLine("\nПеред сортировкой списка:");
            foreach (var x in list) Console.WriteLine(x);

            list.Sort();

            Console.WriteLine("\nПосле сортировки списка:");
            foreach (var x in list) Console.WriteLine(x);

            //********************************************************
            // Стек 
            //********************************************************
            SimpleStack<Figure> stack = new();

            // Добавление данных в стек
            stack.Push(rect);
            stack.Push(square);
            stack.Push(circle);

            Console.WriteLine("\nВывод данных стека:");
            // Чтение данных из стека
            while (stack.Count > 0)
            {
                Figure f = stack.Pop();
                Console.WriteLine(f);
            }
        }
    }
}
