using UnityEngine;

public class EarthLvlPuzzle : MonoBehaviour
{
    [SerializeField] private EarthLvlStoneInteraction[] stones; 
    [SerializeField] private GameObject portal; // Hidden portal (enable when solved)

    public void CheckPuzzleSolution()
    {
       bool allCorrect = true;
       foreach (EarthLvlStoneInteraction stone in stones)
       {
           if (stone == null) continue;

           Vector3 targetDirection = GetOppositeDirection(stone.stoneDirection);
           float angle = Vector3.Angle(stone.symbolFace.forward, targetDirection);
        
           Debug.Log($"{stone.name}: Target={targetDirection} | Angle={angle}°");

           if (angle > 45f) // Tolerance
           {
               allCorrect = false;
           }
       }

       portal.SetActive(allCorrect);
    }
    
    private Vector3 GetOppositeDirection(EarthLvlStoneInteraction.CardinalDirection dir)
    {
        return dir switch
        {
            EarthLvlStoneInteraction.CardinalDirection.North => Vector3.back,  // North stone should face South (back)
            EarthLvlStoneInteraction.CardinalDirection.East => Vector3.left,   // East stone should face West (left)
            EarthLvlStoneInteraction.CardinalDirection.South => Vector3.forward,
            EarthLvlStoneInteraction.CardinalDirection.West => Vector3.right,
            _ => Vector3.zero
        };
    }
}
