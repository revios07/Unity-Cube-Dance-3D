using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class PlayerController : PlayerCollisions
{
    //For Game Started Control
    private bool _isGameStart;
    //Joint Inside This Ref
    HingeJoint[] hingeJoints = new HingeJoint[1];
    [Tooltip("Anchor Connect Pos")]
    private Vector3 connectPos;
    //For Line Renderer
    [Tooltip("Line Renderer")]
    private bool _anchorConnectLineRenderer;
    private bool _anchorConnectJoint;

    void Awake()
    {
        //Camera Load
        _cameraFollow.FollowPlayer(this.transform); 
    }

    private void Start()
    {
        //Create Level by 40 Boxes
        CreatorOfBlocks.Instance.LoadBlocksAtStartOfGame(42, _blockPrefabs, _pointPrefab, diffuculty);

    }

    private void Update()
    {
        SetScore();
    }

    //Physic System like Joints Calling in FixedUpdate
    private void FixedUpdate()
    {
        if (isPlayerUsingTork)
        {
            _meshHolderCubeTransform.Rotate(Vector3.right * 500f * Time.fixedDeltaTime);
        }
        //Touching Joint
        if (_anchorConnectJoint)
        {
            StartCoroutine(WaitForConnect(0.2f));
        }
    }
    private void LateUpdate()
    {
        LineRendererControl();
    }

    //When Click Down
    //At Game Scene Call Back Event Listener
    public void Touched()
    {
        if (!_isGameStart)
        {
            //First Anchor Except Object Pooling
            FindPosForHidgeJoint(new Vector3(0, 0, -1));
            _anchorConnectLineRenderer = true;

            _uiController.holdStartText.SetActive(false);

            _playerRigidbody.useGravity = true;
            isPlayerUsingTork = true;
            _isGameStart = true;
        }
        //Wait 0.2 Sec's For Connect Anchor
        else if (_playerRigidbody.velocity.z > 0.25f && !_anchorConnectJoint)
        {
            _anchorConnectJoint = true;
        }
    }

    //When Click Release
    public void ReleaseTouch()
    {
        //Destroy Joint
        Destroy(hingeJoints[0]);

        //Line Renderer
        _lineRenderer.SetPosition(1, Vector3.zero);
        _anchorConnectLineRenderer = false;

        //Add Force Up For Dynamic Play
        //Add Force Back For Slow Down Cube
        _playerRigidbody.AddForce(Vector3.up * 30f + Vector3.forward * -10f);
    }


    //<summary>
    //Hang To Blocks Down Pos 
    //Take Pos From Blocks[] referance
    //For Current Player Pos
    //</summary>
    public void FindPosForHidgeJoint(Vector3 blockPosition)
    {
        //For First Click Still Roping
        Destroy(hingeJoints[0]);

        //Add Hinge
        hingeJoints[0] = gameObject.AddComponent<HingeJoint>();

        //Down Of 5 unit y for; Upper Blocks Down bound
        blockPosition.y -= 5f;

        //Connector Gameobjects Transform.position = blockPosition
        _connectRBGameObject.position = blockPosition;

        //Connect Hinge Joint To HingeJointConnectorRigidbody
        hingeJoints[0].connectedBody = _connectRigidbody;

        //Motor Set Props
        var motor = hingeJoints[0].motor;
        motor.freeSpin = true;

        motor.force = 30f;
        motor.targetVelocity = 80f;

        hingeJoints[0].motor = motor;
        hingeJoints[0].useMotor = true;
    }

    //Calculating Where Player Hang At Upper
    private IEnumerator WaitForConnect(float sec = 0.2f)
    {
        //Block Pos +2 When Moving Fast
        if (_playerRigidbody.velocity.z > 12)
            connectPos = CreatorOfBlocks.Instance.PosTakeForPlayerForwardBlock((((int)transform.position.z) + 5) % 41).position;
        
        else if(_connectRigidbody.velocity.z > 8)
            connectPos = CreatorOfBlocks.Instance.PosTakeForPlayerForwardBlock((((int)transform.position.z) + 4) % 41).position;
        
        else
            connectPos = CreatorOfBlocks.Instance.PosTakeForPlayerForwardBlock((((int)transform.position.z) + 3) % 41).position;

        
        yield return new WaitForSeconds(sec);

        //Hinge Joint Create
        FindPosForHidgeJoint(connectPos);

        //Show Line Renderer
        _anchorConnectLineRenderer = true;
        _lineRenderer.positionCount = 2;

        _anchorConnectJoint = false;
    }
    private void LineRendererControl()
    {
        if (!_anchorConnectLineRenderer) 
            return;

        _lineRenderer.SetPosition(1, _connectRBGameObject.position - transform.position);
    }

    //Change Rotation Of Player For LineRenderers Pos
    private void EulerEnglesCalculatePlayer()
    {
        
    }
}
