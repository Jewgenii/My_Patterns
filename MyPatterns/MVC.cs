using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{
    class MVC
    {
        /* factors code into functional segments 
         * to achieve reusability one needs to keep boundaries between segments clean
         * Model on the one side, View on the other side and Contoller is between   
         * Model is custom objects, data, logic   
         * controller synchronize the data of  two 
         * it is a few patterns put together
         * controller manipulates the model
         * */

        /* View (user interface)
         * gives a representation of the model usually gets the data and state it 
         * needs to display directly from the model
         * has two parts: for displaying and controlling
         * */

        /*Contoller
         * takes user input and figures out what it means to the model
         * separates the logic of control from the view and decouples the view from the model
         * */

        /* Model 
         * Holds all the data and application logic. The model is obvious to view and contoller
         * it provides an interface to manipulate and retrieve its state and it can send notificationss
         * of state changes to observers 
         * */

        /*As you might have guessed the model uses 
Observer to keep the views and controllers updated on the latest state changes.  
The view and the controller, on the other hand, implement the Strategy Pattern. The controller 
is the behavior of  the view, and it can be easily exchanged with another controller if  you 
want different behavior.  The view itself  also uses a pattern internally to manage the windows, 
buttons and other components of  the display: the Composite Pattern.
 * 
 * 
 * The view and controller implement the classic Strategy Pattern: the 
view is an object that is configured with a strategy.  The controller 
provides the strategy.  The view is concerned only with the visual 
aspects of the application, and delegates to the controller for any 
decisions about the interface behavior. Using the Strategy Pattern also 
keeps the view decoupled from the model because it is the controller 
that is responsible for interacting with the model to carry out user 
requests.  The view knows nothing about how this gets done.

 * 
 * The model implements the Observer Pattern 
to keep interested objects updated when state 
changes occur. Using the Observer Pattern 
keeps the model completely independent of 
the views and controllers.  It allows us to use 
different views with the same model, or even 
use multiple views at once.

        The display consists of a nested set of win-
dows, panels, buttons, text labels and so on.  
Each display component is a composite (like 
a window) or a leaf (like a button).  When the 
controller tells the view to update, it only has 
to tell the top view component, and Composite 
takes care of the rest.
 * */

        public interface BeatModelInterface
        {
            void initialize();
            void on();
            void off();
            void setBPM(int bmp);
            int getBPM();
            void registerObserver(BeatObserver o);
            void registerObserver(BPMObserver o);
            void removeObserver(BeatObserver o);
            void removeObserver(BPMObserver o);
        }
        public interface ControllerInterface
        {
            void start();
            void stop();
            void increaseBMP();
            void decreaseBMP();
            void setBMP(int i);
        }
        public interface BeatObserver
        {
            void update();
        }
        public interface BPMObserver
        {
            void update();
        }

        public class Sequencer
        {
            public void start()
            {
                Console.WriteLine("Playing tune...");
            }

            public void stop()
            {
                Console.WriteLine("Stopping tune...");
            }

            public void setUpMidi()
            {
                Console.WriteLine("seting Up Midi tune...");
            }
        }
        public class BreatBar : BeatObserver
        {
            private int volume = 0;
            public void update()
            {
                Console.WriteLine("Now My Bit is {0}", volume);
            }
            public void setvalue(int i)
            {
                volume = i;
                update();
            }
        }

        public class BreatModel : BeatModelInterface
        {
            private List<BeatObserver> beatObservers = new List<BeatObserver>();
            private List<BPMObserver> BPMObservers = new List<BPMObserver>();
            private Sequencer s = new Sequencer();
            public int bpm = 90;

            public void initialize()
            {
                s.setUpMidi();
            }

            public void off()
            {
                setBPM(0);
                s.stop();
            }

            public void on()
            {
                s.start();
                setBPM(90);
            }

            public void registerObserver(BPMObserver o)
            {
                BPMObservers.Add(o);
            }

            public void registerObserver(BeatObserver o)
            {
                beatObservers.Add(o);
            }

            public void removeObserver(BPMObserver o)
            {
                BPMObservers.Remove(o);
            }

            public void removeObserver(BeatObserver o)
            {
                beatObservers.Remove(o);
            }

            public void setBPM(int bmp)
            {
                this.bpm = bmp;
                //foreach(BeatObserver o in beatObservers)
                //{ 
                //    o.update();
                //}
            }

            public int getBPM()
            {
                return this.bpm;
            }
        }

        public class DjView: BeatObserver, BPMObserver
        {
            BeatModelInterface model;
            ControllerInterface controller;

            BreatBar b;
            /*
             * buttons 
             * bars classes
             * and other
             * */
             public DjView(BeatModelInterface model,
                            ControllerInterface controller)
            {
                this.model = model;
                this.controller = controller;
                b = new BreatBar();
                model.registerObserver((BeatObserver)this);
                model.registerObserver((BPMObserver)this);
            }

            public void createview()
            {
                //create all swing components here
            }

            void BeatObserver.update()
            {
                b.setvalue(100);
            }

            void BPMObserver.update()
            {
                int bmp = model.getBPM();
                if (bmp == 0)
                {
                    Console.WriteLine("OFF");
                }
                else
                {
                    Console.WriteLine("current bmp: {0}", bmp);
                }

            }

            public void createControls()
            {
                // all controls are created here and placed on the interface
            }
            public void enableStopMenu()
            {
                Console.WriteLine("enableStartMenu");
            }
            public void disableStartMenu()
            {
                Console.WriteLine("disableStartMenu");
            }

            internal void disableStopMenu()
            {
                throw new NotImplementedException();
            }
        }

        public class BreatController : ControllerInterface
        {
            BeatModelInterface model;
            DjView view;

            public BreatController(BeatModelInterface model)
            {
                this.model = model;
                view = new DjView(controller:this, model: model);
                view.createview();
                view.createControls();
                model.initialize();
            }

            public void decreaseBMP()
            {
                int bmp = model.getBPM();
                model.setBPM(bmp - 1);
            }

            public void increaseBMP()
            {
                int bmp = model.getBPM();
                model.setBPM(bmp + 1);
            }

            public void setBMP(int i)
            {
                model.setBPM(i);
            }

            public void start()
            {
                model.on();
                view.enableStopMenu();
                view.disableStopMenu();
            }

            public void stop()
            {
                model.off();
                view.disableStartMenu();
                view.enableStopMenu();
            }
        }

        // monitor class of beat rate and pulse
        public interface HeartModelInterface
        {
            int getHeartRate();
            void registerObserver(BeatObserver o);
            void registerObserver(BPMObserver o);
            void removeObserver(BeatObserver o);
            void removeObserver(BPMObserver o);
        }

        public class HeartAdapter : BeatModelInterface
        {
            private HeartModelInterface heart;
            
            public HeartAdapter(HeartModelInterface heart)
            {
                this.heart = heart;
            }

            public int getBPM()
            {
               return heart.getHeartRate();
            }

            public void initialize()
            {

            } //left empty because HeartModelInterface doesnt support such methods

            public void off()
            {
                
            }

            public void on()
            {
               
            }

            public void registerObserver(BPMObserver o)
            {
                heart.registerObserver(o);
            }

            public void registerObserver(BeatObserver o)
            {
                heart.registerObserver(o);
            }

            public void removeObserver(BPMObserver o)
            {
                heart.removeObserver(o);
            }

            public void removeObserver(BeatObserver o)
            {
                heart.removeObserver(o);
            }

            public void setBPM(int bmp)
            {
               
            }
        } // use something like something else

        public class HeartController : ControllerInterface
        {
            DjView view;
            HeartModelInterface model;

            public HeartController(HeartModelInterface model)
            {
                this.model = model;
                view = new DjView(controller: this, model: new HeartAdapter(model)); // monitor of heart
            }

            // left unrelised cause no supported operations

            public void decreaseBMP()
            {
               
            }

            public void increaseBMP()
            {
               
            }

            public void setBMP(int i)
            {
               
            }

            public void start()
            {
               
            }

            public void stop()
            {
               
            }
        }

        /* The Model View Controller 
Pattern (MVC) is a compound 
pattern consisting of the 
Observer, Strategy and 
Composite patterns.


          The model makes use of the 
Observer Pattern so that it can 
keep observers updated yet 
stay decoupled from them.


         The controller is the strategy 
for the view.  The view can use 
different implementations of 
the controller to get different 
behavior.

         The view uses the Composite 
Pattern to implement the 
user interface, which usually 
consists of nested components 
like panels, frames and 
buttons.

         These patterns work together 
to decouple the three players in 
the MVC model, which keeps 
designs clear and flexible.


         The Adapter Pattern can be 
used to adapt a new model to 
an existing view and controller.
         * 
         * */

    }
}
