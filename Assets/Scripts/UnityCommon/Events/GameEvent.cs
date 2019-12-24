using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityCommon.Events
{
	[CreateAssetMenu(menuName = "Events/Parameterless")]
	public class GameEvent : ScriptableObject
	{
		public string eventName = "Event Name";


		protected List<IGameEventListener> listeners = new List<IGameEventListener>();

		private void Awake()
		{
			this.eventName = this.name;
		}

		public void Raise(object sender)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if (listeners[i] == null)
				{
					listeners.RemoveAt(i);
					continue;
				}

				try
				{
					listeners[i].OnEventFired();
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
				}

			}
		}

		public void RegisterListener(IGameEventListener listener)
		{
			if (listeners.Contains(listener) == false)
			{
				listeners.Add(listener);
			}
		}

		public void UnregisterListener(IGameEventListener listener)
		{
			if (listeners.Contains(listener))
			{
				listeners.Remove(listener);
			}
		}

	}


	public class GameEvent<T, TEvent> : ScriptableObject where TEvent : GameEvent<T, TEvent>
	{

		public string eventName = "Event Name";

		protected List<GameEventListener<T, TEvent>> listeners = new List<GameEventListener<T, TEvent>>();

		public void Raise(object sender, T arg)
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				if (listeners[i] == null)
				{
					listeners.RemoveAt(i);
					continue;
				}

				try
				{
					listeners[i].OnEventFired(arg);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
				}

			}
		}

		public void RegisterListener(GameEventListener<T, TEvent> listener)
		{
			if (listeners.Contains(listener) == false)
			{
				listeners.Add(listener);
			}
		}

		public void UnregisterListener(GameEventListener<T, TEvent> listener)
		{
			if (listeners.Contains(listener))
			{
				listeners.Remove(listener);
			}
		}



	}

}
