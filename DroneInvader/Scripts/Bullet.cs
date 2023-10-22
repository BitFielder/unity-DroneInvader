using UnityEngine;
using Meta.Scripts;

namespace Meta.DroneInvader.Scripts
{
    public class Bullet : MonoBehaviour
    {
        public Entity parent;
        public int damage;
        public float speed;

        private Rigidbody _rigid;

        void Start()
        {
            _rigid = GetComponent<Rigidbody>();
        }
        
        void Update()
        {
            _rigid.velocity = transform.forward * speed;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == parent.transform)
                return;
            
            if (other.TryGetComponent(out Bullet bullet))
                return;
            
            if (other.TryGetComponent(out Entity hit))
                hit.TakeDamage(damage, parent);

            Destroy(gameObject);
        }
    }
}