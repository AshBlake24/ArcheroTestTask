using UnityEngine;

namespace Source.Infrastructure.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
    }
}