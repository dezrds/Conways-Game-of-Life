using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace ConwayGOL
{
    class Grid
    {

        int gridSize;
        public int[,] gridArray;

        public Grid(int gridSize) //square so only one parameter
        {
            gridArray = new int[gridSize, gridSize];
            this.gridSize = gridSize;
            initGrid();
        }

        private void initGrid()
        {
            Random random = new Random();
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    gridArray[row, col] = 0;
                }
            }
        }

        public void update(int[] coords)
        {
            foreach (int i in coords)
            {
                if (i == -1) return;
            }
            gridArray[coords[1], coords[0]] = gridArray[coords[1], coords[0]] == 0 ? 1 : 0;
        }

        public List<int[]> applyRules() //row, col format
        {
            List<int[]> gridUpdates = new List<int[]>(); //row, col, state - format
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    int numNeighbors = getNumNeighbors(row, col);
                    int state = gridArray[row, col];
                    if (numNeighbors < 2 && state == 1) gridUpdates.Add(new int[] { row, col, 0});
                    if ((numNeighbors == 2 || numNeighbors == 3) && state == 1) gridUpdates.Add(new int[] { row, col, 1});
                    if (numNeighbors > 3 && state == 1) gridUpdates.Add(new int[] { row, col, 0});
                    if (numNeighbors == 3 && state == 0) gridUpdates.Add(new int[] { row, col, 1});
                }
            }
            return gridUpdates;
        }

        public void applyUpdates(List<int[]> gridUpdates)
        {
            foreach (int[] gridUpdate in gridUpdates)
            {
                int row = gridUpdate[0];
                int col = gridUpdate[1];
                int state = gridUpdate[2];
                gridArray[row, col] = state;
            }
        }

        public int getNumNeighbors(int row, int col)
        {
            int numNeighbors = 0;
            const int INVALID = -1; //-1 is some arbitrary invalid index
            int rowBelow = row + 1 < gridSize ? row + 1 : INVALID;
            int rowAbove = row - 1 >= 0 ? row - 1 : INVALID;
            int colRight = col + 1 < gridSize ? col + 1 : INVALID;
            int colLeft = col - 1 >= 0 ? col - 1 : INVALID;
            //Debug.WriteLine(gridArray.Length);
            //Check horizontally
            if (colRight != INVALID)
            {
                if (gridArray[row, colRight] == 1) numNeighbors++;
            }
            if (colLeft != INVALID)
            {
                if (gridArray[row, colLeft] == 1) numNeighbors++;
            }
            //Check vertically
            if (rowAbove != INVALID)
            {
                if (gridArray[rowAbove, col] == 1) numNeighbors++;
            }
            if (rowBelow != INVALID) { if (gridArray[rowBelow, col] == 1) numNeighbors++; }
            //Check Diagnolly
            if (rowAbove != INVALID && colRight != INVALID) { if (gridArray[rowAbove, colRight] == 1) numNeighbors++; }
            if (rowAbove != INVALID && colLeft != INVALID) { if (gridArray[rowAbove, colLeft] == 1) numNeighbors++; }
            if (rowBelow != INVALID && colRight != INVALID) { if (gridArray[rowBelow, colRight] == 1) numNeighbors++; }
            if (rowBelow != INVALID && colLeft != INVALID) { if (gridArray[rowBelow, colLeft] == 1) numNeighbors++; }

            return numNeighbors; 
        }

        public int getNum() 
        {
            return gridSize;
        }
    }
}
