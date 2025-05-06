using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room3 : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
        HallwayAudio.inRoom3 = true;
   }
}
