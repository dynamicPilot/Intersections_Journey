using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDropdownMenuUI : DropdownMenuUI
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerTo = "toQuit";
    [SerializeField] private string triggerFrom = "toHamburger";

    public override void ClosePanel()
    {
        base.ClosePanel();

        animator.SetTrigger(triggerFrom);
    }

    public override void OpenClosePanel()
    {
        base.OpenClosePanel();

        if (GetState())
        {
            animator.SetTrigger(triggerTo);
        }
        else
        {
            animator.SetTrigger(triggerFrom);
        }
    }


    public void OpenSettings()
    {

    }

    public void QuitGame()
    {
        ClosePanel();
        Application.Quit();
    }

}
