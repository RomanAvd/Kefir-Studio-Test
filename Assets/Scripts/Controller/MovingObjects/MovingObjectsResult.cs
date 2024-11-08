using System.Collections.Generic;
using Asteroids.Common.Observer;

namespace Asteroids.Controller.MovingObjects
{
    public interface IMovingObjectsResult : IResult
    {
        IReadOnlyList<MovingObjectData> MovingObjects { get; }
        IReadOnlyList<int> RemovedObjects { get; }
    }

    internal sealed class MovingObjectsResult : IMovingObjectsResult
    {
        public IReadOnlyList<MovingObjectData> MovingObjects { get; }
        public IReadOnlyList<int> RemovedObjects { get; }

        public MovingObjectsResult(IEnumerable<MovingObjectData> movingObjects, IEnumerable<int> removedObjects)
        {
            MovingObjects = new List<MovingObjectData>(movingObjects);
            RemovedObjects = new List<int>(removedObjects);
        }
    }
}