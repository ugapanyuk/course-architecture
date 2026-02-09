using System.Drawing;

namespace Classes
{
    /// <summary>
    /// Базовый класс
    /// </summary>
    class BaseClass
    {
        //Вместо полей данных рекомендуется использовать свойства
        private int i;

        //Конструктор
        public BaseClass(int param) { i = param; }
        //Методы с различными сигнатурами
        public int MethodReturn(int a) { return i; }
        public string MethodReturn(string a) { return i.ToString(); }

        //Свойство
        //private-значение, которое хранит данные для свойства
        private int _property1 = 0;
        //объявление свойства
        public int property1
        {
            //возвращаемое значение
            get { return _property1; }
            //установка значения, value - ключевое слово
            set { _property1 = value; }
            //private set { _property1 = value; }
        }

        /// <summary>
        /// Вычисляемое свойство
        /// </summary>
        public int property1mul2
        {
            get { return property1 * 2; }
        }

        //Автоматически реализуемые свойства
        //поддерживающая переменная создается автоматически
        public string property2 { get; set; }
        public float property3 { get; private set; }

        //Expression-bodied свойства
        public int property1mul3 => property1 * 2;


        //Expression-bodied аксессоры
        private int _value;
        public int Value
        {
            get => _value;
            // Валидация
            set => _value = value < 0 ? 0 : value;
        }

    }

    /// <summary>
    /// Наследуемый класс 1
    /// </summary>
    class ExtendedClass1 : BaseClass
    {
        private int i2;
        private int i3;

        //Конструктор
        //base(pi) - вызов конструктора базового класса
        public ExtendedClass1(int pi, int pi2) : base(pi) { i2 = pi2; }

        //this(pi, pi2) - вызов другого конструктора этого класса
        public ExtendedClass1(int pi, int pi2, int pi3) : this(pi, pi2) { i3 = pi3; }

        /// <summary>
        /// Метод виртуальный, так как он объявлен в самом базовом классе object
        /// поэтому чтобы его переопределить добавлено ключевое слово override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "i=" + MethodReturn("1") + " i2=" + i2.ToString() + " i3=" + i3.ToString();
        }

    }

    /// <summary>
    /// Объявление интерфейсов для множественного наследования
    /// </summary>
    interface I1
    {
        string I1_method();
    }

    interface I2 : I1
    {
        string I2_method();
    }


    /// <summary>
    /// Наследуемый класс 2
    /// </summary>
    class ExtendedClass2 : ExtendedClass1, I1, I2
    {
        //В конструкторе вызывается конструктор базового класса
        public ExtendedClass2(int pi, int pi2, int pi3) : base(pi, pi2, pi3) { }

        //Реализация методов, объявленных в интерфейсах
        public string I1_method() { return ToString(); }
        public string I2_method() { return ToString(); }
    }

    /// <summary>
    /// Метод расширения
    /// </summary>
    static class ExtendedClass2Extension
    {
        public static int ExtendedClass2NewMethod(this ExtendedClass2 ec2, int i)
        {
            return i + 1;
        }
    }

    //partial - объявление частичного класса
    partial class PartialClass
    {
        int i1;

        public PartialClass(int pi1, int pi2) { i1 = pi1; i2 = pi2; }

        public int MethodPart1(int i1, int i2)
        {
            return i1 + i2;
        }
    }

    partial class PartialClass
    {
        int i2;

        public override string ToString()
        {
            return "Частичный класс. i1=" + i1.ToString()
                                 + " i2=" + i2.ToString();
        }

        public string MethodPart2(string i1, string i2)
        {
            return i1 + i2;
        }
    }


    public class Person
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    //Объявление типа record
    public record Point(int X, int Y);

    public class Book
    {
        public required string Title { get; set; }
        public int Year { get; set; }
    }


    /// <summary>
    /// Основной класс
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Базовый класс");

            //Объект базового класса
            BaseClass bc = new BaseClass(333);
            Console.WriteLine(bc.MethodReturn(1));
            Console.WriteLine(bc.MethodReturn("1"));

            //Обращение к свойству
            bc.property1 = 334;
            Console.WriteLine("\n property1 = {0}", bc.property1);

            Console.WriteLine("\nНаследуемый класс 1");
            //Объекты наследуемого класса
            ExtendedClass1 ex1_1 = new ExtendedClass1(1, 2);
            //Неявно вызывается метод ex1.ToString()
            Console.WriteLine(ex1_1);

            ExtendedClass1 ex1_2 = new ExtendedClass1(1, 2, 3);
            Console.WriteLine(ex1_2);

            Console.WriteLine("\nНаследуемый класс 2");


            ExtendedClass2 ex2 = new ExtendedClass2(100, 200, 300);
            Console.WriteLine(ex2.I1_method());
            Console.WriteLine(ex2.I2_method());
            Console.WriteLine(ex2.ExtendedClass2NewMethod(332));

            Console.WriteLine("\nЧастичный класс");
            PartialClass part = new PartialClass(333, 334);
            Console.WriteLine(part);
            Console.WriteLine("\nИспользование рефлексии");
            Console.WriteLine("Список методов класса ExtendedClass2:");
            foreach (System.Reflection.MemberInfo member in ex2.GetType().GetMethods())
            {
                Console.WriteLine(member.Name);
            }

            var person = new Person
            {
                FirstName = "Иван",
                LastName = "Иванов"
            };

            // Ошибка компиляции:
            //person.FirstName = "Петр";

            //============================================
            //Работа с record
            //============================================

            // Пример 1: Создание и использование
            var point1 = new Point(10, 20);
            Console.WriteLine(point1); // Output: Point { X = 10, Y = 20 }
            Console.WriteLine($"X = {point1.X}, Y = {point1.Y}"); // X = 10, Y = 20

            // Пример 2: Value-based equality (сравнение по значению, а не по ссылке)
            var point2 = new Point(10, 20);
            var point3 = new Point(15, 25);

            Console.WriteLine(point1 == point2); // True (одинаковые значения)
            Console.WriteLine(point1 == point3); // False (разные значения)

            // Пример 3: Иммутабельность - попытка изменить вызовет ошибку компиляции
            // point1.X = 30; // ОШИБКА: init-only property

            // Пример 4: Использование with-expression для создания копии с изменениями
            var point4 = point1 with { X = 100 };
            Console.WriteLine(point1); // Point { X = 10, Y = 20 } (оригинал не изменился)
            Console.WriteLine(point4); // Point { X = 100, Y = 20 } (новый объект)

            // Пример 5: Деконструкция
            var(x, y) = point1;
            Console.WriteLine($"x = {x}, y = {y}"); // x = 10, y = 20

            // Пример 6: Паттерн-матчинг
            string Describe(Point p) => p switch
            {
                { X: 0, Y: 0 } => "Начало координат",
                { X: 0 } => "На оси Y",
                { Y: 0 } => "На оси X",
                { X: var x, Y: var y } when x == y => "На диагонали",
                _ => "Обычная точка"
            };
            Console.WriteLine(Describe(new Point(0, 0))); // "Начало координат"
            Console.WriteLine(Describe(new Point(5, 5)));  // "На диагонали"


            //============================================
            //Required свойства
            //============================================

            // Ошибка компиляции - не указано обязательное свойство Title:
            // var book = new Book { Year = 2024 };

            // Правильно:

            var book1 = new Book
            {
                Title = "Книга1",
                Year = 2026
            };

            var book2 = new Book
            {
                Title = "Книга2",
            };






    }
}
}
