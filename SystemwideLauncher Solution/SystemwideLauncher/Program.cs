using System;
using System.Diagnostics;
using System.IO;
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

			// Systemwideの存在チェック
			FileInfo fiExe = new FileInfo(Settings.Default.SystemwideExe);
			if (!fiExe.Exists) return 2;

			// 指定時間待ってSystemwideを起動
			Thread.Sleep(Settings.Default.WaitTime);

			ProcessStartInfo psInfo = new ProcessStartInfo(fiExe.FullName);
			psInfo.WorkingDirectory = fiExe.DirectoryName; // カレントフォルダを変更
			Process ps = Process.Start(psInfo);

			// Systemwideの画面を閉じる
			// Systemwideがアイドル状態になってもしばらくは待機（そうしないと画面を閉じられない）
			ps.WaitForInputIdle(10000);
			Thread.Sleep(Settings.Default.WaitTimeToClose);

			bool result = ps.CloseMainWindow();
			return result ? 0 : 3;
		}
	}
}
