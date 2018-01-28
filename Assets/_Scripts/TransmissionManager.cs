using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionManager : MonoBehaviour
{
    [SerializeField]
    private PlayerWorld[] playerWorlds;

    [SerializeField]
    private GameObject transmissionVFX;

    [SerializeField]
    private float transmissionTime = 1f;

    private class TransmissionVFXData
    {
        public GameObject vfxGameObject;
        public Vector3 startPosition;
        public Vector3 endPosition;
        public float lerpTimer;
    }

    private List<TransmissionVFXData> movingTransmissionVFXs = new List<TransmissionVFXData>();
    private List<int> finishedTransmissions = new List<int>();

    private const float LERP_THRESHOLD = 0.1f;
	
	void Update ()
    {
        if(movingTransmissionVFXs != null && movingTransmissionVFXs.Count > 0)
        {
            finishedTransmissions.Clear();
            for (int i = 0; i < movingTransmissionVFXs.Count; i++)
            {
                movingTransmissionVFXs[i].lerpTimer += Time.deltaTime;
                Vector3 newPosition = Vector3.Lerp(movingTransmissionVFXs[i].startPosition, movingTransmissionVFXs[i].endPosition, movingTransmissionVFXs[i].lerpTimer / transmissionTime);
                movingTransmissionVFXs[i].vfxGameObject.transform.position = newPosition;

                if(movingTransmissionVFXs[i].lerpTimer > transmissionTime)
                {
                    finishedTransmissions.Add(i);
                }
            }

            foreach(int toRemoveIndex in finishedTransmissions)
            {
                Destroy(movingTransmissionVFXs[toRemoveIndex].vfxGameObject);
                movingTransmissionVFXs.RemoveAt(toRemoveIndex);
            }
        }

        for (int i = 0; i < playerWorlds.Length; i++)
        {
            if(playerWorlds[i].enemiesToTransfer.Count > 0)
            {
                foreach(EnemyBase enemy in playerWorlds[i].enemiesToTransfer)
                {
                    GameObject transmission = Instantiate(transmissionVFX, enemy.transform.position, Quaternion.identity);
                    TransmissionVFXData transmissionData = new TransmissionVFXData();
                    transmissionData.vfxGameObject = transmission;
                    transmissionData.startPosition = enemy.transform.position;

                    int nextPlayerWorld = (i + 1) % playerWorlds.Length;
                    playerWorlds[nextPlayerWorld].RecieveEnemy(enemy.gameObject, transmissionTime);

                    transmissionData.endPosition = enemy.transform.position;
                    transmissionData.lerpTimer = 0;

                    movingTransmissionVFXs.Add(transmissionData);
                }
            }

            playerWorlds[i].enemiesToTransfer.Clear();
        }
	}


}
