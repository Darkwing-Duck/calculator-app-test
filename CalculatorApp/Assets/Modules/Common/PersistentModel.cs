using Modules.Common.Storage;

namespace Modules.Common
{
	public abstract class PersistentModel<TData> where TData : new()
	{
		protected TData _data = new();
		
		public void LoadFrom(IModuleStorage storage) => _data = storage.Get(_data);
		public void SaveTo(IModuleStorage storage) => storage.Set(_data);
	}
}