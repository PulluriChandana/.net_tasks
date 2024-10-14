using System.ComponentModel.Design;
class Student
{
    
    
    static void GetStudentScores(int[] scores)
    {
        var n = 5;
        
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter score of student{i + 1}");
            while (!int.TryParse(Console.ReadLine(), out scores[i]))
            {
                Console.WriteLine($"Enter score of student{i + 1}");
            }
        }
    }
    public static void Main(string[] args)
    {
        var n = 5;
        int[] scores = new int[n];
        //reverseArray(scores);
        GetStudentScores(scores);
        Console.WriteLine("Scores ");

        double average = CalculateAverage(scores);
        Console.WriteLine($"Average of scores:{average}");
        Array.Reverse(scores);
        Console.WriteLine("Reverse of an array:" + string.Join(",", scores));
        Console.WriteLine("Reverse of an array using function:" + string.Join(",", scores));
        Console.WriteLine("Enter a score to search and return index position");
        if (int.TryParse(Console.ReadLine(), out int serach))
        {
            int indexpos = Array.IndexOf(scores, serach);
            if (indexpos != -1)
            {
                Console.WriteLine($"Score {serach} found at index position: {indexpos}");
            }
            else
            {
                Console.WriteLine($"Score {serach} not found in the array");
            }
        }
        static void reverseArray(int[] scores)
        {
            var left = 0;
            var right = scores.Length - 1;
            while (left < right)
            {
                var temp = scores[left];
                scores[left] = scores[right];
                scores[right] = temp;
                left++;
                right--;
            }
        }
        static double CalculateAverage(int[] scores)
        {
            scores.OrderBy(c=>c);
            var total = 0;
            foreach (int var in scores)
            {
                total += var;
            }
            return (double)total / scores.Length;
        }
    }
}