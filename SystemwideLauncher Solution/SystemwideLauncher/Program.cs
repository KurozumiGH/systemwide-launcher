using System;
using System.Diagnostics;
using System.Threading;
using SystemwideLauncher.Properties;

namespace SystemwideLauncher
{
	class Program
	{
		[STAThread]
		static int Main(string[] args)
		{
			// 既にSystemwideが起動している場合は何もせず終了
			if (Process.GetProcessesByName(Settings.Default.SystemwideProcessName).Length > 0) return 1;

			// 指定時間待って、Systemwideの起動
			Thread.Sleep(Settings.Default.WaitTime);
			Process ps = Process.Start(Settings.Default.SystemwideExe);

			// Systemwideの画面を閉じる
			bool result = ps.CloseMainWindow();
			return result ? 0 : 2;
		}
	}
}
