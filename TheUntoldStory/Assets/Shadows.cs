using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    // Start is called before the first frame update

    public static Shadows me;
    public GameObject player;
    public List<GameObject> pool = new List<GameObject>();
    public float cronometro;
    public float speed;
    public Color _color;

    private void Awake()
    {
        me = this;
    }

    public GameObject GetShadows()
    {
        for (int i =0; i <pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {

                pool[i].SetActive(true);
                pool[i].transform.position = transform.position;
                pool[i].transform.rotation = transform.rotation;
                pool[i].transform.localScale = transform.localScale;
                pool[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                pool[i].GetComponent<Solid>()._color = _color;
                return pool[i];
            }
        }
        GameObject obj = Instantiate(player, transform.position, transform.rotation) as GameObject;
        obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<Solid>()._color = _color;
        pool.Add(obj);
        return obj;
    }
    public void PlayerSkill()
    {
        cronometro += speed * Time.deltaTime;
        if (cronometro > 1)
        {
            GetShadows();
            cronometro = 0;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
