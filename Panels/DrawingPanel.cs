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

            g.FillEllipse(b, x, y, width, width);

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
                    foreach (var p in mouseMovingPoints)
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
