using UnityEngine;

namespace Source.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        
        bool IsMoving { get; }
    }
}