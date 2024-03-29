using UnityEditor.UIElements;
using UnityEngine;

namespace Runtime.Extensions
{
    public static class SelfExtensions
    {
        public static Vector2 GetRandomPosition(GameObject spawnZoneObject)
        {
            float width = spawnZoneObject.transform.localScale.x;
            float height = spawnZoneObject.transform.localScale.y;
            
            Vector2 centerPosition = spawnZoneObject.transform.position;
            Vector2 topCenterPosition = centerPosition + new Vector2(0, height / 2f);
            
            Vector2 randomPosition = new Vector2(
                Random.Range(centerPosition.x - width / 2f, centerPosition.x + width / 2f),
                topCenterPosition.y
            );
            return randomPosition;
        }
    }
}