using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private Rigidbody _charRb;
    [SerializeField]
    private PlayerController _playerController;

    public void PointerDown()
    {
        _playerController.enabled = true;
        _uiManager.holdStartText.SetActive(false);
        _charRb.useGravity = true;
        Destroy(this);
    }
}
