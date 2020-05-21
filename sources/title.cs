public class Title : Label 
{
    public Title() 
    {
        AutoSize = true;
        Font = Fonts.SansSerif;
        SetStyle(ControlStyles.Opaque, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
    }

    public static string Format(string key, string value)
    {
        key = ReplaceChars(key);
        value = value.ToUpper();
        return key + ": " + value;
    }

    private static string ReplaceChars(string value)
    {
        return value
            // .Replace("Up",    "\u2BC5")
            // .Replace("Left",  "\u2BC6")
            // .Replace("Down",  "\u2BC7")
            // .Replace("Right", "\u2BC8")

            // .Replace("Left",  "\U0001F808")
            // .Replace("Up",    "\U0001F809")
            // .Replace("Right", "\U0001F80A")
            // .Replace("Down",  "\U0001F80B")

            // .Replace("Left",  "\U0001F81C")
            // .Replace("Up",    "\U0001F81D")
            // .Replace("Right", "\U0001F81E")
            // .Replace("Down",  "\U0001F81F")

            .Replace("Left",  "\U0001F868")
            .Replace("Up",    "\U0001F869")
            .Replace("Right", "\U0001F86A")
            .Replace("Down",  "\U0001F86B")
                        
            .ToUpper();
    }

    protected override CreateParams CreateParams 
    {
        get 
        {
            CreateParams parms = base.CreateParams;
            parms.ExStyle |= 0x20;  // Turn on WS_EX_TRANSPARENT
            return parms;
        }
    }
}