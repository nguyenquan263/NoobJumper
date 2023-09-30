using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

        public DrawingPanel()
        {
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.playerX = this.Width / 2 - 25;
            this.playerY = this.Height - 100;
            this.Focus();
            this.DoubleBuffered = true;
            this.MouseMove += Mouse_Move;
            this.MouseClick += Mouse_Click;
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

            g.FillRectangle(b, x, y, width, height);

            b.Dispose();

            if (mouseMovingPoints.Count > 2)
            {
                g.DrawLines(pen, mouseMovingPoints.ToArray());
            }
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
            int startX = playerX;
            int startY = playerY;
            int steps = 5; // Adjust the number of steps for smoother animation

            for (int i = 1; i <= steps; i++)
            {
                double t = (double)i / steps;
                int newX = (int)Math.Round(startX + t * (endPoint.X - startX));
                int newY = (int)Math.Round(startY + t * (endPoint.Y - startY));

                playerX = newX;
                playerY = newY;
                this.Invalidate();

                await Task.Delay(animationSpeed);
            }

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

        public void Mouse_Click(object sender, MouseEventArgs e)
        {
            if (!isAnimating && mouseMovingPoints.Count > 0)
            {
                if (currentPointIndex < mouseMovingPoints.Count)
                {
                    Point endPoint = mouseMovingPoints[currentPointIndex];
                    AnimateMovement(endPoint);
                    currentPointIndex++;
                }
            }
        }
    }
}
