using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{
    /*****************************************************State Pattern******************************************
     * 
     * allows an object to alter its behavior when its internal state changes.
     * The object appear to change itself
     * it encapsulates the state into separate classes and delegates to the object 
     * representing the current state, the behavior changes with internal state ;
     * set of behaviors encapsulated in state objects
     * its an alternative to putting lots of conditionals in your context one can 
     * change the state object in context to change its behavior  
     * 
     * state classes can be shared between different clients by making them static
     * */

    // this class is hard to modify it doesnt adhere (придерживаться)
    // open closed priciple
    // so each satte must have its owsn class and delegate them to the current state when an action occures
    // to have the design easy to maintaint and extand
    /*  1 must have a state interface with every action in gumball machine 
     *  2 must have a state class for every state of the machine
     *  those will be responsible for the actions when its in the corresponding state
     *  3 get rid of the conditional and delegate to classes to do that work
     * */

    public class GumballMachineTest
    {
        // instances to keep track where we are 
        readonly static int soldOut = 0;
        readonly static int noQueter = 1;
        readonly static int hasQueter = 2;
        readonly static int sold = 3;

        int state = soldOut;
        int count = 0;//number of gumbles 
        public GumballMachineTest(int count)
        {
            this.count = count;
            if (count > 0)
            {
                state = noQueter;
            }
        }
        public void insert()
        {
            if (state == hasQueter)
            {
                Console.WriteLine("cant inserty another quater");
            }
            else if (state == noQueter)
            {
                state = hasQueter;
                Console.WriteLine("you inserted a quater");
            }
            else if (state == soldOut)
            {
                Console.WriteLine("you cant insert a quater the machine is sold out");
            }
            else if (state == sold)
            {
                Console.WriteLine("giving you a gumball");
            }
        }
        public void ejectQuarter()
        {
            if (state == hasQueter)
            {
                Console.WriteLine("quarter returned");
                state = noQueter;
            }
            else if (state == noQueter)
            {
                state = hasQueter;
                Console.WriteLine("you havent inserted a quater");
            }
            else if (state == soldOut)
            {
                Console.WriteLine("you cant eject you havent inserted the quarter yet");
            }
            else if (state == sold)
            {
                Console.WriteLine("you ve already turned the crank");
            }
        }
        public void turnCrank()
        {
            if (state == sold)
            {
                Console.WriteLine("turning twice doesnt get yoy another one");
            }
            else if (state == noQueter)
            {
                Console.WriteLine("you turned but there is no quarter");
            }
            else if (state == soldOut)
            {
                Console.WriteLine("you turned but there is no more gumballs");
            }
            else if (state == hasQueter)
            {
                Console.WriteLine("you turned...");
                state = sold;
                dispense();
            }
        }
        public void dispense()
        {
            if (state == sold)
            {
                Console.WriteLine("a gumball is rolling out of the slot ");
                count -= 1;
                if (count == 0)
                {
                    Console.WriteLine("out of gumballs");
                    state = soldOut;
                }
                else
                {
                    state = noQueter;
                }
            }
            else if (state == noQueter)
            {
                Console.WriteLine("you need to pay first");
            }
            else if (state == soldOut)
            {
                Console.WriteLine("gumball is dispensed");
            }
            else if (state == hasQueter)
            {
                Console.WriteLine("no gumball is dispensed");
            }

        }
        public override string ToString()
        {
            string s = "count: " + count + " state: " + state;
            return s;
        }

    }

    public interface State// map directly what actions could be in any state
    {
        void insert();
        void ejectQuarter();
        void turnCrank();
        void dispense();
    }
    /*
     * the differences :
     * - localize behaivor of each state
     * - removed troublesome if statements that was difficult to maintain 
     * - code closed for modification and open for extension to add new behaviors
     * 
     * 
     * disadvantages:
     *  - more behaviors more classes to write
     * */
    public class GumBallMachine
    {
        private State soldOutState;
        private State noQuarterState;
        private State hasQuarterState;
        private State soldState;
        private State state;
        private State WinnerState;
        private int count = 0; //initially its empty

        public GumBallMachine(int ngumballs)
        {
            soldOutState = new SoldOutState(this);
            noQuarterState = new NOquarterState(this);
            hasQuarterState = new HasQuarterState(this);
            soldState = new SoldState(this);
            WinnerState = new WinnerState(this);

            int count = ngumballs;

            state = soldOutState;

            if (ngumballs > 0)
                state = noQuarterState;
        }
        public void InsertQuarter()
        {
            state.insert();
        }

        public State getSoldState()
        {
            return soldState;
        }
        public State getHasQuartedState()
        {
            return hasQuarterState;
        }
        public State getNOquarterState()
        {
            return noQuarterState;
        }
        public State getSoldOutState()
        {
            return soldOutState;
        }
        public State getWinnerState()
        {
            return WinnerState;
        }


        public void EjectQuarter()
        {
            state.ejectQuarter();
        }

        public void turnCrank()
        {
            state.turnCrank();
            state.dispense();
        }

        public void setState(State state) // change to another state
        {
            this.state = state;
        }

        public void releaseBall()
        {
            Console.WriteLine("a gumball comes rolling out of the slot...");
            state.ejectQuarter();
            if (count != 0)
                count -= 1;
        }
        public int getCount()
        {
            return count;
        }
        public override string ToString()
        {
            string s = "count: " + count + " state: " + state;
            return s;
        }
    }

    public class NOquarterState : State
    {
        private GumBallMachine gumballMachine;
        public NOquarterState(GumBallMachine g)
        {
            gumballMachine = g;
        }
        public void dispense()
        {
            Console.WriteLine("you need to pay first");
        }

        public void ejectQuarter()
        {
            Console.WriteLine("you havent inserted a quater");
        }

        public void insert()
        {
            Console.WriteLine("you inserted a quater");
            gumballMachine.setState(gumballMachine.getHasQuartedState());
        }

        public void turnCrank()
        {
            Console.WriteLine("you turned but there is no quarter");
        }
    }
    public class HasQuarterState : State
    {
        GumBallMachine gumballMachine;
        public HasQuarterState(GumBallMachine g)
        {
            gumballMachine = g;
        }

        public void dispense()
        {
            Console.WriteLine("no gumball dispensed");
        }

        public void ejectQuarter()
        {
            Console.WriteLine("quarter returned");
            gumballMachine.setState(gumballMachine.getSoldState());
        }

        public void insert()
        {
            Console.WriteLine("cant inserty another quater");
        }

        public void turnCrank()
        {
            Console.WriteLine("you turned...");
            gumballMachine.setState(gumballMachine.getSoldState());
        }
    }
    public class SoldState : State
    {
        private GumBallMachine gumballMachine;
        public SoldState(GumBallMachine g)
        {
            gumballMachine = g;
        }

        public void dispense()
        {
            gumballMachine.releaseBall();
            if (gumballMachine.getCount() > 0)
            {
                gumballMachine.setState(gumballMachine.getNOquarterState());
            }
            else
            {
                Console.WriteLine("out of gumballs");
                gumballMachine.setState(gumballMachine.getSoldOutState());
            }
        }

        public void ejectQuarter()
        {
            Console.WriteLine("you have already turned the crank");
        }

        public void insert()
        {
            Console.WriteLine("giving you a gumball");
        }

        public void turnCrank()
        {
            Console.WriteLine("turning twice doesnt get you another one");
        }
    }
    public class SoldOutState : State
    {
        private GumBallMachine gumballMachine;
        public SoldOutState(GumBallMachine g)
        {
            gumballMachine = g;
        }

        public void dispense()
        {
            Console.WriteLine("nothing to give you...");
        }

        public void ejectQuarter()
        {
            Console.WriteLine("rolling back a Quarter");
        }

        public void insert()
        {
            Console.WriteLine("you inserted a Quarter...");
            gumballMachine.setState(gumballMachine.getHasQuartedState());
        }

        public void turnCrank()
        {
            Console.WriteLine("nothing...");
        }
    }
    public class WinnerState : State
    {
        private GumBallMachine gumballMachine;

        public WinnerState(GumBallMachine g)
        {
            gumballMachine = g;
        }

        public void dispense()
        {
            Console.WriteLine("You are winner,get two gumballs");
            gumballMachine.releaseBall();
            if (gumballMachine.getCount() == 0)
            {
                gumballMachine.setState(gumballMachine.getSoldOutState());
            }
            else
            {
                gumballMachine.releaseBall();
                if (gumballMachine.getCount() == 0)
                    gumballMachine.setState(gumballMachine.getNOquarterState());
                else
                {
                    Console.WriteLine("out of gumballs");
                    gumballMachine.setState(gumballMachine.getSoldOutState());
                }
            }
        }

        public void ejectQuarter()
        {
            Console.WriteLine("you have already turned the crank");
        }

        public void insert()
        {
            Console.WriteLine("giving you a gumball");
        }

        public void turnCrank()
        {
            Console.WriteLine("turning twice doesnt get you another one");
        }
    }


}
