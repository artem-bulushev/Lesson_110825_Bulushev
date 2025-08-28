using UnityEngine;

namespace Code
{
    public sealed class Finger : Weapon
    {
        [SerializeField] private FatalShot _fatalShotPrefab;
        
        private FatalShot _instantiateFatalShot;

        protected override void Start()
        {
            base.Start();

            Recharge();
        }

        public override void Fire()
        {
            if (_instantiateFatalShot)
            {
                _instantiateFatalShot.Run(_barrel.forward * Force);
                _instantiateFatalShot = null;
            }
        }

        public override void Recharge()
        {
            if (_instantiateFatalShot != null)
            {
                return;
            }
            _instantiateFatalShot = Instantiate(_fatalShotPrefab, _barrel);
            _instantiateFatalShot.Sleep(_barrel.position);
        }
    }
}
