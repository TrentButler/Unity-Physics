using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public abstract class AgentBehaviour : MonoBehaviour
    {
        protected Agent agent;
        abstract public void setAgent(Agent a);
        abstract public Agent getAgent();
    }
}