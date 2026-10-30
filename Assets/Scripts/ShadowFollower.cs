using UnityEngine;

public class ShadowFollower : MonoBehaviour {
    public Transform player;
    public Rigidbody2D playerRb;
    public float rayDistance = 10f;   
    public float yOffset = 0.05f;     
    public float minScale = 0.8f;
    public float maxScale = 1.25f;
    public float speedForMax = 8f;
    public LayerMask groundMask;

    void LateUpdate(){
        if (!player) return;

        
        Vector3 pos = transform.position;
        pos.x = player.position.x;

        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, rayDistance, groundMask);
        if (hit.collider != null){
            pos.y = hit.point.y + yOffset;
        }

        transform.position = pos;

        
        if (playerRb){
            float speed = Mathf.Abs(playerRb.linearVelocity.x);
            float t = Mathf.Clamp01(speed / speedForMax);
            float s = Mathf.Lerp(minScale, maxScale, t);
            transform.localScale = new Vector3(s, transform.localScale.y, 1f);
        }
    }
}
