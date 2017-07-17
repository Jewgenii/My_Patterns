using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{
                /*                        Strategy   
             * defines a family of algorithms,encapsulates each one ,
             * and makes them interchangengable.
             * Strategy lets the algorithm vary independently from clients that use it.
             * */
    interface IFliable
    {
        void Fly();
    }
    class FlyWithWings : IFliable // an algorithm - a member of IFliable family  
    {
        public void Fly()
        {
            Console.WriteLine("I fly FlyWithFings");
        }
    }
    class FlyWithNoWings : IFliable // an algorithm - a member of IFliable family  
    {
        public void Fly()
        {
            Console.WriteLine("I dont fly FlyWithNoWings");
        }
    }
    class RocketFlyBehavior : IFliable // an algorithm - a member of IFliable family  
    {
        public void Fly()
        {
            Console.WriteLine("I fly by RocketFlyBehavior");
        }
    }
    abstract class Duck
    {
        protected IFliable fly;
        protected string DuckName;
        public IFliable ChangeFlyBehavior
        {
            set
            {
                IFliable tmp = value as IFliable;
                if (tmp != null)
                    fly = value;
                else
                    fly = null;
            }
            get { return fly; }
        }
        public Duck(IFliable fly, string DuckName)
        {
            this.fly = fly;
        }
        public Duck()
        { }
        public void doFly()
        {
            try
            {
                fly.Fly();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    class MyFlyingDuck : Duck
    {
        public MyFlyingDuck(IFliable fly, string DuckName) : base(fly, DuckName)
        { }

        public MyFlyingDuck() { }
    }
}
