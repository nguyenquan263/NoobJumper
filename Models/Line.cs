using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NoobJumper.Models
{
    public class Line
    { 
        public Point startPoint {  get; set; }
        public Point endPoint { get; set; }

        public Line(Point s, Point e) {
            this.startPoint = s;
            this.endPoint = e;
        }
    }
}
