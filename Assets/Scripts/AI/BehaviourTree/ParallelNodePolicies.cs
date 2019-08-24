using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface ParallelNodePolicyAccumulator
{
    NodeResult Policy(NodeResult result);
}


public class NSuccesIsSuccesAccumulator : ParallelNodePolicyAccumulator
{    

    public static ParallelNodePolicyAccumulator Factory()
    {
        return new NSuccesIsSuccesAccumulator(2);
    }
   
    public static ParallelNode.Policy Factory(int n)
    {
        return () => new NSuccesIsSuccesAccumulator(n);
    }

    private readonly int _n = 1;
    private int _count = 0;

    public NSuccesIsSuccesAccumulator(int n)
    {
        _n = n;
    }

    public NodeResult Policy(NodeResult result)
    {
        if (result == NodeResult.Succes)
        {
            _count++;
        }
        return (_count >= _n) ? NodeResult.Succes : NodeResult.Failure;
    }
}



