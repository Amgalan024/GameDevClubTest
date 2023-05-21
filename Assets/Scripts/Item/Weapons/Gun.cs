using System.Collections;
using Player;
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
            var projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);

            projectile.StartCoroutine(MoveProjectileCoroutine(projectile, target));
        }

        private IEnumerator MoveProjectileCoroutine(ProjectileModel projectile, Transform target)
        {
            var targetPosition = target.position;
            
            while (true)
            {
                projectile.transform.position = Vector3.Lerp(projectile.transform.position, targetPosition,
                    projectile.Speed * Time.deltaTime);

                yield return null;
            }
        }

        public override void Use(PlayerModel playerModel)
        {
            var attackService = playerModel.UnitServiceProvider.GetService<PlayerAttackService>();

            attackService.SetWeapon(this);
        }

        public override void OnAdded(PlayerModel playerModel)
        {
            var attackService = playerModel.UnitServiceProvider.GetService<PlayerAttackService>();

            if (attackService.IsWeaponEquipped() == false)
            {
                Use(playerModel);
            }
        }

        public override void OnRemoved(PlayerModel playerModel)
        {
        }
    }
}