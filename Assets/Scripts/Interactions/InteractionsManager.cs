using TMPro;
using UnityEngine;

[RequireComponent(typeof(FPSGeneral))]
public class InteractionsManager : MonoBehaviour
{
    [Header("Events")]
    public VoidEvent OnGunPurchased;
    public VoidEvent OnGunCouldNotPurchase;
    
    [Header("General")]
    [SerializeField] TextMeshProUGUI interactionsText;
    FPSGeneral player;
    [SerializeField] PlayerScore playerScore;
    Transform playerCamTransform;
    InputMaster inputs;

    [Range(1, 25)]
    [SerializeField] float interactionDistance = 5;
    FPSGeneral.GunType wallGunLookingAt;
    int price;
    bool newInfo;

    Vector3 rayDirection; // Instance variable so that it isn't created again every iteration of the update loop

    void Awake()
    {
        inputs = new InputMaster();
        inputs.Game.Interact.performed += ctx => Interact();

        player = GetComponent<FPSGeneral>();
        playerCamTransform = player.GetPlayerCamera().transform;
    }

    private void OnEnable() { inputs.Game.Interact.Enable(); }    
    private void OnDisable() { inputs.Game.Interact.Disable(); }

    void Update()
    {
        rayDirection = playerCamTransform.forward;
        if (Physics.Raycast(playerCamTransform.position, rayDirection, out RaycastHit hit, interactionDistance))
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
        {
            if (playerScore.GetCurrentScore() >= price)
            {
                player.PurchaseGun(wallGunLookingAt, price);
                OnGunPurchased?.Invoke();
            }
            else
            {
                OnGunCouldNotPurchase?.Invoke();
            }
        }
    }
}
