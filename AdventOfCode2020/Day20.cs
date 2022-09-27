using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
			Answer1 = "13983397496713";
			Answer2 = "0";
		}


		class Tile
		{
			public int number;
			public char[,] baseTile = new char[10, 10];

			int tileSize = 10;

			public List<char[,]> tilesRotatesFlips { get; }

			int currentIndex = 0;
			public bool InUse = false;

			public Tile()
			{
				tilesRotatesFlips = new List<char[,]>();
			}

			public Tile(int size)
			{
				tilesRotatesFlips = new List<char[,]>();
				baseTile = new char[size, size];
				tileSize = size;
			}

			public char[,] GetNext()
			{
				currentIndex++;
				if (currentIndex == tilesRotatesFlips.Count)
					currentIndex = 0;
				char[,] output = tilesRotatesFlips[currentIndex];

				return output;
			}

			public char[,] GetCurrent()
			{
				return tilesRotatesFlips[currentIndex];
			}

			public int Count()
			{
				return tilesRotatesFlips.Count;
			}

			public char[,] GetTileRemovedBoarders()
			{
				char[,] output = new char[tileSize - 2, tileSize - 2];

				var temp = GetCurrent();
				for (int x = 1; x < tileSize - 1; x++)
				{
					for (int y = 1; y < tileSize - 1; y++)
					{
						output[x - 1, y - 1] = temp[x, y];
					}
				}
				return output;
			}

			public void CalculateRotatesAndFlips()
			{
				//rotate 4
				tilesRotatesFlips.Add(baseTile);
				tilesRotatesFlips.Add(Helpers.Rotate(baseTile, tileSize));
				tilesRotatesFlips.Add(Helpers.Rotate(Helpers.Rotate(baseTile, tileSize), tileSize));
				tilesRotatesFlips.Add(Helpers.Rotate(Helpers.Rotate(Helpers.Rotate(baseTile, tileSize), tileSize), tileSize));
				//flip X rotate
				tilesRotatesFlips.Add(Helpers.FlipX(baseTile, tileSize));
				tilesRotatesFlips.Add(Helpers.FlipX(Helpers.Rotate(baseTile, tileSize), tileSize));
				tilesRotatesFlips.Add(Helpers.FlipX(Helpers.Rotate(Helpers.Rotate(baseTile, tileSize), tileSize), tileSize));
				tilesRotatesFlips.Add(Helpers.FlipX(Helpers.Rotate(Helpers.Rotate(Helpers.Rotate(baseTile, tileSize), tileSize), tileSize), tileSize));
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

			int tileSize = 10;

			Tile[,] ids = new Tile[arraySize, arraySize];
			TestTile(ids, tiles, arraySize, tileSize, 0, 0);

			//count corners
			ulong corners = (ulong)ids[0, 0].number;
			corners *= (ulong)ids[0, arraySize - 1].number;
			corners *= (ulong)ids[arraySize - 1, 0].number;
			corners *= (ulong)ids[arraySize - 1, arraySize - 1].number;

			return corners.ToString();
		}

		string Reverse(string s)
		{
			char[] charArray = s.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}

		protected override string Solution2(string[] input)
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

			int tileSize = 10;

			Tile[,] ids = new Tile[arraySize, arraySize];
			TestTile(ids, tiles, arraySize, tileSize, 0, 0);

			int mapSize = (tileSize - 2) * arraySize;
			char[,] map = new char[mapSize, mapSize];

			//count # char after removing sea monsters and removing tile boarders

			//generate a full array of the tiles tile
			for (int x = 0; x < arraySize; x++)
			{
				for (int y = 0; y < arraySize; y++)
				{
					Tile currentTile = ids[x, y];

					int xstart = x * (tileSize - 2);

					char[,] temp = currentTile.GetTileRemovedBoarders();

					for (int x2 = 0; x2 < tileSize - 2; x2++)
					{
						int ystart = y * (tileSize - 2);
						for (int y2 = 0; y2 < tileSize - 2; y2++)
						{
							map[xstart, ystart] = temp[x2, y2];
							ystart++;
						}
						xstart++;
					}

					Console.WriteLine(currentTile.number);
					Helpers.PrintMatrix(currentTile.GetCurrent(), tileSize);
					Console.WriteLine();
					Helpers.PrintMatrix(map, mapSize);
					int asdfasdf = 0;
				}
			}

			//foreach (var item in tiles[0].tilesRotatesFlips)
			//{
			//	Console.WriteLine();
			//	Helpers.PrintMatrix(item, 10);
			//}


			//remove and search for sea monsters
			//contains 15 # chars
			string[] seaMonster = new string[] {
				"                  # ",
				"#    ##    ##    ###",
				" #  #  #  #  #  #   "
			};

			int monLength = seaMonster[0].Length;
			int monHeight = seaMonster.Length;


			int monCount = 0;
			int rotateCount = 0;

			char[,] tempMap = new char[mapSize, mapSize];
			int count2 = 0;

			Tile tileMap = new Tile(mapSize);
			tileMap.baseTile = map;
			tileMap.CalculateRotatesAndFlips();

			foreach (var item in tileMap.tilesRotatesFlips)
			{
				tempMap = tileMap.GetNext();
				//Console.WriteLine(count2);
				count2++;
				//tempMap = new char[mapSize, mapSize];
				Array.Copy(map, tempMap, mapSize * mapSize);

				//Console.WriteLine();
				//Helpers.PrintMatrix(map, mapSize);


				//the map does not seem to be correct 
				//double check it generated correct


				//int asdf = 0;

				//Helpers.PrintMatrix(tempMap, mapSize);
				for (int x = 0; x < mapSize - monHeight+1; x++)
				{
					for (int y = 0; y < mapSize - monLength+1; y++)
					{
						List<(int, int)> changed = new List<(int, int)>();
						//int matchingChars = 0;
						for (int j = 0; j < monHeight; j++)
						{
							for (int k = 0; k < monLength; k++)
							{
								if (seaMonster[j][k] == '#' && tempMap[x + j, y + k] == '#')
								{
									changed.Add((x + j, y + k));
									//tempMap[x + j, y + k] = 'O';
									//matchingChars++;
								}
							}
						}

						//Console.WriteLine(matchingChars);
						if (changed.Count == 15)
						{
							foreach (var coords in changed)
							{
								tempMap[coords.Item1, coords.Item2] = 'O';
							}
							monCount++;
						}
					}
				}


				//Console.WriteLine("rotate");
				//Helpers.PrintMatrix(tempMap, mapSize);
				//if (monCount == 0 && rotateCount < 4)
				//{					
				//	map = Helpers.Rotate(map, mapSize);					
				////	rotateCount++;
				//}
				//else
				//{
				//	map = Helpers.FlipX(map, mapSize);
				//	rotateCount = 0;
				//}
				//Console.WriteLine();
				//Helpers.PrintMatrix(map, mapSize);
			}

			//count '#' chars for final output
			int count = 0;

			for (int x = 0; x < mapSize; x++)
			{
				for (int y = 0; y < mapSize; y++)
				{
					if (tempMap[x, y] == '#')
						count++;
				}
			}
			return count.ToString();
		}

		private int FindSeaMonsters(char[,] map, char[,] monster, int mapSize)
		{
			return 0;
		}

		private bool TestTile(Tile[,] ids, List<Tile> tiles, int arraySize, int tileSize, int x, int y)
		{
			//Console.WriteLine("st " + x + ":" + y);

			if (x == arraySize)
				return true;

			foreach (var tile in tiles)
			{
				if (tile.InUse)
					continue;
				//Console.WriteLine("in " + x + ":" + y);
				ids[x, y] = tile;
				//tile.InUse = true;

				for (int i = 0; i < tile.Count(); i++)
				{
					tile.GetNext();
					int validPlace = 0;

					//top
					if ((y - 1) < 0 || (ids[x, y - 1] == null || TestTop(tile.GetCurrent(), ids[x, y - 1].GetCurrent(), tileSize)))
					{
						validPlace++;
					}//bottom
					if ((y + 1) >= arraySize || (ids[x, y + 1] == null || TestBottom(tile.GetCurrent(), ids[x, y + 1].GetCurrent(), tileSize)))
					{
						validPlace++;
					}//left
					if ((x - 1) < 0 || (ids[x - 1, y] == null || TestLeft(tile.GetCurrent(), ids[x - 1, y].GetCurrent(), tileSize)))
					{
						validPlace++;
					}//right
					if ((x + 1) >= arraySize || (ids[x + 1, y] == null || TestRight(tile.GetCurrent(), ids[x + 1, y].GetCurrent(), tileSize)))
					{
						validPlace++;
					}

					if (validPlace == 4)
					{
						bool returnValid = false;
						tile.InUse = true;
						//move onto next tile
						if (y < arraySize - 1)
						{
							returnValid = TestTile(ids, tiles, arraySize, tileSize, x, y + 1);
						}
						else
						{
							returnValid = TestTile(ids, tiles, arraySize, tileSize, x + 1, 0);
						}

						//should be the break out condition
						if (returnValid)
							return returnValid;
						tile.InUse = false;
					}

				}
				//tile.ResetIndex();
				//return tile to unused pool
				//tile.InUse = false;
				ids[x, y] = null;
			}

			return false;
		}

		bool TestTop(char[,] item1, char[,] item2, int tileSize)
		{
			int matchCount = 0;
			for (int i = 0; i < tileSize; i++)
			{
				if (item1[0, i] == item2[tileSize - 1, i])
					matchCount++;
			}
			if (matchCount == tileSize)
				return true;
			return false;
		}

		bool TestBottom(char[,] item1, char[,] item2, int tileSize)
		{
			int matchCount = 0;
			for (int i = 0; i < tileSize; i++)
			{
				if (item1[tileSize - 1, i] == item2[0, i])
					matchCount++;
			}
			if (matchCount == tileSize)
				return true;
			return false;
		}

		bool TestLeft(char[,] item1, char[,] item2, int tileSize)
		{
			int matchCount = 0;
			for (int i = 0; i < tileSize; i++)
			{
				if (item1[i, 0] == item2[i, tileSize - 1])
					matchCount++;
			}
			if (matchCount == tileSize)
				return true;
			return false;
		}

		bool TestRight(char[,] item1, char[,] item2, int tileSize)
		{
			int matchCount = 0;
			for (int i = 0; i < tileSize; i++)
			{
				if (item1[i, tileSize - 1] == item2[i, 0])
					matchCount++;
			}
			if (matchCount == tileSize)
				return true;
			return false;
		}

		private List<Tile> ParseData(string[] input)
		{
			List<Tile> tiles = new List<Tile>();

			Tile tile = new Tile();
			int index = 0;
			foreach (var item in input)
			{
				//start of new tile
				if (item.Contains("Tile"))
				{
					tile = new Tile();
					index = 0;

					tile.number = int.Parse(item.Replace("Tile ", "").Replace(":", ""));
				}
				//at end of tile, add to list
				else if (item.Length == 0)
				{
					tiles.Add(tile);
				}
				else
				{
					for (int i = 0; i < item.Length; i++)
					{
						tile.baseTile[index, i] = item[i];
					}
					index++;
				}
			}

			return tiles;
		}
	}
}
