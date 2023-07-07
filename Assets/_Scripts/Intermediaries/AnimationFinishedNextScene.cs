using MrLule.Managers.SceneTransitionMan;
using UnityEngine;

public class AnimationFinishedNextScene : MonoBehaviour
{
    public void AnimationFinished()
    {
        FindObjectOfType<SceneTransitionManager>().NextSceneDirectly();
    }
}
