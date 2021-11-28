using Google.XR.Cardboard;
using UnityEngine;

namespace Cardboard
{
	public class CardboardInitialize : MonoBehaviour
	{
		void Start()
		{
			SetParams();
		}

		void Update()
		{
			CheckParams();
		}

		private void SetParams()
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			if (!Api.HasDeviceParams())
			{
				Api.ScanDeviceParams();
			}
		}

		private void CheckParams()
		{
			if (Api.IsGearButtonPressed)
			{
				Api.ScanDeviceParams();
			}

			if (Api.IsCloseButtonPressed)
			{
				Application.Quit();
			}

			if (Api.IsTriggerHeldPressed)
			{
				Api.Recenter();
			}

			if (Api.HasNewDeviceParams())
			{
				Api.ReloadDeviceParams();
			}

			Api.UpdateScreenParams();
		}
	}
}