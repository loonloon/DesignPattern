namespace OpenClosePrinciple
{
    class Program
    {
        /*
         * Intent
         * Software entities like classes, modules and functions should be open for extension but closed for modifications.
         */
        static void Main(string[] args)
        {
        }
    }

    // Open-Close Principle - Good example
    public class GraphicEditor
    {
        public void DrawShape(Shape shape)
        {
            shape.Draw();
        }
    }

    public abstract class Shape
    {
        public abstract void Draw();
    }

    public class Rectangle : Shape
    {
        public override void Draw()
        {
            // draw the rectangle
        }
    }

    public class Circle : Shape
    {
        public override void Draw()
        {
            // draw the circle
        }
    }

    // Open-Close Principle - Bad example
    //public class GraphicEditor
    //{
    //    public void DrawShape(Shape shape)
    //    {
    //        switch (shape.Type)
    //        {
    //            case 1:
    //                DrawRectangle(shape as Rectangle);
    //                break;
    //            case 2:
    //                DrawCircle(shape as Circle);
    //                break;
    //        }
    //    }

    //    public void DrawCircle(Circle circle)
    //    {
    //        //...
    //    }

    //    public void DrawRectangle(Rectangle rectangle)
    //    {
    //        //...
    //    }
    //}

    //public class Shape
    //{
    //    public int Type { get; set; }
    //}

    //public class Rectangle : Shape
    //{
    //    public Rectangle()
    //    {
    //        Type = 1;
    //    }
    //}

    //public class Circle : Shape
    //{
    //    public Circle()
    //    {
    //        Type = 2;
    //    }
    //}
}
