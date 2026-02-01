using UnityEngine;

public class cloudscript : MonoBehaviour
{
    public float moveIntervalSeconds = 10f;
    public float moveDistanceX = 5f;
    private float timeSinceLastMove;
    private float limit = 6f; //max allowed to move left (only moving left)
    private float limitAmt = 0f;
    private bool reverse = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeSinceLastMove = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastMove += Time.deltaTime;
        if (timeSinceLastMove >= moveIntervalSeconds)
        {
            
            if((limitAmt < limit)&&!reverse){
                limitAmt+=moveDistanceX;
                transform.position += new Vector3(moveDistanceX, 0f, 0f);
                timeSinceLastMove = 0f;}
            if (reverse && (limitAmt != limit)){ //go all the way back
                limitAmt-=moveDistanceX;
                transform.position += new Vector3(moveDistanceX, 0f, 0f);
                timeSinceLastMove = 0f;
            }
            else{
                reverse = true;
            }
        }
    }
}
