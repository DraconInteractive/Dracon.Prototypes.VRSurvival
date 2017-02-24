using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public partial class Player_Main : MonoBehaviour {
	// TODO: For you own mental safety and sanity. Fix this shit peter. - Tom. 21-2-17.
	// cleaning and commenting only does so much.
	// TODO: FIX: Rewrite this stuff in a separate class (in a separate file please) and
	// if you can, preferably in a separate component or even better, a static class.
	public void PickUpWithLeft()
	{
		// Drop the current item.
		if (leftHandItem != null)
		{
			leftHandItem.PutDown();

			// If the item is a physics item then add angular and linear velocity to it's rigidbody.
			if (leftHandItem is Physics_Item)
			{
				var device = SteamVR_Controller.Input((int)leftController.GetComponent<SteamVR_TrackedObject>().index);
				var rigidbody = (leftHandItem as Physics_Item).rb;
				rigidbody.velocity = device.velocity * itemThrowRatio;
				rigidbody.angularVelocity = device.angularVelocity * itemThrowRatio;
			}

			leftHandItem = null;
			leftRModel.SetActive(true);

		}
		// Pick up an item if one was found.
		else
		{
			Collider[] c = Physics.OverlapSphere(leftController.transform.position, 0.1f);
			for (int i = 0; i < c.Length; i++)
			{
				if (c[i].gameObject.tag == "Item")
				{
					Base_Item it = c[i].GetComponent<Base_Item>();

					if (!it.interactable)
						continue;

					// Fancy hand switching.
					if (it.equipped && it.handRole == HandRole.RightHand)
					{
						it.PutDown();
						rightHandItem = null;
						rightRModel.SetActive(true);
						it.PickUp(rightController.gameObject, HandRole.LeftHand);
					}
					// Just boring old picking up things.
					else
					{
						it.PickUp(leftController.gameObject, HandRole.LeftHand);
					}

					leftHandItem = it;
					leftRModel.SetActive(false);
					break;
				}
			}

		}

	}

	public void PickUpWithRight()
	{
		// Drop the current item.
		if (rightHandItem != null)
		{
			rightHandItem.PutDown();

			// If the item is a physics item then add angular and linear velocity to it's rigidbody.
			if (rightHandItem is Physics_Item)
			{
				var device = SteamVR_Controller.Input((int)rightController.GetComponent<SteamVR_TrackedObject>().index);
				var rigidbody = (rightHandItem as Physics_Item).rb;
				rigidbody.velocity = device.velocity * itemThrowRatio;
				rigidbody.angularVelocity = device.angularVelocity * itemThrowRatio;
			}

			rightHandItem = null;
			rightRModel.SetActive(true);
		}
		// Pick up an item if one was found.
		else
		{
			Collider[] c = Physics.OverlapSphere(rightController.transform.position, 0.1f);
			for (int i = 0; i < c.Length; i++)
			{
				if (c[i].gameObject.tag == "Item")
				{
					Base_Item it = c[i].GetComponent<Base_Item>();

					if (!it.interactable)
						continue;

					// Fancy hand switching.
					if (it.equipped && it.handRole == HandRole.LeftHand)
					{
						it.PutDown();
						leftHandItem = null;
						leftRModel.SetActive(true);
						it.PickUp(rightController.gameObject, HandRole.RightHand);
					}
					// Just boring old picking up things.
					else
					{
						it.PickUp(rightController.gameObject, HandRole.RightHand);
					}

					rightHandItem = it;
					rightRModel.SetActive(false);
					break;
				}
			}
		}

	}

	public void PickUp (HandRole hand) {
		switch (hand)
		{
		case HandRole.LeftHand:
			// Drop the current item.
			if (leftHandItem != null)
			{
				leftHandItem.PutDown();

				// If the item is a physics item then add angular and linear velocity to it's rigidbody.
				if (leftHandItem is Physics_Item)
				{
					var device = SteamVR_Controller.Input((int)leftController.GetComponent<SteamVR_TrackedObject>().index);
					var rigidbody = (leftHandItem as Physics_Item).rb;
					rigidbody.velocity = device.velocity * itemThrowRatio;
					rigidbody.angularVelocity = device.angularVelocity * itemThrowRatio;
				}

				leftHandItem = null;
				leftRModel.SetActive(true);

			}
			// Pick up an item if one was found.
			else
			{
				Collider[] c = Physics.OverlapSphere(leftController.transform.position, 0.1f);
				for (int i = 0; i < c.Length; i++)
				{
					if (c[i].gameObject.tag == "Item")
					{
						Base_Item it = c[i].GetComponent<Base_Item>();

						if (!it.interactable)
							continue;

						// Fancy hand switching.
						if (it.equipped && it.handRole == HandRole.RightHand)
						{
							it.PutDown();
							rightHandItem = null;
							rightRModel.SetActive(true);
							it.PickUp(rightController.gameObject, HandRole.LeftHand);
						}
						// Just boring old picking up things.
						else
						{
							it.PickUp(leftController.gameObject, HandRole.LeftHand);
						}

						leftHandItem = it;
						leftRModel.SetActive(false);
						break;
					}
				}

			}
			break;
		case HandRole.RightHand:
			// Drop the current item.
			if (rightHandItem != null)
			{
				rightHandItem.PutDown();

				// If the item is a physics item then add angular and linear velocity to it's rigidbody.
				if (rightHandItem is Physics_Item)
				{
					var device = SteamVR_Controller.Input((int)rightController.GetComponent<SteamVR_TrackedObject>().index);
					var rigidbody = (rightHandItem as Physics_Item).rb;
					rigidbody.velocity = device.velocity * itemThrowRatio;
					rigidbody.angularVelocity = device.angularVelocity * itemThrowRatio;
				}

				rightHandItem = null;
				rightRModel.SetActive(true);
			}
			// Pick up an item if one was found.
			else
			{
				Collider[] c = Physics.OverlapSphere(rightController.transform.position, 0.1f);
				for (int i = 0; i < c.Length; i++)
				{
					if (c[i].gameObject.tag == "Item")
					{
						Base_Item it = c[i].GetComponent<Base_Item>();

						if (!it.interactable)
							continue;

						// Fancy hand switching.
						if (it.equipped && it.handRole == HandRole.LeftHand)
						{
							it.PutDown();
							leftHandItem = null;
							leftRModel.SetActive(true);
							it.PickUp(rightController.gameObject, HandRole.RightHand);
						}
						// Just boring old picking up things.
						else
						{
							it.PickUp(rightController.gameObject, HandRole.RightHand);
						}

						rightHandItem = it;
						rightRModel.SetActive(false);
						break;
					}
				}
			}
			break;
		}
	}

	public void PickUp (HandRole hand, Base_Item i) {
		if (!i.interactable) {
			return;
		}
		switch (hand) {
		case HandRole.LeftHand:
			if (leftHandItem != null) {
				leftHandItem.PutDown ();

				// If the item is a physics item then add angular and linear velocity to it's rigidbody.
				if (leftHandItem is Physics_Item)
				{
					var device = SteamVR_Controller.Input((int)leftController.GetComponent<SteamVR_TrackedObject>().index);
					var rigidbody = (leftHandItem as Physics_Item).rb;
					rigidbody.velocity = device.velocity * itemThrowRatio;
					rigidbody.angularVelocity = device.angularVelocity * itemThrowRatio;
				}

				leftHandItem = null;
				leftRModel.SetActive(true);
			} else {
//				if (!i.interactable)
//					continue;

				// Fancy hand switching.
				if (i.equipped && i.handRole == HandRole.RightHand)
				{
					i.PutDown();
					rightHandItem = null;
					rightRModel.SetActive(true);
					i.PickUp(rightController.gameObject, HandRole.LeftHand);
				}
				// Just boring old picking up things.
				else
				{
					i.PickUp(leftController.gameObject, HandRole.LeftHand);
				}

				leftHandItem = i;
				leftRModel.SetActive(false);
			}
			break;
		case HandRole.RightHand:
			if (rightHandItem != null) {
				rightHandItem.PutDown ();

				// If the item is a physics item then add angular and linear velocity to it's rigidbody.
				if (rightHandItem is Physics_Item)
				{
					var device = SteamVR_Controller.Input((int)rightController.GetComponent<SteamVR_TrackedObject>().index);
					var rigidbody = (rightHandItem as Physics_Item).rb;
					rigidbody.velocity = device.velocity * itemThrowRatio;
					rigidbody.angularVelocity = device.angularVelocity * itemThrowRatio;
				}

				rightHandItem = null;
				rightRModel.SetActive(true);
			} else {
//				if (!i.interactable)
//					continue;

				// Fancy hand switching.
				if (i.equipped && i.handRole == HandRole.LeftHand)
				{
					i.PutDown();
					leftHandItem = null;
					leftRModel.SetActive(true);
					i.PickUp(leftController.gameObject, HandRole.RightHand);
				}
				// Just boring old picking up things.
				else
				{
					i.PickUp(rightCanvas.gameObject, HandRole.RightHand);
				}

				rightHandItem = i;
				rightRModel.SetActive(false);
			}
			break;
		}
	}
}
