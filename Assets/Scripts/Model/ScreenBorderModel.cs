using UnityEngine;

namespace Asteriods.Model
{
    public interface IScreenBorderModel : ISeamlessPositionHelper
    {
        bool CheckOutOfBounds(Vector2 position);
        void SetBorders(Rect rect);
        Vector2 GetRandomOffscreenPosition();
    }

    public interface ISeamlessPositionHelper
    {
        Vector2 UpdateSeamlessPosition(Vector2 position);
    }

    public sealed class ScreenBorderModel : IScreenBorderModel
    {
        private static readonly Vector2 _outerBorderOffset = new (50, 50);

        private Vector2 _minBorder;
        private Vector2 _outerMinBorder;
        private Vector2 _maxBorder;
        private Vector2 _outerMaxBorder;

        public Vector2 UpdateSeamlessPosition(Vector2 position)
        {
            var x = position.x;
            var y = position.y;
            if (x > _outerMaxBorder.x)
                x = _outerMinBorder.x;
            else if (x < _outerMinBorder.x)
                x = _outerMaxBorder.x;

            if (y > _outerMaxBorder.y)
                y = _outerMinBorder.y;
            else if (y < _outerMinBorder.y)
                y = _outerMaxBorder.y;

            return new Vector2(x, y);
        }

        public Vector2 GetRandomOffscreenPosition()
        {
            var xSide = Random.value;
            var x = xSide <= 0.5f
                ? Random.Range(_outerMinBorder.x, _minBorder.x)
                : Random.Range(_maxBorder.x, _outerMaxBorder.x);
            var ySide = Random.value;
            var y = ySide <= 0.5f
                ? Random.Range(_outerMinBorder.y, _minBorder.y)
                : Random.Range(_maxBorder.y, _maxBorder.y);

            return new Vector2(x, y);
        }

        public bool CheckOutOfBounds(Vector2 position)
        {
            return position.x >= _outerMaxBorder.x || position.x <= _outerMinBorder.x ||
                   position.y >= _outerMaxBorder.y || position.y <= _outerMinBorder.y;
        }

        public void SetBorders(Rect rect)
        {
            _minBorder = rect.min;
            _maxBorder = rect.max;
            _outerMaxBorder = _maxBorder + _outerBorderOffset;
            _outerMinBorder = _minBorder - _outerBorderOffset;
        }
    }
}