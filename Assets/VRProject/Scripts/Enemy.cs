using UnityEngine;

namespace Enemies
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField]
		private Material _selected;
		[SerializeField]
		private Material _deselected;

		private EnemySpawner _spawner;

		private void Start()
		{
			GetComponent<MeshRenderer>().material = _deselected;
		}

		public void Select()
		{
			GetComponent<MeshRenderer>().material = _selected;
		}

		public void Deselect()
		{
			GetComponent<MeshRenderer>().material = _deselected;
		}

		public void OnDestroy()
		{
			_spawner.SpawnEnemy();
		}

		public void SetParent(EnemySpawner spawner)
		{
			_spawner = spawner;
		}
	}
}