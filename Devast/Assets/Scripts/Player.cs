using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.5f;
    public float acceleration = 100;

    public float health = 100;
    public float energy = 100;
    public float hunger = 0;
    public float radiation = 0;
    public float experience = 0;

    public float energyDecreaseRate = 10f;
    public float energyRestoreRate = 10f;

    private Vector3 direction;
    private Rigidbody2D body;
    private bool isRunning = false;

    void Start()
    {
        SettingUp();
    }

    private void SettingUp()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        body.gravityScale = 0;
    }

    void FixedUpdate()
    {
        Debug.Log(energy);
        RadiuscalCulating();
        Move();
    }

    private void RadiuscalCulating()
    {
        if (Mathf.Abs(body.velocity.x) > speed)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
        }

        if (Mathf.Abs(body.velocity.y) > speed)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed);
        }
    }

    private void Move()
    {
        body.AddForce(direction * body.mass * speed * acceleration);
    }

    void LookAtCursor()
    {
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        Run();
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        LookAtCursor();

        if (Input.GetButtonDown("Run"))
        {
            isRunning = true;
            energy -= energyDecreaseRate * Time.deltaTime;
        }
        else if (Input.GetButtonUp("Run"))
        {
            isRunning = false;
            energy += energyRestoreRate * Time.deltaTime;
        }

        // Ограничение количества энергии от 0 до 100
        energy = Mathf.Clamp(energy, 0f, 100f);
    }

    public void Run()
    {
        if (energy > 0 && isRunning)
        {
            speed = 2f;
            energy -= energyDecreaseRate * Time.deltaTime;
        }
        else
        {
            speed = 1.5f;
            energy += energyRestoreRate * Time.deltaTime;
        }

        if (energy <= 0)
        {
            isRunning = false;
        }

        // Ограничение количества энергии от 0 до 100
        energy = Mathf.Clamp(energy, 0f, 100f);
    }
}
