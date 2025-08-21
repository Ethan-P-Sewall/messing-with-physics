using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour
{
    public static List<cube> existingCubes;

    public static Transform lastClickedOn;

    public Rigidbody RB { get; private set; }

    [SerializeField] float debrisLevel; bool alreadyExploded = false;
    [SerializeField] GameObject debris;
    [SerializeField] float debrisOffset; float failsafe = 0;

    public static void EnablePhysics()
    {
        foreach (cube foo in existingCubes)
        {
            foo.RB.constraints = RigidbodyConstraints.None;
        }
    }
    public static void DisablePhysics()
    {
        foreach (cube foo in existingCubes)
        {
            foo.RB.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void Start()
    {
        RB = GetComponent<Rigidbody>();

        if (existingCubes == null)
        {
            existingCubes = new List<cube>();
        }
        existingCubes.Add(this);
    }

    void Update()
    {
        failsafe += Time.deltaTime;
    }

    void OnMouseUpAsButton()
    {
        lastClickedOn = transform;
        controllableCharacter.TargetBlock();
    }

    public void GetExploded(Vector3 v, float force, float rad)
    {
        if (!alreadyExploded)
        {
            float proximity = ((Vector3.Distance(v, transform.position)) / rad);

            switch (debrisLevel)
            {
                case 0:
                    {
                        if (proximity < 1.0f && failsafe > 0.25f)
                        {
                            alreadyExploded = true;
                            Vector3 foo = new Vector3(-debrisOffset, -debrisOffset, -debrisOffset);
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = debrisOffset; foo.x = -debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = foo.x = -debrisOffset; foo.y = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = debrisOffset; foo.x = -debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad);
                            Destroy(gameObject);
                        }
                        else
                        {
                            if (!RB)
                            {
                                RB = GetComponent<Rigidbody>();
                            }
                            RB.AddExplosionForce(force, v, rad);
                        }
                    }
                    break;
                case 1:
                    {
                        if (proximity < 0.6f && failsafe > 0.25f)
                        {
                            alreadyExploded = true;
                            Vector3 foo = new Vector3(-debrisOffset, -debrisOffset, -debrisOffset);
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = debrisOffset; foo.x = -debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = foo.x = -debrisOffset; foo.y = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = debrisOffset; foo.x = -debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad);
                            Destroy(gameObject);
                        }
                        else
                        {
                            if (!RB)
                            {
                                RB = GetComponent<Rigidbody>();
                            }
                            RB.AddExplosionForce(force * 2, v, rad);
                        }
                    }
                    break;
                case 2:
                    {
                        if (proximity < 0.4f && failsafe > 0.25f)
                        {
                            alreadyExploded = true;
                            Vector3 foo = new Vector3(-debrisOffset, -debrisOffset, -debrisOffset);
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = debrisOffset; foo.x = -debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = foo.x = -debrisOffset; foo.y = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = debrisOffset; foo.x = -debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad); foo.z = foo.x = debrisOffset;
                            Instantiate(debris, transform.position + foo, transform.rotation).GetComponent<cube>().GetExploded(v, force, rad);
                            Destroy(gameObject);
                        }
                        else
                        {
                            if (!RB)
                            {
                                RB = GetComponent<Rigidbody>();
                            }
                            RB.AddExplosionForce(force, v, rad);
                        }
                    }
                    break;
                case 3:
                    {
                        if (proximity < 0.3f)
                        {
                            Destroy(gameObject);
                        }
                        else
                        {
                            if (!RB)
                            {
                                RB = GetComponent<Rigidbody>();
                            }
                            RB.AddExplosionForce(force * 2, v, rad);
                        }
                    }
                    break;
                default:
                    if (!RB)
                    {
                        RB = GetComponent<Rigidbody>();
                    }
                    RB.AddExplosionForce(force, v, rad);
                    break;
            }
        }
    }

    //[ContextMenu("snap")]
    public void Snap()
    {
        Vector3 v = transform.position;
        v = new Vector3(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        transform.position = v;
    }
}
