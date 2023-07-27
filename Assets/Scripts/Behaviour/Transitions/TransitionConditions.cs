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
        
        public static Func<bool> IdleForAWhile(RangeAttack rangeAttack, float idleTime) =>
            () => rangeAttack.IdleTime > idleTime;

        public static Func<bool> LastShotInSeries(RangeAttack rangeAttack) =>
            () => rangeAttack.Shots > rangeAttack.MaxShotsInSeries;

        public static Func<bool> ReachedDestination(MoveAgentToRandomPoint moveToRandomPoint) =>
            () => Vector3.Distance(moveToRandomPoint.Self.position, moveToRandomPoint.Destination) < MoveAgentToRandomPoint.MinimalDistanceToPoint;
    }
}