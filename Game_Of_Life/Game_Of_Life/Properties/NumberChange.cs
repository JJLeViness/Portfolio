using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life.Properties
{
    public partial class NumberChange : Form
    {
       
        public NumberChange()
        {
            InitializeComponent();
        }

        public int GetRow()
        {
            return (int)numericUpDownRow.Value;
        }

        public int GetCol()
        {
            return (int)numericUpDownCol.Value;
        }

        public void SetRow(int row)
        {
            numericUpDownRow.Value = row;

        }

        public void SetCol(int col)
        {
            numericUpDownCol.Value = col;

        }
    }
}
