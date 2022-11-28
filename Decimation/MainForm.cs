
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Decimation
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private Map map;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			map = new Map(20, 20, 9, 100);
			pnlMap.Invalidate();
			lblHP.Text = "Rationality: " + map.HeroHP + " / " + map.HeroMaxHP;
		}
		
		
		
		void PnlMapPaint(object sender, PaintEventArgs e)
		{
			var img = new Bitmap((int)(1.1f * pnlMap.Font.Size * map.Width), (int)(1.1f * pnlMap.Font.Size * (map.Height + 1)));
			var g = Graphics.FromImage(img);
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					var sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					g.DrawString(map.GetCharacter(x, y).ToString(), pnlMap.Font, new SolidBrush(map.GetTileColor(x, y)), (x + 0.5f) * pnlMap.Font.Size * 1.1f, y * pnlMap.Font.Size * 1.1f, sf);
				}
			}
			e.Graphics.DrawImage(img, 0, 0);
		}
		
		void MainFormKeyUp(object sender, KeyEventArgs e)
		{
			lblMessage.Text = "";
			if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.Insert)
				MessageBox.Show("Move the zero with the numpad. You can't rest with 5, though!\nUse +, -, *, and / to polymorph adjacent enemies.\nTry to make them all zero!\nSee Readme.txt for more instructions.");
			else if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.End)
				lblMessage.Text = map.MoveHero(-1, 1);
			else if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.Down)
				lblMessage.Text = map.MoveHero(0, 1);
			else if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.PageDown)
				lblMessage.Text = map.MoveHero(1, 1);
			else if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.Left)
				lblMessage.Text = map.MoveHero(-1, 0);
			else if (e.KeyCode == Keys.NumPad5)
				lblMessage.Text = "You cannot sit still!";
			else if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.Right)
				lblMessage.Text = map.MoveHero(1, 0);
			else if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.Home)
				lblMessage.Text = map.MoveHero(-1, -1);
			else if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.Up)
				lblMessage.Text = map.MoveHero(0, -1);
			else if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.PageUp)
				lblMessage.Text = map.MoveHero(1, -1);
			else if (e.KeyCode == Keys.Add)
				lblMessage.Text = map.DoAddition();
			else if (e.KeyCode == Keys.Subtract)
				lblMessage.Text = map.DoSubtraction();
			else if (e.KeyCode == Keys.Multiply)
				lblMessage.Text = map.DoMultiplication();
			else if (e.KeyCode == Keys.Divide)
				lblMessage.Text = map.DoDivision();
			
			pnlMap.Invalidate();
			lblHP.Text = "Rationality: " + map.HeroHP;
			
			// check for end of game
			if (map.HeroHP <= 0)
			{
				MessageBox.Show("Oh no!\nIt appears the wild digits have overrun our valiant zero!\nGAME OVER!");
				Application.Exit();
			}
			else if (map.MonstersLeft == 0)
			{
				MessageBox.Show("It's zero hero day!\nHe stands alone today!\nTah rah rah boom de ay!\nIt's zero hero day!\n*** CONGRATULATIONS! You Win! ***");
				Application.Exit();
			}
		}
	}
}
