using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommon.Events;
using UnityCommon.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCommon.UI.EventBinding
{
	[ExecuteInEditMode]
	public abstract class TextEventBinder<TReference> : UIEventBinder<Text, TReference> where TReference : Reference
	{

		public abstract override void OnEventFired();

	}
}
