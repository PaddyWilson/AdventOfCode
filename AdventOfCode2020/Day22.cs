using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{
	public class Day22 : BaseDay
	{
		public Day22()
		{
			Day = "22";
			Answer1 = "33694";
			Answer2 = "31835";
		}

		protected override string Solution1(string[] input)
		{
			Queue<int> player1 = new Queue<int>();
			Queue<int> player2 = new Queue<int>();

			ProcessInput(player1, player2, input);

			bool running = true;
			while (running)
			{
				int one = player1.Dequeue();
				int two = player2.Dequeue();

				if (one > two)
				{
					player1.Enqueue(one);
					player1.Enqueue(two);
				}
				else if (one < two)
				{
					player2.Enqueue(two);
					player2.Enqueue(one);
				}

				if (player1.Count == 0 || player2.Count == 0)
					running = false;
			}

			if (player1.Count > 0)
				return CountCards(player1).ToString();
			else
				return CountCards(player2).ToString();
		}

		protected override string Solution2(string[] input)
		{
			Queue<int> player1 = new Queue<int>();
			Queue<int> player2 = new Queue<int>();

			ProcessInput(player1, player2, input);

			int win = PlayGameRecursive(player1, player2);

			if (player1.Count > 0)
				return CountCards(player1).ToString();
			else
				return CountCards(player2).ToString();
		}

		private int PlayGameRecursive(Queue<int> player1, Queue<int> player2)
		{
			//List<string> hashOne = new List<string>();
			//List<string> hashTwo = new List<string>();

			HashSet<string> hashOne = new HashSet<string>();
			HashSet<string> hashTwo = new HashSet<string>();

			while (true)
			{
				string hashOneTemp = string.Join(',', player1);
				string hashTwoTemp = string.Join(',', player2);

				if (hashOne.Contains(hashOneTemp) && hashTwo.Contains(hashTwoTemp))
				{
					return 1;
				}

				hashOne.Add(hashOneTemp);
				hashTwo.Add(hashTwoTemp);

				int card1 = player1.Dequeue();
				int card2 = player2.Dequeue();

				int win = 0;

				if (card1 <= player1.Count && card2 <= player2.Count)
				{
					var newHand1 = new int[card1];
					var newHand2 = new int[card2];
					Array.Copy(player1.ToArray(), newHand1, card1);
					Array.Copy(player2.ToArray(), newHand2, card2);

					win = PlayGameRecursive(new Queue<int>(newHand1), new Queue<int>(newHand2));
				}
				else if (card1 > card2)
				{
					win = 1;
				}
				else if (card1 < card2)
				{
					win = 2;
				}

				if (win == 1)
				{
					player1.Enqueue(card1);
					player1.Enqueue(card2);
				}
				else if (win == 2)
				{
					player2.Enqueue(card2);
					player2.Enqueue(card1);
				}

				if (player1.Count == 0 || player2.Count == 0)
				{
					if (win == 1)
					{
						return 1;
					}
					else if (win == 2)
					{
						return 2;
					}
				}
			}
		}

		private long CountCards(Queue<int> deck)
		{
			List<int> winner = new List<int>(deck.ToArray());
			winner.Reverse();

			long output = 0;
			for (int i = 1; i <= winner.Count; i++)
				output += winner[i - 1] * i;

			return output;
		}
		private void ProcessInput(Queue<int> player1, Queue<int> player2, string[] input)
		{
			bool playerOne = true;
			foreach (var item in input)
			{
				if (item.Length == 0)
				{
					playerOne = false;
					continue;
				}
				else if (item.Length > 4)
				{ continue; }

				if (playerOne)
				{
					player1.Enqueue(int.Parse(item));
				}
				else
				{
					player2.Enqueue(int.Parse(item));
				}
			}
		}
	}
}
