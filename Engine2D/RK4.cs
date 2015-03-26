using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine2D
{
    public class RK4
    {
        public struct State
        {
            public Vector3 Position;
            public Vector3 Velocity;
        }

        public struct Derivative
        {
            public Vector3 dx; //velocity
            public Vector3 dv; //acceleration
        }

        public Derivative Evaluate(State state, float time, float dt, Derivative derivative)
        {
            State temp = new State();
            state.Position = state.Position + derivative.dx * dt;
            state.Velocity = state.Velocity + derivative.dv * dt;

            Derivative der = new Derivative();
            der.dx = state.Velocity;
            der.dv = new Vector3(0.0f, 9.81f, 0.0f); //acceleration
            return der;
        }

        public void Integrate(State state, float time, float dt)
        {
            Derivative a, b, c, d;
            a = Evaluate(state, time, 0.0f, new Derivative());
            b = Evaluate(state, time, dt * 0.5f, a);
            c = Evaluate(state, time, dt * 0.5f, b);
            d = Evaluate(state, time, dt, c);
            /*float dxdt = 1.0f / 6.0f *  ( a.dx + 2.0f*(b.dx + c.dx) + d.dx );  
            float dvdt = 1.0f / 6.0f *  ( a.dv + 2.0f*(b.dv + c.dv) + d.dv )  
            state.Position = state.Position + dxdt * dt; 
            state.Velocity = state.Velocity + dvdt * dt;*/
        }
    }
}
