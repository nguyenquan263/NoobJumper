using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NoobJumper.Models
{
    public class PointModel
    {
        public int ID { get; set; }
        public Point point { get; set; }
        public Dictionary<int, bool> connectedPointIDs = new Dictionary<int, bool>();
    }
}
