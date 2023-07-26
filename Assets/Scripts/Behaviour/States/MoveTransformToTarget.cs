using System;
using UnityEngine;

namespace Source.Behaviour.States
{
    public class MoveTransformToTarget : MoveToTarget
    {
        public MoveTransformToTarget(Transform target, Transform self, float speed) : base(target, self, speed) { }

        public override void Tick()
        {
            Vector3 direction = (Target.position - Self.position).normalized;
            Self.position += direction * CurrentSpeed * Time.deltaTime;
            Self.forward = direction;
        }
    }
}