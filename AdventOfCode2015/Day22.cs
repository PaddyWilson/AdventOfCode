using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;

namespace AOC
{
	public class Day22 : BaseDay
	{
		public Day22()
		{
			Day = "22";
			Answer1 = "900";
			Answer2 = "0";
		}

		protected override string Solution1(string[] input)
		{
			int bHP = int.Parse(input[0].Split(' ')[2]);
			int bDamage = int.Parse(input[1].Split(' ')[1]);

			int lowestCost = int.MaxValue;

			WizardAttacker boss = new WizardAttacker(bHP, bDamage, 0);
			boss.MagicMissile = new NoItem();
			boss.Drain = new NoItem();
			boss.Poison = new NoItem();
			boss.Shield = new NoItem();
			boss.Recharge = new NoItem();

			WizardAttacker player = new WizardAttacker(50, 0, 0);
			player.Mana = 500;

			RunTurn(player, boss, ref lowestCost);

			return lowestCost.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int bHP = int.Parse(input[0].Split(' ')[2]);
			int bDamage = int.Parse(input[1].Split(' ')[1]);

			int lowestCost = int.MaxValue;

			WizardAttacker boss = new WizardAttacker(bHP, bDamage, 0);
			boss.MagicMissile = new NoItem();
			boss.Drain = new NoItem();
			boss.Poison = new NoItem();
			boss.Shield = new NoItem();
			boss.Recharge = new NoItem();

			WizardAttacker player = new WizardAttacker(50, 0, 0);
			player.Mana = 500;

			RunTurnV2(player, boss, ref lowestCost);

			return lowestCost.ToString();
		}

		private void RunTurn(WizardAttacker player, WizardAttacker boss, ref int lowestMana, int depth = 0)
		{
			if (depth >= 5000)
			{
				Console.WriteLine("Big");
				return;
			}

			if (boss.Dead())
			{
				if (lowestMana > player.ManaUsed && !player.Dead())
					lowestMana = player.ManaUsed;
				//Console.WriteLine("DOWN Boss {0} : {1}", boss.HP, player.HP);
				return;
			}

			if (player.Dead())
			{
				//Console.WriteLine("DOWN Player {0} : {1}", boss.HP, player.HP);
				return;
			}

			for (int i = player.GetMagicItems().Count - 1; i >= 0; i--)
			///////for (int i = 0; i < player.GetMagicItems().Count; i++)
			{
				WizardAttacker playerTemp = player.DeepCopy();
				WizardAttacker bossTemp = boss.DeepCopy();

				if (!playerTemp.GetMagicItems()[i].CanCast(playerTemp))
					continue;

				//Console.WriteLine("-- Player --");
				playerTemp.TurnStart(bossTemp);

				if (!playerTemp.GetMagicItems()[i].Use(playerTemp, bossTemp))
					continue;
				/////////item.Use(playerTemp, bossTemp);


				//Console.WriteLine("Player HP:{0} Armor:{1} Mana:{2}", playerTemp.HP, playerTemp.GetTotalArmor(), playerTemp.Mana);
				//Console.WriteLine("Boss HP:{0} Armor:{1} Mana:{2}", bossTemp.HP, bossTemp.GetTotalArmor(), bossTemp.Mana);

				//Console.WriteLine("Player used {0}", playerTemp.GetMagicItems()[i].Name);

				if (bossTemp.Dead())
				{
					//Console.WriteLine("Boss died");
					if (lowestMana > playerTemp.ManaUsed)
						lowestMana = playerTemp.ManaUsed;
					continue;
				}

				if (playerTemp.Mana < 53)
					continue;



				//Console.WriteLine("-- Boss --");
				playerTemp.TurnStart(bossTemp);
				int j = bossTemp.Attack(playerTemp);
				//Console.WriteLine("Player HP:{0} Armor:{1} Mana:{2}", playerTemp.HP, playerTemp.GetTotalArmor(), playerTemp.Mana);
				//Console.WriteLine("Boss HP:{0} Armor:{1} Mana:{2}", bossTemp.HP, bossTemp.GetTotalArmor(), bossTemp.Mana);


				//Console.WriteLine("Boss did {0} damage", j);
				if (player.Dead())
				{
					////Console.WriteLine("Player died");
					continue;
				}

				RunTurn(playerTemp, bossTemp, ref lowestMana, depth + 1);
			}

			//Console.WriteLine("DOWN");
		}

