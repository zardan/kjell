using System.Collections;
using System.Collections.Generic;
using PM;
using UnityEngine;

public class KjellInitializer : MonoBehaviour
{
    private void Awake()
    {
        Main.RegisterFunction(new InputFunction());
        Main.RegisterFunction(new PrintFunction());
        Main.RegisterFunction(new RandomIntFunction());

        Main.RegisterCaseDefinitionContract<KjellCaseDefinition>();
    }
}
