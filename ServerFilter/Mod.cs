﻿using GDWeave;
 
namespace ServerFilter;

public class Mod : IMod {
    public Mod(IModInterface modInterface) {
        modInterface.RegisterScriptMod(new RefreshServerPatcher());
    }

    public void Dispose() { }
}