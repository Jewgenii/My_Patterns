using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatterns
{
    /*****************************ITERATOR Pattern********************************************
     * povides a way to access the elements of an aggregate object sequentially 
     * without exposing its underlying implementation
     * */
    namespace Iterator_and_Composite
    {
        public interface Iterator
        {
            bool hasNext();
            object next();
        }
        public interface IMenu
        {
            Iterator CreateIterator();
        }
        //public class MenuItem
        //{
        //    private string description;
        //    private double price;
        //    private string name;
        //    private bool vegeterian;

        //    public string Description { get { return description; } }
        //    public double Price { get { return price; } }
        //    public string Name { get { return name; } }
        //    public bool Vegeterian { get { return vegeterian; } }

        //    public MenuItem(string description,
        //                         double price,
        //                         string name,
        //                         bool vegeterian)
        //    {
        //        this.description = description;
        //        this.price = price;
        //        this.name = name;
        //        this.vegeterian = vegeterian;
        //    }
        //}

        #region Iterators
        internal class DinerMenuIterator : Iterator, IEnumerator
        {
            MenuItem[] menuItems;
            int position = 0;
            public DinerMenuIterator(MenuItem[] menuItems)
            {
                this.menuItems = menuItems;
                menuItems.GetEnumerator();
            }

            public object Current
            {
                get
                {
                    return menuItems[position];
                }
            }
            public bool MoveNext()
            {
                ++position;
                if (position >= menuItems.Length || menuItems[position] == null)
                    return false;
                else
                {
                    return true;
                }

            }
            public void Reset()
            {
                position = -1;
            }

            public bool hasNext()
            {
                if (position >= menuItems.Length || menuItems[position] == null)// check if the next is null because it concerns the array
                                                                                // that has max size, but not all positions within the array could be filled in
                    return false;
                else
                    return true;
            }
            public object next()
            {
                MenuItem mi = menuItems[position++];
                return mi;
            }
        }
        internal class PancakeHouseIterator : Iterator, IEnumerator
        {
            ArrayList menuItems;
            int position = 0;

            public PancakeHouseIterator(ArrayList menuItems)
            {
                this.menuItems = menuItems;
                menuItems.GetEnumerator();
            }
            /*standard methods*/
            public object Current
            {
                get
                {
                    return menuItems[position];
                }
            }
            public bool MoveNext()
            {
                if (++position >= menuItems.Count || menuItems[position] == null)// check if the next is null because it concerns the array
                                                                                 // that has max size, but not all positions within the array could be filled in
                    return false;
                else
                    return true;
            }
            public void Reset()
            {
                position = -1;
            }

            /* my methods*/
            public bool hasNext()
            {
                if (position >= menuItems.Count || menuItems[position] == null)// check if the next is null because it concerns the array
                                                                               // that has max size, but not all positions within the array could be filled in
                    return false;
                else
                    return true;
            }
            public object next()
            {
                return menuItems[++position];
            }
        }
        internal class CafeMenuIterator : Iterator
        {
            Hashtable h;
            object[] a;
            int i = 0;
            public CafeMenuIterator(Hashtable h)
            {
                h = new Hashtable();
                this.h = h;
                a = new object[h.Count];
                h.Keys.CopyTo(a, 0);
            }

            public bool hasNext()
            {
                return i < h.Count;
            }

            public object next()
            {
                return h[a[++i]];
            }
        }
        #endregion

        public class PancakeHouseMenu : IEnumerable, IMenu
        {
            private ArrayList menuItems;
            public PancakeHouseMenu()
            {
                menuItems = new ArrayList();
                this.AddItem("some pancake breakfast", 2.99d, "Pancake with scrumbled eggs and toast", true);
                this.AddItem("regular pancake breakfast", 2.49d, "Pancake with fried eggs and sausage", false);
                this.AddItem("Bluberry pancake", 5.59d, "Pancakes with fresh bluberries", true);
                this.AddItem("Waffles", 1.11d, "your waffles", true);
            }

            public void AddItem(string description,
                                 double price,
                                 string name,
                                 bool vegeterian)
            {
                menuItems.Add(new MenuItem(description: description, name: name, vegeterian: vegeterian, price: price));
            }

            //public ArrayList GetMenuItems()
            //{
            //    return this.menuItems;
            //}

            public Iterator CreateIterator()
            {
                return new PancakeHouseIterator(menuItems);
            }

            public IEnumerator GetEnumerator()
            {
                return menuItems.GetEnumerator();
            }
        }

        public class DinerMenu : IEnumerable, IMenu
        {
            public static readonly int maxItems = 6;
            MenuItem[] menuItems;
            int currentItem = 0;

            public DinerMenu()
            {
                menuItems = new MenuItem[maxItems];
                this.AddItem("vegeterian food", 4.12d, "some vegeterian meal", true);
                this.AddItem("blt", 5.5d, "bacon meat", false);
                this.AddItem("soup of the day", 1.92d, "soup", true);
                this.AddItem("hot dog", 3.05d, "tasty wrpped cat", false);
            }

            public void AddItem(string description,
                               double price,
                               string name,
                               bool vegeterian)
            {
                MenuItem mi = new MenuItem(description: description, price: price, name: name, vegeterian: vegeterian);
                if (this.currentItem >= DinerMenu.maxItems)
                {
                    Console.WriteLine("Cannot add anymore");
                }
                else
                {
                    this.menuItems[this.currentItem++] = mi;
                }
            }
            //public MenuItem[] GetItems()
            //{
            //    return menuItems;
            //}

            // one doesnt need to know how the collection of items is maintained 
            //nor does it need to know how the DinerMenuIterator is implemented
            // it steps through the collection of items
            public Iterator CreateIterator()
            {
                return new DinerMenuIterator(menuItems);
            }

            public IEnumerator GetEnumerator()
            {
                return new DinerMenuIterator(menuItems);
                // return menuItems.GetEnumerator();

                //return menuItems.GetEnumerator(); if an array contains not standart types like UDC
                //then if you create an enumerator so you have to implement Ienumerable interface and 
                // provide a claass which implements Ienumetator interface and accepts 
                //the collection of some UDC(user definides classes)
            }
        }

        public class CafeMenu : IEnumerable, IMenu
        {
            private Hashtable menuItems;
            public CafeMenu()
            {
                menuItems = new Hashtable();
                addItem("Burger", "Burger on wheat bun", false, 3.2);
                addItem("Soup of the day", "a cup of the soup", true, 2.2);
            }
            public void addItem(string name, string description, Boolean vegeterian, double price)
            {
                MenuItem mi = new MenuItem(description: description, price: price, name: name, vegeterian: vegeterian);
                this.menuItems.Add(mi.Name, mi);
            }
            public IEnumerator GetEnumerator()
            {
                return menuItems.Values.GetEnumerator();
            }
            public Iterator CreateIterator()
            {
                return new CafeMenuIterator(menuItems);
            }
        }

        public class Weitress : IEnumerable
        {
            private IEnumerable pancakeHouseMenu;
            private IEnumerable dinerMenu;
            private IEnumerable cafeMenu;
            private ArrayList Munues;

            public Weitress(IEnumerable pancakeHouseMenu, IEnumerable dinerMenu, IEnumerable cafeMenu)
            {
                this.dinerMenu = dinerMenu;
                this.pancakeHouseMenu = pancakeHouseMenu;
                this.cafeMenu = cafeMenu;
            }
            public Weitress(params IEnumerable[] ar)
            {
                Munues.AddRange(ar);
            }
            public Weitress(List<IEnumerable> ar)
            {
                Munues.AddRange(ar);
            }

            public void Ptint()
            {
                IEnumerator pancakeHouseIter = pancakeHouseMenu.GetEnumerator();
                IEnumerator dinnerIter = dinerMenu.GetEnumerator();
                IEnumerator cafeMenuIter = cafeMenu.GetEnumerator();

                Console.WriteLine("Breakfast:");
                this.PrintMenu(pancakeHouseIter);
                Console.WriteLine("Lunch:");
                this.PrintMenu(dinnerIter);
                Console.WriteLine("Cafe:");
                this.PrintMenu(cafeMenuIter);
            }
            public void OldPtint()
            {
                Iterator pancakeHouseIter = ((IMenu)pancakeHouseMenu).CreateIterator();
                Iterator dinnerIter = ((IMenu)dinerMenu).CreateIterator();
                Iterator cafeMenuIter = ((IMenu)cafeMenu).CreateIterator();

                Console.WriteLine("Breakfast:");
                this.PrintMenu(pancakeHouseIter);
                Console.WriteLine("Lunch:");
                this.PrintMenu(dinnerIter);
                Console.WriteLine("Cafe:");
                this.PrintMenu(cafeMenuIter);
            }
            // polymorphically handles any collection of items as long as it implements iterator
            //Weitress doesnt have to know about the implementation of collecions in the classes
            // Weitress can print any class returing the proper type of object 

            // reducing dependencies programming to interfaces in favour of classes!  
            // so any classs of the same interface can be used  
            private void PrintMenu(IEnumerator iter)
            {
                while (iter.MoveNext())
                {
                    MenuItem mi = (MenuItem)iter.Current;
                    Console.WriteLine(mi.Name + " " + mi.Price + " " + mi.Description);
                }
                iter.Reset();
            }
            private void PrintMenu(Iterator iter)
            {
                while (iter.hasNext())
                {
                    MenuItem mi = (MenuItem)iter.next();
                    Console.WriteLine(mi.Name + " " + mi.Price + " " + mi.Description);
                }
            }

            public IEnumerator GetEnumerator()
            {
                return pancakeHouseMenu.GetEnumerator();
            }
        }

        /*********************************Composite Pattern**********************************/
        /* allows to compose objects into tree structures to represent part-whole hierarchies.
         * Composite lets clients treat individual objects and compositions of objects uniformly*/
        /*allows to build structures of trees that contain both compositions of objects 
         * and individual objects as nodes */
        /*using a composite structure we can apply the the operations over the composite and 
         * individual objects in other words we can ignore the differences between
         compositions of objects and individual objects
         */

        /*its used when there are collections of objects with whole part relationships
         * like one component consists of other many the same components or different type components
         * but supporting the common interface(uniformly) having common methods
         * 
         * its a tree structure with haed node on the top
         * with leaves or nodes
         * 
         * - allow access to aggregates elements without exposing its internal structure
         * - takes the job of iterating  over an aggregate and encapsulates it in another object
         * - provides a common interface for traversing the items of an aggregate 
         * allowing   to use polymorphism  when writing code that makes use of the items of the aggregate 
         * - provides structure to hold both individual elements and composites 
         * - allows to treat both composites and elements uniformly
         * - a component is any object in composite structure
         * - lots of tradeoffs so need to balance between transparency
         * (the way how you trea elements in structure) and safety
         * */

        public abstract class MenuComponent : IEnumerable // to allow to iterate over the whole collection
                                                          // in all nodes
        {
            #region Composite Methods
            public virtual void add(MenuComponent menuComponent)
            {
                throw new ApplicationException("Unsuppoted operation");
            }
            public virtual void remove(MenuComponent menuComponent)
            {
                throw new ApplicationException("Unsuppoted operation");
            }
            public virtual MenuComponent getChild(int i)
            {
                throw new ApplicationException("Unsuppoted operation");
            }
            #endregion

            #region Operation methods used by MenuItems
            public virtual string getName()
            {
                throw new ApplicationException("Unsuppoted operation");
            }
            public virtual string getDescription()
            {
                throw new ApplicationException("Unsuppoted operation");
            }
            public virtual double getPrice()
            {
                throw new ApplicationException("Unsuppoted operation");
            }
            public virtual bool isVegeterian()
            {
                throw new ApplicationException("Unsuppoted operation");
            }
            public virtual void print()
            {
                throw new ApplicationException("Unsuppoted operation");
            }
            #endregion
            public virtual IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
        public class MenuItem : MenuComponent
        {
            private string description;
            private double price;
            private string name;
            private bool vegeterian;

            public string Description { get { return description; } }
            public double Price { get { return price; } }
            public string Name { get { return name; } }
            public bool Vegeterian { get { return vegeterian; } }

            public MenuItem(string description,
                                 double price,
                                 string name,
                                 bool vegeterian)
            {
                this.description = description;
                this.price = price;
                this.name = name;
                this.vegeterian = vegeterian;
            }

            public override void print()
            {
                Console.WriteLine("description: {0}", description);
                Console.WriteLine("price: {0}", price);
                Console.WriteLine("name: {0}", name);
                Console.WriteLine("vegeterian: {0}", vegeterian);
            }
            public override IEnumerator GetEnumerator()
            {
                yield return null;
            }
        }
        public class Menu : MenuComponent
        {
            private List<MenuComponent> menuComponents;
            private string name;
            private string description;
            public Menu(string name, string description)
            {
                menuComponents = new List<MenuComponent>(10);
                this.name = name;
                this.description = description;
            }
            public override MenuComponent getChild(int i)
            {
                MenuComponent comp = null;
                try
                {
                    comp = menuComponents[i];
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return comp;
            }

            public override void add(MenuComponent menuComponent)
            {
                menuComponents.Add(menuComponent);
            }
            public override void remove(MenuComponent menuComponent)
            {
                menuComponents.Remove(menuComponent);
            }
            public override string getDescription()
            {
                return description;
            }
            public override string getName()
            {
                return name;
            }
            public override void print()
            {
                Console.WriteLine("name:{0}", name);
                Console.WriteLine("description:{0}", description);
                Console.WriteLine("--------------------");
                IEnumerator<MenuComponent> ie = menuComponents.GetEnumerator();

                while (ie.MoveNext())
                {
                    MenuComponent mc = ie.Current;
                    mc.print();
                }
            }
            public override IEnumerator GetEnumerator()
            {
                return menuComponents.GetEnumerator();
            }
        }

        public class WaitressComposite
        {
            MenuComponent components;
            public WaitressComposite(MenuComponent components)
            {
                this.components = components;
            }
            public void print()
            {
                this.components.print();
            }
        }
        public class CompositeIterator : IEnumerator
        {
            private Stack st;
            public CompositeIterator(IEnumerable inum)
            {
                st = new Stack();
                st.Push(inum);
            }

            public object Current
            {
                get
                {
                    if (MoveNext())
                    {
                        IEnumerator en = (IEnumerator)st.Peek();
                        MenuComponent component = (MenuComponent)en.Current;
                        if (component is Menu)
                        {
                            st.Push(component.GetEnumerator());
                        }
                        return component;
                    }
                    else
                        return null;
                }
            }

            public bool MoveNext()
            {
                if (st.Count == 0)
                    return false;
                else
                {
                    IEnumerator en = (IEnumerator)st.Peek();
                    if (!en.MoveNext())
                    {
                        st.Pop();
                        return MoveNext();
                    }
                    else
                        return true;
                }
            }

            public void Reset()
            {
                st.Clear();
            }
        }
    }

}