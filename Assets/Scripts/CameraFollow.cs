using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float smooth = 8f;
    public Vector2 offset = new Vector2(4f, 2f);
    public bool lockY = true;

    void LateUpdate(){
        if (!target) return;

        Vector3 tp = target.position;
        float y = lockY ? offset.y : tp.y + offset.y;  // keep Y steady if lockY
        Vector3 goal = new Vector3(tp.x + offset.x, y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, goal, smooth * Time.deltaTime);
    }
}
