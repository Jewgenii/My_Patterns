using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace MyPatterns
{
    namespace Template
    {


        /* The Template Method  defines the steps of an algorithm ans allows subclasses to provide the implementation 
         * for one or more steps without changing the algorithm`s structure
         * the benefits:
         *  - avoiding duplication in the code if we had separate classes  with  the same code implementation in methods
         *  - resuse of the existing code 
         *  - code is closed for modification and open for exension
         *  - provides a framework
         * */

        /*
         * - defines the steps of an algorithm, deffering to subclasses for the implementation of those steps
         * - code reuse
         * - abstract class can define  concrete,abstract mothods and hooks
         * - hooks are methods  to do nothing or default behavior in subclasses
         * - to prevent subclasses to change the algorithm its method is forbidden to reimplement in subclasses
         * - strategy and template method both encapsulate algorithms, strategy through object composition
         * and template method - inheritance
         * */
        public abstract class CaffeineBeverage
        {
            // not virtual cannot be overriden anymore  
            public void PrepareRecipe() // this is the template method common for all subclasses
            {
                boilWater();
                Brew();
                PourWaterIntoCup();
                if (CustomerWantsCondiments())
                    AddCondiment();
            }
            /*it serves as template for an algorithm
             * some methods are handled by superclass and some are handled by subclasses
             * */
            /// <summary>
            ///  here we generalized the PrepareRecipe()  and put it in the base class
            /// </summary>
            protected abstract void Brew();
            protected abstract void AddCondiment();
            protected void boilWater()
            {
                Console.WriteLine("{0:20}", "boilWater");
            }
            protected void PourWaterIntoCup()
            {
                Console.WriteLine("{0:20}", "PourWaterIntoCup");
            }

            public virtual void Hook() { } // method that does nothing and can be reimplemented by subclasses
                                           // contains nothing or default implementation
                                           // hooks help to add some ability to change code flow they are optional
            public virtual Boolean CustomerWantsCondiments() { return true; } // for each class can be overriden
        }
        public class Tea : CaffeineBeverage
        {
            sealed protected override void Brew()
            {
                Console.WriteLine("{0:20}", "Steeping the tea");
            }
            sealed protected override void AddCondiment()
            {
                Console.WriteLine("{0:20}", "Adding Lemon");
            }
        }
        public class Coffee : CaffeineBeverage
        {
            sealed protected override void Brew()
            {
                Console.WriteLine("{0:20}", "Dreeping coffee through filter");
            }
            sealed protected override void AddCondiment()
            {
                Console.WriteLine("{0:20}", "Adding sugar and milk");
            }
            public override bool CustomerWantsCondiments()
            {
                if (GetUserInput().ToString().ToLower().StartsWith("y"))
                    return true;
                else
                    return false;
            }
            private char GetUserInput()
            {
                Console.WriteLine("Want condiment?(y/n)");
                return Console.ReadKey(true).KeyChar;
            }

        }
        public class Duck : IComparable
        {
            private string name;
            private int weight;
            public Duck(string name, int weight)
            {
                this.name = name;
                this.weight = weight;
            }
            public int CompareTo(object obj)
            {
                Duck otherDuck = (Duck)obj;
                if (this.weight > otherDuck.weight)
                    return 1;
                else if (this.weight < otherDuck.weight)
                    return -1;
                else
                    return 0;
            }
            public override string ToString()
            {
                return name + " " + weight + " kg";
            }
        }

    }
}
