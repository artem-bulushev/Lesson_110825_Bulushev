using UnityEngine;

namespace Code
{
    public sealed class WeaponController : MonoBehaviour
    {
         [SerializeField] private Weapon _weapon;
         
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _weapon.Fire();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _weapon.Recharge();
            }
        }
    }
}
