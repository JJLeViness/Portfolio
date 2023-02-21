using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Save_Dialogue : Form
    {
        public string File;
        public Save_Dialogue()
        {
            InitializeComponent();
        }

        public string GetName()
        {
            return textBox1.Text;
        }

        public void SaveText(string FileName)
        {
            textBox1.Text = FileName;
        }
    }
}
