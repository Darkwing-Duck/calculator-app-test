using System;

namespace Modules.Common
{
	
	public class ModuleModel<TOutput>
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