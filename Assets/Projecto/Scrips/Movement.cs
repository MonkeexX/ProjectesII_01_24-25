using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveTime = 0.15f;
    private float xInput, yInput;
    private bool isMoving;
    private Vector2 targetPosition;
    private CollisionBoxRobot collisionBoxRobot;
    private CollisionLitelBot collisionSmallRobot;
    private CollisionExplosiveRobot collisionExplosiveRobot;
    private CollisionLaserRobot collisionLaserRobot;
    private CollisionCloneRobot collisionCloneRobot;

    public bool explosive = false;
    public bool clone = false;
    public bool small = false;
    public bool laser = false;
    public bool box = false;

    void Start()
    {
        collisionBoxRobot = GetComponent<CollisionBoxRobot>(); // Obtiene el script CollisionBoxRobot adjunto al mismo GameObject
        collisionSmallRobot = GetComponent<CollisionLitelBot>(); // Obtiene el script CollisionBoxRobot adjunto al mismo GameObject
        collisionExplosiveRobot = GetComponent<CollisionExplosiveRobot>();
        collisionLaserRobot = GetComponent<CollisionLaserRobot>();
        collisionCloneRobot = GetComponent<CollisionCloneRobot>();
        this.enabled = false; // Deshabilitamos este script si es necesario al inicio
    }

    void Update()
    {
        // Obtenemos la entrada del jugador
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // Verificamos si el jugador est� intentando moverse
        if ((xInput != 0f || yInput != 0f) && !isMoving && Input.anyKeyDown)
        {
            CalculateTargetPosition();

            // Verificamos si la posici�n de destino est� libre de colisiones
            if (!collisionBoxRobot.BoxRobotCollision(targetPosition)) // Solo permitimos el movimiento si no hay colisiones
            {
                StartCoroutine(Move());
            }
            
            
        }
    }

    IEnumerator Move()
    {
        isMoving = true;
        float timePassed = 0f;
        Vector2 startPosition = transform.position;

        while (timePassed < moveTime)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, timePassed / moveTime);
            timePassed += Time.deltaTime;
            yield return null;
        }


        isMoving = false;
    }

    private void CalculateTargetPosition()
    {
        if (xInput == 1f)
        {
            targetPosition = (Vector2)transform.position + Vector2.right;
        }
        else if (xInput == -1f)
        {
            targetPosition = (Vector2)transform.position + Vector2.left;
        }
        if (yInput == 1f)
        {
            targetPosition = (Vector2)transform.position + Vector2.up;
        }
        else if (yInput == -1f)
        {
            targetPosition = (Vector2)transform.position + Vector2.down;
        }
    }

    // Esto se usa para mostrar el c�rculo en el inspector
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosition, 0.15f);
    }
}