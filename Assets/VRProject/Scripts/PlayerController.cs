using UnityEngine;
using UnityEngine.UI;

namespace Player
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private GameObject _xrRig;

		[SerializeField]
		private Image _progressBar;

		private GameObject _selectedObject;

		[SerializeField]
		private float _maxDistanceToShoot;

		[SerializeField]
		private float _waitTime;
		private float _timeToTeleport;
		private float _timeToShoot;

		[SerializeField]
		private Animator _pistolAnimator;

		private AudioSource _audioSource;
		[SerializeField]
		private AudioClip _gunSoundEffect;
		[SerializeField]
		private AudioClip _teleportSoundEffect;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();

			_timeToTeleport = 0;
			_timeToShoot = 0;
		}

		private void Update()
		{
			CheckObjectInForward();
		}

		private void CheckObjectInForward()
		{
			RaycastHit _hit;
			if (Physics.Raycast(transform.position, transform.forward, out _hit))
			{
				if (_hit.transform.gameObject.tag == "Teleport" || _hit.transform.gameObject.tag == "Enemy" && _hit.distance <= _maxDistanceToShoot)
				{
					if (_hit.transform.gameObject != _selectedObject)
					{
						_timeToTeleport = 0;
						_timeToShoot = 0;
						_progressBar.fillAmount = 0;

						_selectedObject?.SendMessage("Deselect");
						_selectedObject = _hit.transform.gameObject;
						_selectedObject?.SendMessage("Select");
					}
				}

				if (_hit.transform.gameObject.tag == "Teleport")
				{
					_timeToTeleport += Time.deltaTime;
					_progressBar.fillAmount = 1.0f / _waitTime * _timeToTeleport;
					if (_timeToTeleport >= _waitTime)
					{
						_xrRig.transform.position = _selectedObject.transform.position;
						_xrRig.transform.position -= new Vector3(0f, 1.75f, 0f);
						_audioSource.clip = _teleportSoundEffect;
						_audioSource.Play();
						_timeToTeleport = 0f;
						_progressBar.fillAmount = 0f;
					}
				}
				else if (_hit.transform.gameObject.tag == "Enemy" && _hit.distance <= _maxDistanceToShoot)
				{
					_timeToShoot += Time.deltaTime;
					_progressBar.fillAmount = 1.0f / _waitTime * _timeToShoot;
					if (_timeToShoot >= _waitTime)
					{
						Destroy(_selectedObject);
						_selectedObject = null;
						_audioSource.clip = _gunSoundEffect;
						_audioSource.Play();
						_pistolAnimator.Play("Animation_PistolShoot");
						_timeToShoot = 0f;
						_progressBar.fillAmount = 0f;
					}
				}
			}
			else
			{
				_selectedObject?.SendMessage("Deselect");
				_selectedObject = null;
				_timeToTeleport = 0;
				_timeToShoot = 0;
				_progressBar.fillAmount = 0;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Teleport")
			{
				other.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.tag == "Teleport")
			{
				other.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
	}
}