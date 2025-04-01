using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace A2Object
{
    public class MyObject
    {
        bool isOwned;
        string name;
        float cost;
        int power;
        string description;
       
        public MyObject(bool o, string n, float c, int p, string d)
        {
            this.isOwned = o;
            this.name = n;
            this.cost = c;
            this.power = p;
            this.description = d;
        }
        public bool GetOwned()
        { 
        return isOwned;
        }
        public string GetName()
        { 
        return this.name;
        }
        public float GetCost()
        {
        return this.cost; 
        }
        public int GetPower()
        { 
        return this.power;
        }
        public string GetDescription()
        { 
        return this.description;
        }
        public void SetOwned(bool o)
        { 
        this.isOwned=o;
        }

        public MyObject Clone()
        {
            return new MyObject(this.isOwned, this.name, this.cost, this.power, this.description);
        }
       

    }
}
