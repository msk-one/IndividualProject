using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CellularAutomaton.Other;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CellularAutomaton.MainWindow
{
    public partial class MainWindow : Window
    {
        #region SomeWinAPI
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr
        phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name,
        ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
        ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        internal const int EWX_LOGOFF = 0x00000000;
        internal const int EWX_SHUTDOWN = 0x00000001;
        internal const int EWX_REBOOT = 0x00000002;
        internal const int EWX_FORCE = 0x00000004;
        internal const int EWX_POWEROFF = 0x00000008;
        internal const int EWX_FORCEIFHUNG = 0x00000010;

        private void DoExitWin(int flg)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Other.AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit?", MessageBoxButton.OKCancel,
                MessageBoxImage.Question, MessageBoxResult.Cancel, MessageBoxOptions.None) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
                DoExitWin(EWX_SHUTDOWN);
            } 
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog(this);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            mainCanvas.Background = new SolidColorBrush(Color.FromRgb(255,255,255));
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            mainCanvas.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            RuleSettingsEditor.RuleSettingsEditor ruleSettings = new RuleSettingsEditor.RuleSettingsEditor();
            ruleSettings.Show();
        }
    }
}