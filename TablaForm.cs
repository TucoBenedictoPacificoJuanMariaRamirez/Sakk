﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sakk
{
	public partial class TablaForm : Form
	{
		Rectangle[] mezok = new Rectangle[64];
		Mezo honnan;
		Mezo hova;
		bool lepesJeloles = false;
		public Tisztek gyalogCsereValasztas;
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
			//Graphics g = this.CreateGraphics();
			Graphics g = e.Graphics;
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
			if(lepesJeloles)
			{
				for(char i = 'a';i <= 'h';i++)
				{
					for(int j = 1;j <= 8;j++)
					{
						if(honnan.Babu.Lephet(honnan, Sakk.TABLA[i + j.ToString()], true))
						{
							//TODO: itt mar a hovaLephet metodust kene hasznalni
							Pen kijeloles = new Pen(Color.FromArgb(200, Color.Black));
							kijeloles.Width = 5;
						   	Mezo tempMezo = Sakk.TABLA[i + j.ToString()];
						   	g.DrawRectangle(kijeloles, mezok[Math.Abs(8 - tempMezo.Szam) * 8 + (int)tempMezo.Betu - 96 - 1]);
						   	//g.DrawImage(tempMezo.Babu.Kep, mezok[Math.Abs(8 - tempMezo.Szam) * 8 + (int)tempMezo.Betu - 96 - 1]);
						}
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
			// HA URES MEZOBE KATTINTUNK
			if(tempMezo.Ures() && lepesJeloles == false)
			{
				MessageBox.Show("Ures mezo!");
				this.Invalidate();
				return;
			}
			// HA NEM A KATTINTOTT JATEKOS VAN SORON
			if(honnan == null && tempMezo.Babu.Szin != Sakk.jatszma1.soron.Szin)
			{
				lepesJeloles = false;
				this.Invalidate();
				MessageBox.Show("Nem o van soron!");
			}
			// BABU KIJELOLES, AMELYIKKEL LEPNI SZERETNENK
			else if(lepesJeloles == false)
			{
				honnan = tempMezo;
				lepesJeloles = true;
				this.Invalidate();
			}
			// HA MI VAGYUNK SORON
			else if(Sakk.jatszma1.soron.Szin == honnan.Babu.Szin)
			{
				hova = tempMezo;
				// HELYES LEPES
				if(honnan.Babu.Lephet(honnan, hova, true))
				{
					//MessageBox.Show("Helyes lepes " + honnan.Babu.Tipus + "-el az " + honnan.Mezonev + " a " + hova.Mezonev + "!");
					if(!hova.Ures())
					{
						if(hova.Babu.Szin)
						{
							Sakk.jatszma1.feher.Babuk.Remove(hova.Babu);
						}
						else
						{
							Sakk.jatszma1.fekete.Babuk.Remove(hova.Babu);
						}
					}
					// LEPES
					Sakk.jatszma1.Lepes(honnan.Babu, hova);
					
					// Gyalog promocio
					//Lepes utobbiLepes = Sakk.jatszma1.Lepesek[Sakk.jatszma1.Lepesek.Count - 1];
					GyalogcsereForm gycsere = new GyalogcsereForm(this);
					if(Sakk.jatszma1.soron.Szin && hova.Szam == 8 && hova.Babu.Tipus == "gyalog")
					{
						gycsere.ShowDialog();
						Sakk.jatszma1.feher.gyalogPromocio(hova, gyalogCsereValasztas);
						
					}
					if(!Sakk.jatszma1.soron.Szin && hova.Szam == 1 && hova.Babu.Tipus == "gyalog")
					{
						gycsere.ShowDialog();
						Sakk.jatszma1.fekete.gyalogPromocio(hova, gyalogCsereValasztas);
					}
					
					bool sakk = Sakk.jatszma1.SakkbanVan(!hova.Babu.Szin);
					bool matt = Sakk.jatszma1.Matt(!hova.Babu.Szin);
					Sakk.jatszma1.Lepesek.Add(new Lepes(honnan, hova, sakk, matt));
					
					if(matt)
					{
						MessageBox.Show("Matt!");
						return;
					}
					
					if(sakk)
						MessageBox.Show("Sakk!");
					
					Sakk.jatszma1.soron.Szin = !Sakk.jatszma1.soron.Szin;
					honnan = null;
					this.Invalidate();
				}
				// HELYTELEN LEPES
				else
				{
					MessageBox.Show("Helytelen lepes " + honnan.Babu.Tipus + "-el az " + honnan.Mezonev + " a " + hova.Mezonev + "!");
					honnan = null;
					this.Invalidate();
				}
				
				lepesJeloles = false;
			}
			else
			{
				lepesJeloles = false;
				this.Invalidate();
				MessageBox.Show("Nem o van soron!");
				honnan = null;
			}
				
		}
	}
}
