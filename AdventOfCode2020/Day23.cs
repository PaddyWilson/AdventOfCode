using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day23 : BaseDay
	{
		public Day23()
		{
			Day = "23";
			Answer1 = "25368479";
			Answer2 = "44541319250";
		}

		protected override string Solution1(string[] input)
		{
			//useing this so it does not have to loop through all nodes to find one with a value i want
			Dictionary<int, LinkedListNode<int>> cupsDic = new Dictionary<int, LinkedListNode<int>>();
			LinkedList<int> cups = CreateList(input, ref cupsDic);

			PlayGame(cups, cupsDic, 100);

			string before = "";
			string after = "";
			bool rev = false;
			foreach (var item in cups)
			{
				if (item == 1)
				{
					rev = true;
					continue;
				}
				if (!rev)
				{
					before += item;
				}
				else
				{
					after += item;
				}
			}

			return after + before;
		}

		protected override string Solution2(string[] input)
		{
			//useing this so it does not have to loop through all nodes to find one with a value i want
			Dictionary<int, LinkedListNode<int>> cupsDic = new Dictionary<int, LinkedListNode<int>>();
			LinkedList<int> cups = CreateList(input, ref cupsDic, 1000000);

			PlayGame(cups, cupsDic, 10000000);

			var endCup1 = cups.Find(1).Next;
			var endCup2 = endCup1.Next;

			// "1L" is used to make it a long
			return (1L * endCup1.Value * endCup2.Value).ToString();
		}

		private void PlayGame(LinkedList<int> cups, Dictionary<int, LinkedListNode<int>> cupsDic, int maxTurns)
		{
			var currentCup = cups.First;
			int turnCount = 0;

			while (turnCount < maxTurns)
			{
				turnCount++;

				// gets 3 nodes and the values of them				
				var pickedCups = new LinkedListNode<int>[3];
				LinkedList<int> pickedCupsValues = new LinkedList<int>();

				LinkedListNode<int> pickStart = currentCup.Next;
				for (int i = 0; i < 3; i++)
				{
					if (pickStart == null)
						pickStart = cups.First;

					pickedCups[i] = pickStart;
					pickedCupsValues.AddLast(pickStart.Value);
					pickStart = pickStart.Next;
					cups.Remove(pickedCups[i]);
				}

				//finds the cup to put the above 3 node behind
				var destCup = currentCup.Value - 1;
				if (destCup == 0)
					destCup = cups.Count + 3;
				while (pickedCupsValues.Contains(destCup))
				{
					destCup--;
					if (destCup == 0)
						destCup = cups.Count + 3;
				}

				// add the 3 nodes back into the cups list
				// and updates the cupsDic with there new nodes
				var firstNode = cupsDic[destCup]; //cups.Find(destCup);
				cups.AddAfter(firstNode, pickedCups[0]);
				cups.AddAfter(firstNode.Next, pickedCups[1]);
				cups.AddAfter(firstNode.Next.Next, pickedCups[2]);

				//cupIndex++;
				currentCup = currentCup.Next;
				if (currentCup == null)
					currentCup = cups.First;
			}
		}

		private LinkedList<int> CreateList(string[] input, ref Dictionary<int, LinkedListNode<int>> cupsDic, int max = 0)
		{
			LinkedList<int> cups = new LinkedList<int>();
			if (cupsDic == null)
				cupsDic = new Dictionary<int, LinkedListNode<int>>();

			int highestNum = 0;
			//parse data
			for (int i = 0; i < input[0].Length; i++)
			{
				var item = cups.AddLast(int.Parse(input[0][i].ToString()));
				cupsDic.Add(item.Value, item);
				if (int.Parse(input[0][i].ToString()) > highestNum)
					highestNum = int.Parse(input[0][i].ToString());
			}

			for (int i = highestNum + 1; i <= max; i++)
			{
				var item = cups.AddLast(i);
				cupsDic.Add(item.Value, item);
			}

			return cups;
		}
	}
}
