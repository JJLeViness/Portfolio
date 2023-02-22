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
    public partial class HeadsUp : Form
    {
        public string GetBoundaryStyle()
        {
            return Counting.Text;
        }
        public string GetLivingCells()
        {
            return LivingCell.Text;
        }


        public string GetGeneration()
        {
            return Generation.Text;
        }

        public string GetRows()
        {
            return Rows.Text;
        }
        public string GetCol()
        {
            return Columns.Text;

        }

       

        public void SetBoundaryStyle(string boundaryStyle)
        {
            Counting.Text = boundaryStyle;

        }

        public void SetLivingCells(string LivingCells)
        {
            LivingCell.Text = LivingCells;
        }

        public void SetGeneration(string generation)
        {
            Generation.Text=generation;
        }

        public void SetRows(string rowstext)
        {
            Rows.Text = rowstext;
        }

        public void SetCols(string colstext)
        {
            Columns.Text = colstext;
        }

        public HeadsUp()
        {
            InitializeComponent();
        }
    }
}
