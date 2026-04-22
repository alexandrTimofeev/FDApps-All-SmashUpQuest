using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator LayoutAnimator;
    
    [SerializeField] private GameObject progressBarObject;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private GameObject restartMenuObject;
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject winObject;
    [SerializeField] private GameObject loseObject;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Image barFillImage;
    private BallShooter ballShooter;
    private Platform platform;
    private bool sceneChanged;

    private void Start()
    {
        ballShooter = FindObjectOfType<BallShooter>();
        ballShooter.OnBulletFinish += CheckFailOrWinScreen;
        platform = FindObjectOfType<Platform>();
        platform.OnBoxCountFinish += ActivateWin;
        platform.OnBoxCountFinish += FillAmount;

        //FindFirstObjectByType<RestartMenu>().FreezeScene();
    }
    
    private void CheckFailOrWinScreen() 
    {
        var boxCountOnPlatform = platform.CalculateBoxCountOnPlatform();
        if(boxCountOnPlatform == 0) 
        {
            ActivateWin();
        }
        else  
        {
            DOVirtual.DelayedCall(1f, () =>
            {
                if (platform.CalculateBoxCountOnPlatform() > 0)
                {
                    ActivateFail();
                }
            });
        }
    }

    public void LayoutSettingsOpen()
    {
        LayoutAnimator.SetTrigger("slide_in");
    }

    public void LayoutSettingsClose()
    {
        LayoutAnimator.SetTrigger("slide_out");
    }

    public void TapToButton()
    {
        mainMenuObject.SetActive(false);

        progressBarObject.SetActive(true);
        bulletObject.SetActive(true);
        restartMenuObject.SetActive(true);
        GameObject levelnumber1 = GameObject.Find("FirstLevel");
        GameObject levelnumber2 = GameObject.Find("SecondLevel");
        // Проверяем, найден ли объект
        if (levelnumber1 != null)
        {
            levelnumber1.GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().buildIndex.ToString();
            levelnumber2.GetComponent<TextMeshProUGUI>().text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
            // Теперь можно работать с найденным объектом
        }
    }

    private void ActivateWin() 
    {
        if (sceneChanged) return;
        sceneChanged = true;
        
        winObject.SetActive(true);
        //DOVirtual.DelayedCall(2f, NextLevel);
    }

    private void ActivateFail() 
    {
        if (sceneChanged) return;
        sceneChanged = true;
        
        loseObject.SetActive(true);
        //DOVirtual.DelayedCall(2f, RestartLevel);
    }

    private void FillAmount() 
    {
        barFillImage.fillAmount += 2;
    }
    public void SaveScene()
    {
        // Получаем активную сцену
        Scene currentScene = SceneManager.GetActiveScene();
        
        // Сохраняем её название в PlayerPrefs
        PlayerPrefs.SetInt("SavedScene", currentScene.buildIndex+1);
        
        // Сохраняем PlayerPrefs
        PlayerPrefs.Save();
        
        Debug.Log("Сцена сохранена: " + currentScene.name);
    }
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        SaveScene();
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("Loading next scene: " + nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0); 
            Debug.Log("Starting from the beginning.");
        }
    }
    
    public void SceneChanger(int numScene)
    {
        SceneManager.LoadScene(numScene);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("restart");
    }
}


    