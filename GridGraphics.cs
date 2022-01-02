using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Text;

namespace ConwayGOL
{
    class GridGraphics
    {

        Form1 form;
        Grid grid;
        
        //Grid border parameters
        int formHeight;
        int formWidth;
        int effectiveHeight;
        int effectiveWidth;

        //Grid stylization parameters
        int spacing;
        int squareWidth;
        int squareHeight;
        int sideBorder;

        public GridGraphics(Form1 form, Grid grid) 
        {
            this.form = form;
            this.grid = grid;

            formHeight = form.Bounds.Height;
            formWidth = form.Bounds.Width;
        }

        public void styleGrid(int spacing, int sideBorder) 
        {
            int gridNum = grid.getNum();

            effectiveHeight = (formHeight-25) - (2 * sideBorder);
            effectiveWidth = formWidth - (2 * sideBorder);
            this.spacing = spacing;
            this.sideBorder = sideBorder;

            squareWidth = (effectiveWidth - (spacing * (gridNum - 1))) / gridNum;
            squareHeight = (effectiveHeight - (spacing * (gridNum - 1))) / gridNum;
        }

        public void drawGrid(Graphics g) 
        {


            int gridNum = grid.getNum();
            int startX = sideBorder;
            int startY = sideBorder;
            Point location = new Point(startX, startY);
            Size gridSize = new Size(squareWidth, squareHeight);
            Color active = Color.Red;
            Color inactive = Color.DimGray;
            SolidBrush brush = new SolidBrush(active);

            for(int row = 0; row < gridNum; row++) 
            {
                for(int col = 0; col < gridNum; col++) 
                {
                    if (grid.gridArray[row, col] == 0) brush.Color = inactive;
                    else brush.Color = active;
                    g.FillRectangle(brush, location.X, location.Y, squareWidth, squareHeight);
                    if (col != gridNum - 1) location.X += (spacing+squareWidth);
                    else location.X = startX;
                }
                if (row != gridNum - 1) location.Y += (spacing+squareHeight);
            }

        }


        public int[] getGridFromMouse(int x, int y) 
        {
            int effectiveX = x - sideBorder;
            int effectiveY = y - sideBorder;
            if (effectiveY < 0 || effectiveY > effectiveHeight-sideBorder) return new int[] { -1, -1 };
            if (effectiveX < 0 || effectiveX > effectiveWidth-sideBorder) return new int[] { -1, -1 };
            int gridX = (int)Math.Floor((double)effectiveX / (double)(squareWidth + spacing));
            int gridY = (int)Math.Floor((double)effectiveY / (double)(squareHeight + spacing));

            return new int[] { gridX, gridY };
        }

    }
}
