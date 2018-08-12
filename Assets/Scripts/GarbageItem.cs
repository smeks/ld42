using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageItem : MonoBehaviour
{
    public string Name;
    public float Health = 100;
    public float LastDamage = 1f;
    public GameObject ItemInfoPrefab;
    public GameObject ItemInfo;

    private int _movingDoorLayer = 11;
    private float _startHealth;
    private Transform _garbageContainer;
    
    private Renderer _renderer;
    private ParticleSystem _particleSystem;
    private Color _originalColor;
    private Text _itemInfoText;

    // Use this for initialization
    void Start ()
    {
        _renderer = GetComponent<Renderer>();
        _particleSystem = GetComponent<ParticleSystem>();
        _garbageContainer = transform.parent.transform;
        
        _startHealth = Health;

        // create health bar
        ItemInfo = Instantiate(ItemInfoPrefab, GameObject.Find("GameUI").transform, false);
        _itemInfoText = ItemInfo.GetComponentInChildren<Text>();

        _itemInfoText.text = Name;
        _originalColor = _itemInfoText.color;
    }

    public void Highlight(Color color)
    {
        _itemInfoText.color = color;
        _itemInfoText.text = $"{Name} - Press E to Pickup";
    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        _itemInfoText.color = _originalColor;
        _itemInfoText.text = Name;
    }

	// Update is called once per frame
	void LateUpdate ()
	{
	    ItemInfo.SetActive(_renderer.isVisible);
	    ItemInfo.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        if (Health < 0)
	    {
	        _particleSystem.Play(false);
            Destroy(gameObject, 0.25f);
            Destroy(ItemInfo, 0.25f);
        }
        else
        {
            ItemInfo.GetComponent<Slider>().value = Health / _startHealth;
        }
	}

    public void Pickup(Transform newParent)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        transform.parent = newParent;
    }

    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
        transform.parent = _garbageContainer;
    }

    void OnCollisionStay(Collision collision)
    {
        var coliderLayer = collision.collider.gameObject.layer & _movingDoorLayer;

        if (coliderLayer != _movingDoorLayer) return;

        Health -= collision.relativeVelocity.magnitude;

        LastDamage = collision.relativeVelocity.magnitude;
    }
}
