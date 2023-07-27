using Source.Gameplay;
using Source.Infrastructure.Services;
using UnityEngine;

namespace Source.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject initialPoint);
        void CreateEnemySpawner(GameField gameField);
        void CreateHud();
    }
}