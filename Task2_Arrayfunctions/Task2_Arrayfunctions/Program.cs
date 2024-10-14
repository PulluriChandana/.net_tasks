class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter number of elements in an array");
        int n = int.Parse(Console.ReadLine());
        int[] numbers = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Element {i + 1}");
            numbers[i] = int.Parse(Console.ReadLine());
        }
        int sum=numbers.Sum();
        Console.WriteLine("Sum is: "+ sum);
        int max=numbers.Max();
        Console.WriteLine("Max is: " + max);
        int min=numbers.Min();
        Console.WriteLine("Min is: " + min);
        int[] sortednumbers= numbers.OrderBy(x => x).ToArray();
        Console.WriteLine("Sorted numbers: "+string.Join(", ", sortednumbers));
        int[] sortedDescending=numbers.OrderByDescending(x => x).ToArray();
        Console.WriteLine("Sorted numbers descending :" + string.Join(",", sortedDescending));
    }
}
