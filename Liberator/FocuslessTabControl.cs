using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Liberator;

internal sealed class FocuslessTabControl : TabControl
{
    private const int WM_CHANGEUISTATE = 0x0127;
    private const int WM_UPDATEUISTATE = 0x0128;
    private const int UIS_SET = 1;
    private const int UISF_HIDEFOCUS = 0x1;
    private static readonly IntPtr HideFocus = (IntPtr)(UIS_SET | (UISF_HIDEFOCUS << 16));

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, IntPtr lParam);

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        SendMessage(Handle, WM_CHANGEUISTATE, HideFocus, IntPtr.Zero);
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_UPDATEUISTATE)
        {
            m.WParam = HideFocus;
        }
        base.WndProc(ref m);
    }
}
