using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    public const float baseSpeed = 3.0f;

    private bool _alive;

    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    void Start()
    {
        _alive = true;
    }

    void Awake()
    {
        EventManager.StartListening(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy()
    {
        EventManager.StopListening(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position =
                        transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-90, 90);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    private void OnSpeedChanged(float value)
    {
        Debug.Log("OnSpeedChanged enemy speed: " + value);
        speed = baseSpeed * value;
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
