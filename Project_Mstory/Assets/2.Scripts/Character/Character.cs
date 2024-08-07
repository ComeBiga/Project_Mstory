using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int _maxExp;

    private int mLevel = 1;
    private CharacterExperience mExperience;

    // Start is called before the first frame update
    void Start()
    {
        mExperience = new CharacterExperience();
        mExperience.Init(_maxExp);
        mExperience.onFull += () =>
        {
            ++mLevel;
            mExperience.Init(_maxExp + (int)(_maxExp * .3f));
        };
    }
}
