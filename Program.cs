using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace Sakk
{
	internal sealed class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new TablaForm());
		}
		
	}
	
	public class Sakk
	{
		public static Tabla TABLA;
		const int Jatekosszam = 2;
		
		public Sakk()
		{
			TABLA = new Tabla();
		}
	}
	
	public class Jatszma : Sakk
	{
		public Jatekos feher;
		public Jatekos fekete;
		public Jatekos soron;
		public List<Lepes> Lepesek {get; set;}
		
		public Jatszma()
		{
			// jatekosok
			feher = new Jatekos("Pisti", true);
			fekete = new Jatekos("Janos", false);
			soron = feher;
			Lepesek = new List<Lepes>();
			
			// a 8-8 gyalog hozzaadasa a babukhoz
			for(char i = 'a';i <= 'h';i++)
			{
				feher.TablaraRak(new Gyalog(true, TABLA[i + "2"]), TABLA[i + "2"]);
				fekete.TablaraRak(new Gyalog(false, TABLA[i + "7"]), TABLA[i + "7"]);
			}
			//bastyak
			feher.TablaraRak(new Bastya(true, TABLA["a1"]), TABLA["a1"]);
			feher.TablaraRak(new Bastya(true, TABLA["h1"]), TABLA["h1"]);
			fekete.TablaraRak(new Bastya(false, TABLA["a8"]), TABLA["a8"]);
			fekete.TablaraRak(new Bastya(false, TABLA["h8"]), TABLA["h8"]);
			//lovak
			feher.TablaraRak(new Lo(true, TABLA["b1"]), TABLA["b1"]);
			feher.TablaraRak(new Lo(true, TABLA["g1"]), TABLA["g1"]);
			fekete.TablaraRak(new Lo(false, TABLA["b8"]), TABLA["b8"]);
			fekete.TablaraRak(new Lo(false, TABLA["g8"]), TABLA["g8"]);
			//futok
			feher.TablaraRak(new Futo(true, TABLA["c1"]), TABLA["c1"]);
			feher.TablaraRak(new Futo(true, TABLA["f1"]), TABLA["f1"]);
			fekete.TablaraRak(new Futo(false, TABLA["c8"]), TABLA["c8"]);
			fekete.TablaraRak(new Futo(false, TABLA["f8"]), TABLA["f8"]);
			//vezerek
			feher.TablaraRak(new Vezer(true, TABLA["d1"]), TABLA["d1"]);
			fekete.TablaraRak(new Vezer(false, TABLA["d8"]), TABLA["d8"]);
			//kiralyok
			feher.TablaraRak(new Kiraly(true, TABLA["e1"]), TABLA["e1"]);
			fekete.TablaraRak(new Kiraly(false, TABLA["e8"]), TABLA["e8"]);
		}
		
		public bool Jatek()
		{
			while(!Lepesek[Lepesek.Count - 1].Matt)
			{
				
			}
			return true;
		}
		
		public bool SakkbanVan(bool szin)
		{
			if(szin == true) // feher
			{
				/*int i = 0;
				Babu tempBabu = feher.Babuk[i];
				while(tempBabu.Tipus != "kiraly")
				{
					i++;
				}*/
				foreach(Babu babu in fekete.Babuk)
				{
					if(babu.Lep(babu.Mezo, feher.Babuk[feher.Babuk.Count - 1].Mezo))
						return true;
				}
				return false;
			}
			else // fekete
			{
				/*int i = 0;
				Babu tempBabu = fekete.Babuk[i];
				while(tempBabu.Tipus != "kiraly")
				{
					i++;
				}*/
				foreach(Babu babu in feher.Babuk)
				{
					if(babu.Lep(babu.Mezo,fekete.Babuk[fekete.Babuk.Count - 1].Mezo))
						return true;
				}
				return false;
			}
		}
	}
	
	public class Jatekos
	{
		public string Nev {get; set;}
		public bool Szin {get; set;}
		public List<Babu> Babuk {get; set;}
		public bool Sakkban {get; set;}
		
		public Jatekos(string nev, bool szin)
		{
			this.Nev = nev;
			this.Szin = szin;
			this.Babuk = new List<Babu>();
		}
		
		public bool TablaraRak(Babu babu, Mezo hova)
		{
			if(Sakk.TABLA[hova.Mezonev].Ures())
			{
				Sakk.TABLA[hova.Mezonev].Babu = babu;
				Babuk.Add(babu);
				return true;
			}
			return false;
		}
		
		public void Felad()
		{
			
		}
	}
	
	public sealed class Lepes
	{
		public Mezo Honnan {get; set;}
		public Mezo Hova {get; set;}
		public bool Sakk {get; set;}
		public bool Matt {get; set;}
		public Lepes(Mezo honnan, Mezo hova, bool sakk, bool matt)
		{
			Honnan = honnan;
			Hova = hova;
			Sakk = sakk;
			Matt = matt;
		}
	}
	
	public sealed class Tabla
	{
		private Mezo[,] tabla;
		
		public Tabla()
		{
			this.tabla = new Mezo[8, 8];
			for(int i = 7;i >= 0;i--)
			{
				for(char j = 'a';j <= 'h';j++)
				{
					this.tabla[i, (int)j - 96 - 1] = new Mezo(j + (i + 1).ToString());
				}
			}
		}
		
		/*public Mezo this[Mezo index]
		{
			get
			{
				return tabla[Math.Abs(index.Szam - 8), (int)index.Betu - 96];
			}
			
			set
			{
				this.tabla[Math.Abs(index.Szam - 8), (int)index.Betu - 96] = value;
			}
			
		}*/
		
		public Mezo this[string index]
		{
			get
			{
				return tabla[(int)index[1] - 48 - 1, (int)index[0] - 96 - 1];
			}
			
			set
			{
				tabla[(int)index[1] - 48 - 1, (int)index[0] - 96 - 1] = value;
			}
			
		}
	}
	
	public sealed class Mezo
	{
		public char Betu {get; private set;}
		public int Szam {get; private set;}
		public string Mezonev {get; private set;}
		public Babu Babu {get; set;}
		
		public Mezo(string hova)
		{
			this.Betu = hova.ToLower()[0];
			this.Szam = (int)hova[1] - 48;
			this.Mezonev = hova;
			this.Babu = null;
		}
		
		public Mezo(string hova, Babu babu)
		{
			this.Betu = hova.ToLower()[0];
			this.Szam = (int)hova[1] - 48;
			this.Mezonev = hova;
			this.Babu = babu;
		}
		
		public bool Ures()
		{
			if(this.Babu == null)
				return true;
			else
				return false;
		}
	}
	
	public abstract class Babu
	{
		internal Image Kep {get; set;}
		public string Tipus {get; set;}
		public bool Szin {get; set;}
		public Mezo Mezo{get; set;}
		
		public abstract bool Lep(Mezo honnan, Mezo hova);
	}
	
	#region Babuk
	public sealed class Gyalog : Babu
	{
		public Gyalog(bool szin, Mezo hova)
		{
			this.Tipus = "gyalog";
			if(szin)
			{
				this.Szin = true;
				this.Kep = Resource1.gyalog_feher;
			}
				
			else
			{
				this.Szin = false;
				this.Kep = Resource1.gyalog_fekete;
			}
			this.Mezo = hova;
		}
		
		public override bool Lep(Mezo honnan, Mezo hova)
		{
			//TODO: az en passant lepest es amikor a gyalog eler a szembenlevo alapvonalra
			if(!hova.Ures() && hova.Babu.Szin == honnan.Babu.Szin)
				return false;
			if(honnan.Babu.Szin == true)
			{
				// LEPES
				if((hova.Ures() && hova.Szam == honnan.Szam + 1 && hova.Betu == honnan.Betu) || (hova.Ures() && hova.Szam == honnan.Szam + 2 && honnan.Szam == 2 && hova.Betu == honnan.Betu && Sakk.TABLA[hova.Betu + (hova.Szam - 1).ToString()].Ures()))
					return true;
				// UTES
				if(!hova.Ures() && hova.Szam == honnan.Szam + 1 && (hova.Betu == honnan.Betu -1 || hova.Betu == honnan.Betu + 1))
					return true;
				else
					return false;
			}
			if(honnan.Babu.Szin == false)
			{
				// LEPES
				if((hova.Ures() && hova.Szam == honnan.Szam - 1 && hova.Betu == honnan.Betu) || (hova.Ures() && hova.Szam == honnan.Szam - 2 && honnan.Szam == 7 && hova.Betu == honnan.Betu && Sakk.TABLA[hova.Betu + (hova.Szam + 1).ToString()].Ures()))
					return true;
				// UTES
				if(!hova.Ures() && hova.Szam == honnan.Szam - 1 && (hova.Betu == honnan.Betu -1 || hova.Betu == honnan.Betu + 1))
					return true;
				else
					return false;
			}
			return false;
		}
	}
	
	public sealed class Bastya : Babu
	{
		public Bastya(bool szin, Mezo hova)
		{
			this.Tipus = "bastya";
			if(szin)
			{
				this.Szin = true;
				this.Kep = Resource1.bastya_feher;
			}
			else
			{
				this.Szin = false;
				this.Kep = Resource1.bastya_fekete;
			}
			this.Mezo = hova;
		}
		
		public override bool Lep(Mezo honnan, Mezo hova)
		{
			if(!hova.Ures() && hova.Babu.Szin == honnan.Babu.Szin) // ez kiszuri, hogyha barati babut utnenk le, vagy ugyanabba a pozicioba akarnank lepni
				return false;
			if(hova.Betu == honnan.Betu) // FUGGOLEGES
			{
				int d = Math.Abs(hova.Szam - honnan.Szam) - 1; // a ket mezo kozotti mezok szama
				if(hova.Szam > honnan.Szam) // FEL
				{
					for(int i = 1;i <= d;i++)
					{
						if(!Sakk.TABLA[hova.Betu + (honnan.Szam + i).ToString()].Ures())
							return false;
					}
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
				if(hova.Szam < honnan.Szam) // LE
				{
					for(int i = 1;i <= d;i++)
					{
						if(!Sakk.TABLA[hova.Betu + (honnan.Szam - i).ToString()].Ures())
							return false;
					}
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
				
			}
			if(hova.Szam == honnan.Szam) // VIZSZINTES
			{
				int d = Math.Abs(hova.Betu - hova.Betu) - 1; // a ket mezo kozotti mezok szama
				if(hova.Betu > honnan.Betu) // BALRA
				{
					for(int i = 1;i <= d;i++)
					{
						if(!Sakk.TABLA[(char)(hova.Betu + i) + hova.Szam.ToString()].Ures())
							return false;
					}
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
				if(hova.Betu < honnan.Betu) // JOBBRA
				{
					for(int i = 1;i <= d;i++)
					{
						if(!Sakk.TABLA[(char)(honnan.Betu + i) + hova.Szam.ToString()].Ures())
							return false;
					}
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
			}
			return false;
		}
	}
	
	public sealed class Lo : Babu
	{
		public Lo(bool szin, Mezo hova)
		{
			this.Tipus = "lo";
			if(szin)
			{
				this.Szin = true;
				this.Kep = Resource1.lo_feher;
			}
			else
			{
				this.Szin = false;
				this.Kep = Resource1.lo_fekete;
			}
			this.Mezo = hova;
		}
		
		public override bool Lep(Mezo honnan, Mezo hova)
		{
			if(!hova.Ures() && hova.Babu.Szin == honnan.Babu.Szin) // ez kiszuri, hogyha barati babut utnenk le, vagy ugyanabba a pozicioba akarnank lepni
				return false;
			if(((honnan.Betu - 2 == hova.Betu || honnan.Betu + 2 == hova.Betu) && (honnan.Szam + 1 == hova.Szam || honnan.Szam - 1 == hova.Szam))
			   || ((honnan.Betu - 1 == hova.Betu || honnan.Betu + 1 == hova.Betu) && (honnan.Szam + 2 == hova.Szam || honnan.Szam - 2 == hova.Szam)))
			{
				if(hova.Ures()) // LEPES
					return true;
				if(!hova.Ures()) // UTES
					return true;
			}
			return false;
		}
	}
	
	public sealed class Futo : Babu
	{
		public Futo(bool szin, Mezo hova)
		{
			this.Tipus = "futo";
			if(szin)
			{
				this.Szin = true;
				this.Kep = Resource1.futo_feher;
			}
				
			else
			{
				this.Szin = false;
				this.Kep = Resource1.futo_fekete;
			}
			this.Mezo = hova;				
		}
		
		public override bool Lep(Mezo honnan, Mezo hova)
		{
			if(!hova.Ures() && hova.Babu.Szin == honnan.Babu.Szin) // ez kiszuri, hogyha barati babut utnenk le, vagy ugyanabba a pozicioba akarnank lepni
				return false;
			if(Math.Abs(hova.Betu - honnan.Betu) == Math.Abs(hova.Szam - honnan.Szam))
			{
				int d = Math.Abs(honnan.Betu - hova.Betu) - 1; // a ket mezo kozotti mezok szama
				if(d == 0) // SZOMSZEDOS MEZO
				{
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
				if(hova.Betu > honnan.Betu) // JOBBRA
				{
					if(hova.Szam > honnan.Szam) // FEL
					{
						for(int i = 1;i <= d;i++)
						{
							if(!Sakk.TABLA[(char)(honnan.Betu + i) + (honnan.Szam + i).ToString()].Ures())
								return false;
						}
						if(hova.Ures()) // LEPES
							return true;
						if(!hova.Ures()) // UTES
							return true;
					}
					if(hova.Szam < honnan.Szam) // LE
					{
						for(int i = 1;i <= d;i++)
						{
							if(!Sakk.TABLA[(char)(honnan.Betu + i) + (honnan.Szam - i).ToString()].Ures())
								return false;
						}
						if(hova.Ures()) // LEPES
							return true;
						if(!hova.Ures()) // UTES
							return true;
					}
				}
				if(hova.Betu < honnan.Betu) // BALRA
				{
					if(hova.Szam > honnan.Szam) // FEL
					{
						for(int i = 1;i <= d;i++)
						{
							if(!Sakk.TABLA[(char)(honnan.Betu - i) + (honnan.Szam + i).ToString()].Ures())
								return false;
						}
						if(hova.Ures()) // LEPES
							return true;
						if(!hova.Ures()) // UTES
							return true;
					}
					if(hova.Szam < honnan.Szam) // LE
					{
						for(int i = 1;i <= d;i++)
						{
							if(!Sakk.TABLA[(char)(honnan.Betu - i) + (honnan.Szam - i).ToString()].Ures())
								return false;
						}
						if(hova.Ures()) // LEPES
							return true;
						if(!hova.Ures()) // UTES
							return true;
					}
				}
			}
			return false;
		}
	}
	
	public sealed class Vezer : Babu
	{
		public Vezer(bool szin, Mezo hova)
		{
			this.Tipus = "vezer";
			if(szin)
			{
				this.Szin = true;
				this.Kep = Resource1.vezer_feher;
			}
				
			else
			{
				this.Szin = false;
				this.Kep = Resource1.vezer_fekete;
			}
			this.Mezo = hova;
		}
		
		public override bool Lep(Mezo honnan, Mezo hova)
		{
			if(!hova.Ures() && hova.Babu.Szin == honnan.Babu.Szin) // ez kiszuri, hogyha barati babut utnenk le, vagy ugyanabba a pozicioba akarnank lepni
				return false;
			// ATLOSAN
			if(Math.Abs(hova.Betu - honnan.Betu) == Math.Abs(hova.Szam - honnan.Szam))
			{
				int d = Math.Abs(honnan.Betu - hova.Betu) - 1; // a ket mezo kozotti mezok szama
				if(d == 0) // SZOMSZEDOS MEZO
				{
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
				if(hova.Betu > honnan.Betu) // JOBBRA
				{
					if(hova.Szam > honnan.Szam) // FEL
					{
						for(int i = 1;i <= d;i++)
						{
							if(!Sakk.TABLA[(char)(honnan.Betu + i) + (honnan.Szam + i).ToString()].Ures())
								return false;
						}
						if(hova.Ures()) // LEPES
							return true;
						if(!hova.Ures()) // UTES
							return true;
					}
					if(hova.Szam < honnan.Szam) // LE
					{
						for(int i = 1;i <= d;i++)
						{
							if(!Sakk.TABLA[(char)(honnan.Betu + i) + (honnan.Szam - i).ToString()].Ures())
								return false;
						}
						if(hova.Ures()) // LEPES
							return true;
						if(!hova.Ures()) // UTES
							return true;
					}
				}
				if(hova.Betu < honnan.Betu) // BALRA
				{
					if(hova.Szam > honnan.Szam) // FEL
					{
						for(int i = 1;i <= d;i++)
						{
							if(!Sakk.TABLA[(char)(honnan.Betu - i) + (honnan.Szam + i).ToString()].Ures())
								return false;
						}
						if(hova.Ures()) // LEPES
							return true;
						if(!hova.Ures()) // UTES
							return true;
					}
					if(hova.Szam < honnan.Szam) // LE
					{
						for(int i = 1;i <= d;i++)
						{
							if(!Sakk.TABLA[(char)(honnan.Betu - i) + (honnan.Szam - i).ToString()].Ures())
								return false;
						}
						if(hova.Ures()) // LEPES
							return true;
						if(!hova.Ures()) // UTES
							return true;
					}
				}
			}
			// FUGGOLEGES
			if(hova.Betu == honnan.Betu)
			{
				int d = Math.Abs(hova.Szam - honnan.Szam) - 1; // a ket mezo kozotti mezok szama
				if(hova.Szam > honnan.Szam) // FEL
				{
					for(int i = 1;i <= d;i++)
					{
						if(!Sakk.TABLA[hova.Betu + (honnan.Szam + i).ToString()].Ures())
							return false;
					}
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
				if(hova.Szam < honnan.Szam) // LE
				{
					for(int i = 1;i <= d;i++)
					{
						if(!Sakk.TABLA[hova.Betu + (honnan.Szam - i).ToString()].Ures())
							return false;
					}
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
				
			}
			// VIZSZINTES
			if(hova.Szam == honnan.Szam)// VIZSZINTES
			{
				int d = Math.Abs(hova.Betu - hova.Betu) - 1; // a ket mezo kozotti mezok szama
				if(hova.Betu > honnan.Betu) // BALRA
				{
					for(int i = 1;i <= d;i++)
					{
						if(!Sakk.TABLA[(char)(hova.Betu + i) + hova.Szam.ToString()].Ures())
							return false;
					}
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
				if(hova.Betu < honnan.Betu) // JOBBRA
				{
					for(int i = 1;i <= d;i++)
					{
						if(!Sakk.TABLA[(char)(honnan.Betu + i) + hova.Szam.ToString()].Ures())
							return false;
					}
					if(hova.Ures()) // LEPES
						return true;
					if(!hova.Ures()) // UTES
						return true;
				}
			}
			return false;
		}
	}
	
	public sealed class Kiraly : Babu
	{
		public Kiraly(bool szin, Mezo hova)
		{
			this.Tipus = "kiraly";
			if(szin)
			{
				this.Szin = true;
				this.Kep = Resource1.kiraly_feher;
			}
				
			else
			{
				this.Szin = false;
				this.Kep = Resource1.kiraly_fekete;
			}
			this.Mezo = hova;
		}
		
		public override bool Lep(Mezo honnan, Mezo hova)
		{
			if(!hova.Ures() && hova.Babu.Szin == honnan.Babu.Szin) // ez kiszuri, hogyha barati babut utnenk le, vagy ugyanabba a pozicioba akarnank lepni
				return false;
			if(Math.Abs(hova.Betu - honnan.Betu) <= 1 && Math.Abs(hova.Szam - honnan.Szam) <= 1)
			{
				if(hova.Ures()) // LEPES
					return true;
				if(!hova.Ures()) // UTES
					return true;
			}
			return false;
		}
	}
	#endregion
}
