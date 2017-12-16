using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyPatterns
{
    /******************************************************************************************
     * Decorator:
     * attaches additional responsibilities to an object dynamically.
     * Decorators provide a flexible alternative to subclassing for extending functionality.
     * ****************************************************************************************
     * 
     * info:
     * 1.Decorators have the same supertype as the objects they decorate
     * 2. one can use one or more decorators to wrap an object
     * 3.one can pass around a decorated object in place of the original(wrapped) object
     * as they both have the same supertype
     * 4.the decorator adds its own behavior either before and/or after delegating to the object it decorates
     * to do the rest of the job!!!
     * 5.objects can be decorated at any time, so we can decorate objects dynamically at runtime
     * with as many decorators as we like
     * */

    public abstract class Beverage
    {
        protected string description = "Unknown Beverage";
        public virtual string GetDescription()
        {
            return description;
        }
        public abstract double Cost();
    }

    public abstract class CondimentDecorator : Beverage
    {
        public override string GetDescription()
        {
            return description;
        }
        public CondimentDecorator() { }
    }

    public class Espresso : Beverage
    {
        public override double Cost()
        {
            return 1.99;
        }
        public Espresso() { this.description = "Espresso"; }
    }

    public class HouseBlend : Beverage
    {
        public override double Cost()
        {
            return 0.8;
        }
        public HouseBlend() { this.description = "House Blend Coffee"; }
    }

    public class DarkRoasted : Beverage
    {
        public override double Cost()
        {
            return 0.8;
        }
        public DarkRoasted() { this.description = "Dark Roasted Coffee"; }
    }

    public class Decaf : Beverage
    {
        public override double Cost()
        {
            return 0.8;
        }
        public Decaf() { this.description = "Decaf Coffee"; }
    }

    public class Mocha : CondimentDecorator
    {
        private Beverage beverage;
        public Mocha(Beverage beverage) { this.beverage = beverage; }
        public override string GetDescription()
        {
            return this.beverage.GetDescription() + ", Mocha";
        }
        public override double Cost()
        {
            return .20 + this.beverage.Cost();
        }
    }

    public class Soy : CondimentDecorator
    {
        private Beverage beverage;
        public Soy(Beverage beverage) { this.beverage = beverage; }
        public override string GetDescription()
        {
            return this.beverage.GetDescription() + ", Soy";
        }
        public override double Cost()
        {
            return .15 + this.beverage.Cost();
        }
    }

    public class Whip : CondimentDecorator
    {
        private Beverage beverage;
        public Whip(Beverage beverage) { this.beverage = beverage; }
        public override string GetDescription()
        {
            return this.beverage.GetDescription() + ", Whip";
        }
        public override double Cost()
        {
            return .28 + this.beverage.Cost();
        }
    }

}
