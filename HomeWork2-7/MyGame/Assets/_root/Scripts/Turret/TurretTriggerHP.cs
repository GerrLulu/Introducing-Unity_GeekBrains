using Bullet;
using UnityEngine;

namespace Turret
{
    public class TurretTriggerHP : MonoBehaviour, IBulletDamage
    {
        private Turret _turret;


        private void Start()
        {
            _turret = GetComponentInParent<Turret>();
        }


        public void Hit(float damage)
        {
            _turret.Hp = _turret.Hp - damage;
            Debug.Log($"{_turret.name} HP: {_turret.Hp}");
            //if (_hp <= 0)
            //    Destroy(gameObject);
        }
    }
}