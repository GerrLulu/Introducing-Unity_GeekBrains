using UnityEngine;

namespace MineItem
{
    public interface IMineExplosion
    {
        public void MineHit(float damage, float force, Vector3 position);
    }
}