		private void RunTurnV2(WizardAttacker player, WizardAttacker boss, ref int lowestMana, int depth = 0)
		{
			if (depth >= 5000)
				return;

			if (boss.Dead())
			{
				if (lowestMana > player.ManaUsed && !player.Dead())
					lowestMana = player.ManaUsed;
				//Console.WriteLine("DOWN Boss {0} : {1}", boss.HP, player.HP);
				return;
			}

			if (player.Dead())
			{
				//Console.WriteLine("DOWN Player {0} : {1}", boss.HP, player.HP);
				return;
			}

			for (int i = player.GetMagicItems().Count - 1; i >= 0; i--)
			///////for (int i = 0; i < player.GetMagicItems().Count; i++)
			{
				WizardAttacker playerTemp = player.DeepCopy();
				WizardAttacker bossTemp = boss.DeepCopy();

				//for part two
				//if (playerTemp.GetTotalArmor() <= 0)
				//{
					playerTemp.HP -= 1;
				//}
				//else
				//{
				//	int sdfgs = i;
				//}
				if (playerTemp.Dead())
					continue;

				if (!playerTemp.GetMagicItems()[i].CanCast(playerTemp))
					continue;

				//Console.WriteLine("-- Player --");
				playerTemp.TurnStart(bossTemp);

				if (!playerTemp.GetMagicItems()[i].Use(playerTemp, bossTemp))
					continue;
				/////////item.Use(playerTemp, bossTemp);


				//Console.WriteLine("Player HP:{0} Armor:{1} Mana:{2}", playerTemp.HP, playerTemp.GetTotalArmor(), playerTemp.Mana);
				//Console.WriteLine("Boss HP:{0} Armor:{1} Mana:{2}", bossTemp.HP, bossTemp.GetTotalArmor(), bossTemp.Mana);

				//Console.WriteLine("Player used {0}", playerTemp.GetMagicItems()[i].Name);

				if (bossTemp.Dead() && !playerTemp.Dead())
				{
					//Console.WriteLine("Boss died");
					if (lowestMana > playerTemp.ManaUsed)
						lowestMana = playerTemp.ManaUsed;
					continue;
				}
				
				if (playerTemp.Mana < 53)
					continue;

				//Console.WriteLine("-- Boss --");
				playerTemp.TurnStart(bossTemp);
				int j = bossTemp.Attack(playerTemp);
				//Console.WriteLine("Player HP:{0} Armor:{1} Mana:{2}", playerTemp.HP, playerTemp.GetTotalArmor(), playerTemp.Mana);
				//Console.WriteLine("Boss HP:{0} Armor:{1} Mana:{2}", bossTemp.HP, bossTemp.GetTotalArmor(), bossTemp.Mana);


				//Console.WriteLine("Boss did {0} damage", j);
				if (player.Dead())
				{
					////Console.WriteLine("Player died");
					continue;
				}

				RunTurn(playerTemp, bossTemp, ref lowestMana, depth + 1);
			}

			//Console.WriteLine("DOWN");
		}
	}

	public class WizardAttacker
	{
		public int HP { get; set; }
		public int Damage { get; set; }
		public int Armor { get; set; }

		public int Mana { get; set; }
		public int ManaUsed { get; set; }

		public int MinimumDamage { get; private set; }

		public MagicItem MagicMissile { get; set; }
		public MagicItem Drain { get; set; }
		public MagicItem Shield { get; set; }
		public MagicItem Poison { get; set; }
		public MagicItem Recharge { get; set; }

		public WizardAttacker(int hp, int damage, int armor)
		{
			this.HP = hp;
			this.Damage = damage;
			this.Armor = armor;

			MinimumDamage = 1;// minDamage;

			MagicMissile = new MagicMissile();
			Drain = new Drain();
			Shield = new Shield();
			Poison = new Poison();
			Recharge = new Recharge();
		}

		public void TurnStart(WizardAttacker attack)
		{
			MagicMissile.TurnStart(this, attack);
			Drain.TurnStart(this, attack);
			Shield.TurnStart(this, attack);
			Poison.TurnStart(this, attack);
			Recharge.TurnStart(this, attack);
		}

