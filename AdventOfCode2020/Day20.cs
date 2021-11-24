using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{
    public class Day20 : BaseDay
    {
        public Day20()
        {
            Day = "20";
            Answer1 = "0";
            Answer2 = "0";
        }


        class Tile
        {
            public int number;
            public char[,] tile = new char[10, 10];

            public List<char[,]> tilesRotatesFlips = new List<char[,]>();

            public bool InUse = false;

            void printMatrix(char[,] arr)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                        Console.Write(arr[i, j] + " ");
                    Console.Write("\n");
                }

                Console.Write("\n");
            }

            public void CalculateRotatesAndFlips()
            {
                //rotate 4
                tilesRotatesFlips.Add(tile);
                tilesRotatesFlips.Add(Rotate(tile));
                tilesRotatesFlips.Add(Rotate(Rotate(tile)));
                tilesRotatesFlips.Add(Rotate(Rotate(Rotate(tile))));
                //flip X rotate
                tilesRotatesFlips.Add(FlipX(tile));
                tilesRotatesFlips.Add(FlipX(Rotate(tile)));
                tilesRotatesFlips.Add(FlipX(Rotate(Rotate(tile))));
                tilesRotatesFlips.Add(FlipX(Rotate(Rotate(Rotate(tile)))));
                //flip Y rotate
                tilesRotatesFlips.Add(FlipY(tile));
                tilesRotatesFlips.Add(FlipY(Rotate(tile)));
                tilesRotatesFlips.Add(FlipY(Rotate(Rotate(tile))));
                tilesRotatesFlips.Add(FlipY(Rotate(Rotate(Rotate(tile)))));
                //flip X/Y rotate
                tilesRotatesFlips.Add(FlipY(FlipX(tile)));
                tilesRotatesFlips.Add(FlipY(FlipX(Rotate(tile))));
                tilesRotatesFlips.Add(FlipY(FlipX(Rotate(Rotate(tile)))));
                tilesRotatesFlips.Add(FlipY(FlipX(Rotate(Rotate(Rotate(tile))))));
            }

            public char[,] Rotate(char[,] tile)
            {
                char[,] output = new char[10, 10];

                for (int i = 0; i < 10 / 2; i++)
                {
                    for (int j = 0; j < 10 - i - 1; j++)
                    {
                        char temp = tile[i, j];
                        output[i, j] = tile[10 - 1 - j, i];
                        output[10 - 1 - j, i] = tile[10 - 1 - i, 10 - 1 - j];
                        output[10 - 1 - i, 10 - 1 - j] = tile[j, 10 - 1 - i];
                        output[j, 10 - 1 - i] = temp;
                    }
                }

                return output;
            }

            public char[,] FlipX(char[,] tile)
            {
                char[,] output = new char[10, 10];

                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        output[9 - x, y] = tile[x, y];
                    }
                }

                return output;
            }

            public char[,] FlipY(char[,] tile)
            {
                char[,] output = new char[10, 10];

                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        output[x, 9 - y] = tile[x, y];
                    }
                }

                return output;
            }

        }

        protected override string Solution1(string[] input)
        {

            List<Tile> tiles = ParseData(input);
            foreach (var item in tiles)
                item.CalculateRotatesAndFlips();


            int arraySize = 0;

            for (int i = 0; i < tiles.Count; i++)
            {
                arraySize = i;
                if (i * i == tiles.Count)
                    break;
            }

            //take the first tile
            //try matches with another tile all differtent rotations and flips
            //if one is found continue
            //if not found backtrack

            int tileSize = 10;

            char[,] map = new char[arraySize * tileSize, arraySize * tileSize];
            
            




            return "-1";
        }

        protected override string Solution2(string[] input)
        {
            List<Tile> tiles = ParseData(input);

            return "-1";
        }

        private List<Tile> ParseData(string[] input)
        {
            List<Tile> tiles = new List<Tile>();

            Tile tile = new Tile();
            int index = 0;
            foreach (var item in input)
            {
                if (item.Contains("Tile"))
                {
                    tile = new Tile();
                    index = 0;

                    tile.number = int.Parse(item.Replace("Tile ", "").Replace(":", ""));
                }
                else if (item.Length == 0)
                {
                    tiles.Add(tile);
                }
                else
                {
                    for (int i = 0; i < item.Length; i++)
                    {
                        tile.tile[index, i] = item[i];
                    }
                    index++;
                }
            }

            return tiles;
        }
    }
}
