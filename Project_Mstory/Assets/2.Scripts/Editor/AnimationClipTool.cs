using UnityEditor;
using UnityEngine;

public class AnimationClipTool : EditorWindow
{
    public static AnimationClipTool window;

    private AnimationClip mClip;
    private int mFrameInterval = 20;
    private ObjectReferenceKeyframe[] mKeyframes;

    private Vector2 mFrameScrollPos;

    private const int DEFAULT_FRAME = 60;

    [MenuItem("Tools/AnimationClipTool")]
    public static void CreateWindow()
    {
        window = GetWindow<AnimationClipTool>();
        window.Show();
    }

    private void OnGUI()
    {
        mClip = EditorGUILayout.ObjectField(new GUIContent("Target Clip"), mClip, typeof(AnimationClip), false) as AnimationClip;

        if (mClip == null)
            return;

        if(GUI.changed)
        {
            mKeyframes = AnimationUtility.GetObjectReferenceCurve(mClip, EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"));

            Debug.Log($"Clip Changed!");
        }

        mFrameInterval = EditorGUILayout.IntField("Interval", mFrameInterval);

        // ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(mClip, EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"));

        if(mKeyframes == null || mKeyframes.Length == 0)
        {
            if(GUILayout.Button("추가"))
            {
                mKeyframes = new ObjectReferenceKeyframe[1];
                AnimationUtility.SetObjectReferenceCurve(mClip, EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"), mKeyframes);
            }

            return;
        }

        //float timeInterval = keyframes[1].time - keyframes[0].time;

        //int frameInterval = (int)(keyframes[1].time * DEFAULT_FRAME);

        mFrameScrollPos = EditorGUILayout.BeginScrollView(mFrameScrollPos, GUI.skin.box);
        {
            EditorGUILayout.BeginVertical();
            {
                Rect rect = EditorGUILayout.GetControlRect();
                Vector2 pivot = rect.position;
                Vector2 size = rect.size;

                rect.width = 100f;
                rect.height = 100f;

                float space = 2f;

                EditorGUILayout.BeginHorizontal();
                {
                    for (int i = 0; i < mKeyframes.Length; ++i)
                    {
                        ObjectReferenceKeyframe keyframe = mKeyframes[i];

                        rect.y = pivot.y;
                        rect.height = 100f;

                        mKeyframes[i].value = EditorGUI.ObjectField(rect, keyframe.value, typeof(Sprite), false);

                        rect.y += rect.height + space;
                        rect.height = size.y;

                        int frameInterval = (int)(keyframe.time * DEFAULT_FRAME);
                        frameInterval = EditorGUI.IntField(rect, frameInterval);
                        mKeyframes[i].time = (float)frameInterval / DEFAULT_FRAME;

                        rect.x += rect.width + space;
                    }
                }
                EditorGUILayout.EndHorizontal();

                rect.y = pivot.y;
                rect.width = 30f;
                rect.height = 100f;

                if(GUI.Button(rect, "+"))
                {                    
                    var newKeyframes = new ObjectReferenceKeyframe[mKeyframes.Length + 1];

                    for(int i = 0; i < mKeyframes.Length; ++i)
                    {
                        newKeyframes[i] = mKeyframes[i];
                    }

                    int newFrame = TimeToFrame(newKeyframes[newKeyframes.Length - 2].time) + mFrameInterval;
                    newKeyframes[newKeyframes.Length - 1].time = FrameToTime(newFrame);

                    mKeyframes = newKeyframes;
                    AnimationUtility.SetObjectReferenceCurve(mClip, EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"), mKeyframes);
                }

                if (mKeyframes.Length <= 1)
                    GUI.enabled = false;

                rect.y += rect.height + space;
                rect.height = size.y;

                if (GUI.Button(rect, "-"))
                {
                    var newKeyframes = new ObjectReferenceKeyframe[mKeyframes.Length - 1];

                    for (int i = 0; i < newKeyframes.Length; ++i)
                    {
                        newKeyframes[i] = mKeyframes[i];
                    }

                    mKeyframes = newKeyframes;
                    AnimationUtility.SetObjectReferenceCurve(mClip, EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"), mKeyframes);
                }

                GUI.enabled = true;
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();

        if(GUILayout.Button("적용"))
        {
            AnimationUtility.SetObjectReferenceCurve(mClip, EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"), mKeyframes);
        }
    }

    private int TimeToFrame(float time)
    {
        return (int)(time * DEFAULT_FRAME);
    }

    private float FrameToTime(int frame)
    {
        return (float)frame / DEFAULT_FRAME;
    }
}
