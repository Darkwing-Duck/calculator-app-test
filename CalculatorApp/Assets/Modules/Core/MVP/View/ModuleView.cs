using System;
using UnityEngine;

namespace Modules.Core
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