using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement _characterMovement;
    [SerializeField]
    private CharacterAttack _characterAttack;
    [SerializeField]
    private CircleCollider2D _interactCircle;

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

        if (_characterAttack.IsAttacking)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        var direction = new Vector2(horizontal, vertical).normalized;

        _characterMovement.Move(direction);
    }
}
