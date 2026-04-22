using UnityEngine;
using UnityEngine.SceneManagement;

public class SccenesManager : MonoBehaviour
{
   public void SceneChanger(int numScene)
   {
      SceneManager.LoadScene(numScene);
   }

   public void ReloadScene()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
