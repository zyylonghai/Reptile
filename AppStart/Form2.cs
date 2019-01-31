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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.webBrowser1.Navigate("https://www.terapeak.com/");
            //var elem= this.webBrowser1.Document.GetElementById("");
        }
    }
}
