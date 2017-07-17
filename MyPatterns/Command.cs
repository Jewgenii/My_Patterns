using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{ 
    #region Vendor Classes
    public class Light
    {
        string name;
    public Light(string name = "")
        {
            this.name = name;
        }
    public void on()
        {
            Console.WriteLine(name + " Light is on");
        }
        public void off()
        {
            Console.WriteLine(name + " Light is off");
        }
    }
    public class GarageDoor
    {
        public void Up()
        {
            Console.WriteLine("Door in up");
        }
        public void Down()
        {
            Console.WriteLine("Door in down");
        }
        public void Stop()
        {
            Console.WriteLine("Door in stopped");
        }
        public void LightOn()
        {
            Console.WriteLine("Light in on");
        }
        public void LightOff()
        {
            Console.WriteLine("Light in off");
        }

    }
    public class Stereo
    {
        private int volume;
        public void On()
        {
            Console.WriteLine("Stereo is on");
        }
        public void Off()
        {
            Console.WriteLine("Stereo is off");
        }
        public void SetCD()
        {
        Console.WriteLine("CD is set");
        }
        public void SetDVD()
        {
        Console.WriteLine("DVD is set");
        }
        public void SetRadio()
        {
            Console.WriteLine("Radio is set");
        }
        public void SetVolume(int volume)
        {
            this.volume = volume;
            Console.WriteLine("Volume is set {0}",this.volume);
        }

}
    public class CeleingFan
    {
        public enum SpeedFan: int { HIGH = 3 , MEDIUM = 2, LOW = 1, OFF = 0 }
        String location;
        SpeedFan speed;
        public SpeedFan Speed { get { return speed; } set { speed = value; } }
        public CeleingFan(string location)
        {
            this.location = location;
            speed = SpeedFan.OFF;
        }
        public void high()
        {
            speed = SpeedFan.HIGH;
            Console.WriteLine(location + " fan is on HIGH");
        }
        public void medium()
        {
            speed = SpeedFan.MEDIUM;
            Console.WriteLine(location + " fan is on MEDIUM");
        }
        public void low()
        {
            speed = SpeedFan.LOW;
            Console.WriteLine(location + " fan is on LOW");
        }
        public void off()
        {
            speed = SpeedFan.OFF;
            Console.WriteLine(location + " fan is on OFF");
        }

    }

    #endregion
    public interface ICommand
    {
        void execute();
        void undo();
    }
    #region CommandObjects
    public class LightOnCommand : ICommand
    {
        private Light light;
        public LightOnCommand(Light light)
        {
            this.light = light;
        }
        public void execute()
        {
            light.on();
        }
        public void undo()
        {
            light.off();
        }
    }
    public class LightOffCommand : ICommand
    {
        private Light light;
        public LightOffCommand(Light light)
        {
            this.light = light;
        }
        public void execute()
        {
            light.off();
        }
        public void undo()
        {
            light.on();
        }
    }
    public class GarageDoorCommand : ICommand
    {
        GarageDoor dg;
        public GarageDoorCommand(GarageDoor dg)
        {
            this.dg = dg;
        }

        public void execute()
        {
            dg.Up();
            dg.Stop();
            dg.LightOn();
        }
        public void undo()
        {
            dg.Down();
            dg.Stop();
            dg.LightOff();
        }
    }
    public class GarageDoorOpenCommand : ICommand
    {
        GarageDoor dg;
        public GarageDoorOpenCommand(GarageDoor dg)
        {
            this.dg = dg;
        }

        public void execute()
        {
            dg.Up();
        }
        public void undo()
        {
            dg.Down();
        }
    }
    public class GarageDoorCloseCommand : ICommand
    {
        GarageDoor dg;
        public GarageDoorCloseCommand(GarageDoor dg)
        {
            this.dg = dg;
        }
        public void execute()
        {
            dg.Down();
        }
        public void undo()
        {
            dg.Up();
        }
    }
    public class StereoOffCommand : ICommand
    {
        private Stereo stereo;
        public StereoOffCommand(Stereo stereo)
        {
            this.stereo = stereo;
        }
        public void execute()
        {
            stereo.Off();
        }
        public void undo() // such as we dont have yet any other impelmentation of StereoOnWithCDCommand but if we had then just stereo.on
        {
            stereo.On();
            stereo.SetCD();
            stereo.SetVolume(11); // any number
        }

    }
    public class StereoOnWithCDCommand : ICommand
    {
        private Stereo stereo;
        public StereoOnWithCDCommand(Stereo stereo)
        {
            this.stereo = stereo;
        }
        public void execute()
        {
            stereo.On();
            stereo.SetCD();
            stereo.SetVolume(11); // any number
        }
        public void undo()
        {
            stereo.Off();
        }
    }
    /////////////////////////////////

    public abstract class CeleingFanCommonClassCommand: ICommand
    {
        protected CeleingFan cf;
        protected CeleingFan.SpeedFan prevSpeed;
        public abstract void execute();
        public void undo()
        {
            switch (prevSpeed)
            {
                case CeleingFan.SpeedFan.HIGH:
                    {
                        cf.high();
                    }
                    break;
                case CeleingFan.SpeedFan.MEDIUM:
                    {
                        cf.medium();
                    }
                    break;
                case CeleingFan.SpeedFan.LOW:
                    {
                        cf.low();
                    }
                    break;
                case CeleingFan.SpeedFan.OFF:
                    {
                        cf.off();
                    }
                    break;
            }

        }
    }
    public class CeleingFanHighCommand : CeleingFanCommonClassCommand
    {
        public CeleingFanHighCommand(CeleingFan cf)
        {
            this.cf = cf;
        }
        public override void execute()
        {
            prevSpeed = cf.Speed;
            cf.high();
        }  
    }
    public class CeleingFanMediumCommand : CeleingFanCommonClassCommand
    {
        public CeleingFanMediumCommand(CeleingFan cf)
        {
            this.cf = cf;
        }
        public override void execute()
        {
            prevSpeed = cf.Speed;
            cf.medium();
        }
    }
    public class CeleingFanLowCommand : CeleingFanCommonClassCommand
    {
        public CeleingFanLowCommand(CeleingFan cf)
        {
            this.cf = cf;
        }
        public override void execute()
        {
            prevSpeed = cf.Speed;
            cf.low();
        }
    }
    public class CeleingFanOffCommand : CeleingFanCommonClassCommand
    {   
        public CeleingFanOffCommand(CeleingFan cf)
        {
            this.cf = cf;
        }
        public override void execute()
        {
            prevSpeed = cf.Speed;
            cf.off();
        }
    }
    /////////////////////////////////
    public class MacroCommand:ICommand
    {
        ICommand[] commands;
        public MacroCommand(ICommand[] commands)
        {
            this.commands = commands;
        }
        public void execute()
        {
            foreach (var command in commands)
            {
                command.execute();
            }
        }
        public void undo()
        {
            foreach (var command in commands)
            {
                command.undo();
            }
        }
    }
    #endregion

    public class SimpleRemoteControl
    {
        ICommand slot;
        public void SetCommand(ICommand command)
        {
            this.slot = command;
        }
        public void ButtonWasPressed()
        {
            slot.execute();
        }
        internal int testc { get; set; }
    }
    public class RemoteControl
    {
        ICommand[] OnCommands;
        ICommand[] OffCommands;
        ICommand previous;
        public RemoteControl()
        {
            OnCommands = new ICommand[7];
            OffCommands = new ICommand[7];
            previous = null;

            for (int i = 0; i < 7; ++i)
            {
                OnCommands[i] = null;
                OffCommands[i] = null;
            }

        }
        public void SetCommand(int islot, ICommand OnCommand, ICommand OffCommand)
        {
            try
            {
                OnCommands[islot] = OnCommand;
                OffCommands[islot] = OffCommand;
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void onButtonWasPressed(int ibutton)
        {
            try
            {
                OnCommands[ibutton].execute();
                previous = OnCommands[ibutton];
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void offButtonWasPressed(int ibutton)
        {
            try
            {
                OffCommands[ibutton].execute();
                previous = OffCommands[ibutton];
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void undoButtonWasPushed()
        {
            if (previous != null) previous.undo();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n--------Remote Control--------\n");
            for(int i = 0;i< OnCommands.Length; ++i)
            {
                sb.Append("[slot " + i + " ]" + OnCommands.GetType().Name + "  " + OffCommands.GetType().Name + "\n");
            }
            sb.Append("undo " + OnCommands.GetType().Name + "  " + OffCommands.GetType().Name + "\n");
            return sb.ToString();
        }
    }

    
    class Command
    {

        /*Command pattern encapsulates a request as an object thereby letting you parameterize other objects with
         * different requests,queue or log requests and supports undoable operations
         * ********************************************************************************
         * Each command object(with execute method) has a reference to its receiver.
         * by binding a set of specific actions on the receiver
         * */

        /* Command pattern allows to decouple a requester object from the object that performs the action
         * command objects incapsulates a request to do smth. on specific object 
         * */

        /*Command objects represent the behavioe of each corresponding class to implement with an object of its certain class
     * within that behavior
         * Command objects invoke actions on the receiver(object that is in the command object)
         * 
         * to impelemnt the history of undo operations one has to have a stack of objects were called 
         * and undo operations takes off the most recently called object from the top of the stack
         * */

        /* when you need to decouple an object making requests from the object that knows how to perform the 
         * requests use command pattern
         * 
         *  - encapsulates the receiver with set of actions
         *  - an invoker makes a reques on the command object by calling its execute() method  which invokes thoses actions
         *  on the receiver
         *  - invokers can by parameterized with commands even at runtime
         *  - commands may support undo() by implementing undo method that restores the object to its previous state
         *  before the execute() method was invoked
         *  - Macro commands are simple extension of commands that allow multiple commands to by invoked.Can easly support undo()
         *  - Smart commands can perform the requests on themselves rather then delegating to the receiver.
         *  - commands can also be used for logging and transacional systems.
         * */
    }
}
