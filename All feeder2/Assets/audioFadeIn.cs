using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioFadeIn : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
        HallwayAudio.inRoom2 = true;
   }
}
