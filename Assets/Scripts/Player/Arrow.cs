using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    private void Update()
    {
        GameObject[] sigils = GameObject.FindGameObjectsWithTag("Sigil");
        List<GameObject> incompleteSigils = new List<GameObject>();
        foreach (GameObject sigil in sigils)
        {
            if (!sigil.GetComponent<Activate>().getCompleted()) { 
                incompleteSigils.Add(sigil);
            }
        }
        GameObject closestSigil = null;
        if (incompleteSigils.Count > 0 )
        {
            closestSigil = incompleteSigils[0];
            float minDistance = 0f;
            foreach (GameObject sigil in incompleteSigils)
            {
                Vector2 sigilPos = sigil.transform.position;
                Vector2 currentPos = sigil.transform.position;
                float distance = (sigilPos - currentPos).magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestSigil = sigil;
                }
            }
        }
        if (closestSigil != null)
        {
            LookAtSigil(closestSigil);
        }
    }

    private void LookAtSigil(GameObject sigil)
    {
        Vector2 targetPos = new Vector2(sigil.transform.position.x, sigil.transform.position.y);
        Vector2 currentPos = transform.position;
        var direction = targetPos - currentPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
