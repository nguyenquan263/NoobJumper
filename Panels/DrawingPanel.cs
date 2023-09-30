using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NoobJumper.Panels
{
    public partial class DrawingPanel : Panel
    {
        private const int WIDTH = 50;
        private const int HEIGHT = 50;
        private int playerX;
        private int playerY; 

        public DrawingPanel()
        {
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.playerX = this.Width / 2 - 25;
            this.playerY = this.Height - 100;

            this.Focus();
            this.DoubleBuffered = true;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            int x = playerX;
            int y = playerY;

            int width = WIDTH;
            int height = HEIGHT;

            Brush b = new SolidBrush(Color.Blue);

            g.FillRectangle(b, x, y, width, height);

            b.Dispose();
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
    }
}
