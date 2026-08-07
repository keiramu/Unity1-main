using UnityEngine;

public class DestroyInTime : MonoBehaviour

{
    public float destroyTime = 0.0166f;
    float endTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endTime = Time.time + destroyTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > endTime)
        {
            Destroy(gameObject);        
        }

    }
}
