//using UnityEngine;
//using UnityEngine.UI;
//using Vuforia;

//public class ObjectDetectionHandlerLegacy : MonoBehaviour
//{
//    public Text objectNameText; // Legacy UI Text component

//    private ObserverBehaviour observerBehaviour;

//    void Start()
//    {
//        observerBehaviour = GetComponent<ObserverBehaviour>();
//        if (observerBehaviour)
//        {
//            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
//        }
//    }

//    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
//    {
//        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
//        {
//            Debug.Log("Detected object: " + behaviour.TargetName);
//            if (objectNameText != null)
//            {
//                objectNameText.text = "Detected: " + behaviour.TargetName;
//            }
//        }
//        else
//        {
//            if (objectNameText != null)
//            {
//                objectNameText.text = "No object detected";
//            }
//        }
//    }
//}





using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Collections.Generic;


public class ObjectDetectionHandlerLegacy : MonoBehaviour
{
    public Text objectNameText; // Legacy UI Text component
    public Text buttonText;
    public GameObject infoCanvas;
    public GameObject switchCanvas;
    public Dictionary<string, string> blockInfo = new Dictionary<string, string>();
    public Dictionary<string, string> blockName = new Dictionary<string, string>();
    public RawImage blockImageholder;
    public Texture blockImage;
    public RawImage placementHolder;
    public Texture placementImage;

    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        blockInfo["AT054_doorjamb"] = "<b>Description:</b> Wall jamb of the door of the cella.  Bears a three-fasciae carving at the end, that would have run around the door as a door frame. \n\n <b>Placement:</b> Found in the NW quadrant and thus belongs to the doorwall to the left of the entrance into the cella.";
        blockName["AT054_doorjamb"] = "AT054";

        blockInfo["AT290292_top_pilasterblock"] = "<b>Description:</b> These joining fragments form a pilaster block, which bears a necking in a similar manner to the upper anta block and the upper column shaft, and thus it is restored immediately below the pilaster capital.\n <b>Placement:</b> This fragments were found at the northeast  quadrant and they are restored at the rear left pilaster of the temple.";
        blockName["AT290292_top_pilasterblock"] = "AT290+292";

        blockInfo["AT1600"] = "<b>Description:</b> Rectalinear corinthian capital with three faces with bilateral symmetry, which identifies it as the anta capital. The capital bears a dowel hole.\n\n <b>Placement:</b> Found in the northwest quadrant, it is identified as the capital for the left anta.";
        blockName["AT1600"] = "AT160";

        blockInfo["AT429_SingleBlock"] = "<b>Description:</b> This block includes a cyma and a dentil course on one of its long faces, and thus is identified as a frieze block, over the architrave. However, it differs from other frieze blocks of the temple in that the dentils are at the edge of the block and thus are not crowned by a tainia, which would have made them more vulnerable to fractures during the installation and also would have reduced the overall height of the block.\n\n " +
            "<b>Placement:</b> This frieze block was found at the northeast of the temple. Given its specific findspot as well as other diagnostic elements, it was probably the first frieze block from the back corner on the north flank of the temple.";
        blockName["AT429_SingleBlock"] = "AT429";

        blockInfo["AT186"] = "<b>Description:</b> The frieze course succeeded the architrave, and was underneath the roof cornice (geison). Aesthetically, this course provided a surface for decoration, which in this case included a cyma profile and a dentil course. Functionally, this course at the back face included cuttings for the support of the  joists of the roof.\n\n" +
            "<b>Placement:</b> This is a corner frieze block, found to the southwest of the temple, and tbus is restored at the at the righthand corner of the temple front façade.";
        blockName["AT186"] = "AT186";

        blockInfo["AT638"] = "<b>Description:</b> Unfinished podium base molding. Its slanted side would be carved down at the final stages, to give a molding with an ovolo, cyma and tainia. The molding turns in a U-shape, thus the block has to be restored at the front end of the podium by the stairs. This is the only piece of evidence that gives us the width of the front end of the podium.\n\n" +
            "<b>Placement:</b> Was found about 20 m. southwest of the temple, and it is restored at the right front corner of the podium.";
        blockName["AT638"] = "AT638";

