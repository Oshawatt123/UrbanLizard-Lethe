using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompass : MonoBehaviour {

    public Transform player;
    Vector3 vector;

    void Start() {
        
    }

  void Update () {

    vector.z = player.eulerAngles.y;
    transform.localEulerAngles = vector;

}

}