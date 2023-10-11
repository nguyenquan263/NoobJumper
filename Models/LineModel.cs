using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NoobJumper.Models
{
    public class LineModel
    { 
        public Point startPoint {  get; set; }
        public Point endPoint { get; set; }

        public LineModel(Point s, Point e) {
            this.startPoint = s;
            this.endPoint = e;
        }
    }
}
