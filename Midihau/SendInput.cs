using System.Runtime.InteropServices;
using static Midihau.NativeMethods;

namespace Midihau
{
    public struct Input
    {
        public int Type;
        public InputUnion InputUnion;
    }

    public static class SendInput
    {
        // Shamelessly copied from https://stackoverflow.com/questions/35138778/sending-keys-to-a-directx-game
        public static void SendDirectXKey(DirectXKey key, bool keyUp)
        {
            uint flags;
            if (keyUp)
                flags = (uint)(KeyEventF.KeyUp | KeyEventF.Scancode);
            else
                flags = (uint)(KeyEventF.KeyDown | KeyEventF.Scancode);

            Input[] inputs =
            {
                new Input
                {
                    Type = (int)InputType.Keyboard,
                    InputUnion = new InputUnion
                    {
                        ki = new KeyboardInput
                        {
                            wVk = 0,
                            wScan = (ushort)key,
                            dwFlags = flags,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                }
            };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        // Shamelessly copied from https://stackoverflow.com/questions/35138778/sending-keys-to-a-directx-game
        public static void SendDirectXMouse(MouseEventF flags, int x = 0, int y = 0)
        {
            Input[] inputs =
            {
                new Input
                {
                    Type = (int)InputType.Mouse,
                    InputUnion = new InputUnion
                    {
                        mi = new MouseInput()
                        {
                            dx = x,
                            dy = y,
                            dwFlags = (uint)flags,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                }
            };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }
    }
}
