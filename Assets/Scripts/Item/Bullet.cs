using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 40f;
    public float lifetime = 3f;

    private float timer = 0f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > lifetime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) // destroy on collision
    {
        Destroy(gameObject);
    }
}