using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppStart
{
    public partial class Config : Form
    {
        Timer _tm;
        public Config(Timer tm)
        {
            InitializeComponent();
            this._tm = tm;

            if (Constvariable.Timing == -1)
            {
                this.radioButton1.Checked = true;
                this.txttimespan.Text = this._tm.Interval.ToString();
            }
            else
            {
                this.radioButton2.Checked = true;
                this.textBox1.Text = Constvariable.Timing.ToString();
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.radioButton1.Checked)
                {
                    Constvariable.Timing = -1;
                    this._tm.Interval = Convert.ToInt32(this.txttimespan.Text.Trim());
                    //DBHelp help = new DBHelp();
                    //help.ExecuteNonQuery("");
                }
                if (this.radioButton2.Checked)
                {
                    Constvariable.Timing = Convert.ToInt32(this.textBox1.Text.Trim());
                    this._tm.Interval = 60000*5;
                }
            }
            catch (Exception ex)
            {

            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
