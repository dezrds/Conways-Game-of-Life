using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ConwayGOL
{
    public partial class Form1 : Form
    {

        Grid grid;
        GridGraphics gridGraphics;
        Graphics g;
        bool running = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g = CreateGraphics();
            grid = new Grid(30);
            gridGraphics = new GridGraphics(this, grid);
            gridGraphics.styleGrid(5, 20);
            gridGraphics.drawGrid(g);
            timer1.Start();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            grid.update(gridGraphics.getGridFromMouse(e.X, e.Y));
            Debug.WriteLine("Mouse X: " + e.X);
            Debug.WriteLine("Mouse Y: " + e.Y);
            gridGraphics.drawGrid(g);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            running = !running;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                grid.applyUpdates(grid.applyRules());
                gridGraphics.drawGrid(g);
            }
        }
    }
}
