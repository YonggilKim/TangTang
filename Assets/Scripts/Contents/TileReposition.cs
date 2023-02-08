using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileReposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = Managers.Game.Player.gameObject.transform.position;
        Vector3 tilePos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - tilePos.x);
        float diffY = Mathf.Abs(playerPos.y - tilePos.y);


        float dirX = Managers.Game.Player.RigidBody.velocity.normalized.x > 0 ? 1 :  -1;
        float dirY = Managers.Game.Player.RigidBody.velocity.normalized.y > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 50 * 2);
                }

                if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 50 * 2);
                }
                break;
            case "Enemy":
                break;
        }

    }
}
