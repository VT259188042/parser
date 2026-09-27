using System.Drawing;

namespace парсер
{
    public class Connection
    {
        public RuleBlock From;
        public RuleBlock To;

        public Connection(RuleBlock from, RuleBlock to)
        {
            From = from;
            To = to;
        }

        public void Draw(Graphics g)
        {
            var p1 = new Point(From.Left + From.Width, From.Top + From.Height / 2);
            var p2 = new Point(To.Left, To.Top + To.Height / 2);

            using (var pen = new Pen(Color.LightGray, 2))
            {
                pen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(4, 6);
                g.DrawLine(pen, p1, p2);
            }
        }
    }
}
