﻿using UnityEngine;
namespace EnvioEvents
{
    public struct SpawnerCreated : iEvent
    {
        public readonly Transform spawnerLoc;

        public SpawnerCreated (Transform spawnerTrans)
        {
            spawnerLoc = spawnerTrans;
        }
    }
}