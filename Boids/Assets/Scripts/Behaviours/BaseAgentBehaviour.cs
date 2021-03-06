﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public class BaseAgentBehaviour : AgentBehaviour
    {
        public override Agent getAgent()
        {
            return agent;
        }

        public override void setAgent(Agent a)
        {
            if (a != null)
            {
                agent = a;
            }
        }

        private void LateUpdate()
        {
            var a = agent as Boid;
            transform.position = a.Position;
            transform.forward = a.Velocity.normalized;
        }
    }
}