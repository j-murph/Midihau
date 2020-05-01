using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Midihau.NativeMethods;

namespace Midihau
{
    public class KeyPressedEventArgs
    {
        public Keys Key { get; private set; }

        public KeyPressedEventArgs(Keys key)
        {
            Key = key;
        }
    }

    public static class GlobalKeyListener
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private static IntPtr hookId = IntPtr.Zero;

        private static LowLevelKeyboardProc proc; // Static so this isn't GC'd prematuraly

        private static object onKeyPressLock = new object();
        private static event EventHandler<KeyPressedEventArgs> onKeyPressed;
        public static event EventHandler<KeyPressedEventArgs> OnKeyPressed
        {
            add
            {
                lock (onKeyPressLock)
                {
                    onKeyPressed += value;
                }
            }

            remove
            {
                lock (onKeyPressLock)
                {
                    onKeyPressed -= value;
                }
            }
        }

        public static void Start()
        {
            proc = HookCallback;
            hookId = SetHook(proc);
        }

        public static void CleanUp()
        {
            UnhookWindowsHookEx(hookId);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int keyCode = Marshal.ReadInt32(lParam);
                onKeyPressed?.Invoke(null, new KeyPressedEventArgs((Keys)keyCode));
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }
    }
}
