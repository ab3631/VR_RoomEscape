using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    public void Open()
    {
        leftDoor.transform.DOLocalRotate(new Vector3(0, -80, 0), 1f);
        rightDoor.transform.DOLocalRotate(new Vector3(0, 80, 0), 1f);
    }
}
