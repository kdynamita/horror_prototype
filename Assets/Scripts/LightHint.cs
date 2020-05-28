using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHint : MonoBehaviour
{
    #region Variable

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        if (transform.parent.GetSiblingIndex() != 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == EntityManager.Instance.GetPlayer())
        {
            if (transform.parent.GetSiblingIndex() + 1 < transform.root.childCount)
            {
                transform.root.GetChild(transform.parent.GetSiblingIndex() + 1).gameObject.SetActive(true);
            }
            else
            {
                Destroy(transform.root.gameObject);
            }

            transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnValidate()
    {
        transform.parent.name = "Hint: " + transform.parent.GetSiblingIndex();
    }

    #endregion

    #region Custom Function

    #endregion
}
