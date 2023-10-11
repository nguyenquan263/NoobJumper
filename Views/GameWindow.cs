using NoobJumper.Panels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NoobJumper.Views
{
    public partial class GameWindow : Form
    {
        private DrawingPanel dp;

        public GameWindow()
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.Width = 1600;
            this.Height = 900;

            dp = new DrawingPanel();
            dp.Parent = this;
            dp.Show();

            this.KeyDown += ArrowButton_Click;
            this.SizeChanged += DrawingPanel_SizeChanged;
        }

        private void DrawingPanel_SizeChanged(object sender, EventArgs e)
        {
            dp.Size = this.ClientSize;
        }

        private void ArrowButton_Click(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    dp.MoveBack();
                    return;
                case Keys.Right:
                    dp.MoveForward();
                    return;
                case Keys.Up:
                    dp.MoveUp();
                    return;
                case Keys.Down:
                    dp.MoveDown();
                    return;
                default:
                    return;
            }
        }
    }
}
