using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : MonoBehaviour
{
    [SerializeField] private SpellOnPaper[] allSpells;
    [SerializeField] private Transform[] slots;
    [SerializeField] private List<SpellOnPaper> readySpells;
    [SerializeField] private Animator animator;
    [SerializeField] private int availableSpells = 3;
    [SerializeField] private float positionSpellsSpeed = 1f;
    [SerializeField] private AudioClip castAudio;

    private int spellSelected = 0;
    [SerializeField] private float selectionTick = 1;
    private float selectionTickTimer = 0;
    [SerializeField] private float newSpellTick = 5;
    private float newSpellTickTimer = 0;

    private bool canCast;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < allSpells.Length; i++)
        {
            allSpells[i].SetSpellCaster(this);
            allSpells[i].Deselect();
            allSpells[i].gameObject.SetActive(false);
        }

        readySpells.Clear();
        LoadSpell(true);
        canCast = true;

        spellSelected = 0;
        readySpells[spellSelected]?.Select();
        selectionTickTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PositionSpellsOnPaper();
        TickSpellSelection();
        TickNewSpell();

        if (Input.GetButtonDown("Cast"))
        {
            Cast(spellSelected);
        }
    }

    private void PositionSpellsOnPaper()
    {
        for (int i = 0; i < readySpells.Count; i++)
        {
            readySpells[i].transform.position = Vector3.Lerp(readySpells[i].transform.position, slots[i].position, Time.deltaTime * positionSpellsSpeed);
            readySpells[i].transform.rotation = Quaternion.Lerp(readySpells[i].transform.rotation, slots[i].rotation, Time.deltaTime * positionSpellsSpeed);
        }
    }

    private void TickNewSpell()
    {
        if (readySpells.Count >= availableSpells)
        {
            newSpellTickTimer = 0;
        }
        newSpellTickTimer += Time.deltaTime;
        if (newSpellTickTimer >= newSpellTick)
        {
            newSpellTickTimer = 0;
            LoadSpell();
        }
    }

    private void TickSpellSelection()
    {
        selectionTickTimer += Time.deltaTime;
        if (selectionTickTimer >= selectionTick && canCast)
        {
            selectionTickTimer = 0;
            SelectNextSpell();
        }
    }

    private void SelectNextSpell()
    {
        if (readySpells.Count <= 0) return;

        readySpells[spellSelected]?.Deselect();

        spellSelected++;
        spellSelected %= readySpells.Count;

        readySpells[spellSelected]?.Select();

        if (spellSelected == 0 && readySpells.Count <= 1) animator.SetTrigger("SpellSoloSelect");
        else if (spellSelected == 0) animator.SetTrigger("Spell1Select");
        else if (spellSelected == 1) animator.SetTrigger("Spell2Select");
        else animator.SetTrigger("Spell3Select");

        AudioSource.PlayClipAtPoint(castAudio, transform.position);
    }

    private void SelectInstance()
    {
        if (readySpells.Count <= 0) return;
        spellSelected %= readySpells.Count;

        readySpells[spellSelected].Select();
    }

    private void LoadSpell(bool fullDeck = false)
    {
        SpellOnPaper newSpell = null;
        do
        {
            int r = Random.Range(0, allSpells.Length);
            newSpell = allSpells[r];
            if (readySpells.Contains(newSpell))
            {
                newSpell = null;
            }
        } while (newSpell == null);

        readySpells.Add(newSpell);
        readySpells[readySpells.Count - 1].gameObject.SetActive(true);

        Debug.Log("Spell: " + newSpell.name);

        //recursive
        if (readySpells.Count < availableSpells && fullDeck)
        {
            LoadSpell(true);
        }
    }

    public void Cast(int index)
    {
        if (index < readySpells.Count)
        {
            readySpells[index].Activate();
            animator.SetTrigger("Cast");
            canCast = false;
        }
    }

    public void AnimationEnd()
    {
        //Debug.Log("Index: " + spellSelected);
        readySpells[spellSelected].gameObject.SetActive(false);
        readySpells.RemoveAt(spellSelected);

        SelectInstance();

        canCast = true;
    }
}
