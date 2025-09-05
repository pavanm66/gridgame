using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothness = 1f;
    Vector3 offset;
    Vector3 startPosition;

    private void Awake()
    {
        playerTransform = FindObjectOfType<Player>().transform;
    }
    void Start()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        offset = transform.position - playerTransform.position;
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, offset + playerTransform.position, Time.deltaTime * 1.5f);
    }
    public void Replay()
    {
        transform.position = startPosition;
    }
}
