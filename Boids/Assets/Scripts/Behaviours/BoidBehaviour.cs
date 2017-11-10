using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class BoidBehaviour : AgentBehaviour
    {
        public override Agent getAgent()
        {
            return agent;
        }

        public override void setAgent(Agent a)
        {
            if(a != null)
            {
                agent = a;
            }
        }
    }
}