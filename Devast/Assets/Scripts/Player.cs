using UnityEngine;
using System.Collections;
using System.Globalization;
//using Mirror;
using static UnityEngine.RuleTile.TilingRuleOutput;

//[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{

    public float speed = 1.5f;
    public float acceleration = 100;

    private Vector3 direction;
    private Rigidbody2D body;

    void Start()
    {
        SettingUp();
    }

    private void SettingUp()
    {
        //if (!isLocalPlayer) return;
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        body.gravityScale = 0;
    }

    void FixedUpdate()
    {
        //if (!isLocalPlayer) return;
        body.AddForce(direction * body.mass * speed * acceleration);

        if (Mathf.Abs(body.velocity.x) > speed)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
        }

        if (Mathf.Abs(body.velocity.y) > speed)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed);
        }
    }

    void LookAtCursor()
    {
        //if (!isLocalPlayer) return;
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        //if (!isLocalPlayer) return;
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        LookAtCursor();

    }
}