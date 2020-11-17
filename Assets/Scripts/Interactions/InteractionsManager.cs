using TMPro;
using UnityEngine;

[RequireComponent(typeof(FPSGeneral))]
public class InteractionsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionsText;
    FPSGeneral player;
    Transform playerCamTransform;
    InputMaster inputs;

    [Range(1, 25)]
    [SerializeField] float interactionDistance = 5;
    FPSGeneral.GunType wallGunLookingAt;
    float price;
    bool newInfo;

    void Awake()
    {
        inputs = new InputMaster();
        inputs.Game.Interact.performed += ctx => Interact();

        player = GetComponent<FPSGeneral>();
        playerCamTransform = player.GetPlayerCamera().transform;
    }

    private void OnEnable() { inputs.Game.Interact.Enable(); }    
    private void OnDisable() { inputs.Game.Interact.Disable(); }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayDirection = playerCamTransform.forward;
        RaycastHit hit;
        if (Physics.Raycast(playerCamTransform.position, rayDirection, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("WallGun"))
                HitWallGun(hit);
            else
                HitNothing();
        }
        else
            HitNothing();
    }

    void HitNothing()
    {
        newInfo = (wallGunLookingAt != FPSGeneral.GunType.NONE);
        wallGunLookingAt = FPSGeneral.GunType.NONE;
        if (newInfo)
            interactionsText.text = "";
    }

    void HitWallGun(RaycastHit hit)
    {
        WallGun hitWallGun = hit.collider.GetComponent<WallGun>();
        newInfo = (wallGunLookingAt != hitWallGun.GetGunType() || price != hitWallGun.GetGunPrice());
        wallGunLookingAt = hitWallGun.GetGunType();
        price = hitWallGun.GetGunPrice();
        if (newInfo)
            interactionsText.text = "Purchase " + wallGunLookingAt + " for " + price;
    }

    void Interact()
    {
        if (wallGunLookingAt != FPSGeneral.GunType.NONE)
            player.PurchaseGun(wallGunLookingAt, price);
    }
}
