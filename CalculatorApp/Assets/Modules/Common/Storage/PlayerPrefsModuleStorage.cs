using UnityEngine;

namespace Modules.Common.Storage
{
	public class PlayerPrefsModuleStorage : ModuleStorage
	{
		public PlayerPrefsModuleStorage(string modulePath) : base(modulePath)
		{ }

		protected override void SetValue(string path, string value)
		{
			PlayerPrefs.SetString(path, value);
			Debug.Log($"Path: {path}, Value = {value}");
			PlayerPrefs.Save();
		}

		protected override string GetValue(string path, string defaultValue) => 
			PlayerPrefs.GetString(path, defaultValue);

		protected override bool HasValue(string path) => PlayerPrefs.HasKey(path);
		protected override void RemoveValue(string path) => PlayerPrefs.DeleteKey(path);
		public override void Clear() => PlayerPrefs.DeleteAll();
	}
}