using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Header("Core")]
    public Transform player;
    public int lives = 4;
    public bool isGameOver { get; private set; }
    float elapsed;

    [Header("Imp Spawner")]
    public GameObject impPrefab;
    public float impFirstDelay = 1.5f;
    public float impInterval   = 2.5f;
    public float impSpawnAhead = 10f;

    public float impYOffsetMin = -0.3f;
    public float impYOffsetMax =  0.8f; 

    public bool won { get; private set; } = false;


    void Start() {
        InvokeRepeating(nameof(SpawnImp), impFirstDelay, impInterval);
    }

    void Update() {
        if (!isGameOver) elapsed += Time.deltaTime;

        if (isGameOver && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public float CurrentScoreSeconds() => elapsed;

    public void HitByImp() {
        if (isGameOver) return;
        lives--;
        var fx = player.GetComponent<PlayerEffects>();
        if (fx) fx.Flash();

        AudioHub.I?.PlaySfx(AudioHub.I.hitSfx, 0.9f);


        Debug.Log($"Lives now: {lives}");
        if (lives <= 0) GameOver();
    }

    public void GameOver() {
        if (isGameOver) return;
        isGameOver = true;

        
        foreach (var e in FindObjectsByType<ImpEnemy>(FindObjectsSortMode.None)) {
            if (e) e.enabled = false;
        }
        
        var pr = player.GetComponent<PlayerRunner>(); if (pr) pr.enabled = false;
        var rb = player.GetComponent<Rigidbody2D>();  if (rb) rb.linearVelocity = Vector2.zero;
    }

    void SpawnImp() {
        if (isGameOver) return;

        if (impPrefab == null || player == null) {
            Debug.LogError("GameManager: assign player and impPrefab.");
            return;
        }

        float y = player.position.y + Random.Range(impYOffsetMin, impYOffsetMax);
        Vector3 pos = new Vector3(player.position.x + impSpawnAhead, y, 0f);

        Debug.Log($"SpawnImp -> pos:{pos}  cam:{Camera.main.transform.position}");

        var go  = Instantiate(impPrefab, pos, Quaternion.identity);
        var imp = go.GetComponent<ImpEnemy>();
        if (imp) imp.gm = this;
    }

    public void Win(){
    if (isGameOver) return;
    isGameOver = true;
    won = true;

    foreach (var e in FindObjectsByType<ImpEnemy>(FindObjectsSortMode.None))
        if (e) e.enabled = false;

    var pr = player.GetComponent<PlayerRunner>(); if (pr) pr.enabled = false;
    var rb = player.GetComponent<Rigidbody2D>();  if (rb) rb.linearVelocity = Vector2.zero;

    AudioHub.I?.PlaySfx(AudioHub.I.winSfx, 1f);



    Debug.Log("YOU WON!");
    }

}
