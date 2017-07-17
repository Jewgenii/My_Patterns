using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{       
    /*
     * Factory method pattern defines an interface for creating an object but lets
     * basclasses decide which class no instatiate. Factory Method lets a class defer
     * (подчиняться, уступать (чьим-л. желаниям);полагаться (на кого-л.))
     * instantiation to subclasses.
     * factory method decouples products implementation  form its use so
     * it can be changed for without creator class modification
     * Factories handle the details of object creattion*/

    /*Abstract Facroty Pattern provides an interface for creating 
     * families  of related or dependent objects without specifying 
     * their concrete classes.
     */
    /*absctract factory gives an interface for creating a family of products
    * by writing code that uses this interface we decouple code from actual factory that 
    * creates the product.
    * From abstract factory we derive one or more concrete factories that
    * produce the same products, but with different implementations.
    * By passing in a variety of factories we get a variaty of implementations of
    * those products. And the client code stays the same.
    * */

    /*1. Factory method uses classes to create(through inheritance to extand base class
     *  and override factory method(that creates objects)) :
     *  clients need to know only abstract type they are using the subclasses worries about concrete type 
     *  so clients remain decoupled from concrete types.
     *  it creates one product
     * 2. abstract factory  - objects(through object composition)
     * provides an abstract type for creating a family of products.
     * Subclasses of this type define how those products are produced.
     * to use  you instatiate one and pass it into some code that is written against abstract method
     * it groups together a set related products
     * if to add new product so the code in the abstract factory has to be changed(added)
     * and change every subclass
     * */

    /* Both incapsulate object creation to keep applications loosely coupled and less dependent on 
     * implementations, create when:
     * abstract factory - > have a families of related products and client create products that belong together
     *  Factory method - > to decouple client code from concrete classes you need to instatiate
     *   or if you dont know ahead of time all the concrete classes you are going to need.  
     */
    public abstract class Pizza
    {
        protected string name;
        protected Dough dough;
        protected Sauce sauce;
        protected Veggies[] veggies;
        protected Cheese cheese;
        protected Pepperoni pepperoni;
        protected Clams clams;
        protected IngredientFactory ingredientFactory;
        //public virtual void prepare()
        //{
        //    Console.WriteLine("Preparing..." + name);
        //    Console.WriteLine("Tossing dough..." + dough);
        //    Console.WriteLine("adding sauce..." + sauce);
        //    Console.WriteLine("adding toppings:");
        //    foreach (var testc in toppings)
        //    {
        //        Console.WriteLine(testc);
        //    }
        //}
        public abstract void prepare();// we adhere some special ingredients to certrain pizzas
        public virtual void bake()
        {
            Console.WriteLine("Baking at 350 degrees for 25 minutes");
        }
        public virtual void cut()
        {
            Console.WriteLine("Cutting into slices");
        }
        public virtual void box()
        {
            Console.WriteLine("Placing Pizza into officieal boxes");
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
    #region PizzaTypes
    public class CheesePizza : Pizza // accepts any ingredients
    {
        public CheesePizza(IngredientFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void prepare()//taking some special types of Condiments not all or all if needed
        {
            Console.WriteLine("Preparing " + base.name);
            dough = ingredientFactory.createDough();
            sauce = ingredientFactory.createSauce();
            cheese = ingredientFactory.createCheese();
        }
        public override string ToString()
        {
            string s = null;
            s = "Pizza: " + name + "\n";
            s += "dough " + dough + "\n";
            s += "sauce " + sauce + "\n";
            return s;
        }
    }
    public class ClamPizza : Pizza // accepts any ingredients
    {
        public ClamPizza(IngredientFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }

        public override void prepare()//taking some special types of Condiments not all or all if needed
        {
            Console.WriteLine("Preparing " + base.name);
            dough = ingredientFactory.createDough();
            sauce = ingredientFactory.createSauce();
            cheese = ingredientFactory.createCheese();
            clams = ingredientFactory.createClams();
        }
        public override string ToString()
        {
            string s = null;
            s = "Pizza: " + name +  "\n";
            s += "dough " + dough + "\n";
            s += "sauce " + sauce + "\n";
            s += "clams " + clams + "\n";
            return s;
        }
    }
    #endregion

    #region Ingredients
  
    public abstract class Pepperoni
    {

    }

    public class SlicedPeperroni : Pepperoni
    {
        public override string ToString()
        {
            return "SlicedPeperroni";
        }
    }

    public abstract class Veggies
    {

    }

    public class Garlic : Veggies
    {
        public override string ToString()
        {
            return "Garlic";
        }
    }
    public class Onion : Veggies
    {
        public override string ToString()
        {
            return "Onion";
        }
    }
    public class Mushroom : Veggies
    {
        public override string ToString()
        {
            return "Mushroom";
        }
    }
    public class RedPepper : Veggies
    {
        public override string ToString()
        {
            return "RedPepper";
        }
    }

    public abstract class Sauce
    {
    }

    public class MarinaraSauce : Sauce
    {
        public override string ToString()
        {
            return "MarinaraSauce";
        }
    }
    public class PlumTomatoSauce : Sauce
    {
        public override string ToString()
        {
            return "PlumTomatoSauce";
        }
    }

    public class Spinch : Veggies
    {
        public override string ToString()
        {
            return "Spinch";
        }
    }

    public class EggPlant : Veggies
    {
        public override string ToString()
        {
            return "EggPlant";
        }
    }
    public class BlackOlives : Veggies
    {
        public override string ToString()
        {
            return "BlackOlives";
        }
    }

    public abstract class Dough
    {
    }

    public class ThingCrustDough : Dough
    {
        public override string ToString()
        {
            return "ThingCrustDough";
        }
    }
    public class ThickCrustDough : Dough
    {
        public override string ToString()
        {
            return "ThickCrustDough";
        }
    }

    public abstract class Cheese
    {
    }
    public class ReggianoCheese : Cheese
    {
        public override string ToString()
        {
            return "ReggianoCheese";
        }
    }
    public class MozzarellaCheese : Cheese
    {
        public override string ToString()
        {
            return "MozzarellaCheese";
        }
    }
    public class Clams
    {

    }
    public class FreshClams : Clams
    {
        public override string ToString()
        {
            return "FreshClams";
        }
    }
    public class FrozenClams : Clams
    {
        public override string ToString()
        {
            return "FrozenClams";
        }
    }

    #endregion

    #region IngredientFactory

    public interface IngredientFactory // absctract factory
    {
        Dough createDough();
        Sauce createSauce();
        Cheese createCheese();
        Veggies[] createVeggies();
        Pepperoni createPepperoni();
        Clams createClams();
    }

    public class NYPizzaIngredientFactory : IngredientFactory
    {
        public Dough createDough()
        {
            return new ThingCrustDough();
        }
        public Sauce createSauce()
        {
            return new MarinaraSauce();
        }
        public Cheese createCheese()
        {
            return new ReggianoCheese();
        }
        public Veggies[] createVeggies()
        {
            Veggies[] veggies = { new Garlic(), new Onion(), new Mushroom(), new RedPepper() };
            return veggies;
        }
        public Pepperoni createPepperoni()
        {
            return new SlicedPeperroni();
        }
        public Clams createClams()
        {
            return new FreshClams();
        }
    }

    public class CHicagoPizzaIngredientFactory : IngredientFactory
    {
        public Dough createDough()
        {
            return new ThickCrustDough();
        }
        public Sauce createSauce()
        {
            return new PlumTomatoSauce();
        }
        public Cheese createCheese()
        {
            return new MozzarellaCheese();
        }
        public Veggies[] createVeggies()
        {
            Veggies[] veggies = { new BlackOlives(), new Spinch(), new EggPlant() };
            return veggies;
        }
        public Pepperoni createPepperoni()
        {
            return new SlicedPeperroni();
        }
        public Clams createClams()
        {
            return new FrozenClams();
        }
    }
    #endregion

    //#region commonPizzas
    //public class CheesePizza : Pizza
    //{

    //    public CheesePizza() : base()
    //    {
    //        this.name = "CheesePizza";
    //    }

    //}
    //public class PeperroniPizza : Pizza
    //{

    //    public PeperroniPizza() : base()
    //    {
    //        this.name = "PeperroniPizza";
    //    }

    //}
    //public class ClamPizza : Pizza
    //{

    //    public ClamPizza() : base()
    //    {
    //        this.name = "ClamPizza";
    //    }
    //}
    //#endregion
    //#region NYPizzaStyle

    //public class NYCheesePizza : Pizza
    //{
    //    public NYCheesePizza()
    //    {
    //        this.name = "NewYorkPizza";
    //        this.toppings.Add("Grated Reggiano cheese");
    //        this.sauce = "Plum tomato sauce";
    //        this.dough = "Extra thick crust dough";
    //    }
    //    public override void cut()
    //    {
    //        Console.WriteLine("Slice into square slices");
    //    }
    //}
    //public class NYPeperroniPizza : Pizza
    //{

    //    public NYPeperroniPizza() : base()
    //    {
    //        this.name = "NYPeperroniPizza";
    //    }

    //}

    //public class NYClamPizza : Pizza
    //{

    //    public NYClamPizza() : base()
    //    {
    //        this.name = "NYClamPizza";
    //    }

    //}
    //#endregion
    //#region ChicagoPizzaStyle

    //public class ChicagoCheesePizza : Pizza
    //{
    //    public ChicagoCheesePizza()
    //    {
    //        this.name = "ChicagoPizza";
    //        this.toppings.Add("Grated Reggiano cheese");
    //        this.sauce = "Marina sauce";
    //        this.dough = "the crust dough";
    //    }
    //}
    //public class ChicagoPeperroniPizza : Pizza
    //{
    //    public ChicagoPeperroniPizza() : base()
    //    {
    //        this.name = "ChicagoPeperroniPizza";
    //    }
    //}

    //public class ChicagoClamPizza : Pizza
    //{
    //    public ChicagoClamPizza() : base()
    //    {
    //        this.name = "ChicagoClamPizza";
    //    }

    //}
    //#endregion

    //class SimplePizzaFactory
    //{
    //    public Pizza createPizza(string name)
    //    {
    //        Pizza p = null;
    //        if (name == "ClamPizza") p = new ClamPizza();
    //        else if (name == "PeperroniPizza") p = new PeperroniPizza();
    //        else if (name == "CheesePizza") p = new CheesePizza();
    //        return p;
    //    }
    //}

    #region PizzaStores
    abstract class PizzaStore
    {
        /*PizzaStore store method  createPizza(string type) decides what kind of pizza is gonna
         * be created,not the SimplePizzaFactory in the PizzaStore
         * subclassed of this class dont dynamically determine which store to create pizza from(i choose store)
         * but each store which kind of pizza from the certain store to create (store decides what pizza type)
         * */
        protected IngredientFactory ingredientFactory;
        public virtual Pizza orderPizza(string name)
        {
            Pizza pizza = createPizza(name);// of the same interface    
            pizza.prepare();
            pizza.bake();
            pizza.cut();
            pizza.box();
            return pizza;
        }
        protected abstract Pizza createPizza(string type); // factory method that subclasses implement 
        // to produce some concrte products
        // sublclassed that produce products are called concrete creators
        // its free to implements its own styles of createPizza method 
        // factory method lets to vary the products its creating by subclassing
        /* a factory method handles object creation and encapsulates it in a subclass.
         * This decouples(расцеплять разъединять)
         *  the client code in the superclass from the object creation code
         * in the subclass
         * 
         * abstract Product factoryMethod(string type)
         * 
         * abstract: so the subclasses are counted on to handle object creation
         * Product: return the a Product that is typically used within methods defined in the superclass
         * factoryMethod: isolates the client(the code in the superclass) from knowing what kind 
         * of concrete Product is actually created
         */
    }
    class NYPizzaStore : PizzaStore
    {
        public NYPizzaStore()
        {
            ingredientFactory = new NYPizzaIngredientFactory();
        }
        protected override Pizza createPizza(string name) // factory method
        {
            Pizza p = null;
            if (name == "ClamPizza")
            {
                p = new ClamPizza(ingredientFactory);
                p.Name = "NYPizzaStore.ClamPizza";
            }
            else if (name == "CheesePizza")
            {
                p = new CheesePizza(ingredientFactory);
                
                p.Name = "NYPizzaStore.CheesePizza";
            }
            return p;
        }
    }
    class ChicagoPizzaStore : PizzaStore
    {
        public ChicagoPizzaStore()
        {
            ingredientFactory = new CHicagoPizzaIngredientFactory();
        }

        protected override Pizza createPizza(string type)// factory method
        {
            Pizza p = null;
            if (type == "ClamPizza")
            {
                p = new ClamPizza(ingredientFactory);
                p.Name = "ChicagoPizzaStore.ClamPizza";
            }
            else if (type == "CheesePizza")
            {
                p = new CheesePizza(ingredientFactory);
                p.Name = "ChicagoPizzaStore.CheesePizza";
            }

            return p;
        }
    }
    #endregion

    /*Benefits of factories:
     * by placing creation code in one method or object 
     * avoid duplication in code 
     * a place for maintenence of code 
     * clients depend on the interfaces rather than on concrete classes required to instantiate objects
     * this allows to program to interface not to an implementation 
     * */

}
