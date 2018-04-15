using System.Windows.Forms;

using ConEmu.WinForms;

using GitCommands;
using GitCommands.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using JetBrains.Annotations;

using Microsoft.Build.Utilities;

namespace GitUI.UserControls
{
	/// <summary>
	/// An output control which inserts a fully-functional console emulator window.
	/// </summary>
	public class ConsoleEmulatorOutputControl : ConsoleOutputControl
	{
		private int _nLastExitCode;

		private ConEmuControl _terminal;

		public ConsoleEmulatorOutputControl()
		{
        }

		public override int ExitCode
		{
			get
			{
				return _nLastExitCode;
			}
		}

		public override bool IsDisplayingFullProcessOutput
		{
			get
			{
				return true;
			}
		}

		public static bool IsSupportedInThisEnvironment
		{
			get
			{
				return EnvUtils.RunningOnWindows(); // ConEmu only works in WinNT
			}
		}

		public override void AppendMessageFreeThreaded(string text)
		{
			ConEmuSession session = _terminal.RunningSession;
			if(session != null)
				session.WriteOutputText(text);
		}

		public override void KillProcess()
		{
			ConEmuSession session = _terminal.RunningSession;
			if(session != null)
				session.SendControlCAsync();
		}

        public override void Reset()
        {
            if (_terminal != null)
            {
                KillProcess();
                Controls.Remove(_terminal);
                _terminal.Dispose();
            }

            Controls.Add(_terminal = new ConEmuControl() { Dock = DockStyle.Fill, AutoStartInfo = null /* don't spawn terminal until we have gotten the command */});
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && _terminal != null)
            {
                _terminal.Dispose();
            }
        }


        public override void StartProcess(string command, string arguments, string workdir)
		{
			var cmdl = new CommandLineBuilder();
			cmdl.AppendFileNameIfNotNull(command /* do the escaping for it */);
			cmdl.AppendSwitch(arguments /* expecting to be already escaped */);

			var startinfo = new ConEmuStartInfo();
			startinfo.ConsoleProcessCommandLine = cmdl.ToString();
			startinfo.StartupDirectory = workdir;
			startinfo.WhenConsoleProcessExits = WhenConsoleProcessExits.KeepConsoleEmulatorAndShowMessage;
			startinfo.AnsiStreamChunkReceivedEventSink = (sender, args) => FireDataReceived(new TextEventArgs(args.GetText(GitModule.SystemEncoding)));
			startinfo.ConsoleProcessExitedEventSink = (sender, args) =>
			{
				_nLastExitCode = args.ExitCode;
				FireProcessExited();
			};
			startinfo.ConsoleEmulatorClosedEventSink = (s, e) =>
=======
        public override void StartProcess(string command, string arguments, string workdir, Dictionary<string, string> envVariables)
        {
            var cmdl = new StringBuilder();
            if (command != null)
            {
                cmdl.Append(command.Quote() /* do the escaping for it */);
                cmdl.Append(" ");
            }
            cmdl.Append(arguments /* expecting to be already escaped */);

            var startinfo = new ConEmuStartInfo();
            startinfo.ConsoleProcessCommandLine = cmdl.ToString();
            if (AppSettings.ConEmuStyle.ValueOrDefault != "Default")
            {
                startinfo.ConsoleProcessExtraArgs = " -new_console:P:\"" + AppSettings.ConEmuStyle.ValueOrDefault + "\"";
            }
            startinfo.StartupDirectory = workdir;
            foreach (var envVariable in envVariables)
            {
                startinfo.SetEnv(envVariable.Key, envVariable.Value);
            }
            startinfo.WhenConsoleProcessExits = WhenConsoleProcessExits.KeepConsoleEmulatorAndShowMessage;
            var outputProcessor = new ConsoleCommandLineOutputProcessor(startinfo.ConsoleProcessCommandLine.Length, FireDataReceived);
            startinfo.AnsiStreamChunkReceivedEventSink = outputProcessor.AnsiStreamChunkReceived;

            startinfo.ConsoleProcessExitedEventSink = (sender, args) =>
            {
                outputProcessor.Flush();
                _nLastExitCode = args.ExitCode;
                FireProcessExited();
            };

            startinfo.ConsoleEmulatorClosedEventSink = (s, e) =>
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
                {
                    if (s == _terminal.RunningSession)
                    {
                        FireTerminated();
                    }
                };
			startinfo.IsEchoingConsoleCommandLine = true;


			_terminal.Start(startinfo);
		}
	}
=======
            _terminal.Start(startinfo);
        }
    }

    [CLSCompliant(false)]
    public class ConsoleCommandLineOutputProcessor
    {
        private Action<TextEventArgs> _FireDataReceived;
        private int _commandLineCharsInOutput;
        private string _lineChunk = null;

        public ConsoleCommandLineOutputProcessor(int commandLineCharsInOutput, Action<TextEventArgs> FireDataReceived)
        {
            _FireDataReceived = FireDataReceived;
            _commandLineCharsInOutput = commandLineCharsInOutput;
            _commandLineCharsInOutput += Environment.NewLine.Length;//for \n after the command line
        }

        private string FilterOutConsoleCommandLine(string outputChunk)
        {
            if (_commandLineCharsInOutput > 0)
            {
                if (_commandLineCharsInOutput >= outputChunk.Length)
                {
                    _commandLineCharsInOutput -= outputChunk.Length;
                    return null;
                }
                string rest = outputChunk.Substring(_commandLineCharsInOutput);
                _commandLineCharsInOutput = 0;
                return rest;
            }

            return outputChunk;
        }

        public void AnsiStreamChunkReceived(object sender, AnsiStreamChunkEventArgs args)
        {
            var text = args.GetText(GitModule.SystemEncoding);
            string filtered = FilterOutConsoleCommandLine(text);
            if (filtered != null)
            {
                SendAsLines(filtered);
            }
        }

        private void SendAsLines(string output)
        {
            if (_lineChunk != null)
            {
                output = _lineChunk + output;
                _lineChunk = null;
            }
            string[] outputLines = Regex.Split(output, @"(?<=[\n\r])");
            int lineCount = outputLines.Length;
            if (outputLines[lineCount - 1].IsNullOrEmpty())
            {
                lineCount--;
            }
            for (int i = 0; i < lineCount; i++)
            {
                string outputLine = outputLines[i];
                bool isTheLastLine = i == lineCount - 1;
                if (isTheLastLine)
                {
                    bool isLineCompleted = outputLine.Length > 0 &&
                        ((outputLine[outputLine.Length - 1] == '\n') ||
                        outputLine[outputLine.Length - 1] == '\r');
                    if (!isLineCompleted)
                    {
                        _lineChunk = outputLine;
                        break;
                    }
                }
                _FireDataReceived(new TextEventArgs(outputLine));
            }
        }

        public void Flush()
        {
            if (_lineChunk != null)
            {
                _FireDataReceived(new TextEventArgs(_lineChunk));
                _lineChunk = null;
            }
        }
    }
>>>>>>> 1991c921c26de6ed3baf154db596cac92821677d
}