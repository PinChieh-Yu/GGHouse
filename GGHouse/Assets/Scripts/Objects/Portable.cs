using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectInfo), typeof(Anchor))]
public class Portable : MonoBehaviour
{
    private ObjectInfo objectInfo;
    private Anchor anchor;

    [SerializeField]
    private CharacterRequest requiredCharacters;

    private List<CharacterIdentity> preparedPlayers;

    public bool IsReadyToMove { get { return IsFulfillRequest(); } }

    void Awake()
    {
        preparedPlayers = new List<CharacterIdentity>();
    }

    void Start()
    {
        objectInfo = GetComponent<ObjectInfo>();
        anchor = GetComponent<Anchor>();
    }

    public void PickUp(CharacterIdentity identity, Transform transform)
    {
        if (IsFulfillRequest()) return;
        preparedPlayers.Add(identity);
        Debug.Log(objectInfo.Id.ToString() + "'s prepared:" + IsFulfillRequest().ToString());
        if (preparedPlayers.Count == 1)
        {
            anchor.SetAnchor(transform);
        }
        else if (preparedPlayers.Count > 1)
        {
            GameManager.instance.GetCharacterTransform(identity).GetComponent<PlayerStatus>().SetAnchor(anchor.target);
        }
    }

    public void PutDown(CharacterIdentity identity)
    {
        objectInfo.IsInteractable = true;
        if (preparedPlayers.Contains(identity))
        {
            preparedPlayers.Remove(identity);
            GameManager.instance.GetCharacterTransform(identity).GetComponent<PlayerStatus>().ResetAnchor();
        }

        if (preparedPlayers.Count > 0)
        {
            Debug.Log("PutDown, remain:" + preparedPlayers[0].ToString());
            GameManager.instance.GetCharacterTransform(preparedPlayers[0]).GetComponent<PlayerStatus>().ResetAnchor();
            anchor.SetAnchor(GameManager.instance.GetCharacterTransform(preparedPlayers[0]));
        }
        else
        {
            anchor.ResetAnchor();
        }
    }

    private bool IsFulfillRequest()
    {
        if (requiredCharacters == CharacterRequest.Both && preparedPlayers.Count == 2)
        {
            return true;
        }
        else if (requiredCharacters == CharacterRequest.Single && preparedPlayers.Count == 1)
        {
            return true;
        }
        else if (requiredCharacters == CharacterRequest.Junior && preparedPlayers.Contains(CharacterIdentity.Junior))
        {
            return true;
        }
        else if (requiredCharacters == CharacterRequest.Senior && preparedPlayers.Contains(CharacterIdentity.Senior))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
