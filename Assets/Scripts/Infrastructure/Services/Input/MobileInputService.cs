using UnityEngine;

namespace Source.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const float MinimalVelocity = 0.1f;

        public Vector2 Axis => 
            new Vector2(SimpleInput.GetAxis(HorizontalAxis), SimpleInput.GetAxis(VerticalAxis));
        
        public bool IsMoving => Axis.sqrMagnitude > MinimalVelocity;
    }
}