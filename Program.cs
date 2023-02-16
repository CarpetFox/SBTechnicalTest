using System;
using SB.CoreTest;

/// <summary>
/// SchoolsBuddy Technical Test.
///
/// Your task is to find the highest floor of the building from which it is safe
/// to drop a marble without the marble breaking, and to do so using the fewest
/// number of marbles. You can break marbles in the process of finding the answer.
///
/// The method Building.DropMarble should be used to carry out a marble drop. It
/// returns a boolean indicating whether the marble dropped without breaking.
/// Use Building.NumberFloors for the total number of floors in the building.
///
/// A very basic solution has already been implemented but it is up to you to
/// find your own, more efficient solution.
///
/// Please use the function Attempt2 for your answer.
/// </summary>
namespace SB.TechnicalTest
{
    class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine($"Attempt 1 Highest Safe Floor: {Attempt1()}");
            Console.WriteLine($"Attempt 1 Total Drops: {Building.TotalDrops}");

            Console.WriteLine();
            Building.Reset();

            Console.WriteLine($"Attempt 2 Highest Safe Floor: {Attempt2()}");
            Console.WriteLine($"Attempt 2 Total Drops: {Building.TotalDrops}");
        }

        /// <summary>
        /// First attempt - start at first floor and work up one floor at a time
        /// until you reach a floor at which marble breaks.
        /// The highest safe floor is one below this.
        /// </summary>
        /// <returns>Highest safe floor.</returns>
        static int Attempt1()
        {
            var i = 0;
            while (++i <= Building.NumberFloors && Building.DropMarble(i));

            return i - 1;
        }

        /// <summary>
        /// Second attempt - you do this one!
        /// </summary>
        /// <returns>Highest safe floor.</returns>
        static int Attempt2()
        {
            // we can't rule out that the top floor could be safe, so we use a hypothetical floor one above that as the upper limit to check from
            return FindHighestSafeFloor(Building.NumberFloors + 1, 0);
        }

        private static int FindHighestSafeFloor(int lowestFloorThatBreaks, int highestSafeFloor)
        {
            if (lowestFloorThatBreaks - 1 == highestSafeFloor) return highestSafeFloor;

            var midPoint = GetMidPoint(lowestFloorThatBreaks, highestSafeFloor);

            var midPointIsSafe = Building.DropMarble(midPoint);
            if (midPointIsSafe)
            {
                highestSafeFloor = midPoint;
            }
            else
            {
                lowestFloorThatBreaks = midPoint;
            }

            // if the premise was different and we were checking really quite large datasets, then recursion should be avoided for risk of stack overflow
            return FindHighestSafeFloor(lowestFloorThatBreaks, highestSafeFloor);
        }

        private static int GetMidPoint(int higherValue, int lowerValue)
        {
            if (lowerValue >= higherValue)
            {
                throw new InvalidOperationException();
            }
            
            return ((higherValue - lowerValue) / 2) + lowerValue;
        }
    }
}
