using Asteroids.Common.MonoInjection;
using Asteroids.Common.Settings;
using Asteroids.Model.Ship;

namespace Asteriods.Model.Model
{
    public class ModelInstaller
    {
        public void InstallModels(Container container, IGameSettings settings)
        {
            container.Bind(new ShipModel(settings.ShipSettings));
        }
    }
}