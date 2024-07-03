using System;
using UnityEngine;

namespace Modules.Common
{
	
	public class ModuleView<TOutput> : MonoBehaviour
	{
		protected TOutput Output;
		
		public void Initialize(TOutput output)
		{
			if (Output is not null) {
				throw new Exception($"'{GetType().Name}' is already initialized.");
			}
			
			Output = output;
		}
	}
	
}