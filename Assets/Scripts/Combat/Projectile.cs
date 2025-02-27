using UnityEngine;
using RPG.Resources;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] private UnityEvent onHit;

    [SerializeField] private float speed = 1f;
    [SerializeField] private bool isHoming = false;

    private Health target = null;
    private GameObject instigator = null;

    private float damage = 0;
    private float timer;

    private void Start()
    {
        transform.LookAt(target.transform.position + Vector3.up);
    }

    private void Update()
    {
        if (target == null)
            return;

        if (isHoming && !target.IsDead())
        {
            transform.LookAt(target.transform.position + Vector3.up); //Sumo 1 porque el modelo mide 2 (RichAI)
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer > 3f)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Health target, GameObject instigator, float damage)
    {
        this.target = target;
        this.damage = damage;
        this.instigator = instigator;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) 
            return;

        if (target.IsDead())
            return;

        target.TakeDamage(instigator, damage);
        onHit.Invoke();
        GetComponentInChildren<TrailRenderer>().enabled = false;
        Invoke("DestroyProjectile", 0.5f);
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    //Comentario para agregar en un futuro, para que el enemigo no tengo punteria maxima podemos hacer que
    //el mismo tenga una posibilidad de sacar da�o, entonces ahi esta la Missfire chance
}
