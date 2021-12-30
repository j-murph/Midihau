using System;
using System.Windows.Forms;

namespace Midihau
{
    static class ControlHelper
    {
        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }
    }
}
