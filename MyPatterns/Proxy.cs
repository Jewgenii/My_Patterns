using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{
    namespace Proxy
    {
        //**************************  socket proxy
        /* remote proxy  acts as a local representetive to a remote object
         * (object that is running in different address space)
         * local representetive is an object to call methods on and have them forwarded to 
         * the remote object
         *   
         *   client object acts like its making remote method calls.
         *   but what is really doing is calling methods on the heap - local proxy object that
         *   handles all low level details of network communication.
         *   
         *   
         *   provides a surrogate or placeholder for another object to control access to it
         * */

        /* virtual proxy
         * 
         *  - intercept a method invocation on the subject 
         *  - can look like a decorator but with different intent(proxy controls access to it
         *  while decorator adds on new behaviours)
         *  - 
         *  
         *  */

            /*
             * virtual proxy controls access to an object that is expensive to instantiate 
             * protection proxy controls access to methods of an object based on the caller.
             * structurally similar to decorator but differ in purpose 
             * 
             * 
             * 
             * */
        public interface IPersonBean
        {
            string getName();
            string getGender();
            string getIterests();
            int getHotOrNotRating();
            void setName(string name);
            void setGender(string gender);
            void setInterests(string interests);
            void setHotOrNotRating();
        }
        // protection proxy controlls object acces based  on the rights
// one should not change the someones values any user can call any method it need to be protected!!!
        public class PersonBean : IPersonBean
        {
            string name;
            string gender;
            string interests;
            int rating;
            int ratingCount = 0;

            public string getGender()
            {
                return gender;
            }

            public int getHotOrNotRating()
            {
                if (rating == 0) return 0;
                else return rating / ratingCount;
            }

            public string getIterests()
            {
                return interests;
            }

            public string getName()
            {
                return name;
            }

            public void setGender(string gender)
            {
                this.gender = gender;
            }

            public void setHotOrNotRating()
            {
                this.rating += rating;
                ratingCount++;
            }

            public void setInterests(string interests)
            {
                this.interests = interests;
            }

            public void setName(string name)
            {
                this.name = name;
            }
        }

    }
}
