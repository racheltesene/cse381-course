// CSE 381 REPL 1A
// C# Primer



using System.Windows.Markup;

public class Rectangle
{
    private float _height;

    private float _width;

    public Rectangle(float height, float width){
        _height = height;
        _width = width;
    }
    public float Area() {
        return _height * _width;
    }
}
public static class Stats {
    public static float Average(List<int> numbers) {
        return (float) numbers.Sum() / numbers.Count;
    }
    public static int Max<T>(List<T> values){
        var maxValue = ValueSerializerAttribute[0];
        for (var i = 1; i < ValueSerializerAttribute.Count; i++){
            if (Values[1])
        }
    }
}
public static class Program 
{
    public static void Main (string[] args) 
    {
        Console.WriteLine ("Hello World");

        int x = 5;
        var y = 6;

        Console.WriteLine($"x = {x} y = {y} x+y = {x+y}");

        for (var i = 0; i < 10; i++) {
            Console.WriteLine(i);
        }

        foreach (var i in Enumerable.Range(0,10)) {
            Console.WriteLine(i);
        }
        var list = new List<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

         foreach (var i in list) {
             Console.WriteLine(i);
         }
        
        Console.WriteLine($"First: {list[0]}");
        Console.WriteLine($"Last: {list[^1]}");

        var list2 = Enumerable.Range(0,10).ToList();
        list2 = list2.Select(x => 2*x).ToList();
        list2 = list2.Select(_ => 42).ToList();

         foreach (var i in list2) {
             Console.WriteLine(i);
         }

        var r = new Rectangle(3,5);

        Console.WriteLine($"Area = {r.Area()}");
        Console.WriteLine($"Area = {r.Area()}");

        var list3 = new List<int> {3,1,6,5,4,0};
        var avg = Stats.Average(list3);
       
 

        Console.WriteLine($"Avg: {avg}");

        var max = Stats.Max(list3);
         Console.WriteLine($"Max: {max}");

        var list4 = new List<string> {"dog", "cat", "pig", "hamster", "bird"};
        var max4 = Stats.Max(list4)

        Console.WriteLine($"Max: {max4}");


        // Console.WriteLine(string.Join(", ", firstHalf.ToList()));
        // Console.WriteLine(string.Join(", ", secondHalf.ToList()));

        
        // Console.WriteLine(result);
        

        // Console.WriteLine(result);

        
        // Console.WriteLine(result);

    }
}