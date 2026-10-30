using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FloatBob : MonoBehaviour {
    public float amplitude = 0.15f;   
    public float frequency = 2.5f;    
    public float fadeAmplitude = 0.12f;

    float baseY;
    SpriteRenderer sr;

    void Awake(){ baseY = transform.localPosition.y; sr = GetComponent<SpriteRenderer>(); }

    void Update(){
        float t = Time.time;
        
        float y = baseY + Mathf.Sin(t * frequency) * amplitude;
        var p = transform.localPosition; p.y = y; transform.localPosition = p;

        
        if (sr){
            var c = sr.color;
            c.a = 0.85f + Mathf.Sin(t * (frequency * 0.5f)) * fadeAmplitude;
            sr.color = c;
        }
    }
}
