using System;
using Source.Behaviour.States;
using UnityEngine;

namespace Source.Behaviour.Transitions
{
    public static class TransitionConditions
    {
        public static Func<bool> DashStarted(DashTimer dashTimer) => () => dashTimer.IsDashing;
        public static Func<bool> DashTimerIsOut(DashTimer dashTimer) => () => dashTimer.IsDashing == false;
        public static Func<bool> TargetReached(MoveAgentToTarget moveTowardsTarget, float distance) => 
            () => moveTowardsTarget.RemainingDistance < distance;
        public static Func<bool> TargetTooFar(Transform self, Transform target, float distance) => 
            () => Vector3.Distance(self.position, target.position) > distance;
    }
}