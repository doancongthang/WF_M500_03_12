using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_M500_03_12
{
    class Params
    {
        public static double COUNT_HYDRAULICS_PUMP = 50;
        public static double COUNT_DELAY_START = 100;      //
        public static double COUNT_MAX_PREMINARY = 50;     //4s
        public static double COUNT_MAX_LOWPRESSURE = 100;   //8s
        public static double COUNT_READY_FOR_SPEED = 10;//1s
        public static double COUNT_SPEED_ENGINE = 75;
        public static double COUNT_TEMPERATURE_ENGINE = 50;
        public static double COUNT_STEP_ENGINE = 5;
        public static double COUNT_SPEED_REVERS = 80;
        public static double COUNT_QUICKDOWN;

        //public static int COUNT_HYDRAULICS_PUMP = 50;
        //public static int COUNT_HYDRAULICS_PUMP = 50;
        //public static int COUNT_HYDRAULICS_PUMP = 50;
        //public static int COUNT_HYDRAULICS_PUMP = 50;
        //public static int COUNT_HYDRAULICS_PUMP = 50;
        public Params() { }
    }
}
