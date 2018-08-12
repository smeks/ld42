using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Slider HealthMeter;
    public float Health = 100;
    public GameObject ItemSlot;
    private GarbageItem _pickedUpItem = null;

    void Awake()
    {
        Instance = this;
    }

    public void Tick()
    {
        HealthMeter.value = Health / 100f;

        if (_pickedUpItem && Input.GetKeyUp(KeyCode.E))
        {
            _pickedUpItem.Drop();
            _pickedUpItem = null;

            return;
        }

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;


        if (Physics.Raycast(ray, out hitInfo))
        {
            if (_pickedUpItem != null)
                return;

            if (hitInfo.distance > 5.0f || hitInfo.collider.gameObject.layer != LayerMask.NameToLayer("GarbageItem"))
                return;

            var item = hitInfo.collider.GetComponent<GarbageItem>();
            item.Highlight(Color.green);

            if (Input.GetKeyUp(KeyCode.E) && item != _pickedUpItem)
            {
                _pickedUpItem = item;
                item.Pickup(ItemSlot.transform);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer != LayerMask.NameToLayer("MovingWall"))
            return;

        Health -= collision.relativeVelocity.magnitude;
    }
}
