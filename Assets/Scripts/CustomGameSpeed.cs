using UnityEngine;

public class CustomGameSpeed : MonoBehaviour
{
    public static CustomGameSpeed I;          // acesso global: GameSpeed.I
    [Range(0.1f, 500f)]
    public float speedScale = 1f;       // ajuste no Inspector

    float baseFixedDeltaTime;

    void Awake()
    {
        if (I != null) { 
           Destroy(gameObject); 
           return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);

        baseFixedDeltaTime = Time.fixedDeltaTime;
        Apply();
    }

    void OnValidate()
    {
        // aplica quando você mexe no Inspector (mesmo em play)
        if (Application.isPlaying) Apply();
    }

    public void Apply()
    {
        Time.timeScale = speedScale;

        // IMPORTANTE: isso mantém a física 2D “acelerando junto” com o timeScale
        Time.fixedDeltaTime = baseFixedDeltaTime / speedScale;
    }
}
