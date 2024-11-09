using Asteriods.Model.Enemies;
using Asteriods.Model.Movement;
using Asteriods.Model.Weapons;
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
            var weaponFactory = new WeaponModelFactory(movingObjectsModel);
            container.Bind(weaponFactory);

            var shipModel = new ShipModel(settings.ShipSettings, screenBorderModel, weaponFactory);
            container.Bind(shipModel);

            var enemiesModel = new EnemiesModel(movingObjectsModel, shipModel);
            container.Bind(enemiesModel);
        }
    }
}