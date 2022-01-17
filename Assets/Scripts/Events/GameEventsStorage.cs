﻿using System;
using UnityEngine;

namespace GameEvent
{
    public static class GameEventsStorage
    {
        public static Action<Vector3, Vector3> OnHitAction;

        public static void OnHitActionHandler(Vector3 origin, Vector3 end)
        {
            OnHitAction?.Invoke(origin, end);
        }
    }
}

