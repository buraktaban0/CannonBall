using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommon.Modules
{
	public class TimerModule : Module
	{

		private List<TimedAction> actions;

		public TimedAction ScheduleAction(Action action, float delay, float period)
		{
			TimedAction timedAction = new TimedAction(action, delay, period);

			for (int i = 0; i < actions.Count; i++)
			{
				var act = actions[i];
				if (act.countdown > delay)
				{
					actions.Insert(i, timedAction);
					return timedAction;
				}
			}

			actions.Add(timedAction);
			return timedAction;

		}

		public void CancelAction(TimedAction timedAction)
		{
			if (actions.Contains(timedAction))
			{
				actions.Remove(timedAction);
			}
		}



		public override void Start()
		{
			actions = new List<TimedAction>(8);
		}

		public override void Stop()
		{
			actions.Clear();
			actions = null;
		}

		public override void Update()
		{
			float dt = UnityEngine.Time.deltaTime;
			int index = 0;
			for (int i = 0; i < actions.Count; i++, index++)
			{
				var act = actions[index];
				act.countdown -= dt;
				if (act.countdown <= 0)
				{
					act.countdown += act.period;
					act.action.Invoke();
					actions.RemoveAt(index);
					actions.Add(act);
					index--;
				}
			}

		}




	}

	public class TimedAction
	{
		public Action action;
		public float countdown;
		public float period;

		public TimedAction(Action action, float countdown, float period)
		{
			this.action = action;
			this.countdown = countdown;
			this.period = period;
		}



	}


}
