using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sakk
{
	public partial class TablaForm : Form
	{
		Rectangle[] mezok = new Rectangle[64];
		Jatszma jatszma1 = new Jatszma();
		Mezo honnan;
		Mezo hova;
		bool lepesJeloles = false;
		public TablaForm()
		{
			InitializeComponent();
			
			for(int i = 0;i < 8;i++)
			{
				for(int j = 0;j < 8;j++)
				{
					mezok[i * 8 + j] = new Rectangle(ClientSize.Width / 8 * j, ClientSize.Height / 8 * i, ClientSize.Width / 8, ClientSize.Height / 8);
				}
			}
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = this.CreateGraphics();
			Pen pen = new Pen(Color.White);
			pen.Width = 1;
			SolidBrush black = new SolidBrush(Color.Brown);
			Rectangle r = new Rectangle(0, 0, ClientSize.Width / 8, ClientSize.Height / 8);
			g.DrawRectangles(pen, mezok);
			for(int i = 0;i < 8;i++)
			{
				for(int j = 0;j < 8;j++)
				{
					if(i % 2 != 0)
					{
						if(j % 2 == 0)
							g.FillRectangle(black, mezok[i * 8 + j]);
					}
						
					else
					{
						if(j % 2 != 0)
							g.FillRectangle(black, mezok[i * 8 + j]);
					}
						
				}
			}
			
			for(char i = 'a';i <= 'h';i++)
			{
				for(int j = 1;j <= 8;j++)
				{
					if(!Sakk.TABLA[i + j.ToString()].Ures())
					{
						Mezo tempMezo = Sakk.TABLA[i + j.ToString()];
						g.DrawImage(tempMezo.Babu.Kep, mezok[Math.Abs(8 - tempMezo.Szam) * 8 + (int)tempMezo.Betu - 96 - 1]);
					}
				}
			}
			g.Dispose();
		}
		void TablaFormClicked(object sender, EventArgs e)
		{
			Point eger = PointToClient(Cursor.Position);
			int mezomeret = ClientSize.Width / 8;
			Mezo tempMezo = Sakk.TABLA[(char)((int)(eger.X / mezomeret) + 97) + (8 - (int)(eger.Y / mezomeret)).ToString()];
			if(tempMezo.Ures() && lepesJeloles == false)
			{
				//MessageBox.Show("Ures mezo!");
				return;
			}
				
			if(lepesJeloles == false)
			{
				honnan = tempMezo;
				lepesJeloles = true;
			}
			else
			{
				hova = tempMezo;
				if(honnan.Babu.Lep(honnan, hova))
				{
					//MessageBox.Show("Helyes lepes " + honnan.Babu.Tipus + "-el az " + honnan.Mezonev + " a " + hova.Mezonev + "!");
					hova.Babu = honnan.Babu;
					honnan.Babu = null;
					this.Invalidate();
				}
					
				else
				{
					//MessageBox.Show("Helytelen lepes " + honnan.Babu.Tipus + "-el az " + honnan.Mezonev + " a " + hova.Mezonev + "!");
				}
				
				lepesJeloles = false;
			}
		}
	}
}
