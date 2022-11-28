using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace Decimation
{
	/// <summary>
	/// The game map.
	/// </summary>
	public class Map
	{
		private static Random random = new Random();
		
		public Map(int width, int height, int monsters, int heroMaxHP)
		{
			if (width * height < monsters + 1)
				throw new Exception("Not enough room for the monsters and the hero!");
			
			Width = width;
			Height = height;
			HeroHP = HeroMaxHP = heroMaxHP;
			tiles = new char[width, height];
			
			// place floors
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					tiles[x, y] = '.';
				}
			}
			
			// place monsters
			MonstersLeft = monsters;
			for (int m = 0; m < monsters; m++)
			{
				// what kind of monster?
				var mnum = m % 9 + 1;
				
				// where to put it?
				var targets = new List<Point>();
				for (int x = 0; x < Width; x++)
				{
					for (int y = 0; y < Height; y++)
					{
						if (tiles[x, y] == '.')
							targets.Add(new Point(x, y));
					}
				}
				var target = targets.ElementAt(random.Next(targets.Count()));
				tiles[target.X, target.Y] = mnum.ToString()[0];
			}
			
			// place hero
			var targets2 = new List<Point>();
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					if (tiles[x, y] == '.')
						targets2.Add(new Point(x, y));
				}
			}
			var target2 = targets2.ElementAt(random.Next(targets2.Count()));
			tiles[target2.X, target2.Y] = '0';
			HeroPosition = target2;
		}
		
		/// <summary>
		/// Retrieves the character at the specified coordinates.
		/// </summary>
		/// <param name="x">The X coordinate.</param>
		/// <param name="y">The Y coordinate.</param>
		/// <returns></returns>
		public char GetCharacter(int x, int y)
		{
			return tiles[x, y];
		}
		
		public int Width {get; private set;}
		public int Height {get; private set;}
		public int HeroHP {get; private set;}
		
		private char[,] tiles;
		
		/// <summary>
		/// Moves the hero. Takes 10 game turns if successful.
		/// </summary>
		/// <param name="dx">The change in X-position.</param>
		/// <param name="dy">The change in Y-position.</param>
		/// <returns>Messages generated.</returns>
		public string MoveHero(int dx, int dy)
		{
			var nx = HeroPosition.X + dx;
			var ny = HeroPosition.Y + dy;
			
			// no moving off the map
			if (nx < 0 || nx >= Width || ny < 0 || ny >= Height)
				return "You cannot flee in that direction!";
			
			// no bumping monsters
			if (tiles[nx, ny] != '.')
				return "You can't fight in melee - try +, -, *, or / to polymorph adjacent monsters.";
			
			// move the hero
			tiles[HeroPosition.X, HeroPosition.Y] = '.';
			tiles[nx, ny] = '0';
			HeroPosition = new Point(nx, ny);
			
			// let monsters move
			return MoveMonsters();
		}
		
		private string MoveMonsters()
		{
			var text = "";
			for (int i = 0; i < 10; i++)
			{
				var monsters = new Dictionary<Point, char>();
				for (int x = 0; x < Width; x++)
				{
					for (int y = 0; y < Height; y++)
					{
						// monsters move every N turns where N is their number
						if (tiles[x, y] != '.' && tiles[x, y] != '0' && TurnsElapsed % int.Parse(tiles[x, y].ToString()) == 0)
						{
							monsters.Add(new Point(x, y), tiles[x, y]);
						}
					}
				}
				foreach (var pos in monsters.Keys)
				{
					if (CanMonsterSee(pos, HeroPosition))
					{
						// move monster toward hero
						int mdx = Math.Sign(HeroPosition.X - pos.X);
						int mdy = Math.Sign(HeroPosition.Y - pos.Y);
						var nmx = mdx + pos.X;
						var nmy = mdy + pos.Y;
						text += MoveMonster(pos.X, pos.Y, nmx, nmy);
					}
					else
					{
						// move monster randomly but don't run off screen
						int mdx, mdy;
						do
						{
							mdx = random.Next(-1, 2);
							mdy = random.Next(-1, 2);
						} while (mdx == 0 && mdy == 0 || mdx + pos.X < 0 || mdx + pos.X >= Width || mdy + pos.Y < 0 || mdy + pos.Y >= Height);
						var nmx = mdx + pos.X;
						var nmy = mdy + pos.Y;
						text += MoveMonster(pos.X, pos.Y, nmx, nmy);
					}
				}
				TurnsElapsed++;
			}
			return text;
		}
		
		/// <summary>
		/// Moves a monster from an old position to a new position.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="nx"></param>
		/// <param name="ny"></param>
		/// <returns>Messages generated.</returns>
		private string MoveMonster(int x, int y, int nx, int ny)
		{
			if (tiles[nx, ny] == '0')
			{
				// attack hero with NdN where N is the monster's number!
				var monster = int.Parse(tiles[x, y].ToString());
				var damage = 0;
				for (int i = 0; i < monster; i++)
				{
					damage += random.Next(monster) + 1;
				}
				HeroHP -= damage;
				return "The " + monster + " hits you for " + damage + " damage! ";
			}
			else if (tiles[nx, ny] == '.')
			{
				// move monster
				tiles[nx, ny] = tiles[x, y];
				tiles[x, y] = '.';
				return "";
			}
			else
			{
				// monster bumps into another monster and cannot move
				return "";
			}
		}
		
		public Point HeroPosition {get; private set;}
		
		public int TurnsElapsed {get; private set;}
		
		public string DoAddition()
		{
			var text = "";
			if (MonstersNextToHero.Count() == 0)
				return "There's nothing here to add to - that would cause arithmetic overflow!";
			foreach (var mpos in MonstersNextToHero)
			{
				var oldnum = int.Parse(tiles[mpos.X, mpos.Y].ToString());
				var newnum = (oldnum + 3) % 10;
				if (newnum == 0)
				{
					tiles[mpos.X, mpos.Y] = '.';
					HeroHP = HeroHP + HeroMaxHP / 2;
					text = "You annihilate a number and regain half your lost rationality! ";
					MonstersLeft--;
				}
				else
					tiles[mpos.X, mpos.Y] = newnum.ToString()[0];
			}
			text += MoveMonsters();
			return text;			
		}
		
		public string DoSubtraction()
		{
			var text = "";
			if (MonstersNextToHero.Count() == 0)
				return "There's nothing here to subtract from - that would cause arithmetic underflow!";
			foreach (var mpos in MonstersNextToHero)
			{
				var oldnum = int.Parse(tiles[mpos.X, mpos.Y].ToString());
				var newnum = (oldnum - 1) % 10;
				if (newnum == 0)
				{
					tiles[mpos.X, mpos.Y] = '.';
					HeroHP = HeroHP + HeroMaxHP / 2;
					text = "You annihilate a number and regain half your lost rationality! ";
					MonstersLeft--;
				}
				else
					tiles[mpos.X, mpos.Y] = newnum.ToString()[0];
			}
			text += MoveMonsters();
			return text;
		}
		
		public string DoMultiplication()
		{
			var text = "";
			if (MonstersNextToHero.Count() == 0)
				return "There's nothing here to multiply by - that would cause infinite recursion!";
			foreach (var mpos in MonstersNextToHero)
			{
				var oldnum = int.Parse(tiles[mpos.X, mpos.Y].ToString());
				var newnum = (oldnum * oldnum) % 10;
				if (newnum == 0)
				{
					tiles[mpos.X, mpos.Y] = '.';
					HeroHP = HeroHP + HeroMaxHP / 2;
					text = "You annihilate a number and regain half your lost rationality! ";
					MonstersLeft--;
				}
				else
					tiles[mpos.X, mpos.Y] = newnum.ToString()[0];
			}
			text += MoveMonsters();
			return text;
		}
		
		public string DoDivision()
		{
			var text = "";
			if (MonstersNextToHero.Count() == 0)
				return "There's nothing here to divide by - that would cause division by zero!";
			foreach (var mpos in MonstersNextToHero)
			{
				var oldnum = int.Parse(tiles[mpos.X, mpos.Y].ToString());
				var newnum = (Math.Ceiling((float)oldnum / 2f)) % 10;
				if (newnum == 0)
				{
					tiles[mpos.X, mpos.Y] = '.';
					HeroHP = HeroHP + HeroMaxHP / 2;
					text = "You annihilate a number and regain half your lost rationality! ";
					MonstersLeft--;
				}
				else
					tiles[mpos.X, mpos.Y] = newnum.ToString()[0];
			}
			text += MoveMonsters();
			return text;
		}
		
		private IEnumerable<Point> MonstersNextToHero
		{
			get
			{
				for (int x = HeroPosition.X - 1; x <= HeroPosition.X + 1; x++)
				{
					for (int y = HeroPosition.Y - 1; y <= HeroPosition.Y + 1; y++)
					{
						if (x >= 0 && x < Width && y >= 0 && y < Height && tiles[x, y] != '0' && tiles[x, y] != '.')
							yield return new Point(x, y);
					}
				}
			}
		}
		
		public int MonstersLeft {get; private set;}
		
		/// <summary>
		/// Determines the color of a tile.
		/// </summary>
		/// <param name="x">The tile X coord.</param>
		/// <param name="y">The tile Y coord.</param>
		/// <returns>Red if a tile contains a monster that can see the player. Yellow if the tile contains a monster that can't see the player. Brown if the tile is in sight of a monster. White otherwise.</returns>
		public Color GetTileColor(int x, int y)
		{
			var p = new Point(x, y);
			if (tiles[x, y] != '.' && tiles[x, y] != '0')
			{
				if (CanMonsterSee(p, HeroPosition))
					return Color.Red;
				else
					return Color.Yellow;
			}
			
			for (int mx = 0; mx < Width; mx++)
			{
				for (int my = 0; my < Height; my++)	
				{
					if (CanMonsterSee(new Point(mx, my), p))
						return Color.Brown;
				}
			}
			return Color.White;
		}
		
		/// <summary>
		/// Determines if a monster can see a target.
		/// </summary>
		/// <param name="mpos">The position of the monster.</param>
		/// <param name="target">The tile targeted.</param>
		/// <returns>true if the monster can see the target, false if the monster cannot see the target or there is no monster.</returns>
		public bool CanMonsterSee(Point mpos, Point target)
		{
			if (tiles[mpos.X, mpos.Y] == '.' || tiles[mpos.X, mpos.Y] == '0')
				return false; // no monster
			
			// monsters can see as far as their number (1's can see 1 tile away, 2's can see 2 tiles away, etc.)
			return Math.Max(Math.Abs(mpos.X - target.X), Math.Abs(mpos.Y - target.Y)) <= int.Parse(tiles[mpos.X, mpos.Y].ToString());
		}
		
		/// <summary>
		/// The max HP of the hero. When the hero defeats a monster, his HP will be restored by half the difference rounded down -
		/// e.g. at 65/100 HP, they will be restored by 17 points to 82/100. Thanks Nick! :D
		/// </summary>
		public int HeroMaxHP {get; private set;}
	}
}