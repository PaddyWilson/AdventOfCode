using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	public class Day21 : BaseDay
	{
		public Day21()
		{
			Day = "21";
			Answer1 = "111";
			Answer2 = "188";
		}

		protected override string Solution1(string[] input)
		{
			int bHP = int.Parse(input[0].Split(' ')[2]);
			int bDamage = int.Parse(input[1].Split(' ')[1]);
			int bArmor = int.Parse(input[2].Split(' ')[1]);

			List<Item> weapons = Item.GetWeapons();
			List<Item> armor = Item.GetArmor();
			List<Item> rings = Item.GetRings();

			int lowestCost = int.MaxValue;

			foreach (var weapon in weapons)
			{
				foreach (var arm in armor)
				{
					foreach (var rring in rings)
					{
						foreach (var lring in rings)
						{
							Attacker boss = new Attacker(bHP, bDamage, bArmor);

							Attacker player = new Attacker(100, 0, 0);
							player.IWeapon = weapon;
							player.IArmor = arm;
							player.RRing = rring;
							player.LRing = lring;

							if (rring.Name == lring.Name)
								continue;//matching rings

							while (!player.Dead() && !boss.Dead())
							{
								player.Attack(boss);

								if (boss.Dead())
								{
									if (player.GetTotalCost() < lowestCost)
										lowestCost = player.GetTotalCost();
									break;
								}

								boss.Attack(player);

								if (player.Dead())
								{
									break;
								}
							}
						}
					}
				}
			}

			return lowestCost.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int bHP = int.Parse(input[0].Split(' ')[2]);
			int bDamage = int.Parse(input[1].Split(' ')[1]);
			int bArmor = int.Parse(input[2].Split(' ')[1]);

			List<Item> weapons = Item.GetWeapons();
			List<Item> armor = Item.GetArmor();
			List<Item> rings = Item.GetRings();

			int highestCost = int.MinValue;

			foreach (var weapon in weapons)
			{
				foreach (var arm in armor)
				{
					foreach (var rring in rings)
					{
						foreach (var lring in rings)
						{
							Attacker boss = new Attacker(bHP, bDamage, bArmor);

							Attacker player = new Attacker(100, 0, 0);
							player.IWeapon = weapon;
							player.IArmor = arm;
							player.RRing = rring;
							player.LRing = lring;

							if (rring.Name == lring.Name)
								continue;//matching rings

							while (!player.Dead() && !boss.Dead())
							{
								player.Attack(boss);

								if (boss.Dead())
								{
									break;
								}

								boss.Attack(player);

								if (player.Dead())
								{
									if (player.GetTotalCost() > highestCost)
										highestCost = player.GetTotalCost();
									break;
								}
							}
						}
					}
				}
			}

			return highestCost.ToString();
		}
	}

	public class Attacker
	{
		public int HP { get; set; }
		public int Damage { get; set; }
		public int Armor { get; set; }

		public int MinimumDamage { get; private set; }

		public Item IWeapon { get; set; }
		public Item IArmor { get; set; }
		public Item RRing { get; set; }
		public Item LRing { get; set; }

		public Attacker(int hp, int damage, int armor)
		{
			this.HP = hp;
			this.Damage = damage;
			this.Armor = armor;

			MinimumDamage = 1;// minDamage;

			IWeapon = new Item();
			IArmor = new Item();
			RRing = new Item();
			LRing = new Item();
		}

		public int Attack(Attacker attack)
		{
			int damage = GetTotalDamage() - attack.GetTotalArmor();

			if (damage <= MinimumDamage)
				damage = MinimumDamage;

			attack.HP = attack.HP - damage;

			return damage;
		}

		public int GetTotalArmor()
		{
			return Armor + IWeapon.Armor + RRing.Armor + LRing.Armor + IArmor.Armor;
		}

		public int GetTotalDamage()
		{
			return Damage + IWeapon.Damage + IArmor.Damage + RRing.Damage + LRing.Damage;
		}

		public int GetTotalCost()
		{
			return IWeapon.Cost + IArmor.Cost + RRing.Cost + LRing.Cost;
		}

		public bool Dead()
		{
			return HP <= 0;
		}
	}

	public enum ItemTypes
	{
		NoItem, Weapon, Armor, Ring
	}
	public class Item
	{
		//ItemTypes are not really needed
		public ItemTypes Type { get; set; }
		public string Name { get; set; }
		public int Cost { get; set; }
		public int Damage { get; set; }
		public int Armor { get; set; }

		public Item(ItemTypes type, string name, int cost, int damage, int armor)
		{
			Type = type;
			Name = name;
			Cost = cost;
			Damage = damage;
			Armor = armor;
		}

		public Item()
		{
			Type = ItemTypes.NoItem;
			Name = "NO";
			Cost = 0;
			Damage = 0;
			Armor = 0;
		}

		public static List<Item> GetWeapons()
		{
			List<Item> weapons = new List<Item> {
				new Item(ItemTypes.Weapon, "Dagger"     , 8, 4, 0),
				new Item(ItemTypes.Weapon, "Shortsword" , 10, 5, 0),
				new Item(ItemTypes.Weapon, "Warhammer"  , 25, 6, 0),
				new Item(ItemTypes.Weapon, "Longsword"  , 40, 7, 0),
				new Item(ItemTypes.Weapon, "Greataxe"   , 74, 8, 0),
			};
			return weapons;
		}
		public static List<Item> GetArmor()
		{
			List<Item> armor = new List<Item> {
				new Item(),
				new Item(ItemTypes.Armor, "Leather"     , 13, 0, 1),
				new Item(ItemTypes.Armor, "Chainmail"   , 31, 0, 2),
				new Item(ItemTypes.Armor, "Splintmail"  , 53, 0, 3),
				new Item(ItemTypes.Armor, "Bradedmail"  , 75, 0, 4),
				new Item(ItemTypes.Armor, "Platemail"   , 102, 0, 5),
			};
			return armor;
		}
		public static List<Item> GetRings()
		{
			List<Item> rings = new List<Item> {
				new Item(ItemTypes.Ring, "no"    ,0, 0, 0),
				new Item(ItemTypes.Ring, "Damage +1"    , 25, 1, 0),
				new Item(ItemTypes.Ring, "Damage +2"    , 50, 2, 0),
				new Item(ItemTypes.Ring, "Damage +3"    , 100, 3, 0),
				new Item(ItemTypes.Ring, "Defense +1"   , 20, 0, 1),
				new Item(ItemTypes.Ring, "Defense +2"   , 40, 0, 2),
				new Item(ItemTypes.Ring, "Defense +3"   , 80, 0, 3),
			};
			return rings;
		}
	}
}
