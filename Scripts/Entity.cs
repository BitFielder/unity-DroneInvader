using UnityEngine;
using UnityEngine.Events;

namespace Meta.Scripts
{
    public class Entity : MonoBehaviour
    {
        public int maxHealth = 100;
        public int curHealth;

        public UnityEvent<Entity> onDamaged, onDeath;
        
        private void Start()
        {
            curHealth = maxHealth;
        }
        
        public void TakeDamage(int damage, Entity attacker)
        {
            curHealth -= damage;
            onDamaged?.Invoke(attacker);
            if (curHealth <= 0)
                Die(attacker);
        }
        
        public void TakeHeal(int heal)
        {
            curHealth += heal;
            if (curHealth > maxHealth)
                curHealth = maxHealth;
        }
        
        private void Die(Entity killer)
        {
            onDeath?.Invoke(killer);
        }
    }
}