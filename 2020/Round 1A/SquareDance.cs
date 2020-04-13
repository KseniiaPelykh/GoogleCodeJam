using System;
using System.Collections.Generic;

public class SquareDance
{
    public enum CompetitorType
    {
        Right, Left, Above, Below
    }

    public static int[] CompetitorMirror = new int[4] {1, 0, 3, 2};

    public class Dancer
    {
        public string Id { get; }
        public int Value { get; }
        public Dancer[] Competitors { get; }

        public Dancer(string id, int value)
        {
            Id = id;
            Value = value;
            Competitors = new Dancer[4];
        }

        public decimal GetCompetitorsSkillLevel()
        {
            var sum = 0;
            var count = 0;

            for (var i = 0; i < 4; i++)
            {
                if (Competitors[i] != null)
                {
                    sum += Competitors[i].Value;
                    count++;
                }
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
                        aboveCompetitor.Competitors[(int)CompetitorType.Below] = dancer;
                        dancer.Competitors[(int)CompetitorType.Above] = aboveCompetitor;
                    }

                    if (j > 0)
                    {
                        var leftCompetitor = dancers[i, j - 1];
                        leftCompetitor.Competitors[(int)CompetitorType.Right] = dancer;
                        dancer.Competitors[(int)CompetitorType.Left] = leftCompetitor;
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

                    for (var i = 0; i < 4; i++)
                    {
                        if (dancer.Competitors[i] != null)
                        {
                            dancer.Competitors[i].Competitors[CompetitorMirror[i]] = dancer.Competitors[CompetitorMirror[i]];
                            dancersIds.Add(dancer.Competitors[i].Id);
                        }
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