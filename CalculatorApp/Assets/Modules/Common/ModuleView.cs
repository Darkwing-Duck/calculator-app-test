using System;
using UnityEngine;

namespace Modules.Common
{
	public class ModuleView : MonoBehaviour
	{
		public event Action OnDestroyed;
		
		private void OnDestroy()
		{
			OnDestroyed?.Invoke();
		}
	}
}