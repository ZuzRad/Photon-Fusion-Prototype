using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMessegesHandler : MonoBehaviour
{
    public TextMeshProUGUI[] rpcMessages;
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
            rpcMessages[queueIndex].text = messageInQue;
            queueIndex++;
        }
    }

    public void OnPingReceived(int index, double ping)
    {
      //  table.SetPing(index, ping);
    }
}
