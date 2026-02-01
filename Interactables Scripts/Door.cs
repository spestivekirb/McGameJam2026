using UnityEngine;


public class Door : MonoBehaviour, IActivatable
{

    public float speed;
    public float openHeight;

    private Vector3 closedPos;
    private Vector3 openPos;
    private Vector3 curPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + new Vector3(0, openHeight, 0);

        curPos = closedPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != curPos)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, curPos, speed * Time.deltaTime
            );
        }
    }

    public void Activate()
    {
        curPos = openPos;
    }

    public void Deactivate()
    {
        curPos = closedPos;
    }
}
