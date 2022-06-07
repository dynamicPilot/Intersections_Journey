using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VEffectWithViewAndCargo : VEffectWithView
{
    [SerializeField] private Cargo cargo;

    public override void SetSortingLayerById(int id)
    {
        base.SetSortingLayerById(id);
        cargo.SetSortingLayerById(id);
    }
}