		public int Attack(WizardAttacker attack)
		{
			int d = GetTotalDamage();
			int a = attack.GetTotalArmor();
			int damage = d - a;

			if (damage <= MinimumDamage)
				damage = MinimumDamage;

			attack.HP = attack.HP - damage;

			return damage;
		}

		public int GetTotalArmor()
		{
			return Armor + MagicMissile.Armor + Drain.Armor + Shield.Armor + Poison.Armor;
		}

		public int GetTotalDamage()
		{
			return Damage + MagicMissile.Damage + Drain.Damage + Shield.Damage + Poison.Damage;
		}

		public bool Dead()
		{
			return HP <= 0;
		}

		public List<MagicItem> GetMagicItems()
		{
			return new List<MagicItem> { MagicMissile, Drain, Shield, Poison, Recharge };
		}

		public WizardAttacker DeepCopy()
		{
			WizardAttacker output = new WizardAttacker(HP, Damage, Armor)
			{
				Mana = this.Mana,
				ManaUsed = this.ManaUsed,
				Armor = this.Armor,
				MagicMissile = (MagicItem)this.MagicMissile.DeepCopy(),
				Drain = (MagicItem)this.Drain.DeepCopy(),
				Shield = (MagicItem)this.Shield.DeepCopy(),
				Poison = (MagicItem)this.Poison.DeepCopy(),
				Recharge = (MagicItem)this.Recharge.DeepCopy(),
			};
			return output;
		}
	}

	public abstract class MagicItem
	{
		public string Name { get; set; }
		public int Mana { get; set; }
		public int Damage { get; set; }
		public int Armor { get; set; }

		public MagicItem(string name, int mana, int damage, int armor)
		{
			Name = name;
			Mana = mana;
			Damage = damage;
			Armor = armor;
		}

		public virtual bool CanCast(WizardAttacker self)
		{
			return (Mana < self.Mana);
		}

		public virtual bool TurnStart(WizardAttacker self, WizardAttacker attacker) { return false; }
		public virtual bool Use(WizardAttacker self, WizardAttacker attacker) { return false; }
		public virtual object DeepCopy() { return null; }

	}


	public class NoItem : MagicItem
	{
		public NoItem() : base("NoItem", 0, 0, 0) { }

		public override object DeepCopy() { return new NoItem(); }
	}

	public class MagicMissile : MagicItem
	{
		public MagicMissile() : base("Magic Missile", 53, 4, 0) { }

		public override bool Use(WizardAttacker self, WizardAttacker attacker)
		{
			if (self.Mana < this.Mana)
				return false;

			self.Mana -= Mana;
			self.ManaUsed += Mana;
			attacker.HP -= Damage;

			//Console.WriteLine("-Player used {2} pHP:{0} Boss:{1}", self.HP, attacker.HP, Name);

			return true;
		}
		public override MagicMissile DeepCopy()
		{
			MagicMissile output = new MagicMissile();
			output.Damage = Damage;
			return output;
		}
	}

	public class Drain : MagicItem
	{
		int Heal;

		public Drain() : base("Drain", 73, 2, 0)
		{
			Heal = 2;
		}

		public override bool Use(WizardAttacker self, WizardAttacker attacker)
		{
			if (self.Mana < this.Mana)
				return false;

			self.Mana -= Mana;
			self.ManaUsed += Mana;

			attacker.HP -= Damage;
			self.HP += Heal;

			//Console.WriteLine("-Player used {2} pHP:{0} Boss:{1}", self.HP, attacker.HP, Name);

			return true;
		}

		public override Drain DeepCopy()
		{
			Drain output = new Drain();
			output.Heal = Heal;
			output.Damage = this.Damage;
			return output;
		}
	}

	public class Shield : MagicItem
	{
		int TempArmour;
		int LastsTicks;
		int CurrentTick;
		public Shield() : base("Shield", 113, 0, 0)
		{
			TempArmour = 7;
			LastsTicks = 6;
			CurrentTick = 0;
		}

		public override bool CanCast(WizardAttacker self)
		{
			if (CurrentTick > 0)
				return false;
			if (CurrentTick <= 0 && Mana < self.Mana)
				return true;

			return (Mana < self.Mana);
		}

