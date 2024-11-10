﻿using System.Reflection;
using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;
using Serilog;

namespace ServerFilter;

public class RefreshServerPatcher : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/SteamNetwork.gdc";
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var waiter = new FunctionWaiter("_find_all_webfishing_lobbies");
        var forLoopWaiter = new TokenWaiter(t => t.Type == TokenType.CfFor);
        var insideFirstLineOfForLoop = false;
        var insideFunction = false;
        
        foreach (var token in tokens)
        {
            if (waiter.Check(token))
            {
               
                insideFunction = true;
                yield return token;

            }
            else if (forLoopWaiter.Check(token))
            {
                yield return token;
                insideFirstLineOfForLoop = true;
            } else if (insideFunction  && insideFirstLineOfForLoop && token.Type == TokenType.Colon)
            {
                // Yield the colon ":"
                yield return token;
                
                var currentIndentation = token.AssociatedData ?? 0;
                
                const string injectedCode =
                    @"
if has_node(""/root/PotatoServerFilter""):
    var ServerFilterNode = get_node(""/root/PotatoServerFilter"")
    if ServerFilterNode:
        ServerFilterNode.ApplyFilters()
    else
        print(""Unable to find PotatoServerFilter node"")
else:
    print(""Unable to find PotatoServerFilter node"")
";
                IEnumerable<Token> injectedTokens = ScriptTokenizer.Tokenize(injectedCode, currentIndentation + 2);
                foreach(var injectedToken in injectedTokens)
                    yield return injectedToken;
                insideFunction = false;
                insideFirstLineOfForLoop = false;
            }
            else
            {
                yield return token;
            }
        }
    }
}