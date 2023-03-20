using static Petwork.Additions.Tools;
using UnityEngine;


//Simple scenemanager I needed real quick
public class SceneMNGR : MonoBehaviour
{
    public virtual void sceneRedirect(string scene)
    {
        ChangeScene(scene.ToString());
    }

}
