using UnityEngine;

namespace Meta.DroneInvader.Scripts.FSM
{
    public abstract class State : MonoBehaviour
    {
        public abstract void OnStateEnter();
        public abstract State RunCurrentState();
        public abstract void OnStateExit();
    }
}