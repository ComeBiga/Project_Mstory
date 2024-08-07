using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _goIdle;
    [SerializeField]
    private GameObject _goWalk;    
    [SerializeField]
    private GameObject _goHit;    
    [SerializeField]
    private GameObject _goAttack;

    public void Rotate(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);
    }

    public void Idle()
    {
        if (_goIdle.activeSelf)
            return;

        setAnimation(0);
    }

    public void Walk()
    {
        if (_goWalk.activeSelf)
            return;

        setAnimation(1);
    }

    public void Hit()
    {
        setAnimation(2);
    }

    public void Attack()
    {
        setAnimation(3);
    }

    private void setAnimation(int index)
    {
        _goIdle.SetActive(false);
        _goWalk.SetActive(false);
        _goHit.SetActive(false);
        _goAttack.SetActive(false);

        switch(index)
        {
            case 0:
                _goIdle.SetActive(true);
                break;
            case 1:
                _goWalk.SetActive(true);
                break; 
            case 2:
                _goHit.SetActive(true);
                break; 
            case 3:
                _goAttack.SetActive(true);
                break;
            default:
                break;
        }
    }
}
