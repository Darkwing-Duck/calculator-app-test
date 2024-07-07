using Modules.Screens.Main.Presenter;
using VContainer.Unity;

namespace Infrastructure
{
	public class Startup : IInitializable
	{
		private RootLifetimeScope _rootLifetimeScope;

		public Startup(RootLifetimeScope rootLifetimeScope)
		{
			_rootLifetimeScope = rootLifetimeScope;
		}

		public void Initialize()
		{
			var presenter = new MainScreenPresenter();
			presenter.ShowUnder(_rootLifetimeScope.transform);
		}
	}
}