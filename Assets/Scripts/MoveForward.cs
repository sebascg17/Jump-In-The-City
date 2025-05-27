using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float speed = 2f; // Speed of the object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
