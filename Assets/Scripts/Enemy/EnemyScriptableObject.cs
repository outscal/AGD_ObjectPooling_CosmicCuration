using System;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Enemy
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemySO")]
    public class EnemyScriptableObject : ScriptableObject
    {
        public float spawnDistance;
        public float initialSpawnRate;
        public float minimumSpawnRate;
        public float difficultyDelta;
        public List<EnemyData> enemyData;
    }

    [Serializable]
    public struct EnemyData
    {
        public EnemyType enemyType;
        public EnemyView enemyPrefab;
        public int maxHealth;
        public float minimumSpeed;
        public float maximumSpeed;
        public int damageToInflict;
        public int scoreToGrant;
        public float movementDuration;
        public float rotationSpeed;
        public float rotationTolerance;
        public float dashSpeed;
        public float shootingDistance;
        public float shootingCooldown;
    } 
}