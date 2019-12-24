using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityCommon.Logging
{
	public static class Log
	{

		private static readonly int MAX_INDENT = 6;

		private static readonly int INDENT_LENGTH = 4;

		private static int indent = 0;


		private static StringBuilder builder;

		private static StreamWriter writer;


		public static void Initialize()
		{
			if (writer != null)
			{
				Debug.LogError("Tried to initialize logging when it's already initialized.");
				return;
			}

			Debug.Log("Logging initialized");

			indent = 0;


			builder = new StringBuilder();

			var fileName = "log" + UnityCommon.Time.UnixTimestamp + ".txt";
			var dataFolder = new DirectoryInfo(UnityEngine.Application.dataPath);
			var rootFolder = dataFolder.Parent;

			var logFile = new FileInfo(rootFolder + "/gamelogs/" + fileName);

			Directory.CreateDirectory(logFile.Directory.FullName);

			var fs = logFile.Create();

			writer = new StreamWriter(fs, Encoding.UTF8);
			writer.AutoFlush = true;
		}

		public static void Shutdown()
		{
			writer?.Close();
			writer = null;
		}


		public static void AddIndent()
		{
			indent = Math.Min(MAX_INDENT, indent + 1);
		}

		public static void RemoveIndent()
		{
			indent = Math.Max(0, indent - 1);
		}


		private static void FormatAndAppend(string s, ILogger logger = null, string context = null)
		{
			var indentString = String.Concat(Enumerable.Repeat(" ", INDENT_LENGTH * indent));
			var indentedNewline = "\r\n" + indentString;


			if (logger != null)
			{
				s = ":\n" + s;

				if (context != null)
				{
					s = "->" + context + s;
				}

				s = logger.GetLoggerIdentity() + s;

			}

			s = s.Trim().Replace("\n\r", "\n").Replace("\r\n", "\n").Replace("\n", indentedNewline);


			builder.Append(indentString).Append(s);



		}

		public static void WriteLine(string line, ILogger logger = null, string context = null)
		{
			if (builder == null)
				Initialize();

			FormatAndAppend(line, logger, context);

#if UNITY_EDITOR
			UnityEngine.Debug.Log(builder.ToString());
#endif

			if (writer == null)
			{
#if UNITY_EDITOR
				builder.Clear();
				return;
#else
				throw new NullReferenceException("Tried to use logging without calling Log.Initialize()");
#endif
			}



			writer.WriteLine(builder.ToString());

			builder.Clear();

		}


	}
}
