public struct Spacing
{
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }
    public int Left { get; set; }

    public Spacing(int all)
        : this(all, all, all, all) {}

    public Spacing(int topBottom, int rightLeft)
        : this (topBottom, rightLeft, topBottom, rightLeft) {}
    
    public Spacing(int top, int rightLeft, int bottom)
        : this (top, rightLeft, bottom, rightLeft) {}

    public Spacing(int top, int right, int bottom, int left)
        : this()
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }
}
