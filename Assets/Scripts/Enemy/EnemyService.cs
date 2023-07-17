using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CosmicCuration.Enemy
{
    public class EnemyService
    {
        #region Dependencies
        private EnemyScriptableObject enemyScriptableObject;
        private EnemyPool enemyPool;
        #endregion

        #region Variables
        private bool isSpawning;
        private float currentSpawnRate;
        private float spawnTimer; 
        #endregion

        #region Initialization
        public EnemyService(EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyScriptableObject = enemyScriptableObject;
            enemyPool = new EnemyPool();
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            isSpawning = true;
            currentSpawnRate = enemyScriptableObject.initialSpawnRate;
            spawnTimer = currentSpawnRate;
        } 
        #endregion

        public void Update()
        {
            if (isSpawning)
            {
                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0)
                {
                    SpawnEnemy();
                    IncreaseDifficulty();
                    ResetSpawnTimer();
                }
            }
        }

        #region Spawning Enemies
        private void SpawnEnemy()
        {
            // Select a random enemy (Shooting/RapidDash/ContinousChase/Normal)
            EnemyType randomEnemy = (EnemyType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length);

            // Fetch the corresponding EnemyController.
            EnemyController enemy = FetchEnemy(randomEnemy);

            // Get a random orientation for the enemy (Up / Down / Left / Right)
            EnemyOrientation randomOrientation = (EnemyOrientation)Random.Range(0, Enum.GetValues(typeof(EnemyOrientation)).Length);

            // Configure the enemy to be spawned.
            enemy.Configure(CalculateSpawnPosition(randomOrientation), randomOrientation);
           
        }

        private EnemyController FetchEnemy(EnemyType typeToFetch)
        {
            EnemyData fetchedData = enemyScriptableObject.enemyData.Find(item => item.enemyType == typeToFetch);

            switch (typeToFetch)
            {
                case EnemyType.Shooting:
                    return (EnemyController)enemyPool.GetEnemy<ShootingEnemy>(fetchedData);
                case EnemyType.RapidDash:
                    return (EnemyController)enemyPool.GetEnemy<RapidDashEnemy>(fetchedData);
                case EnemyType.ContinousChase:
                    return (EnemyController)enemyPool.GetEnemy<ContinousChaseEnemy>(fetchedData);
                case EnemyType.Normal:
                    return (EnemyController)enemyPool.GetEnemy<NormalEnemy>(fetchedData);
                default:
                    throw new Exception($"Failed to Create EnemyController for: {typeToFetch}");
            }
        }

        //private void SpawnEnemyAtPosition(Vector2 spawnPosition, EnemyOrientation enemyOrientation)
        //{
        //    EnemyController spawnedEnemy = enemyPool.GetEnemy();
        //    spawnedEnemy.Configure(spawnPosition, enemyOrientation);
        //}

        private Vector2 CalculateSpawnPosition(EnemyOrientation enemyOrientation)
        {
            // Calculate a random spawn position outside the visible screen
            Vector3 spawnPosition = Vector3.zero;
            float halfScreenWidth = Camera.main.aspect * Camera.main.orthographicSize;
            float halfScreenHeight = Camera.main.orthographicSize;

            switch (enemyOrientation)
            {
                case EnemyOrientation.Left:
                    spawnPosition.x = halfScreenWidth + enemyScriptableObject.spawnDistance;
                    spawnPosition.y = Random.Range(-halfScreenHeight, halfScreenHeight);
                    break;

                case EnemyOrientation.Right:
                    spawnPosition.x = -halfScreenWidth - enemyScriptableObject.spawnDistance;
                    spawnPosition.y = Random.Range(-halfScreenHeight, halfScreenHeight);
                    break;

                case EnemyOrientation.Up:
                    spawnPosition.x = Random.Range(-halfScreenWidth, halfScreenWidth);
                    spawnPosition.y = -halfScreenHeight - enemyScriptableObject.spawnDistance;
                    break;

                case EnemyOrientation.Down:
                    spawnPosition.x = Random.Range(-halfScreenWidth, halfScreenWidth);
                    spawnPosition.y = halfScreenHeight + enemyScriptableObject.spawnDistance;
                    break;
            }

            return spawnPosition;
        } 
        #endregion

        private void IncreaseDifficulty()
        {
            if (currentSpawnRate > enemyScriptableObject.minimumSpawnRate)
                currentSpawnRate -= enemyScriptableObject.difficultyDelta;
            else
                currentSpawnRate = enemyScriptableObject.minimumSpawnRate;
        }

        private void ResetSpawnTimer() => spawnTimer = currentSpawnRate;

        public void SetEnemySpawning(bool setActive) => isSpawning = setActive;

        public void ReturnEnemyToPool(EnemyController enemyToReturn) => enemyPool.ReturnItem(enemyToReturn);
    }

    public enum EnemyOrientation
    {
        Up,
        Down,
        Left,
        Right
    } 
}