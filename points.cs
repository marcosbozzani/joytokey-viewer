public class Points : Window
{
    public Points(string[] args)
    {
        Text = "Points";

        var canvas = new Canvas();
        canvas.Left = 0;
        canvas.Top = 0;
        FillWindow(canvas);

        canvas.MouseDown += (sender, e) => 
        {
            if (e.Button == MouseButtons.Left) 
            {
                canvas.Points.Add(new Point(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Right) 
            {
                if (canvas.Points.Count > 0) 
                {
                    canvas.Points.RemoveAt(canvas.Points.Count - 1);
                } 
            }
            else if (e.Button == MouseButtons.Middle) 
            {
                canvas.Points.Clear();
            }

            canvas.Invalidate();
        };
        
        Controls.Add(canvas);

        ShowDialog();
    }
}