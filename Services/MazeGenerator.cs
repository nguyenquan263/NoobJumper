using NoobJumper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace NoobJumper.Services
{
    class MazeGenerator
    {
        private const int X_DISTANCE = 50;
        private const int Y_DISTANCE = 50;
        private const int DEPT = 30;

        private List<Point> mazePoints;
        private List<Line> mazeLines;

        private int width;
        private int height;

        private int[] XValTD = { -X_DISTANCE, 0, X_DISTANCE, 0 };
        private int[] YValTD = { 0, Y_DISTANCE, 0, -Y_DISTANCE };

        private Dictionary<string, bool> dd = new Dictionary<string, bool>();

        private Random rand = new Random();

        public MazeGenerator(int w, int h)
        {
            this.width = w;
            this.height = h;
            this.mazePoints = new List<Point>();
            this.mazeLines = new List<Line>();
        }

        public List<Point> GetPoints()
        {
            return this.mazePoints;
        }

        public List<Line> GetLines()
        {
            return this.mazeLines;
        }

        public void pointGeneration(int x, int y, int dept)
        {
            if (dept == DEPT)
            {
                return;
            }

            if (dept == 0) {
                this.mazePoints.Add(new Point(x, y));
            }

            int nextDept = dept + 1;
            for (int i = 0; i < XValTD.Length; i++)
            {
                int xTD = x + XValTD[i];
                int yTD = y + YValTD[i];

                if (xTD <= 0 || xTD > width)
                {
                    return;
                }

                if (yTD <= 0 || yTD > height)
                {
                    return;
                }

                if (!dd.ContainsKey(xTD + ":" + yTD))
                {
                    this.mazePoints.Add(new Point(xTD, yTD));
                    this.mazeLines.Add(new Line(new Point(x, y), new Point(xTD, yTD)));
                    dd.Add(xTD + ":" + yTD, true);
                    this.pointGeneration(xTD, yTD, nextDept);
                }
            }
        }
    }
}
