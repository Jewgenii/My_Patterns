using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MyPatterns.Compound;

namespace MyPatterns
{
    namespace Compound
    {


        public interface Quackobservable // who sends notifications
        {
            void registerObserver(Observer observer);
            void notifyObservers();
        }

        public interface Quackable: Quackobservable // who sends notifications plus quacks
        {
            void quack();
        }

        public interface Observer // who accepts the notifications
        {
            void update(Quackobservable duck);
        }

        class Duck: Quackobservable
        {
            protected Observable observable;

            public void notifyObservers()
            {
                observable.notifyObservers();
            }

            public void registerObserver(Observer observer)
            {
                observable.registerObserver(observer);
            }
        }

        class MallardDuck : Duck, Quackable
        {
            public MallardDuck()
            {
                observable = new Observable(this);
            }

            public void quack()
            {
                Console.WriteLine("quack");
                this.notifyObservers();
            }

        }

        class ReadHeadDuck : Duck, Quackable
        {
            public ReadHeadDuck()
            {
                observable = new Observable(this);
            }

            public void quack()
            {
                Console.WriteLine("quack");
                notifyObservers();
            }
          
        }

        class DuckCall :Duck, Quackable
        {
            public DuckCall()
            {
                observable = new Observable(this);
            }

            public void quack()
            {
                Console.WriteLine("quack");
            }
        }

        class RubberDuck :Duck, Quackable
        {
            public RubberDuck()
            {
                observable = new Observable(this);
            }

            public void quack()
            {
                Console.WriteLine("no quack");
                notifyObservers();
            }
        }

        //adapter
        public class Goose 
        {
            public void honk()
            {
                Console.WriteLine("honk");
            }
        }

        public class GooseAdapter: Quackable // use goose as a duck
        {
            private Goose goose;

            public GooseAdapter(Goose g)
            {
                this.goose = g;
            }

            public void notifyObservers()
            {
                throw new NotImplementedException();
            }

            public void quack()
            {
                goose.honk();
            }

            public void registerObserver(Observer observer)
            {
                throw new NotImplementedException();
            }
        }
        //decorator
        public class QuackableCounter: Quackable
        {
            Observable observable;
            Quackable duck;
            static int numberOfQuacks = 0;
            public QuackableCounter(Quackable duck)
            {
                this.duck = duck;
                observable = new Observable(this);
            }
            public void quack()
            {
                duck.quack();
                numberOfQuacks++;
                notifyObservers();
            }
            public static int getQuacks()
            {
                return numberOfQuacks;
            }

            public void notifyObservers()
            {
                observable.notifyObservers();
            }

            public void registerObserver(Observer observer)
            {
                observable.registerObserver(observer);
            }
        }
        //factory
        public abstract class AbstractFactory
        {
            public abstract Quackable createMallardDuck();
            public abstract Quackable createRedHeadDuck();
            public abstract Quackable createDuckCall();
            public abstract Quackable createRubberDuck();  
        }
        public class DuckFactory : AbstractFactory
        {
            public override Quackable createDuckCall()
            {
                return new DuckCall();
            }

            public override Quackable createMallardDuck()
            {
                return new MallardDuck();
            }

            public override Quackable createRedHeadDuck()
            {
                return new ReadHeadDuck();
            }

            public override Quackable createRubberDuck()
            {
                return new RubberDuck();
            }
        }
        public class DuckCountingFactory : AbstractFactory
        {
            public override Quackable createDuckCall()
            {
                return new QuackableCounter(new DuckCall());
            }

            public override Quackable createMallardDuck()
            {
                return new QuackableCounter(new MallardDuck());
            }

            public override Quackable createRedHeadDuck()
            {
                return new QuackableCounter(new ReadHeadDuck());
            }

            public override Quackable createRubberDuck()
            {
                return new QuackableCounter(new RubberDuck());
            }
        }

        // composite and iterator
        public class Flock : Quackable
        {
            List<Quackable> quackers = new List<Quackable>();
            public void add(Quackable quacker)
            {
                quackers.Add(quacker);
            }

            public void notifyObservers()
            {
                throw new NotImplementedException();
            }

            public void quack()
            {
                IEnumerator iter = quackers.GetEnumerator();
                while (iter.MoveNext())
                {
                    Quackable q = (Quackable)iter.Current;
                    q.quack();
                }
            }

            public void registerObserver()
            {
                throw new NotImplementedException();
            }

            public void registerObserver(Observer observer)
            {
                foreach (Quackable q in quackers)
                {
                    q.registerObserver(observer);
                }
            }
        }
        // obsever

        public class Observable: Quackobservable
        {
            Quackable duck;
            List<Observer> observers = new List<Observer>();
            public Observable(Quackable duck)
            {
                this.duck = duck;
            }
            public void quack()
            {
                Console.WriteLine("quack");
                notifyObservers();
            }

            public void registerObserver(Observer  observer)
            {
                observers.Add(observer);
            }

            public void notifyObservers()
            {
                IEnumerator iter = observers.GetEnumerator();
                while (iter.MoveNext())
                {
                    Observer ob = (Observer)iter.Current;
                    ob.update(duck);
                }
            }     

        }

        public class Quackologist : Observer // who accepts notifications
        {
            public void update(Quackobservable duck)
            {
                Console.WriteLine(duck.ToString() + " just quacked...");
            }
        }

        class MyDuck : Duck, Quackable
        {
            void Quackable.quack()
            {
                Console.WriteLine("My quack");
            }
            public MyDuck()
            {
                this.observable = new Observable(this);
            }
        }


    }
}


