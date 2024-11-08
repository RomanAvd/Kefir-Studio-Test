using Asteriods.Model.Enemies;
using Asteriods.Model.Movement;
using Asteroids.Common.MonoInjection;
using Asteroids.Common.Settings;
using Asteroids.Model.Ship;

namespace Asteriods.Model
{
    public static class ModelInstaller
    {
        public static void InstallModels(IContainer container, IGameSettings settings)
        {
            var screenBorderModel = new ScreenBorderModel();
            container.Bind(screenBorderModel);
            var shipModel = new ShipModel(settings.ShipSettings, screenBorderModel);
            container.Bind(shipModel);

            var movingObjectFactory = new MovingObjectFactory(shipModel, screenBorderModel);
            container.Bind(movingObjectFactory);
            var movingObjectsModel = new MovingObjectsModelModel(movingObjectFactory);
            container.Bind(movingObjectsModel);
            var enemiesModel = new EnemiesModel(movingObjectsModel);
            container.Bind(enemiesModel);
        }
    }
}