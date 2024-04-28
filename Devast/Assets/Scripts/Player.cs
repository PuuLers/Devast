using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.5f;
    public float acceleration = 100;

    public int health = 100;
    public int energy = 100;
    public int hunger = 0;
    public int radiation = 0;
    public int experience = 0;

    private float energyDecreaseRate = 1.0f;
    private float energyRestoreRate = 0.5f;

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
        body.AddForce(direction * body.mass * speed * acceleration);

        if (Mathf.Abs(body.velocity.x) > speed)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
        }

        if (Mathf.Abs(body.velocity.y) > speed)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed);
        }

        //// Уменьшаем энергию со временем
        //if (energy > 0)
        //{
        //    energy -= Mathf.RoundToInt(energyDecreaseRate * Time.deltaTime);
        //}

        //// Восстанавливаем энергию со временем
        //if (energy < 100)
        //{
        //    energy += Mathf.RoundToInt(energyRestoreRate * Time.deltaTime);
        //}
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
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        LookAtCursor();
        if (Input.GetButtonDown("Run"))
        {
            Run(true);
        }
        else if (Input.GetButtonUp("Run"))
        {
            Run(false);
        }

    }

    public void Run(bool Active)
    {
        if (energy > 0 && Active)
        {
            speed *= 1.5f; // увеличиваем скорость бега на 1.5 раза
            energy -= Mathf.RoundToInt(energyDecreaseRate * Time.deltaTime);
        }
        else
        {
            speed /= 1.5f; // если энергии недостаточно для бега, возвращаем базовую скорость
            energy += Mathf.RoundToInt(energyRestoreRate * Time.deltaTime);
        }
    }

}
