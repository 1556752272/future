using JKFrame;
using UnityEngine;
using UnityEngine.UI;
[UIElement(true, "UI/GameMainWindow", 1)]
public class UI_GameMainWindow : UI_WindowBase
{
    [SerializeField] private Text Score_Text;
    [SerializeField] private Image HPBar_Fill_Image;
    [SerializeField] private Image ShieldsBar_Fill_Image;
    //[SerializeField] private Text BulletNum_Text;
    private const string LocalSetPackName = "UI_GameMainWindow";
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        EventManager.AddEventListener<int>("UpdateHP", UpdateHP);
        EventManager.AddEventListener<int>("UpdateShield", UpdateShields);
        EventManager.AddEventListener<int>("UpdateScore", UpdateScore);
        //EventManager.AddEventListener<int,int>("UpdateBullet", UpdateBullet);
    }

    protected override void CancelEventListener()
    {
        base.CancelEventListener();
        EventManager.RemoveEventListener<int>("UpdateHP", UpdateHP);
        EventManager.RemoveEventListener<int>("UpdateScore", UpdateScore);
        //EventManager.RemoveEventListener<int, int>("UpdateBullet", UpdateBullet);
    }

    private void UpdateHP(int hp)
    {
        HPBar_Fill_Image.fillAmount = (float)((float)hp / (float)Player_Controller.Instance.HPmaxx());
    }
    private void UpdateShields(int shield)
    {
        ShieldsBar_Fill_Image.fillAmount = (float)((float)shield / (float)Player_Controller.Instance.shieldmaxx());
    }
    private void UpdateScore(int num)
    {
        Score_Text.text = num.ToString();
    }
    private void UpdateBullet(int curr,int max)
    {
       // BulletNum_Text.text = curr + "/" + max;
    }
}
