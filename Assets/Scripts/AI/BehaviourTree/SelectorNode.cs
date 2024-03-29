﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class SelectorNode : CompositeNode
{
  
    public SelectorNode(params INode[] nodes) : base(nodes)
    {

    }

 
    public override IEnumerator<NodeResult> Tick()
    {
        NodeResult returnNodeResult = NodeResult.Failure;

        foreach(INode node in _nodes)
        {
            IEnumerator<NodeResult> result = node.Tick();
            while (result.MoveNext() && result.Current == NodeResult.Running)
            {
                yield return NodeResult.Running;
            }

            returnNodeResult = result.Current;

            if (result.Current == NodeResult.Failure)
                continue;                

            if(result.Current == NodeResult.Succes)
                break;
            
        }
        yield return returnNodeResult;

    }
}
