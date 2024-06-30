using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum EDirection { Front, Back, Left, Right };

    private EDirection mDirection = EDirection.Front;
    public EDirection Direction => mDirection;

    public int AttackPower => attackPower;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [Header("AttackBox")]
    [SerializeField]
    private BoxCollider2D boxCollider_FrontAttack;
    [SerializeField]
    private BoxCollider2D boxCollider_BackAttack;
    [SerializeField]
    private BoxCollider2D boxCollider_LeftAttack;
    [SerializeField]
    private BoxCollider2D boxCollider_RightAttack;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private int attackPower = 4;

    [Header("HP")]
    [SerializeField]
    private int maxHP = 0;
    [SerializeField]
    private int currentHP = 0;
    [SerializeField]
    private HpBar hpBar;

    [Header("EXP")]
    [SerializeField]
    private int maxExp = 10;
    [SerializeField]
    private int currentExp = 0;
    [SerializeField]
    private Slider _sliderEXP;
    [SerializeField]
    private TextMeshProUGUI _txtLevel;
    private int mLevel = 1;

    private List<Quest> mQuests = new List<Quest>(100);

    private PlayerMoveState mCurrentState;
    public PlayerMoveIdleState MoveIdleState { get; private set; }
    public PlayerMoveUpState MoveUpState { get; private set; }
    public PlayerMoveDownState MoveDownState { get; private set; }
    public PlayerMoveLeftState MoveLeftState { get; private set; }
    public PlayerMoveRightState MoveRightState { get; private set; }

    public void AddEXP(int amount)
    {
        currentExp += amount;

        if (currentExp >= maxExp)
        {
            currentExp = 0;
            maxExp += (int)(maxExp / 2f);
            ++mLevel;

            _txtLevel.text = $"Lv {mLevel}";
        }

        _sliderEXP.value = (float)currentExp / maxExp;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        hpBar.Set(currentHP);

        Debug.Log($"Player is attacked!(currentHP : {currentHP}");
    }

    public void AddQuest(Quest quest)
    {
        mQuests.Add(quest);
    }

    public void UpdateQuest()
    {
        if(mQuests.Count < 1)
            return;

        Quest targetQuest = mQuests[0];
        targetQuest.AddAmount(1);
    }

    public void SetDirection(EDirection direction)
    {
        mDirection = direction;
    }

    public void ChangeState(PlayerMoveState state)
    {
        state.OnExitState();

        mCurrentState = state;
        mCurrentState.OnEnterState();
    }

    private void Start()
    {
        currentHP = maxHP;

        hpBar.Init(maxHP);
        hpBar.Set(currentHP);

        _sliderEXP.value = (float)currentExp / maxExp;
        _txtLevel.text = $"Lv {mLevel}";

        MoveIdleState = new PlayerMoveIdleState(this, animator, null);
        MoveUpState = new PlayerMoveUpState(this, animator, boxCollider_BackAttack);
        MoveDownState = new PlayerMoveDownState(this, animator, boxCollider_FrontAttack);
        MoveLeftState = new PlayerMoveLeftState(this, animator, boxCollider_LeftAttack);
        MoveRightState = new PlayerMoveRightState(this, animator, boxCollider_RightAttack);

        ChangeState(MoveIdleState);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            var filter = new ContactFilter2D();
            filter.useTriggers = true;

            var results = new List<Collider2D>();
            int count = boxCollider.OverlapCollider(filter, results);

            foreach (Collider2D collider in results)
            {
                if (collider.tag == "NPC")
                {
                    NPC targetNPC = collider.gameObject.GetComponent<NPC>();
                    targetNPC.InteractTo(this);

                    // Debug.Log($"{collider.gameObject.name}");
                    break;
                }
            }

            mCurrentState.Attack();
            //UpdateAttack();

            //animator.SetTrigger("Attack");
        }

        UpdateMovement();
    }

    private void UpdateMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        var deltaDirection = new Vector3(horizontal, vertical);

        transform.position += moveSpeed * deltaDirection * Time.deltaTime;

        //animator.SetFloat("Horizontal", horizontal);
        //animator.SetFloat("Vertical", vertical);

        //if (Input.GetButton("Horizontal") && horizontal > 0.001f)
        //{
        //    mDirection = EDirection.Right;
        //    animator.SetFloat("hDirection", 1f);
        //    animator.SetFloat("vDirection", 0);
        //}
        //if (Input.GetButton("Horizontal") && horizontal < -0.001f)
        //{
        //    mDirection = EDirection.Left;
        //    animator.SetFloat("hDirection", -1f);
        //    animator.SetFloat("vDirection", 0);
        //}
        //if (Input.GetButton("Vertical") && vertical > 0.001f)
        //{
        //    mDirection = EDirection.Back;
        //    animator.SetFloat("hDirection", 0);
        //    animator.SetFloat("vDirection", 1f);
        //}
        //if (Input.GetButton("Vertical") && vertical < -0.001f)
        //{
        //    mDirection = EDirection.Front;
        //    animator.SetFloat("hDirection", 0);
        //    animator.SetFloat("vDirection", -1f);
        //}

        mCurrentState.OnUpdate(deltaDirection);
    }

    private void UpdateAttack()
    {
        BoxCollider2D boxCollider = null;

        switch(mDirection)
        {
            case EDirection.Front:
                boxCollider = boxCollider_FrontAttack;
                break;
            case EDirection.Back:
                boxCollider = boxCollider_BackAttack;
                break;
            case EDirection.Left:
                boxCollider = boxCollider_LeftAttack;
                break;
            case EDirection.Right:
                boxCollider = boxCollider_RightAttack;
                break;
            default:
#if UNITY_EDITOR
                Debug.LogError($"Wrong direction enum type![type:{mDirection}]");
#endif
                break;
        }

        var filter = new ContactFilter2D();
        filter.useTriggers = true;

        var results = new List<Collider2D>();
        int count = boxCollider.OverlapCollider(filter, results);

        foreach (Collider2D collider in results)
        {
            if (collider.tag == "Monster")
            {
                Monster targetMonster = collider.gameObject.GetComponent<Monster>();
                targetMonster.Damage(attackPower, 
                                    onDied : () =>
                                    {
                                        UpdateQuest();
                                        AddEXP(3);
                                    });

                // Debug.Log($"{mDirection} Attack to '{collider.gameObject.name}'");
                break;
            }
        }
    }
}
