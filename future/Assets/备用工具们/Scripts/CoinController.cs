using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{//�Խ�ҵ������ӣ����ѽ�ҽ��и���
    public static CoinController instance;

    public void Awake()
    {
        instance = this;
    }
   
   public int currentCoins;

   public CoinPickup coin;

   public void AddCoins(int coinsToAdd)
   {
        currentCoins += coinsToAdd;
        UIController.instance.UpdateCoins();

       // SFXManager.instance.PlaySFXPitched(2);
   }

   public void DropCoin(Vector3 position, int value)
   {
        //��������Ż�Ϊ����������Լ������ҵ�λ��
        CoinPickup newCoin = Instantiate(coin, position + new Vector3(.2f,.1f,0f),Quaternion.identity);
        newCoin.coinAmount = value;
        newCoin.gameObject.SetActive(true);
   }

   public void SpendCoins(int coinToSpend)
   {
     currentCoins -= coinToSpend;

     UIController.instance.UpdateCoins();
   }
}
