using Source.Behaviour;
using Source.Enemies.Data;
using Source.Infrastructure.Services;
using UnityEngine;

namespace Source.Enemies.Factories
{
    public interface IEnemyBehaviourFactory : IService
    {
        StateMachine CreateEnemyBehaviour(EnemyStaticData enemyData, Transform target, Enemy enemy);
    }
}