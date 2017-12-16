using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{
    /*************************OBSERVER*********************************
     * defines one-to-many dependency between objects so that when
     * one object  changes its state all of its dependents are notified
     * and updated automatically
     * */

    public interface ISubject
    {
        void RegisterObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObservers();
    }

    public interface IObserver
    {
        void Update(ISubject sbj, object args = null);// specific form for all observers to update its contents
        void Update(float temp, float humidity, float pressure);
        void Unsubscribe(); // possible if Observer keeps track of the Subject  it benlongs to
        void Subscribe(WeatherData wd);
    }

    public interface IDisplayElement
    {
        void Display();
    }

    #region Displays
    public class CurrentConditionDisplay : IObserver, IDisplayElement
    {
        float temp;
        float humidity;
        private ISubject weatherdata;
        public CurrentConditionDisplay(ISubject weatherdata)
        {
            this.weatherdata = weatherdata;
            weatherdata.RegisterObserver(this);
        }
        public void Display()
        {
            Console.WriteLine("CurrentConditionDisplay :\ntemp :{0}\nhumidity : {1}",
                temp, humidity);
        }
        public void Update(float temp, float humidity, float pressure)
        {
            this.temp = temp;
            this.humidity = humidity;
            Display();
        }
        public void Update(ISubject sbj, object args = null) // this way it accepts any subjects of ISubject interface
        {
            WeatherData wd = sbj as WeatherData;
            if (wd != null)
            {
                this.humidity = wd.GetHumidity;
                this.temp = wd.GetTemp;
            }
        }

        public void Unsubscribe()
        {
            weatherdata.RemoveObserver(this);
        }

        public void Subscribe(WeatherData wd)
        {
            wd.RegisterObserver(this);
        }
    }

    public class PressureDisplay : IObserver, IDisplayElement
    {
        float pressure;
        ISubject weatherdata; // keeps for better if any other operations needed

        public PressureDisplay(ISubject weatherdata)
        {
            this.weatherdata = weatherdata; /// not neccessery
            weatherdata.RegisterObserver(this);
        }
        public void Display()
        {
            Console.WriteLine("PressureDisplay :\npressure :{0}", pressure);
        }
        public void Unsubscribe()
        {
            weatherdata.RemoveObserver(this);
        }
        public void Subscribe(WeatherData wd)
        {
            wd.RegisterObserver(this);
        }
        public void Update(float temp, float humidity, float pressure)
        {
            this.pressure = pressure;
            Display();
        }
        public void Update(ISubject sbj, object args = null) // this way it accepts any subjects of ISubject interface
        {
            WeatherData wd = sbj as WeatherData;
            if (wd != null)
            {
                this.pressure = wd.GetHumidity;
            }
             ();
        }
    }
    #endregion

    sealed public class WeatherData : ISubject
    {
        private List<IObserver> observers; // object are different thats why the type of them must be common
        private float temp;
        private float humidity;
        private float pressure;
        public float GetHumidity { get { return humidity; } }
        public float GetTemp { get { return temp; } }
        public float GetPressure { get { return temp; } }

        public WeatherData()
        {
            observers = new List<IObserver>();
        }

        public void RegisterObserver(IObserver o) // garentees that passed object realized IObserver
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            int index = observers.IndexOf(o);

            if (index >= 0)
                observers.RemoveAt(index);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(this); // - pull
                                       //    ((IObserver)observer).Update(temp, humidity, pressure); - push
            }

            // lets let each o
        }

        public void MeasurementsChanged()
        {
            //add smth like cheching for input values 
            // should it by actually notified depending on the values
            NotifyObservers();
        }

        public void SetMeasurements(float temp, float humidity, float pressure)
        {
            this.temp = temp;
            this.humidity = humidity;
            this.pressure = pressure;
            MeasurementsChanged();
        }
    }

}
/*
 * 1)defines one to many relationships between object
 * 2)Subjects update observers(listeners) using common interface
 * 3)Observers are loosely coupled in that the Observable(interface for add, del and notify )
 * knows nothins about them other then they implement common interface  
 * 4) one can push and pull data to observers
 * push its send all data in the object to observers
 * pull is when observer takes some certain data from object
 * 5)
 * */
