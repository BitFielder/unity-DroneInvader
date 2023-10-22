using UnityEngine;

namespace Meta.Scripts
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void OnInteract(Entity interactor);
    }
}