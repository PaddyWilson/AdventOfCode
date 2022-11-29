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

			List<RPGItem> weapons = RPGItem.GetWeapons();
			List<RPGItem> armor = RPGItem.GetArmor();
			List<RPGItem> rings = RPGItem.GetRings();

			int lowestCost = int.MaxValue;

			foreach (var weapon in weapons)
			{
				foreach (var arm in armor)
				{
					foreach (var rring in rings)
					{
						foreach (var lring in rings)
						{
							RPGAttacker boss = new RPGAttacker(bHP, bDamage, bArmor);

							RPGAttacker player = new RPGAttacker(100, 0, 0);
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

			List<RPGItem> weapons = RPGItem.GetWeapons();
			List<RPGItem> armor = RPGItem.GetArmor();
			List<RPGItem> rings = RPGItem.GetRings();

			int highestCost = int.MinValue;

			foreach (var weapon in weapons)
			{
				foreach (var arm in armor)
				{
					foreach (var rring in rings)
					{
						foreach (var lring in rings)
						{
							RPGAttacker boss = new RPGAttacker(bHP, bDamage, bArmor);

							RPGAttacker player = new RPGAttacker(100, 0, 0);
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

	public class RPGAttacker
	{
		public int HP { get; set; }
		public int Damage { get; set; }
		public int Armor { get; set; }

		public int MinimumDamage { get; private set; }

		public RPGItem IWeapon { get; set; }
		public RPGItem IArmor { get; set; }
		public RPGItem RRing { get; set; }
		public RPGItem LRing { get; set; }

		public RPGAttacker(int hp, int damage, int armor)
		{
			this.HP = hp;
			this.Damage = damage;
			this.Armor = armor;

			MinimumDamage = 1;// minDamage;

			IWeapon = new RPGItem();
			IArmor = new RPGItem();
			RRing = new RPGItem();
			LRing = new RPGItem();
		}

		public int Attack(RPGAttacker attack)
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

	public enum RPGItemTypes
	{
		NoItem, Weapon, Armor, Ring
	}
	public class RPGItem
	{
		//ItemTypes are not really needed
		public RPGItemTypes Type { get; set; }
		public string Name { get; set; }
		public int Cost { get; set; }
		public int Damage { get; set; }
		public int Armor { get; set; }

		public RPGItem(RPGItemTypes type, string name, int cost, int damage, int armor)
		{
			Type = type;
			Name = name;
			Cost = cost;
			Damage = damage;
			Armor = armor;
		}

		public RPGItem()
		{
			Type = RPGItemTypes.NoItem;
			Name = "NO";
			Cost = 0;
			Damage = 0;
			Armor = 0;
		}

		public static List<RPGItem> GetWeapons()
		{
			List<RPGItem> weapons = new List<RPGItem> {
				new RPGItem(RPGItemTypes.Weapon, "Dagger"     , 8, 4, 0),
				new RPGItem(RPGItemTypes.Weapon, "Shortsword" , 10, 5, 0),
				new RPGItem(RPGItemTypes.Weapon, "Warhammer"  , 25, 6, 0),
				new RPGItem(RPGItemTypes.Weapon, "Longsword"  , 40, 7, 0),
				new RPGItem(RPGItemTypes.Weapon, "Greataxe"   , 74, 8, 0),
			};
			return weapons;
		}
		public static List<RPGItem> GetArmor()
		{
			List<RPGItem> armor = new List<RPGItem> {
				new RPGItem(),
				new RPGItem(RPGItemTypes.Armor, "Leather"     , 13, 0, 1),
				new RPGItem(RPGItemTypes.Armor, "Chainmail"   , 31, 0, 2),
				new RPGItem(RPGItemTypes.Armor, "Splintmail"  , 53, 0, 3),
				new RPGItem(RPGItemTypes.Armor, "Bradedmail"  , 75, 0, 4),
				new RPGItem(RPGItemTypes.Armor, "Platemail"   , 102, 0, 5),
			};
			return armor;
		}
		public static List<RPGItem> GetRings()
		{
			List<RPGItem> rings = new List<RPGItem> {
				new RPGItem(RPGItemTypes.Ring, "no"    ,0, 0, 0),
				new RPGItem(RPGItemTypes.Ring, "Damage +1"    , 25, 1, 0),
				new RPGItem(RPGItemTypes.Ring, "Damage +2"    , 50, 2, 0),
				new RPGItem(RPGItemTypes.Ring, "Damage +3"    , 100, 3, 0),
				new RPGItem(RPGItemTypes.Ring, "Defense +1"   , 20, 0, 1),
				new RPGItem(RPGItemTypes.Ring, "Defense +2"   , 40, 0, 2),
				new RPGItem(RPGItemTypes.Ring, "Defense +3"   , 80, 0, 3),
			};
			return rings;
		}
	}
}
