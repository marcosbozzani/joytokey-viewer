public class Setting
{
    public Setting(string name)
    {
        Name = name;
        Type = "";
        Buttons = new string[0];
        Comment = "";
    }

    public string Name { get; set; }
    public string Type { get; set; }
    public string[] Buttons { get; set; }
    public string Comment { get; set; }
}