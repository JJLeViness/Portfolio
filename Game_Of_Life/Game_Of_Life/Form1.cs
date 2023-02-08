using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        // The universe array
        
        bool[,] universe = new bool[20,20];

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;


        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        //For Neighbour Counts
        bool NeighbourDisplay=false;
        int Count = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false;
        }

        private int CountNeighbours(int row, int Col)
        {
            int count = 0;
            if (row - 1 >= 0 && Col - 1 >= 0 && universe[row - 1, Col - 1] == true) { count++; }
            
            if (row - 1 >= 0 && universe[row - 1, Col] == true) { count++; }
            if (row - 1 >= 0 && Col + 1 < 20 && universe[row - 1, Col + 1] == true) { count++;}
            if (Col - 1 >= 0 && universe[row,Col-1] == true) { count++; }
            if (Col + 1 < 20 && universe[row,Col+1] == true) { count++; }
            if (row + 1 < 20 && Col - 1 >= 0 && universe[row+1,Col-1] == true) { count++; }
            if (row + 1 < 20 && universe[row+1,Col] == true) { count++; }
            if (row + 1 < 20 && Col + 1 < 20 && universe[row+1,Col+1] == true) { count++; }

            return count;


        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            bool[,] Grid = new bool[20,20];
            for (int row = 0; row < universe.GetLength(0); row++)
            {
                for (int Col = 0; Col < universe.GetLength(1); Col++)
                {
                    int Count = CountNeighbours(row, Col);
                    if (universe[row, Col])
                    {

                        if (Count == 2 || Count == 3)
                        {
                            Grid[row, Col] = true;
                        }
                        if (Count < 2 || Count > 3)
                        {
                            Grid[row, Col] = false;
                        }
                        else
                        {
                            if (Count == 3)
                            {
                                Grid[row, Col] = true;
                            }
                        }
                    }

                }


                // Increment generation count
                generations++;

                // Update status strip generations
            }
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            universe = Grid;
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }


        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                        if(NeighbourDisplay==true)
                        {
                            Font font = new Font("Retrieve", 10f);
                            StringFormat NumberFormat=new StringFormat();
                            NumberFormat.Alignment= StringAlignment.Center;
                            NumberFormat.LineAlignment = StringAlignment.Center;
                            Count=CountNeighbours(x, y);
                            if (Count > 3 || Count < 2)
                            {
                                e.Graphics.DrawString(Count.ToString(), font, Brushes.Black, cellRect, NumberFormat);
                            }
                            else
                            {
                                e.Graphics.DrawString(Count.ToString(),font,Brushes.Green, cellRect, NumberFormat);
                            }
                        }
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cellColor = Color.DarkOrchid;
            graphicsPanel1.Invalidate();
        }

        private void cyanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cellColor = Color.Cyan;
            graphicsPanel1.Invalidate();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cellColor = Color.Crimson;
            graphicsPanel1.Invalidate();
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cellColor = Color.Orange;
            graphicsPanel1.Invalidate();
        }

        private void pinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Fuchsia;
            graphicsPanel1.Invalidate();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Green;
            graphicsPanel1.Invalidate();
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Blue;
            graphicsPanel1.Invalidate();
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridColor = Color.Black;
            graphicsPanel1.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)//new 
        {
            timer.Enabled= false;
            generations = 0;
            
            

        }

        private void toolStripButton1_Click(object sender, EventArgs e)//Play Button
        {
            timer.Enabled = true;
            

        }

        private void toolStripButton2_Click(object sender, EventArgs e)//Pause Button
        {
            timer.Enabled=false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//Next Button
        {
            NextGeneration();
        }

        private void neighbourCountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NeighbourDisplay = true;
            graphicsPanel1.Invalidate();
        }
    }
}
