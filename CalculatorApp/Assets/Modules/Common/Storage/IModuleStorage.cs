namespace Modules.Common.Storage
{
	public interface IModuleStorage
	{
		bool Has();
		void Set(object value);
		T Get<T>(T defaultValue);

		void Remove();
		void Clear();
	}
}