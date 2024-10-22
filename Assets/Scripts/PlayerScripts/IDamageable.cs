using UnityEngine;

namespace PlayerScripts
{
    public interface IDamageable
    {
        public void Damage(float dmgAmount, GameObject enemy)
        {
            enemy.GetComponent<Enemy>().ChangeHp(dmgAmount);
        }
    }
}