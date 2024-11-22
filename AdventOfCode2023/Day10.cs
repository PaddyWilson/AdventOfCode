using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day10 : BaseDay
    {
        public Day10()
        {
            Day = "10";
            Answer1 = "6800";
            Answer2 = "";
        }
        protected override string Solution1(string[] input)
        {
            char[,] map = new char[input.Length + 2, input[0].Length + 2];
            int[,] distance = new int[input.Length + 2, input[0].Length + 2];

            (int, int) startPosition = (0, 0);
            for (int x = 1; x <= input.Length; x++)
            {
                for (int y = 1; y <= input[0].Length; y++)
                {
                    map[x, y] = input[x - 1][y - 1];
                    distance[x, y] = -1;
                    if (map[x, y] == 'S')
                        startPosition = (x, y);
                }
            }

            Queue<(int, int)> nextPoint = new();
            nextPoint.Enqueue(startPosition);

            while (nextPoint.Count > 0)
            {
                var point = nextPoint.Dequeue();
                char tile = map[point.Item1, point.Item2];

                int x = point.Item1;
                int y = point.Item2;

                (int, int) next = (-1, -1);

                switch (tile)
                {
                    case '|':
                        //down
                        if (distance[x + 1, y] == -1)
                        {
                            next = (x + 1, y);
                            distance[x, y] = distance[x - 1, y] + 1;
                        }
                        //up
                        else if (distance[x - 1, y] == -1)
                        {
                            next = (x - 1, y);
                            distance[x, y] = distance[x + 1, y] + 1;
                        }
                        break;
                    case '-':
                        //right
                        if (distance[x, y + 1] == -1)
                        {
                            next = (x, y + 1);
                            distance[x, y] = distance[x, y - 1] + 1;
                        }
                        //left
                        else if (distance[x, y - 1] == -1)
                        {
                            next = (x, y - 1);
                            distance[x, y] = distance[x, y + 1] + 1;
                        }
                        break;
                    case 'L':
                        //up
                        if (distance[x - 1, y] == -1)
                        {
                            next = (x - 1, y);
                            distance[x, y] = distance[x, y + 1] + 1;
                        }
                        //right
                        else if (distance[x, y + 1] == -1)
                        {
                            next = (x, y + 1);
                            distance[x, y] = distance[x - 1, y] + 1;
                        }
                        break;
                    case 'J':
                        //up
                        if (distance[x - 1, y] == -1)
                        {
                            next = (x - 1, y);
                            distance[x, y] = distance[x, y - 1] + 1;
                        }
                        //left
                        else if (distance[x, y - 1] == -1)
                        {
                            next = (x, y - 1);
                            distance[x, y] = distance[x - 1, y] + 1;
                        }
                        break;
                    case '7':
                        //down
                        if (distance[x + 1, y] == -1)
                        {
                            next = (x + 1, y);
                            distance[x, y] = distance[x, y - 1] + 1;
                        }
                        //left
                        else if (distance[x, y - 1] == -1)
                        {
                            next = (x, y - 1);
                            distance[x, y] = distance[x + 1, y] + 1;
                        }
                        break;
                    case 'F':
                        //down
                        if (distance[x + 1, y] == -1)
                        {
                            next = (x + 1, y);
                            distance[x, y] = distance[x, y + 1] + 1;
                        }
                        //right
                        else if (distance[x, y + 1] == -1)
                        {
                            next = (x, y + 1);
                            distance[x, y] = distance[x + 1, y] + 1;
                        }
                        break;
                    case '.':

                        break;
                    case 'S':
                        distance[x, y] = 0;
                        //find possible connecteed pipes

                        //up
                        if ("|7F".Contains(map[x - 1, y]))
                            nextPoint.Enqueue((x - 1, y));
                        //down
                        if ("|LJ".Contains(map[x + 1, y]))
                            nextPoint.Enqueue((x + 1, y));
                        //left
                        if ("-FL".Contains(map[x, y - 1]))
                            nextPoint.Enqueue((x, y - 1));
                        //right
                        if ("-J7".Contains(map[x, y + 1]))
                            nextPoint.Enqueue((x, y + 1));
                        break;
                    default:
                        Console.WriteLine("Should not happen");
                        break;
                }

                if (next != (-1, -1))
                    nextPoint.Enqueue(next);
            }


            //find farthest distance
            int output = 0;
            for (int x = 1; x <= input.Length; x++)
                for (int y = 1; y <= input[0].Length; y++)
                    if (distance[x, y] > output)
                        output = distance[x, y];

            //add 1 because the distances are calculated do not go all the way to the furthest pipe section
            return (output + 1).ToString();
        }

        enum OutsideDirection { UP, DOWN, LEFT, RIGHT };
        protected override string Solution2(string[] input)
        {
            char[,] map = new char[input.Length + 2, input[0].Length + 2];
            int[,] distance = new int[input.Length + 2, input[0].Length + 2];

            (int, int) startPosition = (0, 0);
            for (int x = 1; x <= input.Length; x++)
            {
                for (int y = 1; y <= input[0].Length; y++)
                {
                    map[x, y] = input[x - 1][y - 1];
                    distance[x, y] = 0;
                    if (map[x, y] == 'S')
                        startPosition = (x, y);
                }
            }

            //Queue<(int, int, OutsideDirection)> nextPoint = new();

            //(int, int) previousPoint;
            Queue<(int, int)> nextPoint = new();
            //just assume left is outside
            nextPoint.Enqueue((startPosition.Item1, startPosition.Item2));
            //nextPoint.Enqueue((startPosition.Item1, startPosition.Item2, OutsideDirection.LEFT));

            List<(int, int, OutsideDirection)> path = new();
            path.Add(((startPosition.Item1, startPosition.Item2, OutsideDirection.LEFT)));

            while (nextPoint.Count > 0)
            {
                var point = nextPoint.Dequeue();
                char tile = map[point.Item1, point.Item2];

                int x = point.Item1;
                int y = point.Item2;

                distance[x, y] = 1;

                (int, int) next = (-1, -1);
                switch (tile)
                {
                    case '|':
                        //down
                        if (distance[x + 1, y] == 0) { 
                            next = (x + 1, y);}
                        //up
                        else if (distance[x - 1, y] == 0)
                            next = (x - 1, y);
                        break;
                    case '-':
                        //right
                        if (distance[x, y + 1] == 0)
                            next = (x, y + 1);
                        //left
                        else if (distance[x, y - 1] == 0)
                            next = (x, y - 1);
                        break;
                    case 'L':
                        //up
                        if (distance[x - 1, y] == 0)
                            next = (x - 1, y);
                        //right
                        else if (distance[x, y + 1] == 0)
                            next = (x, y + 1);
                        break;
                    case 'J':
                        //up
                        if (distance[x - 1, y] == 0)
                            next = (x - 1, y);
                        //left
                        else if (distance[x, y - 1] == 0)
                            next = (x, y - 1);
                        break;
                    case '7':
                        //down
                        if (distance[x + 1, y] == 0)
                            next = (x + 1, y);
                        //left
                        else if (distance[x, y - 1] == 0)
                            next = (x, y - 1);
                        break;
                    case 'F':
                        //down
                        if (distance[x + 1, y] == 0)
                            next = (x + 1, y);
                        //right
                        else if (distance[x, y + 1] == 0)
                            next = (x, y + 1);
                        break;
                    case '.':

                        break;
                    case 'S':
                        distance[x, y] = 1;
                        //find possible connecteed pipes
                        //up
                        if ("|7F".Contains(map[x - 1, y]))
                        {
                            nextPoint.Enqueue((x - 1, y));
                        }
                        //down
                        else if ("|LJ".Contains(map[x + 1, y]))
                        {
                            nextPoint.Enqueue((x + 1, y));
                        }
                        //left
                        else if ("-FL".Contains(map[x, y - 1]))
                        {
                            nextPoint.Enqueue((x, y - 1));
                        }
                        //right
                        else if ("-J7".Contains(map[x, y + 1]))
                        {
                            nextPoint.Enqueue((x, y + 1));
                        }
                        break;
                    default:
                        Console.WriteLine("Should not happen");
                        break;
                }

                if (next != (-1, -1))
                    nextPoint.Enqueue(next);
            }


            //find farthest distance
            int output = 0;
            for (int x = 1; x <= input.Length; x++)
            {
                Console.WriteLine();
                for (int y = 1; y <= input[0].Length; y++)
                {
                    Console.Write(distance[x, y]);
                    if (distance[x, y] > output)
                        output = distance[x, y];
                }
            }

            //add 1 because the distances are calculated do not go all the way to the furthest pipe section
            return (output + 1).ToString();
        }
    }
}
