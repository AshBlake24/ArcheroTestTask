using UnityEngine;

namespace Source.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        public Vector2 Axis => 
            new Vector2(SimpleInput.GetAxis(HorizontalAxis), SimpleInput.GetAxis(VerticalAxis));
    }
}