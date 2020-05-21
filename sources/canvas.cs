public class Canvas : Control
{
    public List<Point> Points { get; set; }

    private const int size = 2;
    private const int size2 = 2 * size;
    private const int size3 = 3 * size;
    private const int size4 = 4 * size;
    private const int size6 = 6 * size;
    private Pen lineOuterPen;
    private Pen lineInnerPen;
    private Brush circleOuterBrush;
    private Brush circleInnerBrush;

    public Canvas()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        Points = new List<Point>();
        BackColor = Color.Transparent;
        lineOuterPen = new Pen(Color.Black, size3);
        lineInnerPen = new Pen(Color.White, size);
        circleOuterBrush = new SolidBrush(Color.Black);
        circleInnerBrush = new SolidBrush(Color.White);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.InterpolationMode = InterpolationMode.High;
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.CompositingQuality = CompositingQuality.HighQuality;

        var p = Points;
        for (int i = 0; i < Points.Count; i++)
        {
            if (i + 1 != Points.Count)
            {
                g.DrawLine(lineOuterPen, p[i + 0].X, p[i + 0].Y, p[i + 1].X, p[i + 1].Y);
                g.DrawLine(lineInnerPen, p[i + 0].X, p[i + 0].Y, p[i + 1].X, p[i + 1].Y);
            }

            g.FillEllipse(circleOuterBrush, p[i].X - size3, p[i].Y - size3, size6, size6);
            g.FillEllipse(circleInnerBrush, p[i].X - size2, p[i].Y - size2, size4, size4);
        }
    }
}