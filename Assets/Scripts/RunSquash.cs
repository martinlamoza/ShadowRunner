using UnityEngine;

public class RunSquash : MonoBehaviour {
    public Rigidbody2D rb;
    public float bobAmp = 0.06f;
    public float bobFreq = 10f;
    public float squashAmp = 0.06f;
    public Transform visual; 

    float baseY;

    void Start(){ if (!rb) rb = GetComponent<Rigidbody2D>(); baseY = visual.localPosition.y; }

    void Update(){
        float speed = Mathf.Abs(rb.linearVelocity.x);
        float groundedFactor = 1f; 
        float t = Time.time * bobFreq;

        
        float bob = Mathf.Sin(t) * bobAmp * Mathf.Clamp01(speed / 6f) * groundedFactor;
        var lp = visual.localPosition; lp.y = baseY + bob; visual.localPosition = lp;

        
        float squash = Mathf.Sin(t) * squashAmp * Mathf.Clamp01(speed / 6f) * groundedFactor;
        visual.localScale = new Vector3(1f + squash, 1f - squash, 1f);
    }
}