        blockInfo["AT308"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT308"] = "AT308";

        blockInfo["AT145"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT145"] = "AT145";

        blockInfo["AT146"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT146"] = "AT146";

        blockInfo["AT175"] = "<b>Description:</b> Fragment of the top column drum of the shaft of the column. It is not known if the column consisted of 2 or 3 column drums. In either case, the columns were unfluted, and were made of the same marble as throughout the temple, and had entasis and a smaller diameter at the upper part. This frament preserves the upper resting surface of the column, where the column capital would be supported. It bears a dowel hole, for the alignment and fastening of the capital.\n\n" +
            "<b>Placement:</b> This column drum was found to the southeast of the temple and is restored at the rightmost column.";
        blockName["AT175"] = "AT175";

        blockInfo["AT173"] = "<b>Description:</b> Column capital, the best preserved of the four original ones. The capital is of the corinthian genus, bearing stylized acanthus leaves and tendrils in the corners and the middle part of the capital. The corinthian order was commonly used in Imperial Roman Architecture, as the most ornate order. \n\n" +
            "<b>Placement:</b> Found in the SW quadrant.";
        blockName["AT173"] = "AT173";

        blockInfo["AT085"] = "<b>Description:</b> This base bears the scotia-torus-scotia molding in three sides with bilateral symmetry and thus is identified as the anta base, meaning the base to the buldging extension of one of the two lateral walls of the cella.\n\n" +
            "<b>Placement:</b> Anta base found in NW quadrant, and restored on the left side of the temple.";
        blockName["AT085"] = "AT085";

        blockInfo["AT159"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT159"] = "AT159";

        //Combine 091 and 051
        blockInfo["AT051+091"] = "<b>Description:</b> Combination of two door lintels, left and right respecitvely. AT051 is the left side of the door lintel of the entrance to the cella. AT091 is the right side of the door lintel of the entrance to the cella. Both continue the three-fasciate door frame at the top, which is crowned by a lintel, framed by a scroll in either end.\n\n" +
            "<b>Placement:</b> AT051 and AT091 were found in the northwest quadrant along with each other corner of the lintel.";
        blockName["AT051+091"] = "AT051091";
        //Combine 091 and 051

        blockInfo["AT188"] = "<b>Description:</b> \n\n" + "<b>Placement:</b> ";
        blockName["AT188"] = "AT188";

        blockInfo["AT421"] = "<b>Description:</b> This is a corner block of the architrave. It includes three fasciae of increasing height crowned by a simple ovolo and cavetto. It's short length shows that it could not have spanned the opening between columns, and has to be restored in the cella, over the wall blocks. \n\n" +
            "<b>Placement:</b> It was found at the northeast of the temple, and it is restored at the rear left corner of temple.";
        blockName["AT421"] = "AT421";

        blockInfo["AT067"] = "<b>Description:</b> This block has the typical design of the Attic Ionic column base with the scotia-torus-scotia sequence. It's role is to provide a decorative transition between the vertical element of the column and the horizontal plane of the stylobate.\n\n" +
            "<b>Placement:</b> This base was the one found farthest away from the left side of the temple, and thus it is restored as the leftmost column base.";
        blockName["AT067"] = "AT067";

        blockInfo["AT119"] = "<b>Description:</b> Pediment with wall block carved together in one piece. This is of smaller scale than the temple itself and thus it is identified as a pediment of an aedicula in the interior of the temple, likely to frame the statue of the temple. It bears a geison molding and a sculptural floral relief in the underside, which gives the minimum depth of the aedicula. " + "<b>Placement:</b> It was found in the northeast quadrant.";
        blockName["AT119"] = "AT119";

        blockInfo["AT194"] = "<b>Description:</b> This is the fully intact right corner geison block from the front façade. It preserves the raking profile of a geison drip surmounted (from bottom to top) by fascia, ovolo, fascia, ovolo, and cyma recta. The underside of the drip is decorated with alternating modillions and carved cassettes with floral motifs. Cassettes in other geison blocks also include figures, such as marine animals, tools, and spirals. The structural function of this block is to bear cuttings to support the rafters of the roof." + "<b>Placement:</b> Retrieving data. Wait a few seconds and try to cut or copy again.";
        blockName["AT194"] = "AT194";

        blockInfo["AT203"] = "<b>Description:</b> This block is part of the horizontal geison block. It bears cuttings for the rafters of the roof on the upper surface, and sculptural decoration in the underside of it, including modilions and sculptural cassettes with floral decoration and exceptionally, a crab." + "<b>Placement:</b> Found in the southwest quadrant, about 12 m. from its place on the temple. It is restored as part of the horizontal geison course on the right flank of the temple, adjacent to AT204.";
        blockName["AT203"] = "AT203";

        blockInfo["AT258"] = "<b>Description:</b> This corinthian capital has bilateral symmetry on the diagonal axis, and thus it needs to be restored as a corner block at the top of a pilaster. The capital is broken at its lower part, but when restored, it bears similar carving to the column capital with three rows of acanthus leaves: two leaves on the bottom row, three on the middle and two on the top, from where two tendrils spring, one to the middle axis of each face and one to the corner of the bloc" + "<b>Placement:</b> It was found in the SE quadrant, close to the rear right corner of the building, and thus it is restored there.";
        blockName["AT258"] = "AT258";


        switchCanvas.SetActive(false);
        infoCanvas.SetActive(false);
        //imagesCanvas.SetActive(false);
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            switchCanvas.SetActive(true);
            infoCanvas.SetActive(true);
            if (blockInfo.ContainsKey(behaviour.TargetName) && blockName.ContainsKey(behaviour.TargetName))
            {
                objectNameText.text = blockInfo[behaviour.TargetName];
                buttonText.text = blockName[behaviour.TargetName];
                blockImageholder.texture = blockImage;
                placementHolder.texture = placementImage;
            }


        }
        else
        {
            infoCanvas.SetActive(false);
            switchCanvas.SetActive(false);
            if (objectNameText != null)
            {
                objectNameText.text = "No object detected";
            }
        }
    }


}
