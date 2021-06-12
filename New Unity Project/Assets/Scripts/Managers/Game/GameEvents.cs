using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    public struct GameOver:iEvent{};

    public struct MemorySelected:iEvent
    {
        public readonly bool wasPlayerCorrect;

        public MemorySelected(bool pStatus)
        {
            wasPlayerCorrect = pStatus;
        }
    }
}