using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool Controlling => mbControlling;

    [SerializeField]
    private CharacterMovement _characterMovement;
    [SerializeField]
    private CharacterAttack _characterAttack;
    [SerializeField]
    private CircleCollider2D _interactCircle;

    private bool mbControlling = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            var filter = new ContactFilter2D();
            filter.useTriggers = true;

            var results = new List<Collider2D>();
            int count = _interactCircle.OverlapCollider(filter, results);

            foreach (Collider2D collider in results)
            {
                if (collider.tag == "NPC")
                {
                    NPC targetNPC = collider.gameObject.GetComponent<NPC>();
                    targetNPC.Interact();

                    // Debug.Log($"{collider.gameObject.name}");
                    return;
                }
            }

            _characterAttack.Attack();
        }

        //if (_characterAttack.IsAttacking)
        //    return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        var direction = new Vector2(horizontal, vertical).normalized;

        if(direction.magnitude >= .001f)
        {
            _characterAttack.StopAttack();
            _characterMovement.Move(direction);
            mbControlling = true;
        }
        else if(mbControlling && direction.magnitude < .001f)
        {
            _characterMovement.Move(Vector2.zero);
            mbControlling = false;
        }
        else
        {
            _characterAttack.Attack();
        }
    }
}
