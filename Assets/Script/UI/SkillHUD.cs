using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SkillHUD : MonoBehaviour
{

    [Header("Skill UI")]
    [SerializeField]
    private Image skillIcon;


    [SerializeField]
    private TMP_Text skillNameText;


    [SerializeField]
    private TMP_Text skillCostText;


    [SerializeField]
    private TMP_Text skillDescriptionText;



    [Header("Skill Icons")]
    [SerializeField]
    private Sprite kingSkillIcon;

    [SerializeField]
    private Sprite queenSkillIcon;

    [SerializeField]
    private Sprite rookSkillIcon;

    [SerializeField]
    private Sprite bishopSkillIcon;

    [SerializeField]
    private Sprite knightSkillIcon;

    [SerializeField]
    private Sprite pawnSkillIcon;



    private TransformingPiece transformingPiece;


    private CatForm lastForm;



    private void Start()
    {

        transformingPiece =
            FindFirstObjectByType<TransformingPiece>();


        UpdateSkill();

    }





    private void Update()
    {

        if (transformingPiece == null)
        {
            transformingPiece =
                FindFirstObjectByType<TransformingPiece>();

            return;
        }


        UpdateSkill();

    }







    private void UpdateSkill()
    {

        CatForm currentForm =
            transformingPiece.CurrentForm;



        if (currentForm == lastForm)
            return;



        lastForm =
            currentForm;



        switch (currentForm)
        {


            case CatForm.King:

                SetSkill(
                    "KING SHIELD",
                    "⚡⚡",
                    "Memblokir 1 serangan Boss.",
                    kingSkillIcon
                );

                break;




            case CatForm.Queen:

                SetSkill(
                    "QUEEN LASER",
                    "⚡⚡⚡",
                    "Laser lurus dengan damage besar.",
                    queenSkillIcon
                );

                break;




            case CatForm.Rook:

                SetSkill(
                    "IRON ARMOR",
                    "⚡⚡",
                    "Mengurangi damage yang diterima.",
                    rookSkillIcon
                );

                break;




            case CatForm.Bishop:

                SetSkill(
                    "BLINK",
                    "⚡⚡",
                    "Teleport ke tile tujuan.",
                    bishopSkillIcon
                );

                break;




            case CatForm.Knight:

                SetSkill(
                    "POUNCE",
                    "⚡⚡⚡",
                    "Melompat menyerang dengan damage tinggi.",
                    knightSkillIcon
                );

                break;




            case CatForm.Pawn:

                SetSkill(
                    "HEAL",
                    "⚡",
                    "Memulihkan HP Hero.",
                    pawnSkillIcon
                );

                break;

        }

    }









    private void SetSkill(
        string skillName,
        string cost,
        string description,
        Sprite icon)
    {

        if (skillNameText != null)
            skillNameText.text =
                skillName;



        if (skillCostText != null)
            skillCostText.text =
                "COST : "
                +
                cost;



        if (skillDescriptionText != null)
            skillDescriptionText.text =
                description;



        if (skillIcon != null)
            skillIcon.sprite =
                icon;

    }

}