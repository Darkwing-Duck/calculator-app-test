using Modules.Common.Storage;

namespace Modules.Common
{
	public abstract class PersistentModel<TData> where TData : new()
	{
		protected TData PersistentData = new();
		
		public void LoadFrom(IModuleStorage storage) => PersistentData = storage.Get(PersistentData);
		public void SaveTo(IModuleStorage storage) => storage.Set(PersistentData);
	}
}