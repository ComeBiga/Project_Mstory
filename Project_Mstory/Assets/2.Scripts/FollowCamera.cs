using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Transform trCharacter;

    // Update is called once per frame
    void LateUpdate()
    {
        camera.transform.position = new Vector3(trCharacter.position.x, trCharacter.position.y, camera.transform.position.z);
    }
}
