using UnityEngine;
public class ParallaxLayer : MonoBehaviour {
    public Transform cam; public float factor = 0.5f;
    Vector3 start;
    void Start(){ start = transform.position; }
    void LateUpdate(){
        var c = cam.position;
        transform.position = new Vector3(start.x + c.x * factor, start.y, start.z);
    }
}
