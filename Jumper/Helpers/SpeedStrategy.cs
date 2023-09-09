using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper.Helpers
{
    public interface ISpeedStrategy
    {
        int Speed { get; }
    }

    public interface CopyOfISpeedStrategy
    {
        int Speed { get; }
    }

    public class SlowSpeed : ISpeedStrategy
    {
        int ISpeedStrategy.Speed { get { return 1; } }
    }

    public class MediumSpeed : ISpeedStrategy
    {
        int ISpeedStrategy.Speed { get { return 2; } }
    }

    public class CopyOfMediumSpeed : ISpeedStrategy
    {
        int ISpeedStrategy.Speed { get { return 2; } }
    }

    public class FastSpeed : ISpeedStrategy
    {
        int ISpeedStrategy.Speed { get { return 3; } }
    }

    
    public class RandomSpeed
    {
        private static Random random = new Random();

        public static ISpeedStrategy GetSpeed()
        {
            int number = random.Next(3);

            if (number == 1)
                return new SlowSpeed();
            else if (number == 2)
                return new MediumSpeed();
            return new FastSpeed();
        }
    }



}
