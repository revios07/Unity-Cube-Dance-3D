using UnityEngine;

public class PlayerProps : MonoBehaviour
{
    [Tooltip("Player Referances Here")]

    [Header("Connect Go's")]
    [SerializeField]
    protected Transform _connectRBGameObject;
    [SerializeField]
    protected Rigidbody _connectRigidbody;
    [SerializeField]
    protected GameObject[] _blockPrefabs;
    [SerializeField]
    protected LineRenderer _lineRenderer;
    [SerializeField]
    protected Rigidbody _playerRigidbody;
    [SerializeField]
    protected CameraFollower _cameraFollow;
    [SerializeField]
    protected GameObject _pointPrefab;
    [SerializeField]
    protected Transform _meshHolderCubeTransform;

    [Header("UI")]
    [SerializeField]
    protected UIManager _uiController;

    [Header("Diffuculty Of Level Create")]
    [SerializeField]
    protected int diffuculty = 1;

    protected float score = 0;
    protected bool _isGameOver = false;
    protected bool isPlayerUsingTork;

    protected void SetScore()
    {
        if (_playerRigidbody.velocity.z > 0.0f)
            score += _playerRigidbody.velocity.z * Time.fixedDeltaTime * 0.1f;

        _uiController.realtimeScoreText.text = score.ToString("0.00");
    }
}
