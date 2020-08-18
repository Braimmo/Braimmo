using UnityEngine;

public class SceneManager2 : MonoBehaviour
{
    public void goCharacterScene(){            
        UnityEngine.SceneManagement.SceneManager.LoadScene("Inventory_asset");
    }

    public void goStoryMode(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("AgePickScene");
    }
}
