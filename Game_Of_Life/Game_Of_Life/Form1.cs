using Game_Of_Life.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        // The universe array

        bool[,] universe = new bool[Rows, Cols];

        //Scratchpad array
        bool[,] Grid = new bool[Rows, Cols];

        //Variables to Edit the Size of the Universe, Beginning size of 20x20
        static int Rows=20;
        static int Cols = 20;

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;
        
        
        


        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        //For Neighbour Counts
        bool NeighbourDisplay = false;
        int Count = 0;

        //View Grid
        bool GridDisplay = true;

        //Living Cell Display
        int LivingCells = 0;

        

        //For Count Style Change
        bool CountStyle = false;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false;
            cellColor =Properties.Settings.Default.CellColor;
            gridColor= Properties.Settings.Default.GridColor;
            graphicsPanel1.BackColor = Properties.Settings.Default.PanelColor;
            Rows = Properties.Settings.Default.Rows;
            Cols=Properties.Settings.Default.Columns;
            timer.Interval = Properties.Settings.Default.TimerSpeed;
            
        }

        private int LivingCellsCount(int LivingCells)
        {
            LivingCells = 0;
            for (int col = 0; col < universe.GetLength(1); col++)
            {
                for (int row = 0; row < universe.GetLength(0); row++)
                {
                    if (universe[row, col])
                    {
                        LivingCells++;
                    }
                }
            }
            return LivingCells;
        }
        

       

        private int CountNeighbours(int row, int Col) //Finite Count using sketch notes
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int rowcheck = row + xOffset;
                    int Colcheck = Col + yOffset;
                    if (yOffset == 0 && xOffset == 0)
                    {
                        continue;
                    }
                    if (rowcheck < 0)
                    {
                        continue;

                    }
                    if (Colcheck < 0)
                    {
                        continue;
                    }
                    if (rowcheck >= xLen)
                    {
                        continue;
                    }
                    if (Colcheck >= yLen)
                    {
                        continue;
                    }
                    if (universe[rowcheck, Colcheck] == true)
                    {
                        count++;
                    }
                }
            }
            return count;


        }

        private int CountNeighboursToroidal(int row, int col)//Toroidal Count from sketch notes
        {
            int count = 0;
            int xlen = universe.GetLength(0);
            int ylen = universe.GetLength(1);

            for (int yoffset = -1; yoffset <= 1; yoffset++)
            {
                for (int xoffset = -1; xoffset <= 1; xoffset++)
                {
                    int xcheck = row + xoffset;
                    int ycheck = col + yoffset;
                    if (xoffset == 0 && yoffset == 0)
                    { continue; }
                    if (xcheck < 0)
                    {
                        xcheck = xlen - 1;
                    }
                    if (ycheck < 0)
                    {
                        ycheck = ylen - 1;
                    }
                    if (xcheck >= xlen)
                    {
                        xcheck = 0;
                    }
                    if (ycheck >= ylen)
                    {
                        ycheck = 0;
                    }
                    if (universe[xcheck, ycheck] == true)
                    { count++; }
                }
                
            }
            return count;
        }
        // Calculate the next generation of cells
        private void NextGeneration()
        {


            for (int row = 0; row < universe.GetLength(0); row++)
            {
                for (int Col = 0; Col < universe.GetLength(1); Col++)
                {
                    if (CountStyle == true)//Option to allow for choosing counting behaviour
                    {
                        int Count = CountNeighboursToroidal(row, Col);
                        if (universe[row, Col] == true)
                        {

                            if (Count == 2 || Count == 3)
                            {
                                Grid[row, Col] = true;
                            }
                            if (Count < 2 || Count > 3)
                            {
                                Grid[row, Col] = false;
                            }

                        }
                        else
                        {
                            if (Count == 3)
                            {
                                Grid[row, Col] = true;
                            }
                            else
                            {
                                Grid[row, Col] = false;
                            }
                        }
                    }
                    else
                    {

                        int Count = CountNeighbours(row, Col);
                        if (universe[row, Col] == true)
                        {

                            if (Count == 2 || Count == 3)
                            {
                                Grid[row, Col] = true;
                            }
                            if (Count < 2 || Count > 3)
                            {
                                Grid[row, Col] = false;
                            }

                        }
                        else
                        {
                            if (Count == 3)
                            {
                                Grid[row, Col] = true;
                            }
                            else
                            {
                                Grid[row, Col] = false;
                            }
                        }

                    }

                }
            
            }
            //Method to count living cells total
            LivingCells = 0;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (universe[x, y])
                    {
                        LivingCells++;
                    }
                }
            }
            // Increment generation count
            generations++;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            toolStripStatusLabel1.Text="Living Cells ="+LivingCells.ToString();
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
                        if (NeighbourDisplay == true)//Added to allow for drawing in the number of neighbours
                        {
                            Font font = new Font("Retrieve", 10f);
                            StringFormat NumberFormat = new StringFormat();
                            NumberFormat.Alignment = StringAlignment.Center;
                            NumberFormat.LineAlignment = StringAlignment.Center;
                            if(CountStyle== true)
                            {
                                Count=CountNeighboursToroidal(x, y);
                            }
                            else
                            {
                                Count=CountNeighbours(x, y);
                            }
                            if (Count > 3 || Count < 2)
                            {
                                e.Graphics.DrawString(Count.ToString(), font, Brushes.Black, cellRect, NumberFormat);
                            }
                            else
                            {
                                e.Graphics.DrawString(Count.ToString(), font, Brushes.Green, cellRect, NumberFormat);
                            }
                        }
                    }

                    // Outline the cell with a pen
                    if (GridDisplay == true)
                    {
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }
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
                if (universe[x,y])//Added logic to enable a real time count of the living cells on screen
                {
                    LivingCells++;
                }
                else
                {
                    LivingCells--;
                }
                toolStripStatusLabel1.Text = "Living Cells = " + LivingCells.ToString();

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
            timer.Enabled = false;
            generations = 0;
            LivingCells= 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            toolStripStatusLabel1.Text="Living Cells = "+LivingCells.ToString();

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(1); x++)
                {
                    universe[x, y] = false;
                }
                graphicsPanel1.Invalidate();
            }




        }

        private void toolStripButton1_Click(object sender, EventArgs e)//Play Button
        {
            timer.Enabled = true;


        }

        private void toolStripButton2_Click(object sender, EventArgs e)//Pause Button
        {
            timer.Enabled = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//Next Button
        {
            NextGeneration();
        }



        private void milisecondsToolStripMenuItem_Click(object sender, EventArgs e)//Miliseconds interval
        {
            timer.Interval = 100;
        }

        private void secondsToolStripMenuItem_Click(object sender, EventArgs e)//Seconds interval
        {
            timer.Interval = 1000;
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)//Turn Off Neighbour Display
        {
            NeighbourDisplay = false;
            graphicsPanel1.Invalidate();
        }



        private void newRandomToolStripMenuItem_Click(object sender, EventArgs e)//Random Generated Array
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(1); x++)
                {
                    universe[x, y] = false;
                }
                graphicsPanel1.Invalidate();
            }
            Random random = new Random();
            for (int col = 0; col < universe.GetLength(1); col++)
            {
                for (int row = 0; row < universe.GetLength(0); row++)
                {
                    int rand = random.Next();

                    if (rand % 4 == 0) { Grid[row, col] = true; }
                }
            }
            universe = Grid;
            timer.Enabled = false;
            generations = 0;
            LivingCells = 0;
            for(int col = 0; col< universe.GetLength(1);col++)//Looping through array to count living cells
            {
                for(int row=0;row<universe.GetLength(0);row++)
                {
                    if (universe[row,col])
                    {
                        LivingCells++;
                    }
                }
            }
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
                toolStripStatusLabel1.Text=" Living Cells = "+LivingCells.ToString();    
            graphicsPanel1.Invalidate();
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)//Turn on Grid Outline
        {
            GridDisplay = true;
            graphicsPanel1.Invalidate();
        }

        private void offToolStripMenuItem1_Click(object sender, EventArgs e)//Turn off Grid Outline
        {
            GridDisplay = false;
            graphicsPanel1.Invalidate();
        }

        private void onToolStripMenuItem1_Click(object sender, EventArgs e)//Turn on Neighbour Count
        {
            NeighbourDisplay = true;
            graphicsPanel1.Invalidate();
        }

        private void offToolStripMenuItem_Click_1(object sender, EventArgs e)//Turn Off Neighbour Count
        {
            NeighbourDisplay = false;
            graphicsPanel1.Invalidate();

        }

        private void heightAndWidthToolStripMenuItem_Click(object sender, EventArgs e)//Edit Rows and Columns
        {
            NumberChange RowCol = new NumberChange();
            RowCol.SetRow(Rows);
            RowCol.SetCol(Cols);
            if(DialogResult.OK==RowCol.ShowDialog())
            {
                Rows = RowCol.GetRow();
                Cols= RowCol.GetCol();
                
                
            }
            Grid = new bool[Rows,Cols];//Resizing scratchpad array for resizing main universe
            for(int x=0;x<Cols&&x<universe.GetLength(1);x++)
            {
                for(int y=0;y<Rows&&y<universe.GetLength(0);y++)
                {
                    Grid[x, y] = universe[x,y];
                    
                }
            }
            universe = Grid;
            graphicsPanel1.Invalidate();
            
            

            

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)//Save
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "All Files|*.*|Cells|*.cells";
            save.FilterIndex = 2; save.DefaultExt = "cells";
            if (DialogResult.OK == save.ShowDialog())
            {
                StreamWriter Saver = new StreamWriter(save.FileName);
                Saver.WriteLine('!' + save.FileName);//! added to define comments in file
                Saver.WriteLine('!' + "Saved on " + DateTime.Now);
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    string Row = "";
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        if (universe[x, y])
                        {
                            Row += 'O';
                        }
                        else
                        {
                            Row += '.';
                        }
                    }
                    Saver.WriteLine(Row);
                }


                Saver.Close();
            }
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)//Load Option
        {
            OpenFileDialog openFileDialogL = new OpenFileDialog();
            openFileDialogL.Filter = "All Files|*.*|Cells|*.cells";
            openFileDialogL.FilterIndex = 2;
            if(DialogResult.OK==openFileDialogL.ShowDialog())
            {
                //Variables to Resize Array if necessary 
                int Width=0;
                int Height=0;
                
                StreamReader Load= new StreamReader(openFileDialogL.FileName);
                
                while(!Load.EndOfStream)
                {
                    string DocLength = Load.ReadLine();
                    if (DocLength[0]=='!')//Ignore Comments to get proper universe size
                    { continue; }
                    Height++;

                    if(Width<DocLength.Length)
                    {
                        Width = DocLength.Length;
                    }
                    universe=new bool[Width,Height];
                    Grid=new bool[Width,Height];
                }
                Load.BaseStream.Seek(0, SeekOrigin.Begin);//Resets reader to top of file
                int y = 0;
                while(!Load.EndOfStream)
                {
                    
                    string row= Load.ReadLine();
                    if (row[0]=='!')//Ignore Comments in File
                    { continue; }   
                    for(int x=0;x<row.Length;x++)
                    {
                        if (row[x]=='O')
                        {
                            universe[x,y] = true;
                        }
                        if (row[x]=='.')
                        {
                            universe[x,y] = false;
                        }
                       
                        
                    }
                    y++;
                }
                graphicsPanel1.Invalidate();
                Load.Close();
                
            }

        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e) //finite Style
        {
            CountStyle = false;
            
        }

        private void toToolStripMenuItem_Click(object sender, EventArgs e)//Toroidal Style
        {
            CountStyle= true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)//Speed up Timer
        {
            timer.Interval = timer.Interval / 2;
        }

        private void ChangeBackPanel_Click(object sender, EventArgs e)//Change BackPanel Color
        {
            ColorDialog Color=new ColorDialog();
            if(DialogResult.OK==Color.ShowDialog())
            {
                graphicsPanel1.BackColor = Color.Color;

            }
        }

        private void SlowTimer_Click(object sender, EventArgs e)//Speed up Timer
        {
            timer.Interval = timer.Interval * 2;
        }

        private void ResetColors_Click(object sender, EventArgs e)
        {
            cellColor = Color.Gray;
            gridColor = Color.Black;
            graphicsPanel1.BackColor = Color.White;
            timer.Interval = 1000;
            Rows = 20;
            Cols= 20;
            Grid = new bool[Rows, Cols];
            for (int x = 0; x < Cols && x < universe.GetLength(1); x++)
            {
                for (int y = 0; y < Rows && y < universe.GetLength(0); y++)
                {
                    Grid[x, y] = universe[x, y];

                }
            }
            universe = Grid;
            
            graphicsPanel1.Invalidate();
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.CellColor = cellColor;
            Properties.Settings.Default.GridColor = gridColor;
            Properties.Settings.Default.PanelColor = graphicsPanel1.BackColor;
            Properties.Settings.Default.Rows = Rows;
            Properties.Settings.Default.Columns = Cols;
            Properties.Settings.Default.TimerSpeed = timer.Interval;
            Properties.Settings.Default.Save();
            
        }

        private void onToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            HeadsUp Display=new HeadsUp();
            timer.Enabled = false;
            Display.SetGeneration("Generation = "+generations.ToString());
            Display.SetLivingCells("Living Cells = "+ LivingCells.ToString());
            Display.SetRows("Number of Rows = "+Rows.ToString());
            Display.SetCols("Number of Columns = "+Cols.ToString());
            if(CountStyle==true)
            {
                Display.SetBoundaryStyle("Boundary Style = Toroidal");
            }
            else
            {
                Display.SetBoundaryStyle("Boundary Style = Finite");
            }

            Display.Show();
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            HeadsUp Display = new HeadsUp();
            timer.Enabled=false;//Pause Game to view current stats in Display
            Display.SetGeneration("Generation = " + generations.ToString());
            Display.SetLivingCells("Living Cells = " + LivingCells.ToString());
            Display.SetRows("Number of Rows = " + Rows.ToString());
            Display.SetCols("Number of Columns = " + Cols.ToString());
            if (CountStyle == true)
            {
                Display.SetBoundaryStyle("Boundary Style = Toroidal");
            }
            else
            {
                Display.SetBoundaryStyle("Boundary Style = Finite");
            }

            Display.Show();

        }
    }
}
