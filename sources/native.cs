public class Native
{
    [DllImport("user32.dll")]
    public static extern int ToUnicode(
        uint virtualKeyCode, 
        uint scanCode,
        byte[] keyboardState,
        [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
        StringBuilder receivingBuffer,
        int bufferSize, 
        uint flags
    );
}