using System;
using System.IO;
using System.Diagnostics;

public class logon
{
	static void Main (string[] args)
	{
		connectDrive ("Y", "\\\\ws\\Buero");
		connectDrive ("R", "\\\\ws\\Daten");
		connectDrive ("T", "\\\\ws\\Temp");
		connectDrive ("W", "\\\\ws\\CATS");
		connectDrive ("X", "\\\\ws\\Fax");

	}

	private static int connectDrive (string letter, string path)
	{
		int sleepMillis = 1000;
		int retryes = 5;

		bool connected = false;
		int c = 0;
		while (c < retryes && !connected) {

			Process p = new Process();
			p.StartInfo.FileName = "net.exe";
			p.StartInfo.Arguments = "use " + letter + ": /delete /y";
			p.StartInfo.UseShellExecute = false;
			p.Start();
			p.WaitForExit ();

			p = new Process();
			p.StartInfo.FileName = "net.exe";
			p.StartInfo.Arguments = "use " + letter + ": " + path;
			p.StartInfo.UseShellExecute = false;
			p.Start();
			p.WaitForExit ();


			DirectoryInfo dir = new DirectoryInfo (letter + ":\\");
			if (dir.Exists) {
				connected = true;
			} else {
				System.Threading.Thread.Sleep (sleepMillis);
			}
			c++;
		}
		if (c >= retryes) {
			System.Console.WriteLine ("Can not connect to "+path);
			return -1;
		} else {
			System.Console.WriteLine ("Connected to "+path+" after "+c+" tries");
			return c;
		}

	}

}

