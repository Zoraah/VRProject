using System.Collections;
using UnityEngine;

namespace Enemies
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField]
		private Enemy _enemyPrefab;
		private Enemy _spawnedEnemy;

		[SerializeField]
		private float _timeToCreateEnemy;

		private void Start()
		{
			_spawnedEnemy = Instantiate(_enemyPrefab, transform) as Enemy;
			_spawnedEnemy.SetParent(this);
		}

		public void SpawnEnemy()
		{
			StartCoroutine("EnemyCreating");
		}

		private IEnumerator EnemyCreating()
		{
			yield return new WaitForSeconds(_timeToCreateEnemy);
			_spawnedEnemy = Instantiate(_enemyPrefab, transform);
			_spawnedEnemy.SetParent(this);
		}
	}
}