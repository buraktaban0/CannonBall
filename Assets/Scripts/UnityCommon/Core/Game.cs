using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Collections;
using UnityCommon.Logging;
using UnityCommon.Modules;
using UnityCommon.ResourceManagement;
using UnityCommon.Singletons;
using UnityEngine;


namespace UnityCommon.Core
{

	[DefaultExecutionOrder(-10)]
	public class Game : SingletonBehaviour<Game>, Logging.ILogger
	{

		public GamePhase gamePhase;


		public ArgumentCollection arguments;


		private ConcurrentQueue<Action> updateQueue = new ConcurrentQueue<Action>();

		private List<IModule> modules;



		private void Awake()
		{
			ResourceManager.LoadAllResources();
			var modulesLoaded = ResourceManager.GetResources<IModule>();

			modules = new List<IModule>(modulesLoaded);

			modules.Sort(new Comparison<IModule>((m1, m2) => m1.ExecutionOrder.CompareTo(m2.ExecutionOrder)));

			foreach (var m in modules)
			{
				Debug.Log("Module loaded: " + m.GetName());
			}

		}
		


		public void SetPhase(string phaseName)
		{
			SetPhase(GamePhase.Get(phaseName));
		}

		public void SetPhase(GamePhase gamePhase)
		{
			if (gamePhase == null)
			{
				Log.WriteLine("GamePhase is null", this, "SetPhase");
				return;
			}

			this.gamePhase?.OnStop();
			this.gamePhase = gamePhase;
			this.gamePhase?.OnStart();

		}


		public void RunOnMainThread(Action a)
		{
			updateQueue.Enqueue(a);
		}




		private void OnEnable()
		{
			if (Instance != null && Instance != this)
			{
				Log.WriteLine("An instance already exists, disabling self", this);
				this.enabled = false;
				return;
			}

			foreach (var m in modules)
				m.Start();

			Instance = this;

			gamePhase?.OnEnabled();
		}

		private void OnDisable()
		{

			foreach (var m in modules)
				m.Stop();

			gamePhase?.OnDisabled();

			Instance = null;
		}


		private void FixedUpdate()
		{
			gamePhase?.OnFixedUpdate();
		}

		private void Update()
		{

			foreach (var m in modules)
				m.Update();

			Action a;
			if (updateQueue.TryDequeue(out a))
			{
				a?.Invoke();
			}

			gamePhase?.OnUpdate();
		}


		private void OnApplicationQuit()
		{
			gamePhase?.OnApplicationQuit();
		}

		public string GetLoggerIdentity()
		{
			return "GameManager";
		}
	}
}
