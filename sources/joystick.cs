// #https://poshgui.com/Editor?Tab=1

public class Joystick : Window
{
    private Canvas canvas;
    private RichTextBox logViewer;
    private Panel joystickPanel;
    private PictureBox joystickBackground;
    private Dictionary<string, Title> titles;
    private Dictionary<string, Setting> map;
    private Positions positions;
    private string backgroundPath;

    public Joystick(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine();
            Console.WriteLine("Usage: joystick $profile $config");
            Console.WriteLine();
            Console.WriteLine("$profile = profile name inside joysticks folder (e.g. dualshock)");
            Console.WriteLine("$config = path to joytokey configuration file (e.g. c:\\joytokey\\game.cfg)");
            Console.WriteLine();
            return;
        }

        map = Maps.Load(args[0]);
        Parser.Parse(args[1], map);
        positions = new Positions(args[0]);   
        positions.Load();
        backgroundPath = Background.Get(args[0]);

        InitWindow();
        InitLogViewer();
        InitJoystickPanel();
        InitTitles();
        InitJoystickBackground();
        
        Printer.Print(AppendLog, map);

        ShowDialog();
    }

    private void InitWindow()
    {
        Text = "Joystick";
        MaximizeBox = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Width = 900;

        KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.F4) 
            {
                logViewer.Visible = !logViewer.Visible;
            }

            e.Handled = false;
        };
    }

    private void InitLogViewer()
    {
        logViewer = new RichTextBox();
        FillWindow(logViewer);
        logViewer.ReadOnly = true;
        logViewer.Font = Fonts.FixedSize;
        logViewer.BorderStyle = BorderStyle.None;
        logViewer.Visible = false;
        
        Controls.Add(logViewer);
    }

    private void InitJoystickPanel()
    {
        joystickPanel = new Panel();
        FillWindow(joystickPanel);

        Controls.Add(joystickPanel);
    }

    private void InitJoystickBackground()
    {
        joystickBackground = new PictureBox();
        FillParent(joystickBackground, joystickPanel, new Spacing(10));
        joystickBackground.SizeMode = PictureBoxSizeMode.Zoom;
        joystickBackground.ImageLocation = backgroundPath;

        joystickPanel.Controls.Add(joystickBackground);
    }

    private void InitTitles()
    {
        titles = new Dictionary<string, Title>();

        foreach (var pair in map) 
        {
            var key = pair.Key;
            var value = pair.Value;

            var title = new Title();
            title.Draggable(true);            
            title.Location = positions.Get(value.Name);
            title.Text = Title.Format(value.Name, value.Comment);
            
            title.MouseUp += (sender, e) =>
            {
                Log.Debug("{0} {1}", value.Name, title.Location);
                positions.Set(value.Name, title.Location);
                positions.Save();
            };

            titles[key] = title;
            joystickPanel.Controls.Add(title);
        }
    }

    private void AppendLog(string line)
    {
        logViewer.AppendText(line + "\r\n");
        logViewer.ScrollToCaret();
    }
}

