using UnityEngine;

namespace Meta.DroneInvader.Scripts.FSM
{
    public class IdleState : State
    {
        public override void OnStateEnter()
        {
            
        }

        public override void OnStateExit()
        {
            
        }

        public override State RunCurrentState()
        {
            return this;
        }
    }
}