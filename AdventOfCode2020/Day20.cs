using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
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
			Answer2 = "2424";
		}

		class Tile
		{
			public int number { get; set; }
			public char[,] baseTile { get; set; }

			int arraySize = 10;
			public List<char[,]> FlipRotated { get; }

			public int CurrentIndex { get; private set; }
			public bool InUse { get; set; }

			public Tile()
			{
				baseTile = new char[arraySize, arraySize];
				CurrentIndex = 0;
				FlipRotated = new List<char[,]>();
			}

			public Tile(int number, char[,] array, int arraySize)
			{
				this.number = number;
				this.baseTile = array;
				this.arraySize = arraySize;

				this.CurrentIndex = 0;
				FlipRotated = new List<char[,]>();
			}

			public char[,] GetNext()
			{
				CurrentIndex++;
				if (CurrentIndex == FlipRotated.Count)
					CurrentIndex = 0;
				return FlipRotated[CurrentIndex];
			}

			public char[,] GetCurrent()
			{
				return FlipRotated[CurrentIndex];
			}

			public int Count()
			{
				return FlipRotated.Count;
			}

			public char[,] GetTileRemovedBoarders()
			{
				char[,] output = new char[arraySize - 2, arraySize - 2];

				var temp = GetCurrent();
				for (int x = 1; x < arraySize - 1; x++)
				{
					for (int y = 1; y < arraySize - 1; y++)
					{
						output[x - 1, y - 1] = temp[x, y];
					}
				}
				return output;
			}

			public void CalculateRotatesAndFlips()
			{
				//rotate 4
				FlipRotated.Add(baseTile);
				FlipRotated.Add(Helpers.Rotate(FlipRotated[FlipRotated.Count - 1], arraySize));
				FlipRotated.Add(Helpers.Rotate(FlipRotated[FlipRotated.Count - 1], arraySize));
				FlipRotated.Add(Helpers.Rotate(FlipRotated[FlipRotated.Count - 1], arraySize));

				//flip X rotate
				FlipRotated.Add(Helpers.FlipX(baseTile, arraySize));
				FlipRotated.Add(Helpers.Rotate(FlipRotated[FlipRotated.Count - 1], arraySize));
				FlipRotated.Add(Helpers.Rotate(FlipRotated[FlipRotated.Count - 1], arraySize));
				FlipRotated.Add(Helpers.Rotate(FlipRotated[FlipRotated.Count - 1], arraySize));
			}

			public void PrintTile()
			{
				Helpers.PrintMatrix(GetCurrent(), arraySize, "");
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

			//generate a map from the tiles with boarders removed
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

				}
			}

			//count # char after removing sea monsters and removing tile boarders

			//search for sea monsters and remove # from map
			//contains 15 # chars
			string[] seaMonster = new string[] {
				"                  # ",
				"#    ##    ##    ###",
				" #  #  #  #  #  #   "
			};

			int monSegments = 15;//the amount of # chars in monster
			int monLength = seaMonster[0].Length;
			int monHeight = seaMonster.Length;

			int monCount = 0;
			Tile tileMap = new Tile(0, map, mapSize);
			tileMap.CalculateRotatesAndFlips();

			char[,] highestMonMap = new char[mapSize, mapSize];
			int monCountHeighest = 0;

			foreach (var item in tileMap.FlipRotated)
			{
				monCount = 0;
				char[,] tempMap = tileMap.GetNext();

				//loop through the map and find monsters
				for (int x = 0; x < mapSize - monHeight + 1; x++)
				{
					for (int y = 0; y < mapSize - monLength + 1; y++)
					{
						if (FindSeaMonster(tempMap, seaMonster, x, y, mapSize) == 15)
						{
							monCount++;
						}
					}
				}

				//select the map with the highest monster count
				if (monCount > monCountHeighest)
				{
					monCountHeighest = monCount;
					highestMonMap = tempMap;
				}
			}

			//count '#' chars for final output
			int count = 0;
			for (int x = 0; x < mapSize; x++)
			{
				for (int y = 0; y < mapSize; y++)
				{
					if (highestMonMap[x, y] == '#')
						count++;
				}
			}
			return count.ToString();
		}

		private int FindSeaMonster(char[,] map, string[] monster, int x, int y, int mapSize, char replacementChar = 'O')
		{
			List<(int, int)> changed = new List<(int, int)>();
			for (int j = 0; j < monster.Length; j++)
			{
				for (int k = 0; k < monster[0].Length; k++)
				{
					if (monster[j][k] == '#' && map[x + j, y + k] == '#')
					{
						changed.Add((x + j, y + k));
					}
				}
			}

			if (changed.Count == 15)
			{
				foreach (var coords in changed)
				{
					map[coords.Item1, coords.Item2] = replacementChar;
				}
			}

			return changed.Count;
		}

		private bool TestTile(Tile[,] ids, List<Tile> tiles, int arraySize, int tileSize, int x, int y)
		{
			//breakout condition
			if (x == arraySize)
				return true;

			foreach (var tile in tiles)
			{
				if (tile.InUse)
					continue;
				ids[x, y] = tile;

				for (int i = 0; i < tile.Count(); i++)
				{
					tile.GetNext();
					int validPlace = 0;

					//left
					if ((y - 1) < 0 || (ids[x, y - 1] == null || TestLeft(tile.GetCurrent(), ids[x, y - 1].GetCurrent(), tileSize)))
					{
						validPlace++;
					}//right
					if ((y + 1) >= arraySize || (ids[x, y + 1] == null || TestRight(tile.GetCurrent(), ids[x, y + 1].GetCurrent(), tileSize)))
					{
						validPlace++;
					}//top
					if ((x - 1) < 0 || (ids[x - 1, y] == null || TestTop(tile.GetCurrent(), ids[x - 1, y].GetCurrent(), tileSize)))
					{
						validPlace++;
					}//bottom
					if ((x + 1) >= arraySize || (ids[x + 1, y] == null || TestBottom(tile.GetCurrent(), ids[x + 1, y].GetCurrent(), tileSize)))
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
						//return tile to unused pool
						tile.InUse = false;
					}

				}

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
