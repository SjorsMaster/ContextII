using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendInput : MonoBehaviour
{

    public GameObject self;
    public void sendInput() {
        GameObject.FindAnyObjectByType<ReceiveInput>().GetNewObject(self, self.GetComponent<ItemWithStats>().self);
    }
}
