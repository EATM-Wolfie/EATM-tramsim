using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RouteBuilder
{
	public partial class FrmMDI : Form
	{
		public FrmMDI()
		{
			InitializeComponent();
		}

		private void mnuMap_Click(object sender, EventArgs e)
		{
			FrmMap f = new FrmMap();
			f.Parent = this;
			f.Show();
		}
	}
}
