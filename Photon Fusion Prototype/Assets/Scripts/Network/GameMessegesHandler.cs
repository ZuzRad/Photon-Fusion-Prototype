using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMessegesHandler : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshes;
   // [SerializeField] TableUI table;
    Queue messageQueue = new Queue();

    public void OnGameMessegeReceived(string messege)
    {
        Debug.Log(messege);

        messageQueue.Enqueue(messege);
        if(messageQueue.Count > 3)
        {
            messageQueue.Dequeue();
        }

        int queueIndex = 0;
        foreach(string messageInQue in messageQueue)
        {
            Debug.Log(messageInQue);
            textMeshes[queueIndex].text = messageInQue;
            queueIndex++;
        }
    }


    public void OnSetPointsReceived(int index)
    {
       // PlayersManager.Instance.gamePointsList[index]++;
    }

    public void OnReadyStateReceived(int index, bool isReady)
    {
      //  table.SetReady(index, isReady);
    }

    public void OnPingReceived(int index, double ping)
    {
      //  table.SetPing(index, ping);
    }
}
