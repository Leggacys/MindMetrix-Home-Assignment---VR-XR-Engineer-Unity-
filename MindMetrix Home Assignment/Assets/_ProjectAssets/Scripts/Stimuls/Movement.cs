using UnityEngine;


/// <summary>
/// Stimulus that moves the GameObject to a random position within a defined radius when executed,
/// </summary>
public class Movement : StimulusBase
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float moveRadius = 3f;
    public float minDistance = 1.0f;

    private Vector3 targetPos;
    private Vector3 startCenter;

    void Awake()
    {
        targetPos = transform.position;
        startCenter = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    public override void Execute()
    {
        float x = Random.Range(-moveRadius, moveRadius);
        float y = Random.Range(-moveRadius, moveRadius);
        targetPos = startCenter + new Vector3(x, y, 0);
    }

    public override void Stop()
    {
        transform.position = startCenter;
        targetPos = startCenter;
    }
}
