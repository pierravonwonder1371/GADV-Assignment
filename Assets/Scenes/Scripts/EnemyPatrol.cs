//using UnityEngine;

//public class EnemyPatrol : MonoBehaviour
//{
//    [Header ("Patrol Points")]
//    [SerializeField] private Transform leftEdge;
//    [SerializeField] private Transform rightEdge;

//    [Header("Enemy")]
//    [SerializeField] private Transform enemy;

//    [Header("Movement parameters")]
//    [SerializeField] private float speed;
//    private Vector3 initScale;
//    private bool movingLeft;

//    private void Awake()
//    {
//        initScale = enemy.localScale;
//    }
//    private void Update()
//    {
//        if (movingLeft)
//        {
//            if (enemy.position.x >= leftEdge.position.x)
//            {
//                MoveInDirection(-1);
//            }
//            else
//            {
//                DirectionChange();
//            }
//        }
//        else
//        {
//            if (enemy.position.x <= rightEdge.position.x)
//            {
//                MoveInDirection(1);
//            }
//            else
//            {
//                DirectionChange();
//            }
//        }
//    }

//    private void DirectionChange()
//    {
//        movingLeft = !movingLeft;
//    }

//    private void MoveInDirection(int direction)
//    {
//        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);
//        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
//    }
//}

using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Vector3 initScale;
    private bool movingLeft = false;

    private void Awake()
    {
        initScale = transform.localScale;
    }

    private void Update()
    {
        int direction = movingLeft ? -1 : 1;
        transform.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Reverse direction if the collided object is NOT the player
        if (!collision.CompareTag("Player"))
        {
            movingLeft = !movingLeft;
        }
    }
}
