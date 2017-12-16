using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{

    /* Adapter pattern converts an interface of a class into another interface the clients expect.
     * Adapter lets classes work together that couldnt otherwise because of incompetible interfaces.
     * */

    /* it allow to use a client with incompatible interface by creating an adapter that does the convertion
     * this decouples the client from the implemented interface and if we expcet the interface to change over the time 
     * the adapter encapsulates that change so that the client doesnt have to be modifyed each time it needs to
     * operate against a different interface.
     * */
    namespace Adapter
    {
        public interface ITurkey
        {
            void gobble();
            void fly();
        }
        public interface IDuck
        {
            void quack();
            void fly();
        }

        /*
         * - the client makes a request to the adapter by callining a method  on its using the target interface.
         * - the adapter translates the request into one or more calls on the adaptee using the adaptee interface.  
         * - the client receives the results of the call and never knows there is an adapter doing the translation.
         * - client and the adaptee are decoupled
         * */
        class MalardDuck : IDuck
        {
            public void quack()
            {
                Console.WriteLine("quack");
            }
            public void fly()
            {
                Console.WriteLine("I am flying");
            }
        }

        public class WildTurkey : ITurkey
        {
            public void gobble()
            {
                Console.WriteLine("Gobble gobble");
            }
            public void fly()
            {
                Console.WriteLine("I am flying a short distance");
            }
        }

        /*The adapter implements the target interface and holds an instance of  the Adaptee */
        public class TurkeyAdapterToIDuck : IDuck // implementing the inteface of the type we are adapting to, that client expects to see
        {
            private ITurkey turkey;
            public TurkeyAdapterToIDuck(ITurkey turkey) // get the reference to the object that we are adapting
            {
                this.turkey = turkey;
            }
            public void quack()
            {
                turkey.gobble();
            }
            public void fly()
            {
                for (int i = 0; i < 5; i++)
                    turkey.fly();
            }
        } // it implements the target interface the IDuck

        public class DuckAdapterToITurkey : ITurkey
        {
            private IDuck duck;
            public DuckAdapterToITurkey(IDuck duck)
            {
                this.duck = duck;
            }
            void ITurkey.gobble()
            {
                duck.quack();
            }
            void ITurkey.fly()
            {
                duck.fly();
            }
        }
        /* the client is implemented against the target interface */
        public class TestDuck
        {
            public void Test(MyPatterns.Adapter.IDuck d)
            {
                d.quack();
                d.fly();
            }
        }
        public class TestTurkey
        {
            public void Test(MyPatterns.Adapter.ITurkey t)
            {
                t.gobble();
                t.fly();
            }
        }
        /*one can create two way adapter that implements two interfaces
         * */
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // the example about that standart java collections like hashtable, stack ,vector can be 
        //  iterrated in two ways old(enumaration) and new(iterator) the difference is that iterator supports new mothod remove
        // unlike enumaration
        /******************************************FACADE**********************
         * tos use this pattern wer create a class that simplifies and unifies a set of more complex classes 
         * that belong to some subsystem.
         *  - allows to avoid tight coupling between clients and subsystems.
         *  
         *  definition:
         *  provides a unified interface to a set of interfaces in a subsystem, 
         *  Facade defines a higher-lvel interface that makses hte subsystem easier to use.
         *  T***********
         *  so the intent is to make a system easier to use through a simplified interface.
         *  it keeps system easy and flexible
         * */

        /* - when you need to use an existing class and its interface is not the one you need use adapter
         * - when you need simplify and unify a large interface or complex set of inheritance use facade
         * - an adapter changes interface in the one that the client expects
         * - a facade decouples the clients from complex subsystem
         * - implementing an adapter may require little work or great deal of work depending on the size 
         * and complexity of the target interface 
         * - implementing the facade requires that we compose the facade with its subsystem and use delegation 
         * to perform the work of the facade 
         * - there are two forms of adapter pattern: class and object.Class adapter require multiple inheritance
         * - the adapter wraps an object to change its interface
         * a decorator wraps an object to add new behaviors and responsibilities
         * a facade "wraps" a set of objects to simplify
         * 
         * 
         * */

        //public class HomeTheatherFacade
        //{
        //    Amplifier a;
        //    Tuner t;
        //    DVDPlayer dvd;
        //    public HomeTheatherFacade(  Amplifier a,
        //                                Tuner t,
        //                                DVDPlayer dvd)
        //    {
        //        this.a = a;
        //        this.t = t;
        //        this.dvd = dvd;
        //    }
        //    public void watchMovie()
        //    {
        //        a.on();
        //        t.down();
        //        dvd.play(); // ...and so on...
        //    }
        //    public void endMovie()
        //    {
        //        a.off();
        //        t.stop();
        //        dvd.off(); // ...and so on...
        //    }
        //}

    }
}
