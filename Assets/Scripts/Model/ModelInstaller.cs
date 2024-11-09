using Asteriods.Model.Enemies;
using Asteriods.Model.Movement;
using Asteriods.Model.Score;
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

            var movingObjectFactory = new MovingObjectFactory(screenBorderModel);
            container.Bind(movingObjectFactory);
            var movingObjectsModel = new MovingObjectsModelModel(movingObjectFactory);
            container.Bind(movingObjectsModel);

            var shipModel = new ShipModel(settings.ShipSettings, screenBorderModel);
            container.Bind(shipModel);

            var scoreModel = new ScoreModel(shipModel);
            container.Bind(scoreModel);
            var enemiesModel = new EnemiesModel(movingObjectsModel, shipModel, scoreModel);
            container.Bind(enemiesModel);
        }
    }
}