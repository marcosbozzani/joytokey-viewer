public class Window : Form
{
    static Window()
    {
        Application.EnableVisualStyles();       
        Application.SetCompatibleTextRenderingDefault(false); 
    }

    //ControlStyles.AllPaintingInWmPaint
    //ControlStyles.UserPaint
    //ControlStyles.OptimizedDoubleBuffer
    //ControlStyles.ResizeRedraw
    //ControlStyles.SupportsTransparentBackColor

    public Window()
    {
        Width = 800;
        Height = 600;
        Font = Fonts.SansSerif;
        SizeGripStyle = SizeGripStyle.Hide;
        KeyPreview = true;
        DoubleBuffered = true;
        AutoScaleMode = AutoScaleMode.Dpi;
        StartPosition = FormStartPosition.CenterScreen;
    }

    protected void FillWindow(Control control, Spacing spacing = new Spacing())
    {
        control.Left = spacing.Left;
        control.Top = spacing.Top;
        control.Width = ClientSize.Width - (spacing.Left + spacing.Right);
        control.Height = ClientSize.Height - (spacing.Top + spacing.Bottom);
        control.Anchor =
            AnchorStyles.Top | 
            AnchorStyles.Bottom | 
            AnchorStyles.Right | 
            AnchorStyles.Left;
    }

    protected void FillParent(Control control, Control parent, Spacing spacing = new Spacing())
    {
        control.Left = spacing.Left;
        control.Top = spacing.Top;
        control.Width = parent.Width - (spacing.Left + spacing.Right);
        control.Height = parent.Height - (spacing.Top + spacing.Bottom);
        control.Anchor =
            AnchorStyles.Top | 
            AnchorStyles.Bottom | 
            AnchorStyles.Right | 
            AnchorStyles.Left;
    }

    protected override CreateParams CreateParams 
    {
        get 
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
            return cp;
        }
    }
}
