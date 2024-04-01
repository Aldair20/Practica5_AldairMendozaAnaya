using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userWeapon : MonoBehaviour
{
    private CharacterController userCharacter;
    private Vector3 initialPosition;
    public Vector3 userDirection;
    public float userSpeed;
    private Transform cameraTransform;

    //Para interacción del usuario
    public Transform bulletStart;
    public GameObject bullet;
    public float bulletForce;

    // Start is called before the first frame update
    void Start()
    {
        userCharacter = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // Guardar la posición inicial en Y
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //para el movimiento
        Vector2 userControl = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float cameraRotation = cameraTransform.eulerAngles.y;
        Vector3 camerarotation = Quaternion.Euler(new Vector3(0, 90, 0)) * cameraTransform.forward;
        userDirection = (camerarotation * Input.GetAxis("Horizontal") + cameraTransform.forward * Input.GetAxis("Vertical")).normalized;

        // Aplicar movimiento
        userCharacter.Move(userDirection * Time.deltaTime * userSpeed);

        // Restaurar la posición en Y
        Vector3 currentPosition = transform.position;
        currentPosition.y = initialPosition.y; // Mantener la misma altura
        transform.position = currentPosition;

        if(Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, bulletStart.transform.position, bulletStart.rotation);
            Rigidbody bulletforce = newBullet.GetComponent<Rigidbody>();
            bulletforce.AddForce(bulletStart.forward * Time.deltaTime * bulletForce, ForceMode.Impulse);
        }
    }

    // Called when the controller hits a collider while performing a Move
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Limite"))
        {
            // Si el collider con el que colisionamos tiene la etiqueta "Limite", revertimos el movimiento
            userCharacter.Move(-userDirection * Time.deltaTime * userSpeed);
        }
    }
}
