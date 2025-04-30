using UnityEngine;
/* Class used for storing character gender
 * 0 for male (default), 1 for female 
 */
public class GenderManager : MonoBehaviour
{
    // Creating reference
    public static int gender = 0;
    
    // Method to get chosen gender
    public static int GetGender()
    {
        return gender;
    }
    
    // Method to store chosen gender
    public static void SaveGender(int g)
    {
        gender = g; 
    }
}
