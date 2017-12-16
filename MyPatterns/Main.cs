using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adapter = MyPatterns.Adapter;
using IterComp = MyPatterns.Iterator_and_Composite;
using com = MyPatterns.Compound;
namespace MyPatterns
{
    /* ********************design principles***************************
     * 1)encapsulate what varies    
     * 2)favor compostion over inheritance
     * 3)program to interface not to implementation   
     * 4)strive looseley coupled design beween ojects that interact     
     * 5)classed should be open for extension and closed for modification
     * 6)depend upon abstractions. Do not depend upon concrete classes
     * 7)talk only to your immediate friends
     * 8)hollywood principle 
     * 9)single responsibility
     * OO principles
     * 1) dependancy inversion principle :depend upon abstractions. Do not depend upon concrete classes
     * (high level components(classes with behavior defined in terms of other low level comp) should not depend on the low level conmonents
     *guidlines principles:
     * -  no variable should hold a reference to a conctrete class(use a factory to get around that)
     * - no class should derive from a cocrete class
     * - no method should override an implemented method of any of its base classes
     * 7) be carefull of the number of classes it interects with and also how it  comes to interect with those classes
     * this principle prevents from creating designs that have a large number of classes coupled together 
     * so that changes in one part of the system cascade to other parts. And that can lead to a building 
     * a fragile system that will be costly to maintain and complex for others to understand:
     * public float method(){
     *  return station.getparam().getsec();
     * }
     * this is a bad implementation because object station depends on the predetemined objects with methods
     *  
     * the guidline:
     * take any object now from any method in that object and we should invoke methods that belong to:
     *  - the object itself
     *  - objects passed in as a parameter in the method
     *  - any objects the method created or instatiates 
     *  - any components of the object  
     *    
     *    // a bad implementation
     * public float method(){
     *   Thermometer t = station.getparam();
     *   return t..getsec();
     * }
     * 
     * // a good implementation
     * public float method(){
     *  return station.getsec();
     * }
     * put in the class station object this reduces the number of classes we are dependent on
     * 
     * 8) prvents "dependency rot" it happens when there are high level components depending on the
     * low level components or side level components
     * its a technique for building frameworks or components so that low level components can be hooked 
     * into the computation but without creating a dependency between low level and hight level components
     * (framework controls smth gets done but leaves you to specify the details that are actually happening at each step)
     * 9) every responsibility of a class is a potential area of change(more different methods in classes more areas to change)
     * this guids to keep each class to a single responsibility
     * (if class  takes a responsibility for iteration of inner collections so it can change if the
     * collection is changed and also it keeps its main responsibilities)
     * assign one responsibility to only one class
     */
    using MyPatterns.Compound;
    class Programs
    {
        static void Main(string[] args)
        {
            #region StrategyPattern
            MyFlyingDuck myfly = new MyFlyingDuck(new FlyWithWings(), " duck");

            myfly.doFly();
            myfly.ChangeFlyBehavior = new RocketFlyBehavior();
            myfly.doFly();
            #endregion

            #region ObserverPattern
            //WeatherData wd = new WeatherData();
            //PressureDisplay pd = new PressureDisplay(wd);
            //CurrentConditionDisplay ccd = new CurrentConditionDisplay(wd);
            //wd.SetMeasurements(25, 80, 880);
            //ccd.Unsubscribe();
            //wd.SetMeasurements(34, 66, 820);
            //wd.SetMeasurements(25, 47, 790);
            #endregion

            #region Decorator
            //Beverage bv1 = new Espresso();
            //bv1 = new Mocha(bv1);
            //bv1 = new Mocha(bv1);
            //Beverage bv2 = new DarkRoasted();
            //Console.WriteLine(bv1.GetDescription() + " " + bv1.Cost());

            //bv2 = new Soy(bv2);
            //bv2 = new Whip(bv2);
            //bv2 = new Mocha(bv2);

            //Console.WriteLine(bv2.GetDescription() + " " + bv2.Cost());

            #endregion

            #region Factory
            #region FactoryMethod
            //PizzaStore nyStore = new NYPizzaStore();
            //PizzaStore chStore = new ChicagoPizzaStore();
            //Pizza nyPizza = nyStore.orderPizza("CheesePizza");
            //Pizza chPizza = chStore.orderPizza("CheesePizza");
            /*
             * superclass had never had to know details the subclass handled all that just by 
             * instantiating the right pizza
             */
            #endregion

            //Pizza chp = new CheesePizza(new CHicagoPizzaIngredientFactory());
            //Pizza clp = new ClamPizza(new NYPizzaIngredientFactory());
            //chp.Name = "CheesePizza";
            //chp.prepare();
            //clp.Name = "ClamPizza";
            //clp.prepare();
            //Console.WriteLine(chp);
            //Console.WriteLine(clp);

            //PizzaStore nyPizzaStore = new NYPizzaStore();
            //nyPizzaStore.orderPizza("CheesePizza");
            //PizzaStore chPizzaStore = new ChicagoPizzaStore();
            //chPizzaStore.orderPizza("CheesePizza");
            #endregion

            #region Singleton
            //Singleton s = Singleton.getInstance();
            //Singleton s2 = Singleton.getInstance();
            //Console.WriteLine( s.Equals(s2));
            #endregion

            #region Command

            RemoteControl rc = new RemoteControl();
            Light livingRoomLight = new Light("LivingRoom");
            Light KitchenLight = new Light("Kitchen");
            GarageDoor doorGarage = new GarageDoor();
            Stereo stereo = new Stereo();

            LightOnCommand loc1 = new LightOnCommand(livingRoomLight);
            LightOffCommand loffc1 = new LightOffCommand(livingRoomLight);
            LightOnCommand loc2 = new LightOnCommand(KitchenLight);
            LightOffCommand loffc2 = new LightOffCommand(KitchenLight);

            GarageDoorOpenCommand gdo = new GarageDoorOpenCommand(doorGarage);
            GarageDoorCloseCommand gdc = new GarageDoorCloseCommand(doorGarage);

            StereoOnWithCDCommand sowc = new StereoOnWithCDCommand(stereo);
            StereoOffCommand soff = new StereoOffCommand(stereo);

            rc.SetCommand(0, loc1, loffc1);
            rc.SetCommand(1, loc2, loffc2);
            rc.SetCommand(2, gdo, gdc);
            rc.SetCommand(3, sowc, soff);

            Console.WriteLine(rc);

            //for (int i = 0; i < 4; i++)
            //{
            //    rc.onButtonWasPressed(i);
            //    rc.offButtonWasPressed(i);
            //    rc.undoButtonWasPushed();
            //}
            /////////////////////////////////////////////////////////////////

            CeleingFan cf = new CeleingFan("Living Room");
            CeleingFanCommonClassCommand high = new CeleingFanHighCommand(cf);
            CeleingFanCommonClassCommand medium = new CeleingFanMediumCommand(cf);
            CeleingFanCommonClassCommand low = new CeleingFanLowCommand(cf);
            CeleingFanCommonClassCommand off = new CeleingFanOffCommand(cf);

            RemoteControl remote = new RemoteControl();
            remote.SetCommand(0, high, off);
            remote.SetCommand(1, medium, off);
            remote.SetCommand(2, low, off);

            //for (int i = 0; i < 3; i++)
            //{
            //    remote.onButtonWasPressed(i);
            //   // remote.offButtonWasPressed(i);    
            //    Console.WriteLine(remote);
            //   // remote.undoButtonWasPushed();
            //}

            //remote.onButtonWasPressed(0);
            //remote.undoButtonWasPushed();

            MacroCommand macroDo = new MacroCommand(new ICommand[] { loc1, loc2, gdo, sowc });
            MacroCommand macroUndo = new MacroCommand(new ICommand[] { loffc1, loffc2, gdc, soff });

            RemoteControl remoteControlMacroCommand = new RemoteControl();
            remoteControlMacroCommand.SetCommand(0, macroDo, macroUndo);
            remoteControlMacroCommand.onButtonWasPressed(0);
            remoteControlMacroCommand.undoButtonWasPushed();
            remoteControlMacroCommand.offButtonWasPressed(0);
            remoteControlMacroCommand.undoButtonWasPushed();

            #endregion

            #region Adapter and Facade

            //Adapter.MalardDuck duck = new Adapter.MalardDuck();
            //Adapter.WildTurkey turkey = new Adapter.WildTurkey();
            //Adapter.TurkeyAdapterToIDuck turkeyAdapter = new Adapter.TurkeyAdapterToIDuck(turkey);
            //Adapter.TestDuck testduck = new Adapter.TestDuck();

            //testduck.Test(turkeyAdapter); // the Iduck interface in here  it never knows its a turkey disguised as a duck
            ////testClass.Test(duck); // works the same way

            //Adapter.DuckAdapterToITurkey duckdapter = new Adapter.DuckAdapterToITurkey(duck);

            //Adapter.TestTurkey testturkey = new Adapter.TestTurkey();

            //testturkey.Test(duckdapter);
            #endregion

            #region TemplateMethod

            //Template.Coffee coffee = new Template.Coffee();
            //coffee.PrepareRecipe();

            //Template.Duck[] ducks =
            //    {
            //    new Template.Duck("Lucy",5),
            //    new Template.Duck("Bob",6),
            //    new Template.Duck("Jack",1)
            //    };
            //foreach(var duck in ducks)
            //{
            //    Console.WriteLine(duck);
            //}

            //Array.Sort(ducks);
            //foreach (var duck in ducks)
            //{
            //    Console.WriteLine(duck);
            //}
            #endregion

            #region Iterator and Composite

            #region Iterator
            //IterComp.Weitress w = new IterComp.Weitress(new IterComp.PancakeHouseMenu(), new IterComp.DinerMenu(),new IterComp.CafeMenu());
            //w.OldPtint();
            //w.Ptint();
            //IterComp.PancakeHouseMenu pancake = new IterComp.PancakeHouseMenu();
            //IterComp.DinerMenu dinner = new IterComp.DinerMenu();

            //foreach (var i in pancake)
            //{
            //    Console.WriteLine(((Iterator.MenuItem)i).Name  + ((Iterator.MenuItem)i).Price);
            //}

            //foreach (var i in dinner)
            //{
            //   // Console.WriteLine(((Iterator.MenuItem)i).Name + ((Iterator.MenuItem)i).Price);
            //    Console.WriteLine(i);
            //}
            #endregion

            #region Composite

            //IterComp.MenuComponent pacncackeHouse = new IterComp.Menu("PancakeHouseMune", "Breakfast");
            //IterComp.MenuComponent DinnerMenu = new IterComp.Menu("DinnerMenu", "Lunch");
            //IterComp.MenuComponent CaffeeMenu = new IterComp.Menu("CaffeeMenu", "Dinner");

            //IterComp.MenuComponent allmenus = new IterComp.Menu("AllMenus", "all combined");
            //allmenus.add(pacncackeHouse);
            //allmenus.add(DinnerMenu);
            //allmenus.add(CaffeeMenu);

            //DinnerMenu.add(new IterComp.MenuItem(description: "Burger", name: "Burger on wheat bun", vegeterian: false, price: 3.2));
            //DinnerMenu.add(new IterComp.MenuItem(description: "Burger", name: "Burger on wheat bun", vegeterian: false, price: 3.2));
            //DinnerMenu.add(new IterComp.MenuItem(description: "Burger", name: "Burger on wheat bun", vegeterian: false, price: 3.2));
            //DinnerMenu.add(CaffeeMenu);
            //allmenus.add(new IterComp.MenuItem(description: "Burger", name: "Burger on wheat bun", vegeterian: false, price: 3.2));

            ////cannt add MenuItem to MenuItem!!!Must check for type safety!!!Or reimplenet it 
            //// separate responsibilities into different itefaces!!! interafce hierachy(my thought)
            ////allmenus.getChild(3).add(new IterComp.MenuItem(description: "Burger", name: "Burger on wheat bun", vegeterian: false, price: 3.2));

            //IterComp.WaitressComposite w = new IterComp.WaitressComposite(allmenus);
            //w.print();
            //#endregion

            //Console.WriteLine("--------------------------************************************---------------------------------------------");
            //foreach (IterComp.MenuComponent v in allmenus)
            //{
            //    v.print();
            //}

            #endregion

            #endregion

            #region State

            //GumballMachineTest g = new GumballMachineTest(5);
            //g.insert();
            //g.turnCrank();
            //Console.WriteLine(g);

            //g.insert();
            //g.ejectQuarter();
            //g.turnCrank();

            //Console.WriteLine(g);

            //g.insert();
            //g.turnCrank();
            //g.insert();
            //g.ejectQuarter();

            //Console.WriteLine(g);
            //g.insert();
            //g.turnCrank();
            //g.insert();
            //g.turnCrank();
            //g.insert();
            //g.turnCrank();
            //g.turnCrank();
            //Console.WriteLine(g);

            //GumBallMachine gumball = new GumBallMachine(5);
            //gumball.InsertQuarter();
            //gumball.turnCrank();
            //Console.WriteLine(gumball);
            //gumball.InsertQuarter();
            //gumball.turnCrank();
            //Console.WriteLine(gumball);
            //gumball.InsertQuarter();
            //gumball.turnCrank();
            #endregion

            #region Compound
            //stategey

            //Quackable mallardDuck = new MallardDuck();
            //Quackable readHeadDuck = new ReadHeadDuck();
            //mallardDuck.quack();
            //readHeadDuck.quack();

            //adapter

            //GooseAdapter gooseAdapter = new GooseAdapter(new Goose());
            //gooseAdapter.quack();
            //decorator

            //Quackable mallardDuck = new QuackableCounter(new MallardDuck());
            //Quackable readHeadDuck = new QuackableCounter(new ReadHeadDuck());
            //mallardDuck.quack();
            //readHeadDuck.quack();
            //QuackableCounter.getQuacks();

            //factory
            AbstractFactory factory = new DuckCountingFactory();
            Quackable mallardDuck = factory.createMallardDuck();
            Quackable readHeadDuck = factory.createRedHeadDuck();
            //mallardDuck.quack();
            //readHeadDuck.quack();
            //QuackableCounter.getQuacks();

            // composite and iterator
            Flock flock = new Flock();

            Quackologist quackologist = new Quackologist();

            flock.add(mallardDuck);
            flock.add(readHeadDuck);
            flock.add(factory.createRubberDuck());
            flock.add(factory.createDuckCall());

            flock.registerObserver(quackologist);

            flock.quack();


            // obsever


            #endregion
        }

    }

}









