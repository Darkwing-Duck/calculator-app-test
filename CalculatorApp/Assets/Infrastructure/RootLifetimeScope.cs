using Modules.Calculator.Presenter;
using Modules.Common;
using Modules.Screens.Main.Presenter;
using Modules.Screens.Main.View;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
	public class RootLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			// builder.Register<IViewProvider, ResourcesViewProvider>(Lifetime.Singleton);
			builder.Register<IViewProvider<MainScreenView>, ResourcesViewProvider<MainScreenView>>(Lifetime.Singleton);
			builder.Register<MainScreenPresenter>(Lifetime.Transient);

			// builder.Register<MainScreenPresenter.Factory>(Lifetime.Singleton);
			// builder.Register<CalculatorPresenter.Factory>(Lifetime.Singleton);

			builder.RegisterEntryPoint<Startup>();
		}
	}
}