public class Parser
{
    public static void Parse(string config, Dictionary<string, Setting> map)
    {
        if (!File.Exists(config))
        {
            throw new Exception("File not found: " + config);
        }

        Log.Info("Reading file: {0}", config);

        using (var reader = new StreamReader(config))
        {
            string line;
            while((line = reader.ReadLine()) != null)  
            {
                Log.Debug("===================================");
                Log.Debug("Line: {0}", line);
                Log.Debug("===================================");
                var items = Split(line, "=");
                if (items.Length != 2) 
                {
                    continue;
                }

                var key = items[0];
                Log.Debug("Key: {0}", key);
                if (!map.ContainsKey(key)) 
                {
                    continue;
                }

                var values = Split(items[1], "##");
                if (values.Length == 0 || values.Length > 2) 
                {
                    continue;
                }

                var value = "";
                var comment = "";
                if (values.Length > 0) 
                {
                    value = values[0];
                }
                if (values.Length > 1) 
                {
                    comment = values[1];
                }

                Log.Debug("Value: {0}", value);
                var type = "";
                string[] buttons = null;
                var subvalues = Split(value, ",");
                if (IsMouse(subvalues))
                {
                    type = "Mouse";
                    Log.Debug("Type: {0}", type);
                    buttons = HandleMouse(subvalues);
                }
                else if (IsKeyboard(subvalues))
                {
                    type = "Keyboard";
                    Log.Debug("Type: {0}", type);
                    buttons = HandleKeyboard(subvalues);
                }
                else
                {
                    continue;
                }

                map[key].Type = type;
                map[key].Buttons = buttons;
                if (!string.IsNullOrEmpty(comment)) 
                {
                    map[key].Comment = comment;
                }
                else
                {
                    map[key].Comment = string.Format("{0} {1}",
                        type, string.Join(", ", buttons));
                }
            }
        }
    }

    private static string[] Split(string input, string separator)
    {
        return input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }

    private static bool IsMouse(string[] subvalues)
    {
        return subvalues.Length == 16 && subvalues[0].Trim() == "2";
    }

    private static string[] HandleMouse(string[] subvalues)
    {
        var buttons = new List<string>();

        var leftRight = int.Parse(subvalues[1].Trim());
        if (leftRight < 0) 
        {
            buttons.Add("Left");
        }
        else if (leftRight > 0) 
        {
            buttons.Add("Right");
        }

        var upDown = int.Parse(subvalues[2].Trim());
        if (upDown < 0) 
        {
            buttons.Add("Up");
        }
        else if (upDown > 0) 
        {
            buttons.Add("Down");
        }

        if (subvalues[4].Trim() == "1") 
        {
            buttons.Add("LClick");
        }
        if (subvalues[5].Trim() == "1") 
        {
            buttons.Add("RClick");
        }
        if (subvalues[6].Trim() == "1") 
        {
            buttons.Add("MClick");
        }

        return buttons.Where(x => !string.IsNullOrEmpty(x)).ToArray();
    }

    private static bool IsKeyboard(string[] subvalues)
    {
        return subvalues.Length == 5 && subvalues[0].Trim() == "1";
    }

    private static string[] HandleKeyboard(string[] subvalues)
    {
        var buttons = new List<string>();
        Log.Debug("ScanCodes: {0}", subvalues[1].Trim());
        var scanCodes = Split(subvalues[1], ":");

        foreach (var scanCodeString in scanCodes)
        {
            var code = ParseHex(scanCodeString.Trim());
            var chars = GetChars(code);
            Log.Debug("Chars.1: {0}", chars);

            if (chars == " ")
            {
                chars = "Space";
                Log.Debug("Chars.2: {0}", chars);
            }
            else if (chars == "" && code != 0)
            {
                // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                var keyName = ((Keys)code).ToString();
                if (keyName == "Menu") 
                {
                    Log.Debug("Chars.3: {0}", chars);
                    chars = "Alt";
                }
                else if (keyName == "LMenu") 
                {
                    chars = "LAlt";
                    Log.Debug("Chars.4: {0}", chars);
                }
                else if (keyName == "RMenu") 
                {
                    chars = "RAlt";
                    Log.Debug("Chars.5: {0}", chars);
                }
                else {
                    chars = keyName;
                    Log.Debug("Chars.6: {0}", chars);
                }
            }

            buttons.Add(chars);
            Log.Debug("ScanCode: {0}", code, chars);
        }

        return buttons.Where(x => !string.IsNullOrEmpty(x)).ToArray();
    }

    private static uint ParseHex(string value)
    {
        return uint.Parse(value, NumberStyles.HexNumber);
    }

    private static string GetChars(uint key)
    {
        var buffer = new StringBuilder(256);
        var keyboardState = new byte[256];
        Native.ToUnicode(key, 0, keyboardState, buffer, 256, 0);
        return buffer.ToString().ToUpper();
    }
}


    

