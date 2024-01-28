using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum SpellType
{
    Instant = 0,
    Slowburn = 1
}

public class SpellOnPaper : MonoBehaviour
{
    [SerializeField] private SpellType spellType;
    [TextArea(3,10)]
    [SerializeField] private string message;
    [SerializeField] private TextMeshPro messageDisplay;
    [SerializeField] private Spell spell;
    private SpellCast spellCaster;
    [Space(20)]
    [SerializeField] private GameObject highlighter;
    [SerializeField] private GameObject[] castedOnceObjectsInstant;
    [SerializeField] private GameObject[] castedOnceObjectsSlowBurn;
    bool castedOnce;

    private void Start()
    {
        castedOnce = false;
    }

    public void OnEnable()
    {
        messageDisplay.text = message;

        if (castedOnce)
        {
            if (spellType == SpellType.Instant)
            {
                for (int i = 0; i < castedOnceObjectsInstant.Length; i++)
                {
                    castedOnceObjectsInstant[i].SetActive(true);
                }
            }
            if (spellType == SpellType.Slowburn)
            {
                for (int i = 0; i < castedOnceObjectsSlowBurn.Length; i++)
                {
                    castedOnceObjectsSlowBurn[i].SetActive(true);
                }
            }
        }
    }

    public void Select()
    {
        highlighter?.SetActive(true);
    }

    public void Deselect()
    {
        highlighter?.SetActive(false);
    }

    public void Activate()
    {
        castedOnce = true;
        //spellCaster.SpellCasted(this);
        //gameObject.SetActive(false);
        Spell castedSpell = Instantiate(spell);
        castedSpell.Setup(spellCaster);
    }

    public void SetSpellCaster(SpellCast spellCast)
    {
        spellCaster = spellCast;
    }
}
