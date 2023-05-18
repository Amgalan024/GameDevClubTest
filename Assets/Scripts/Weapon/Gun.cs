using System.Collections;
using Projectile;
using UnityEngine;

namespace Weapon
{
    public class Gun : BaseWeapon
    {
        [SerializeField] private ProjectileModel _projectilePrefab;
        [SerializeField] private Transform _shootPoint;

        public override void Shoot(Transform target)
        {
            var projectile = Instantiate(_projectilePrefab, _shootPoint);

            projectile.StartCoroutine(MoveProjectileCoroutine(projectile, target));
        }

        private IEnumerator MoveProjectileCoroutine(ProjectileModel projectile, Transform target)
        {
            while (true)
            {
                projectile.transform.position = Vector3.Lerp(projectile.transform.position, target.position,
                    projectile.Speed * Time.deltaTime);

                yield return null;
            }
        }
    }
}