using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Line
    {
        protected List<Point> plist;
        public List<Point> PointsList { get { return plist; } }
        
        public virtual void Draw()
        {
            foreach (Point p in plist)
                p.Draw();
        }
    }
}
