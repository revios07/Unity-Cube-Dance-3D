using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform playerTransform { get; set; }
    private Vector3 offsetFromPlayer { get; set; }
    private float whenUpdateBlockPos { get; set; } = 15f;
    private bool startFollow { get; set; }

    public void FollowPlayer(Transform playerTransform)
    {
        this.playerTransform = playerTransform;

        offsetFromPlayer = playerTransform.position - transform.position;
        startFollow = true;
    }

    private void LateUpdate()
    {
        if (!startFollow) return;

        transform.position = new Vector3(transform.position.x, playerTransform.position.y - offsetFromPlayer.y, playerTransform.position.z - offsetFromPlayer.z);

        //Recalculate Position Of Player
        if (playerTransform.position.z > whenUpdateBlockPos)
        {
            whenUpdateBlockPos += 10f;
            CreatorOfBlocks.Instance.UpdatePosForFront();
        }
    }
}
