using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;              // unidades por segundo
    public float turnSpeedDegPerSec = 180f;   // graus por segundo (enquanto segura W/S)

    [Header("Turn Limit")]
    public float maxTurnDegEachSide = 90f;   // limite: -180 a +180 a partir do ângulo inicial

    [Header("Sprite Forward Axis")]
    public bool forwardIsUp = false;           // marque se o "nariz" do sprite aponta para cima

    Rigidbody2D rb;

    float initialAngleDeg;
    float relativeTurnDeg; // varia de -maxTurn a +maxTurn

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialAngleDeg = rb.rotation; // ângulo no começo do jogo
        relativeTurnDeg = 0f;
    }

    void FixedUpdate()
    {
        // ====== 1) Movimento (D = frente, A = trás) ======
        float move = 0f;
        if (Input.GetKey(KeyCode.D)) move += 1f;
        if (Input.GetKey(KeyCode.A)) move -= 1f;

        Vector2 forward = forwardIsUp ? (Vector2)transform.up : (Vector2)transform.right;
        Vector2 newPos = rb.position + forward * (moveSpeed * move * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // ====== 2) Giro (W = horário, S = anti-horário) ======
        float turnInput = 0f;
        if (Input.GetKey(KeyCode.W)) turnInput += 1f;  // horário
        if (Input.GetKey(KeyCode.S)) turnInput -= 1f;  // anti-horário

        relativeTurnDeg += turnInput * turnSpeedDegPerSec * Time.fixedDeltaTime;
        relativeTurnDeg = Mathf.Clamp(relativeTurnDeg, -maxTurnDegEachSide, maxTurnDegEachSide);

        float targetAngle = initialAngleDeg + relativeTurnDeg;
        rb.MoveRotation(targetAngle);
    }
}
