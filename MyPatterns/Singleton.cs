using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{
    /******************************Singleton****************************************************
     *  ensures a class has only one instatnce
     *  provides a global point of access to it 
     *  examine your performance and resource constraints and carefully choose the multithread singleton implementation
     *  beware of multiple class loaders  this could defeat the singleton implementation  and result in 
     *  multiple instances
     * */
    public class Singleton//public means that all objects from other .cs files can get access to Singleton
    {
        private static Singleton unique = null;
        //  private static Singleton unique = null;  // eager instantiation // thread safe for using class
        // volatile
        // be ware of situations when the same class of singleton is declared in different
        // namespaces!! - can cause to reseting initial values in the singleton class
        private Singleton()
        {

        }
        public static Singleton  getInstance() //synchronized  - thread safe but slows down the performance
        {
            if (unique != null)
                unique = new Singleton();// if it never needs the instance it never gets created(lazy instantiation)
            return unique;
        }
    }
}
