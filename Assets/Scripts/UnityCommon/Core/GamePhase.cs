using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Logging;
using UnityCommon.Singletons;
using UnityEngine;

namespace UnityCommon.Core
{



	public abstract class GamePhase : ScriptableObject, IGamePhase
	{

		public bool IsRunning { get; set; }

		public virtual void OnStart()
		{
			IsRunning = true;
		}

		public abstract void OnEnabled();

		public abstract void OnDisabled();

		public abstract void OnUpdate();

		public abstract void OnLateUpdate();

		public abstract void OnFixedUpdate();


		public virtual void OnStop()
		{
			IsRunning = false;
		}


		public abstract void OnApplicationQuit();




		public static GamePhase Get(string name)
		{
			var gamePhase = Resources.FindObjectsOfTypeAll<GamePhase>().Where(phase => phase.name == name).FirstOrDefault();

			if (gamePhase == null)
			{

			}

			return gamePhase;
		}




	}


}
