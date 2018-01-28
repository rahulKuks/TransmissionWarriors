using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionManager : MonoBehaviour
{
    [SerializeField]
    private PlayerWorld[] playerWorlds;

    [SerializeField]
    private GameObject transmissionVFX;

    private class TransmissionVFXData
    {
        public GameObject vfxGameObject;
        public Vector3 startPosition;
        public Vector3 endPosition;
    }

    private List<TransmissionVFXData> movingTransmissionVFXs = new List<TransmissionVFXData>();
    private List<int> finishedTransmissions = new List<int>();

    private const float LERP_THRESHOLD = 0.1f;
	
	void Update ()
    {
        /*if(movingTransmissionVFXs != null && movingTransmissionVFXs.Count > 0)
        {
            finishedTransmissions.Clear();
            for (int i = 0; i < movingTransmissionVFXs.Count; i++)
            {
                Vector3 newPosition = Vector3.Lerp(movingTransmissionVFXs[i].startPosition, movingTransmissionVFXs[i].endPosition, Time.deltaTime);
                movingTransmissionVFXs[i].vfxGameObject.transform.position = newPosition;

                if( (movingTransmissionVFXs[i].vfxGameObject.transform.position - movingTransmissionVFXs[i].endPosition).magnitude < LERP_THRESHOLD)
                {
                    finishedTransmissions.Add(i);
                }
            }

            foreach(int toRemoveIndex in finishedTransmissions)
            {
                Destroy(movingTransmissionVFXs[toRemoveIndex].vfxGameObject);
                movingTransmissionVFXs.RemoveAt(toRemoveIndex);
            }
        }*/

        for (int i = 0; i < playerWorlds.Length; i++)
        {
            if(playerWorlds[i].enemiesToTransfer.Count > 0)
            {
                foreach(EnemyBase enemy in playerWorlds[i].enemiesToTransfer)
                {
                    /*GameObject transmission = Instantiate(transmissionVFX, enemy.transform.position, Quaternion.identity);
                    TransmissionVFXData transmissionData = new TransmissionVFXData();
                    transmissionData.vfxGameObject = transmission;
                    transmissionData.startPosition = enemy.transform.position;*/

                    int nextPlayerWorld = (i + 1) % playerWorlds.Length;
                    playerWorlds[nextPlayerWorld].RecieveEnemy(enemy.gameObject);

                    //transmissionData.endPosition = enemy.transform.position;

                    //movingTransmissionVFXs.Add(transmissionData);
                }
            }

            playerWorlds[i].enemiesToTransfer.Clear();
        }
	}


}
