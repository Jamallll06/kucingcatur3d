using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class Tile : MonoBehaviour
{

    [Header("Grid Data")]

    public Vector2Int GridPosition
    {
        get;
        private set;
    }


    public bool IsOccupied
    {
        get;
        set;
    }


    public bool IsBlocked
    {
        get;
        private set;
    }



    [Header("Telegraph")]

    [SerializeField]
    private Material telegraphMaterial;



    private MeshRenderer meshRenderer;


    private Material defaultMaterial;



    //--------------------------------------------------

    private void Awake()
    {
        meshRenderer =
            GetComponent<MeshRenderer>();
    }



    //--------------------------------------------------

    public void Initialize(
        Vector2Int position,
        Material baseMaterial)
    {

        GridPosition =
            position;


        defaultMaterial =
            baseMaterial;



        if (meshRenderer != null)
        {
            meshRenderer.material =
                baseMaterial;
        }



        gameObject.name =
            $"Tile ({position.x},{position.y})";

    }



    //--------------------------------------------------
    // MATERIAL
    //--------------------------------------------------

    public void SetMaterial(
        Material material)
    {

        if (meshRenderer == null)
            return;


        meshRenderer.material =
            material;

    }




    //--------------------------------------------------
    // RESET
    //--------------------------------------------------

    public void ResetTile()
    {

        if (meshRenderer == null)
            return;


        meshRenderer.material =
            defaultMaterial;

    }



    //--------------------------------------------------
    // BOSS TELEGRAPH
    //--------------------------------------------------

    public void ShowTelegraph(
        bool active)
    {

        if (meshRenderer == null)
            return;



        if (active)
        {

            if (telegraphMaterial != null)
            {
                meshRenderer.material =
                    telegraphMaterial;
            }

        }
        else
        {

            meshRenderer.material =
                defaultMaterial;

        }

    }




    //--------------------------------------------------
    // BLOCK SYSTEM
    //--------------------------------------------------

    public void SetBlocked(
        bool value)
    {

        IsBlocked =
            value;

    }




    //--------------------------------------------------
    // CLICK
    //--------------------------------------------------

    private void OnMouseDown()
    {

        if (GridManager.Instance != null)
        {

            GridManager.Instance
                .SelectTile(this);

        }

    }


}