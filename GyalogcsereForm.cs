using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sakk
{
	public partial class GyalogcsereForm : Form
	{
		public TablaForm tablaFormAktiv;
		public GyalogcsereForm(TablaForm form)
		{
			tablaFormAktiv = form;
			InitializeComponent();
		}
		
		void vezerClick(object sender, EventArgs e)
		{
			tablaFormAktiv.gyalogCsereValasztas = Tisztek.Vezer;
			this.Hide();
		}
		
		void bastyaClick(object sender, EventArgs e)
		{
			tablaFormAktiv.gyalogCsereValasztas = Tisztek.Bastya;
			this.Hide();
		}
		
		void futoClick(object sender, EventArgs e)
		{
			tablaFormAktiv.gyalogCsereValasztas = Tisztek.Futo;
			this.Hide();
		}
		
		void loClick(object sender, EventArgs e)
		{
			tablaFormAktiv.gyalogCsereValasztas = Tisztek.Lo;
			this.Hide();
		}
		
		void elrejtControl(object sender, EventArgs e)
		{
			this.Opacity = 0.01;
		}
		
		void felfedControl(object sender, EventArgs e)
		{
			this.Opacity = 1;
		}
	}
}
