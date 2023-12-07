﻿using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day7 : BaseDay
    {
        public Day7()
        {
            Day = "7";
            Answer1 = "251927063";
            Answer2 = "255632664";
        }

        static Dictionary<char, int> cardStrength = new Dictionary<char, int>()
        {
            {'A', 13}, {'K', 12}, {'Q', 11}, {'J', 10},
            {'T', 9}, {'9', 8}, {'8', 7}, {'7', 6},
            {'6', 5}, {'5', 4}, {'4', 3}, {'3', 2},
            {'2', 1},
        };

        enum HandType { Five, Four, FullHouse, Three, TwoPair, OnePair, HighCard }

        class Hand
        {
            public string cards = "";
            public int bet;

            public HandType type;
            public int value;

            public bool IsHigher(Hand hand2)
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    if (cards[i] == hand2.cards[i])
                        continue;

                    return cardStrength[cards[i]] > cardStrength[hand2.cards[i]];
                }
                return false;
            }
        }

        protected override string Solution1(string[] input)
        {
            cardStrength['J'] = 10;
            Dictionary<HandType, List<Hand>> hands = new();
            //List<Hand> cards = new();

            foreach (string card in input)
            {
                var t = card.Split(' ');

                Hand hand = new Hand();
                hand.cards = t[0];
                hand.bet = int.Parse(t[1]);
                hand.type = GetHandType(hand.cards);

                if (!hands.ContainsKey(hand.type))
                    hands.Add(hand.type, new List<Hand>());
                hands[hand.type].Add(hand);
            }

            List<Hand> ordered = new();
            if (hands.ContainsKey(HandType.HighCard))
                ordered.AddRange(Sort(hands[HandType.HighCard]));

            if (hands.ContainsKey(HandType.OnePair))
                ordered.AddRange(Sort(hands[HandType.OnePair]));

            if (hands.ContainsKey(HandType.TwoPair))
                ordered.AddRange(Sort(hands[HandType.TwoPair]));

            if (hands.ContainsKey(HandType.Three))
                ordered.AddRange(Sort(hands[HandType.Three]));

            if (hands.ContainsKey(HandType.FullHouse))
                ordered.AddRange(Sort(hands[HandType.FullHouse]));

            if (hands.ContainsKey(HandType.Four))
                ordered.AddRange(Sort(hands[HandType.Four]));

            if (hands.ContainsKey(HandType.Five))
                ordered.AddRange(Sort(hands[HandType.Five]));

            int output = 0;

            for (int i = 0; i < ordered.Count; i++)
                output += (i + 1) * ordered[i].bet;

            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            //Jacks are now the lowest strength card
            cardStrength['J'] = 0;

            Dictionary<HandType, List<Hand>> hands = new();

            foreach (string card in input)
            {
                var t = card.Split(' ');

                Hand hand = new Hand();
                hand.cards = t[0];
                hand.bet = int.Parse(t[1]);
                hand.type = GetHandTypePart2(hand.cards);

                if (!hands.ContainsKey(hand.type))
                    hands.Add(hand.type, new List<Hand>());
                hands[hand.type].Add(hand);
            }

            List<Hand> ordered = new();
            if (hands.ContainsKey(HandType.HighCard))
                ordered.AddRange(Sort(hands[HandType.HighCard]));

            if (hands.ContainsKey(HandType.OnePair))
                ordered.AddRange(Sort(hands[HandType.OnePair]));

            if (hands.ContainsKey(HandType.TwoPair))
                ordered.AddRange(Sort(hands[HandType.TwoPair]));

            if (hands.ContainsKey(HandType.Three))
                ordered.AddRange(Sort(hands[HandType.Three]));

            if (hands.ContainsKey(HandType.FullHouse))
                ordered.AddRange(Sort(hands[HandType.FullHouse]));

            if (hands.ContainsKey(HandType.Four))
                ordered.AddRange(Sort(hands[HandType.Four]));

            if (hands.ContainsKey(HandType.Five))
                ordered.AddRange(Sort(hands[HandType.Five]));

            int output = 0;
            for (int i = 0; i < ordered.Count; i++)
                output += (i + 1) * ordered[i].bet;

            return output.ToString();
        }

        List<Hand> Sort(List<Hand> hands)
        {
            List<Hand> ordered = new();
            while (hands.Count > 0)
            {
                Hand lowest = hands[0];
                for (int i = 1; i < hands.Count; i++)
                {
                    if (lowest.IsHigher(hands[i]))
                    {
                        lowest = hands[i];
                    }
                }
                hands.Remove(lowest);
                ordered.Add(lowest);
            }
            return ordered;
        }

        HandType GetHandType(string hand)
        {
            Dictionary<char, int> counts = new();

            foreach (var item in hand)
            {
                if (!counts.ContainsKey(item))
                    counts.Add(item, 0);
                counts[item]++;
            }

            List<int> count = new List<int>();
            foreach (var item in counts)
                count.Add(item.Value);

            // 5 or 4 of kind
            foreach (var item in count)
            {
                if (item == 5)
                    return HandType.Five;
                if (item == 4)
                    return HandType.Four;
            }

            //full house
            for (int i = 0; i < count.Count; i++)
            {
                if (count[i] == 2)
                    for (int j = i + 1; j < count.Count; j++)
                        if (count[j] == 3)
                            return HandType.FullHouse;

                if (count[i] == 3)
                    for (int j = i + 1; j < count.Count; j++)
                        if (count[j] == 2)
                            return HandType.FullHouse;
            }

            //3 of kind
            foreach (var item in count)
            {
                if (item == 3)
                    return HandType.Three;
            }

            //two pair
            for (int i = 0; i < count.Count; i++)
            {
                if (count[i] == 2)
                    for (int j = i + 1; j < count.Count; j++)
                        if (count[j] == 2)
                            return HandType.TwoPair;
            }

            //one pair
            for (int i = 0; i < count.Count; i++)
            {
                if (count[i] == 2)
                    return HandType.OnePair;
            }

            //high card
            return HandType.HighCard;
        }
        HandType GetHandTypePart2(string hand)
        {
            Dictionary<char, int> counts = new();
            foreach (var item in hand)
            {
                if (!counts.ContainsKey(item))
                    counts.Add(item, 0);
                counts[item]++;
            }

            //Jacks are wild cards
            int JackCount = 0;
            if (counts.ContainsKey('J'))
            {
                JackCount = counts['J'];
                counts.Remove('J');
            }

            List<int> count = new List<int>();
            foreach (var item in counts)
                count.Add(item.Value);

            if (JackCount == 5)
                return HandType.Five;

            // 5 of kind : done
            foreach (var item in count)
            {
                if (item == 5)
                    return HandType.Five;
                for (int jack = 1; jack <= JackCount; jack++)
                {
                    if (item + jack == 5)
                        return HandType.Five;
                }
            }

            // 4 of kind : done
            foreach (var item in count)
            {
                if (item == 4)
                    return HandType.Four;

                for (int jack = 1; jack <= JackCount; jack++)
                {
                    if (item + jack == 4)
                        return HandType.Four;
                }
            }

            //full house
            for (int i = 0; i < count.Count; i++)
            {
                if (count[i] == 2)
                    for (int j = i + 1; j < count.Count; j++)
                        if (count[j] == 3)
                            return HandType.FullHouse;

                if (count[i] == 3)
                    for (int j = i + 1; j < count.Count; j++)
                        if (count[j] == 2)
                            return HandType.FullHouse;

                for (int jack = 1; jack <= JackCount; jack++)
                {
                    int remainJack = JackCount - jack;

                    if (count[i] + jack == 2)
                        for (int j = i + 1; j < count.Count; j++)
                        {
                            if (count[j] == 3)
                                return HandType.FullHouse;

                            for (int k = 1; k <= remainJack; k++)
                            {
                                int remainJack2 = remainJack - k;
                                if (count[j] + remainJack2 == 3)
                                    return HandType.FullHouse;
                            }
                        }

                    if (count[i] + jack == 3)
                        for (int j = i + 1; j < count.Count; j++)
                        {
                            if (count[j] == 2)
                                return HandType.FullHouse;

                            for (int k = 1; k <= remainJack; k++)
                            {
                                int remainJack2 = remainJack - k;
                                if (count[j] + remainJack2 == 2)
                                    return HandType.FullHouse;
                            }
                        }
                }
            }

            //3 of kind : done
            foreach (var item in count)
            {
                if (item == 3)
                    return HandType.Three;

                for (int jack = 1; jack <= JackCount; jack++)
                {
                    if (item + jack == 3)
                        return HandType.Three;
                }
            }

            //two pair
            for (int i = 0; i < count.Count; i++)
            {
                if (count[i] == 2)
                    for (int j = i + 1; j < count.Count; j++)
                        if (count[j] == 2)
                            return HandType.TwoPair;

                for (int jack = 1; jack <= count.Count; jack++)
                {
                    int remainJack = JackCount - jack;
                    if (count[i] + jack == 2)
                        for (int j = i + 1; j < count.Count; j++)
                        {
                            for (int k = 1; k <= remainJack; k++)
                            {
                                int remainJack2 = remainJack - k;
                                if (count[j] + remainJack2 == 2)
                                    return HandType.TwoPair;
                            }
                        }
                }
            }

            //one pair
            for (int i = 0; i < count.Count; i++)
            {
                if (count[i] == 2)
                    return HandType.OnePair;

                for (int jack = 1; jack <= JackCount; jack++)
                {
                    int remainJack = JackCount - jack;
                    if (count[i] + jack == 2)
                        return HandType.OnePair;
                }
            }

            //high card
            return HandType.HighCard;
        }
    }
}
