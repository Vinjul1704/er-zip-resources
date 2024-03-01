using NAudio.Wave;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ZipHelper
{
    public partial class ZipHelper : Form
    {
        public static System.Timers.Timer TickTimer;
        public static System.Media.SoundPlayer player;
        public static bool firstBeat = true;
        public static int initialOffset = 0;
        public static double bpm = 100;
        public static int guardButton = 2;

        public ZipHelper()
        {
            InitializeComponent();
            player = new System.Media.SoundPlayer(System.AppContext.BaseDirectory + "ticksound.wav");

            TickTimer = new System.Timers.Timer();
            TickTimer.Interval = 60000 / bpm;
            TickTimer.Elapsed += new System.Timers.ElapsedEventHandler(MyTimer_Tick);
            TickTimer.Stop();

            MouseHook.Start();
            MouseHook.RightMouseDown += new EventHandler(RMEventDown);
            MouseHook.RightMouseUp += new EventHandler(RMEventUp);
        }

        private void MyTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            player.Play();
            if(initialOffset != 0 && firstBeat)
            {
                TickTimer.Interval = 60000 / bpm;
                firstBeat = false;
            }
        }

        private void RMEventDown(object sender, EventArgs e)
        {
            if (initialOffset != 0)
            {
                TickTimer.Interval = TickTimer.Interval - initialOffset;
            }
            firstBeat = true;
            TickTimer.Start();
        }
        private void RMEventUp(object sender, EventArgs e)
        {
            TickTimer.Stop();
        }

        public static void StartMetronome()
        {
            if (initialOffset != 0)
            {
                TickTimer.Interval = TickTimer.Interval - initialOffset;
            }
            firstBeat = true;
            TickTimer.Start();
        }
        public static void StopMetronome()
        {
            TickTimer.Stop();
        }

        private void trackBarBpm_ValueChanged(object sender, System.EventArgs e)
        {
            double val = ((float)trackBarBpm.Value / 2);
            bpm = (val + 100);
            labelBpm.Text = "BPM: " + bpm;

            double interval = (60000 / bpm);
            TickTimer.Interval = interval;
        }

        protected void textBoxOffset_TextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (!Int32.TryParse(textBoxOffset.Text, out result))
            {
                initialOffset = 0;
                textBoxOffset.Text = "0";
            }
            else
            {
                double interval = 60000 / bpm;
                if (result > interval)
                { 
                    result = (int)Math.Floor(interval);
                }
                initialOffset = result;
                textBoxOffset.Text = result.ToString();
            }
        }

        protected void textBoxGuardButton_TextChanged(object sender, EventArgs e)
        {
            Keys key;
            object result;
            if (Enum.TryParse(typeof(Keys), textBoxGuardButton.Text, true, out result))
            {
                if (result != null)
                {
                    key = (Keys)result;
                    guardButton = (int)key;

                    if (key == Keys.RButton || key == Keys.LButton || key == Keys.MButton)
                    {
                        
                        MouseHook.Start();
                    }
                    else
                    {
                        MouseHook.Stop();
                        new Thread(() =>
                        {
                            Thread.CurrentThread.IsBackground = true;
                            InterceptKeys.Start();
                        }).Start();
                    }
                }
            }
            else
            {
                guardButton = 2;
                MouseHook.Start();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.twitch.tv/hyp3rsomniac") { UseShellExecute = true });
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.youtube.com/channel/UC09Apz8iLdw3DIk28rBGYSg") { UseShellExecute = true });
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Cursor = Cursors.Hand;
        }
    }

    public static class MouseHook
    {
        public static event EventHandler RightMouseUp = delegate { };
        public static event EventHandler RightMouseDown = delegate { };
        public static event EventHandler LeftMouseAction = delegate { };

        public static void Start()
        {
            _hookID = SetHook(_proc);
        }
        public static void Stop()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                  GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
          int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                LeftMouseAction(null, new EventArgs());
            }
            if (nCode >= 0 && MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                RightMouseDown(null, new EventArgs());
            }
            if (nCode >= 0 && MouseMessages.WM_RBUTTONUP == (MouseMessages)wParam)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                RightMouseUp(null, new EventArgs());
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
          LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
          IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

    }

    public class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private static bool guardKeyDown = false;

        public static void Start()
        {
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (!guardKeyDown && nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == ZipHelper.guardButton)
                {
                    guardKeyDown = true;
                    ZipHelper.StartMetronome();
                    Debug.WriteLine("keydown");
                }
            }
            if (guardKeyDown && nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == ZipHelper.guardButton)
                {
                    ZipHelper.StopMetronome();
                    Debug.WriteLine("keyup");
                    guardKeyDown = false;
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}