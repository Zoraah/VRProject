using UnityEngine;

namespace Teleport
{
	public class Teleport : MonoBehaviour
	{
		[SerializeField]
		private Material _selected;
		[SerializeField]
		private Material _deselected;

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
	}
}