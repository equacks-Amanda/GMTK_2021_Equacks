using UnityEngine;

namespace MemoryEvents
{
    public struct SpawnMemory : iEvent
    {
        public Transform memoryTransform;

        public SpawnMemory(Transform memoryTransformReference)
        {
            memoryTransform = memoryTransformReference;
        }
    }
}
