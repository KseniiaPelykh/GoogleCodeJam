using System;
using System.Collections.Generic;

public class SquareDance
{
    public class Dancer
    {
        public string Id {get;}
        public int Value { get; }
        public Dancer RightCompetitor { get; set; }
        public Dancer LeftCompetitor { get; set; }
        public Dancer AboveCompetitor { get; set; }
        public Dancer BelowCompetitor { get; set; }

        public Dancer(string id, int value)
        {
            Id = id;
            Value = value;
        }

        public decimal GetCompetitorsSkillLevel()
        {
            var sum = 0;
            var count = 0;
            if (RightCompetitor != null)
            {
                sum += RightCompetitor.Value;
                count++;
            }

            if (LeftCompetitor != null)
            {
                sum += LeftCompetitor.Value;
                count++;
            }

            if (AboveCompetitor != null)
            {
                sum += AboveCompetitor.Value;
                count++;
            }

            if (BelowCompetitor != null)
            {
                sum += BelowCompetitor.Value;
                count++;
            }

            return count != 0 ? (decimal)sum / count : 0;
        }
    }

    public static void Main(string[] args)
    {
        var testCases = int.Parse(Console.ReadLine());
        var results = new HashSet<string>();

        for (var t = 0; t < testCases; t++)
        {
            var values = Array.ConvertAll(Console.ReadLine().Split(' '), x => int.Parse(x));
            var rows = values[0];
            var colums = values[1];

            var dancers = new Dancer[rows, colums];
            var dancersIds = new HashSet<string>();

            long result = 0;
            long sum = 0;
            for (var i = 0; i < rows; i++)
            {
                var numbers = Array.ConvertAll(Console.ReadLine().Split(' '), x => int.Parse(x));

                for (var j = 0; j < colums; j++)
                {
                    var dancer = new Dancer($"{i}_{j}", numbers[j]);
                    dancersIds.Add(dancer.Id);
                    if (i > 0)
                    {
                        var aboveCompetitor = dancers[i - 1, j];
                        aboveCompetitor.BelowCompetitor = dancer;
                        dancer.AboveCompetitor = aboveCompetitor;
                    }

                    if (j > 0)
                    {
                        var leftCompetitor = dancers[i, j - 1];
                        leftCompetitor.RightCompetitor = dancer;
                        dancer.LeftCompetitor = leftCompetitor;
                    }

                    dancers[i, j] = dancer;
                    sum += dancer.Value;
                }
            }

            while (dancersIds.Count > 0)
            {
                var dancersIdsToRemove = new List<string>();
                result += sum;

                foreach (var dancerId in dancersIds)
                {
                    var coordinates = Array.ConvertAll(dancerId.Split('_'), x => int.Parse(x));
                    var dancer = dancers[coordinates[0], coordinates[1]];

                    if ((decimal)dancer.Value < dancer.GetCompetitorsSkillLevel())
                    {
                        dancersIdsToRemove.Add(dancerId);
                        sum -= dancer.Value;
                    }
                };

                dancersIds = new HashSet<string>();
        
                foreach (var dancerId in dancersIdsToRemove)
                {
                    var coordinates = Array.ConvertAll(dancerId.Split('_'), x => int.Parse(x));
                    var dancer = dancers[coordinates[0], coordinates[1]];
                    
                    dancersIds.Remove(dancerId);
                    if (dancer.AboveCompetitor != null)
                    {
                        dancer.AboveCompetitor.BelowCompetitor = dancer.BelowCompetitor;
                        dancersIds.Add(dancer.AboveCompetitor.Id);
                    };

                    if (dancer.BelowCompetitor != null)
                    {
                        dancer.BelowCompetitor.AboveCompetitor = dancer.AboveCompetitor;
                        dancersIds.Add(dancer.BelowCompetitor.Id);
                    };

                    if (dancer.RightCompetitor != null)
                    {
                        dancer. RightCompetitor.LeftCompetitor = dancer.LeftCompetitor;
                        dancersIds.Add(dancer.RightCompetitor.Id);
                    }

                    if (dancer.LeftCompetitor != null)
                    {
                        dancer.LeftCompetitor.RightCompetitor = dancer.RightCompetitor;
                        dancersIds.Add(dancer.LeftCompetitor.Id);
                    }
                }
            }

            results.Add($"Case #{t + 1}: {result}");
        }

        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
}