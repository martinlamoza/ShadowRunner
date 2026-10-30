using UnityEngine;

public class CameraFollowSimple : MonoBehaviour {
    public Transform target;
    public float xOffset = 4f;   // how far ahead to look
    public float fixedY = -3.5f; // choose a value that shows player + ground

    void LateUpdate(){
        if (!target) return;
        transform.position = new Vector3(target.position.x + xOffset, fixedY, -10f);
    }
}
