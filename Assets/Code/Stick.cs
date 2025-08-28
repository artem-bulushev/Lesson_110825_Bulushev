using UnityEngine;

namespace Code
{
    public sealed class Stick : Weapon
    {
        [SerializeField] private int _countInClip = 1000;
        [SerializeField] private Bullet _bulletPrefab;

        private Transform _bulletRoot;
        private Bullet[] _bullets;

        protected override void Start()
        {
            base.Start();

            _bulletRoot = new GameObject("BulletRoot").transform;
            Recharge();
        }

        public override void Recharge()
        {
            if (IsAnyActiveBullet())
            {
                return;
            }
            
            _bullets = new Bullet[_countInClip];
            for (int i = 0; i < _countInClip; i++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
                bullet.Sleep();
                _bullets[i] = bullet;
            }
        }

        private bool IsAnyActiveBullet()
        {
            if (_bullets == null)
            {
                return false;
            }
            
            for (int i = 0; i < _countInClip; i++)
            {
                Bullet bullet = _bullets[i];
                
                if (bullet == null)
                {
                    continue;
                }

                if (bullet.IsActive)
                {
                    return true;
                }
            }

            return false;
        }

        public override void Fire()
        {
            if (CanShoot == false)
            {
                return;
            }
            
            if (TryGetBullet(out Bullet bullet))
            {
                bullet.Run(_barrel.forward * Force, _barrel.position);
                LastShootTime = 0.0f;
            }
        }

        private bool TryGetBullet(out Bullet result)
        {
            int candidate = -1;
            
            if (_bullets == null)
            {
                result = default;
                return false;
            }

            for (var i = 0; i < _bullets.Length; i++)
            {
                Bullet bullet = _bullets[i];
                if (bullet == null)
                {
                    continue;
                }
                
                if (bullet.IsActive)
                {
                    continue;
                }

                candidate = i;
                break;
            }

            if (candidate == -1)
            {
                result = default;
                return false;
            }

            result = _bullets[candidate];
            return true;
        }

        public override void GetInfo()
        {
            base.GetInfo();
            
            Debug.LogError(_countInClip);
        }
    }
}
