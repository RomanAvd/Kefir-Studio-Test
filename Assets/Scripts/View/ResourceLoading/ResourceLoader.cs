﻿using Asteroids.View.View.MovingObjects;
using UnityEngine;

namespace Asteroids.View.ResourceLoading
{
    internal static class ResourceLoader
    {
        private const string MovingObjectsPath = "MovingObjects/";

        public static MovingObject LoadMovingObject(string resourceKey)
        {
            return Resources.Load<MovingObject>(MovingObjectsPath + resourceKey);
        }
    }
}