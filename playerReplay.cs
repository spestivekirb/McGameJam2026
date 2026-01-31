using System.Collections.Generic;
using UnityEngine;

public class playerReplay : MonoBehaviour
{
    public struct InputFrame
    {
        public float time;
        public float horizontal;
        public bool jump; 
    }

    [SerializeField] private float maxRecordSeconds = 10f;
    private readonly List<InputFrame> frames = new List<InputFrame>(2048);
    public IReadOnlyList<InputFrame> Frames => frames;

    private bool jumpQueued;


    public void QueueJump() => jumpQueued = true;



    private void FixedUpdate()
    {
        // get what player did in this frame
        frames.Add(new InputFrame
        {
            time = Time.time,
            horizontal = Input.GetAxisRaw("Horizontal"),
            jump = jumpQueued
        });

        // jump happened, anything past max record seconds is shaved
        jumpQueued = false; 

        float cutoff = Time.time - maxRecordSeconds;
        int removeCount = 0;
        while (removeCount < frames.Count && frames[removeCount].time < cutoff)
            removeCount++;
        if (removeCount > 0) frames.RemoveRange(0, removeCount);
    }

    // get player input from a certain time
    public bool TryGetFrame(float targetTime, out InputFrame frame)
    {
        frame = default;

        // if nothing happened, doesn't do anything
        if (frames.Count == 0) return false;

        // show the frames to the shadow in opposite order, find the most recent one
        for (int i = frames.Count - 1; i >= 0; i--)
        {
            if (frames[i].time <= targetTime)
            {
                frame = frames[i];
                return true;
            }
        }

        // can't go back that far, oldest frame is the first one
        frame = frames[0];
        return true;
    }
}
