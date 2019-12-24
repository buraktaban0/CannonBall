using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnityCommon.Threading
{
	public class ProcessFactory
	{

		private Thread thread;
		private bool isRunning = true;

		private BlockingCollection<Process> processQueue = new BlockingCollection<Process>(new ConcurrentQueue<Process>());

		public ProcessFactory(ThreadPriority priority = ThreadPriority.Lowest)
		{
			thread = new Thread(Run);
			thread.Priority = priority;
		}

		private void Run()
		{
			try
			{
				while (isRunning)
				{
					var process = processQueue.Take();

					try
					{
						process.Run();
						process.IsCompleted = true;
					}
					catch (Exception ex)
					{

					}

				}
			}
			catch (Exception ex)
			{

			}
		}

		public void Dispose()
		{
			isRunning = false;
			processQueue.CompleteAdding();
			thread.Join();
		}

	}
}
