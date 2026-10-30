using UnityEngine;

[System.Serializable]
public class SfxSlot {
    public AudioClip clip;
    [Range(0f,1f)] public float volume = 1f;
    [Range(0f,0.3f)] public float pitchJitter = 0.06f;
}

[RequireComponent(typeof(AudioSource))]
public class AudioHub : MonoBehaviour {
    public static AudioHub I;

    [Header("Music")]
    public AudioClip ambientLoop;
    [Range(0f,1f)] public float musicVolume = 0.35f;

    [Header("SFX (simple fields, for older calls)")]
    public AudioClip hitSfx;
    public AudioClip winSfx;
    public AudioClip heartSfx;
    public AudioClip[] ghostWhooshesClips;

    [Header("SFX (slot style, with per-effect volume)")]
    public SfxSlot hitSlot;
    public SfxSlot winSlot;
    public SfxSlot heartSlot;
    public SfxSlot[] ghostWhooshes;

    AudioSource musicSrc;   // looped music
    AudioSource sfxSrc;     // one-shot SFX

    void Awake(){
        if (I && I != this) { Destroy(gameObject); return; }
        I = this;

        musicSrc = GetComponent<AudioSource>();
        musicSrc.loop = true;
        musicSrc.playOnAwake = false;

        sfxSrc = gameObject.AddComponent<AudioSource>();
        sfxSrc.playOnAwake = false;
    }

    void Start(){
        if (ambientLoop){
            musicSrc.clip = ambientLoop;
            musicSrc.volume = musicVolume;
            musicSrc.Play();
        }
    }

    // --- New API (slot-based) ---
    public void Play(SfxSlot slot){
        if (slot == null || slot.clip == null) return;
        sfxSrc.pitch = 1f + Random.Range(-slot.pitchJitter, slot.pitchJitter);
        sfxSrc.PlayOneShot(slot.clip, slot.volume);
    }

    public void PlayRandomGhost(){
        // prefer slot array if set
        if (ghostWhooshes != null && ghostWhooshes.Length > 0){
            Play(ghostWhooshes[Random.Range(0, ghostWhooshes.Length)]);
            return;
        }
        // fallback to simple clips list
        if (ghostWhooshesClips != null && ghostWhooshesClips.Length > 0){
            PlaySfx(ghostWhooshesClips[Random.Range(0, ghostWhooshesClips.Length)], 0.6f);
        }
    }

    public void StopMusic(){ if (musicSrc.isPlaying) musicSrc.Stop(); }

    // --- Legacy API (kept so existing calls compile) ---
    public void PlaySfx(AudioClip clip, float volume = 1f, float pitchJitter = 0.06f){
        if (!clip) return;
        sfxSrc.pitch = 1f + Random.Range(-pitchJitter, pitchJitter);
        sfxSrc.PlayOneShot(clip, volume);
    }
}