		public override bool TurnStart(WizardAttacker self, WizardAttacker attacker)
		{
			if (CurrentTick <= 0)
			{
				Armor = 0;
				return false;
			}
			CurrentTick--;

			//Console.WriteLine("--TS Used {2} pHP:{0} Uses:{3} Mana:{4} - Boss:{1}", self.HP, attacker.HP, Name, CurrentTick, Mana);
			Armor = TempArmour;
			return true;
		}

		public override bool Use(WizardAttacker self, WizardAttacker attacker)
		{
			if (self.Mana < this.Mana)
				return false;

			if (CurrentTick > 0)//Already active
				return false;

			self.Mana -= Mana;
			self.ManaUsed += Mana;

			Armor = TempArmour;
			CurrentTick = LastsTicks;

			//Console.WriteLine("-Player used {2} pHP:{0} Boss:{1}", self.HP, attacker.HP, Name);
			return true;
		}
		public override Shield DeepCopy()
		{
			Shield output = new Shield();
			output.Armor = Armor;
			output.TempArmour = TempArmour;
			output.LastsTicks = this.LastsTicks;
			output.CurrentTick = this.CurrentTick;
			output.Damage = this.Damage;
			return output;
		}
	}

	public class Poison : MagicItem
	{
		int LastsTicks;
		int CurrentTick;
		public Poison() : base("Poison", 173, 3, 0)
		{
			LastsTicks = 6;
			CurrentTick = 0;
		}
		public override bool CanCast(WizardAttacker self)
		{
			if (CurrentTick > 0)
				return false;
			if (CurrentTick <= 0 && Mana < self.Mana)
				return true;

			return (Mana < self.Mana);
		}
		public override bool TurnStart(WizardAttacker self, WizardAttacker attacker)
		{
			if (CurrentTick <= 0)
			{
				return false;
			}
			CurrentTick--;

			//Console.WriteLine("--TS Used {2} pHP:{0} Uses:{3} Mana:{4} - Boss:{1}", self.HP, attacker.HP, Name, CurrentTick, Mana);
			attacker.HP -= Damage;
			return true;
		}

		public override bool Use(WizardAttacker self, WizardAttacker attacker)
		{
			if (self.Mana < this.Mana)
				return false;

			if (CurrentTick > 0)//Already active
				return false;

			self.Mana -= Mana;
			self.ManaUsed += Mana;

			CurrentTick = LastsTicks;

			//Console.WriteLine("-Player used {2} pHP:{0} Boss:{1}", self.HP, attacker.HP, Name);
			return true;
		}
		public override Poison DeepCopy()
		{
			Poison output = new Poison();
			output.LastsTicks = this.LastsTicks;
			output.CurrentTick = this.CurrentTick;
			output.Damage = this.Damage;
			return output;
		}
	}

	public class Recharge : MagicItem
	{
		int AddMana;
		int LastsTicks;
		int CurrentTick;
		public Recharge() : base("Recharge", 229, 0, 0)
		{
			AddMana = 101;
			LastsTicks = 5;
			CurrentTick = 0;
		}
		public override bool CanCast(WizardAttacker self)
		{
			if (CurrentTick > 0)
				return false;
			if (CurrentTick <= 0 && Mana < self.Mana)
				return true;

			return (Mana < self.Mana);
		}
		public override bool TurnStart(WizardAttacker self, WizardAttacker attacker)
		{
			if (CurrentTick <= 0)
				return false;

			CurrentTick--;

			//Console.WriteLine("--TS Used {2} pHP:{0} Uses:{3} Mana:{4} - Boss:{1}", self.HP, attacker.HP, Name, CurrentTick, Mana);

			self.Mana += AddMana;
			return true;
		}

		public override bool Use(WizardAttacker self, WizardAttacker attacker)
		{
			if (self.Mana < this.Mana)
				return false;

			if (CurrentTick > 0)//Already active
				return false;

			self.Mana -= Mana;
			self.ManaUsed += Mana;

			CurrentTick = LastsTicks;

			//Console.WriteLine("-Player used {2} pHP:{0} Boss:{1}", self.HP, attacker.HP, Name);
			return true;
		}
		public override Recharge DeepCopy()
		{
			Recharge output = new Recharge();
			output.AddMana = this.AddMana;
			output.LastsTicks = this.LastsTicks;
			output.CurrentTick = this.CurrentTick;
			output.Damage = this.Damage;
			return output;
		}
	}
}
