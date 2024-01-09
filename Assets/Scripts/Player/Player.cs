using EnumDef;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{        
    public enum ModelType
    {
        Character,
        Ship
    }

    MoveDirection mMoveHorizon = MoveDirection.None;
    MoveDirection mMoveVeritcal = MoveDirection.None;

    [Header("캐릭터 이동 속도")]
    public float kSpeed = 1f;

    Rigidbody2D mRigidbody;
    SpriteRenderer mSpriteRenderer;
    Animator mAnimator;
    
    GameObject mModel;
    public GameObject kShadowGo;

    public bool isCanMove { get; set; }
    public bool isPortalTransit { get; set; }

    ModelType mModelType = ModelType.Character;

    void Awake()
    {
        mModel = transform.Find("Model").gameObject;
        mRigidbody = GetComponent<Rigidbody2D>();

        mSpriteRenderer = mModel.GetComponent<SpriteRenderer>();
        mAnimator = mModel.GetComponent<Animator>();

        isPortalTransit = false;
        isCanMove = true;
    }

    private void Update()
    {
        InputUpdate();
        AnimationUpdate();        
    }
    private void FixedUpdate()
    {
        MoveUpdate();
    }

    void InputUpdate()    
    {
        mMoveHorizon = MoveDirection.None;
        mMoveVeritcal = MoveDirection.None;

        if (isCanMove == false)
            return;

        if (Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true){
            mMoveHorizon = MoveDirection.Left;            
        }
        else if(Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true){
            mMoveHorizon = MoveDirection.Right;
        }

        
        if (Input.GetKey(KeyCode.UpArrow) == true || Input.GetKey(KeyCode.W) == true){
            mMoveVeritcal = MoveDirection.Up;
        }
        else if (Input.GetKey(KeyCode.DownArrow) == true || Input.GetKey(KeyCode.S) == true){
            mMoveVeritcal = MoveDirection.Down;
        }
    }

    void MoveUpdate()
    {
        if (mMoveHorizon == MoveDirection.None && mMoveVeritcal == MoveDirection.None)
            return;

        Vector2 moveHor = Vector3.zero;
        Vector2 moveVer = Vector3.zero;

        if (mMoveHorizon == MoveDirection.Left)
            moveHor = Vector2.left * kSpeed;
        if (mMoveHorizon == MoveDirection.Right)
            moveHor = Vector2.right * kSpeed;

        if(mMoveVeritcal == MoveDirection.Up)
            moveVer = Vector2.up * kSpeed;
        if (mMoveVeritcal == MoveDirection.Down)
            moveVer = Vector2.down * kSpeed;

        Vector2 move = (moveHor + moveVer).normalized;
        mRigidbody.MovePosition(mRigidbody.position + (move * kSpeed * Time.deltaTime) );
    }

    void AnimationUpdate()
    {
        if(mMoveHorizon == MoveDirection.Left)
            mSpriteRenderer.flipX = false;
        if (mMoveHorizon == MoveDirection.Right)
            mSpriteRenderer.flipX = true;

        if (mMoveHorizon != MoveDirection.None || mMoveVeritcal != MoveDirection.None)
        {
            switch(mModelType)
            {
                case ModelType.Character:
                    mAnimator.Play("Character Run");
                    break;
                case ModelType.Ship:
                    mAnimator.Play("Ship Run");
                    break;
            }            
        }
        else
        {
            switch (mModelType)
            {
                case ModelType.Character:
                    mAnimator.Play("Character Stand");
                    break;
                case ModelType.Ship:
                    mAnimator.Play("Ship Stand");
                    break;
            }
        }   
    }

    public void ChangeModel(ModelType _type)
    {
        mModelType = _type;

        switch (mModelType)
        {
            case ModelType.Character:
                mAnimator.Play("Character Stand");
                kShadowGo.SetActive(true);
                break;
            case ModelType.Ship:
                mAnimator.Play("Ship Stand");
                kShadowGo.SetActive(false);
                break;
        }
    }
}
