using System;
using System.Collections.Generic;
using System.Linq;

public class PascalWalk
{
    public static void Main(string[] args)
    {
        var testCases = int.Parse(Console.ReadLine());
        var results = new List<string>();

        for (var t = 0; t < testCases; t++)
        {
            long n = long.Parse(Console.ReadLine());
            results.Add($"Case #{t + 1}:");
            
            var h = (int) Math.Log(n, 2);

            var currentResults = new List<string>();
            var left = true;

            for (var i = h + 1; i > 0; i--)
            {
                var currentRowSum = (long)Math.Pow(2, i - 1);
                if (n - currentRowSum - i - 1 >= 0)
                {
                    n -= currentRowSum;
                    var steps = left 
                        ? Enumerable.Range(1, i).Select(x => $"{i} {x}")
                        : Enumerable.Range(1, i).Select(x => $"{i} {x}").Reverse();

                    currentResults.AddRange(steps);
                    left = !left;
                }
                else
                {
                    n -= 1;
                    var step = left ? $"{i} {1}" : $"{i} {i}";
                    currentResults.Add(step);
                }
            }

            currentResults.Reverse();
            results.AddRange(currentResults);

            for (var i = 1; i <= n; i++)
            {
                results.Add($"{h + i + 1} 1");
            }
        }

        foreach (var elem in results)
        {
            Console.WriteLine(elem);
        }
    }
}
