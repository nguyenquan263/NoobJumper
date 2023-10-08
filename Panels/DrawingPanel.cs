using NoobJumper.Models;
using NoobJumper.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoobJumper.Panels
{
    public partial class DrawingPanel : Panel
    {
        private const int WIDTH = 50;
        private const int HEIGHT = 50;
        private int playerX;
        private int playerY;
        private List<Point> mouseMovingPoints = new List<Point>();
        private Graphics g;
        private Brush b;
        private Pen pen;
        private int animationSpeed = 5;
        private int currentPointIndex = 0;
        private bool isAnimating = false;

        private List<Point> mazePointsList;
        private List<Line> mazeLinesList;

        public DrawingPanel()
        {
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.playerX = this.Width / 2 - 25;
            this.playerY = this.Height - 100;
            this.Focus();
            this.DoubleBuffered = true;
            this.MouseMove += Mouse_Move;
            this.MouseClick += Mouse_Click;

            MazeGenerator mg = new MazeGenerator(1600, 900);
            mg.pointGeneration(this.Width / 2, this.Height / 2, 0);
            this.mazePointsList = mg.GetPoints();
            this.mazeLinesList = mg.GetLines();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            g = e.Graphics;

            int x = playerX;
            int y = playerY;

            int width = WIDTH;
            int height = HEIGHT;

            b = new SolidBrush(Color.Blue);
            pen = new Pen(Color.Black, 2);

            g.FillEllipse(b, x, y, width, height);

            

            if (mouseMovingPoints.Count > 2)
            {
                g.DrawLines(pen, mouseMovingPoints.ToArray());
            }

            //if (mazePointsList.Count > 2)
            //{
            //    g.DrawLines(pen, mazePointsList.ToArray());
            //}

            for (int i = 0; i < mazeLinesList.Count; i++)
            {
                g.DrawLine(pen, mazeLinesList[i].startPoint, mazeLinesList[i].endPoint);
            }

            b = new SolidBrush(Color.Red);
            foreach (Point point in mazePointsList)
            {
                g.FillEllipse(b, point.X - 10, point.Y - 10, 20, 20);
            }

            b.Dispose();
            pen.Dispose();

        }

        public void MoveUp() 
        {
            this.playerY -= 5;
            this.Invalidate();
        }

        public void MoveForward()
        { 
            this.playerX += 5;
            this.Invalidate();
        }

        public void MoveBack()
        {
            this.playerX -= 5;
            this.Invalidate();
        }

        public void MoveDown()
        {
            this.playerY += 5;
            this.Invalidate();
        }

        public async void AnimateMovement(Point endPoint)
        {
            isAnimating = true;

            playerX = endPoint.X - 25;
            playerY = endPoint.Y - 25;
            this.Invalidate();

            await Task.Delay(animationSpeed);
            
            isAnimating = false;
        }

        public void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (!isAnimating)
            {
                Point movingPoint = new Point(e.X, e.Y);
                mouseMovingPoints.Add(movingPoint);
                this.Invalidate();
            }
        }

        public async void Mouse_Click(object sender, MouseEventArgs e)
        {
            if (!isAnimating && mouseMovingPoints.Count > 0)
            {
                if (currentPointIndex < mouseMovingPoints.Count)
                {
                    foreach (var p in mouseMovingPoints.ToArray())
                    {
                        Point endPoint = p;
                        AnimateMovement(endPoint);
                        await Task.Delay(animationSpeed * 2);
                    }
                }
            }
        }
    }
}
