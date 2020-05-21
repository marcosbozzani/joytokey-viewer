public static class DraggableExtension
{
    private static Size offset;
    private static Dictionary<Control, bool> items = 
        new Dictionary<Control, bool>();
    
    public static void Draggable(this Control control, bool enable)
    {
        if (enable)
        {
            if (items.ContainsKey(control))
            {
                return;
            }
            
            items.Add(control, false);

            control.MouseDown += control_MouseDown;
            control.MouseUp += control_MouseUp;
            control.MouseMove += control_MouseMove;
        }
        else
        {
            if (!items.ContainsKey(control))
            {
                return;
            }
            
            control.MouseDown -= control_MouseDown;
            control.MouseUp -= control_MouseUp;
            control.MouseMove -= control_MouseMove;

            items.Remove(control);
        }
    }

    static void control_MouseDown(object sender, MouseEventArgs e)
    {
        offset = new Size(e.Location);
        items[(Control)sender] = true;
    }

    static void control_MouseUp(object sender, MouseEventArgs e)
    {
        items[(Control)sender] = false;
    }

    static void control_MouseMove(object sender, MouseEventArgs e)
    {
        if (items[(Control)sender] == true)
        {
            Point newLocation = e.Location - offset;
            ((Control)sender).Left += newLocation.X;
            ((Control)sender).Top += newLocation.Y;
        }
    }
}
