using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Common
{
    public class CommonEnums
    {
        public enum MealType { Veg = 1, NonVeg = 2 }
        public enum ScheduleType { Daily = 1, WeekDays = 2, WeekEnds = 3, Monday = 4, Tuesday = 5, WednesDay = 6, ThursDay = 7, FriDay = 8, SaturDay = 9
                                    , SunDay = 10}
        public enum Status { Active = 1, Inactive = 0}
        public enum UserType { User = 1, Admin = 2}
    }
}
