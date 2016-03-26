using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EditorMenu : MonoBehaviour {



    public void openSheetStyleOneEditor()
    {
        SceneManager.LoadScene("SheetEditorOne");
    }

    public void openSheetStyleTwoEditor()
    {
        SceneManager.LoadScene("SheetEditorTwo");
    }

}
