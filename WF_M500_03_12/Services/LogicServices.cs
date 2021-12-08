using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WF_M500_03_12;

namespace WF_M500_03_12.Services
{
    enum StateMachine
    {
        REMOTE_CONTROL,
        MACHINE_OFF,
        MACHINE_ON,
        HYDRAULICS_PUMP,
        READY_HYDRAULICS_PUPM,
        W_OFFTEST,
        TEST,
        STARTMACHINE,
        CONTROLSPEED,
    }
    enum STMC
    {
        IDLE,
        PROCESS_AUTO_W,
        PROCESS_AUTO_START,
        PRESSURE_PREMINARY_PUMP,
        READY_HIGH_PRESURE,
        START_OK,

        PROCESS_MANUAL,
        PROCESS_MANUAL_START,
        MANUAL_PRESSURE_PREMINARY_PUMP,
        MANUAL_READY_HIGH_PRESURE,

        MACHINEUP,
        PROCESS_MACHINE_UP,
        MACHINEHIGHSPEED,
        MACHINEDOWN,
        QUICKDOWN,
        REVERSE,
        STOP_POSITION
    }
    class LogicServices
    {
        #region Declair
        StateMachine stateMachine;
        STMC stateMc1, stateMc2, stateMc3;
        public Machine mc1;
        public Machine mc2;
        public Machine mc3;

        public double countdown_hydraulics_pump = 0;
        public double coundown_preminary_pump = 0;
        public double coundown_speed_engine = 0;
        public double coundown_increte_pump = 0;
        public double coundown_temp_engine = 0;
        public double coundown_temp_oil = 0;
        public double coundown_temp_water = 0;

        public double countdown_hydraulics_pump2 = 0;
        public double coundown_preminary_pump2 = 0;
        public double coundown_speed_engine2 = 0;
        public double coundown_increte_pump2 = 0;
        public double coundown_temp_engine2 = 0;
        public double coundown_temp_oil2 = 0;
        public double coundown_temp_water2 = 0;

        public double countdown_hydraulics_pump3 = 0;
        public double coundown_preminary_pump3 = 0;
        public double coundown_speed_engine3 = 0;
        public double coundown_increte_pump3 = 0;
        public double coundown_temp_engine3 = 0;
        public double coundown_temp_oil3 = 0;
        public double coundown_temp_water3 = 0;
        #endregion
        public LogicServices(Machine _mc1, Machine _mc2, Machine _mc3)
        {
            mc1 = _mc1;
            mc2 = _mc2;
            mc3 = _mc3;
            stateMachine = StateMachine.MACHINE_OFF;
            stateMc1 = STMC.IDLE;
            stateMc2 = STMC.IDLE;
            stateMc3 = STMC.IDLE;

            Thread delay1Thread = new Thread(Timer1Second);
            //Thread delay2Thread = new Thread(Timer2Second);
            //Thread delay3Thread = new Thread(Timer3Second);

            delay1Thread.Start();
            //delay2Thread.Start();
            //delay3Thread.Start();
        }
        private void Timer1Second()
        {
            while (true)
            {
                if (countdown_hydraulics_pump > 0)
                    countdown_hydraulics_pump--;
                if (coundown_preminary_pump > 0)
                    coundown_preminary_pump--;
                if (coundown_increte_pump > 0)
                    coundown_increte_pump--;
                if (coundown_speed_engine > 0)
                    coundown_speed_engine--;
                if (coundown_temp_engine > 0)
                    coundown_temp_engine--;
                if (coundown_temp_oil > 0)
                    coundown_temp_oil--;
                if (coundown_temp_water > 0)
                    coundown_temp_water--;
                // await Task.Delay(100);
                //****************************************//
                if (countdown_hydraulics_pump2 > 0)
                    countdown_hydraulics_pump2--;
                if (coundown_preminary_pump2 > 0)
                    coundown_preminary_pump2--;
                if (coundown_increte_pump2 > 0)
                    coundown_increte_pump2--;
                if (coundown_speed_engine2 > 0)
                    coundown_speed_engine2--;
                if (coundown_temp_engine2 > 0)
                    coundown_temp_engine2--;
                if (coundown_temp_oil2 > 0)
                    coundown_temp_oil2--;
                if (coundown_temp_water2 > 0)
                    coundown_temp_water2--;
                //****************************************//
                if (countdown_hydraulics_pump3 > 0)
                    countdown_hydraulics_pump3--;
                if (coundown_preminary_pump3 > 0)
                    coundown_preminary_pump3--;
                if (coundown_increte_pump3 > 0)
                    coundown_increte_pump3--;
                if (coundown_speed_engine3 > 0)
                    coundown_speed_engine3--;
                if (coundown_temp_engine3 > 0)
                    coundown_temp_engine3--;
                if (coundown_temp_oil3 > 0)
                    coundown_temp_oil3--;
                if (coundown_temp_water3 > 0)
                    coundown_temp_water3--;
                Thread.Sleep(100);
            }
        }
        private void Timer2Second()
        {
            while (true)
            {
                if (countdown_hydraulics_pump2 > 0)
                    countdown_hydraulics_pump2--;
                if (coundown_preminary_pump2 > 0)
                    coundown_preminary_pump2--;
                if (coundown_increte_pump2 > 0)
                    coundown_increte_pump2--;
                if (coundown_speed_engine2 > 0)
                    coundown_speed_engine2--;
                if (coundown_temp_engine2 > 0)
                    coundown_temp_engine2--;
                // await Task.Delay(100);
                Thread.Sleep(100);
            }
        }
        private void Timer3Second()
        {
            while (true)
            {
                if (countdown_hydraulics_pump3 > 0)
                    countdown_hydraulics_pump3--;
                if (coundown_preminary_pump3 > 0)
                    coundown_preminary_pump3--;
                if (coundown_increte_pump3 > 0)
                    coundown_increte_pump3--;
                if (coundown_speed_engine3 > 0)
                    coundown_speed_engine3--;
                if (coundown_temp_engine3 > 0)
                    coundown_temp_engine3--;
                // await Task.Delay(100);
                Thread.Sleep(100);
            }
        }
        public void Subcribe()
        {
            Task control = Task.Run(() => loopUpdateActionsStateMachine());  // Khởi chạy loop services
                                                                             //  Task timer1s = Task.Run(() => Timer1Second());
            Task stateEachMachine1 = Task.Run(() => parallel1_updateMachine());
            Task stateEachMachine2 = Task.Run(() => parallel2_updateMachine());
            Task stateEachMachine3 = Task.Run(() => parallel3_updateMachine());
        }
        private async void loopUpdateActionsStateMachine()
        {
            while (true)
            {
                Console.WriteLine(stateMachine);
                switch (stateMachine)  // KIEM TRA CAC DIEU KIEN
                {
                    case StateMachine.REMOTE_CONTROL:
                        btn_only();
                        if (Orionsystem.sw_main_CPU == true & Orionsystem.sw_main_VPU == false)
                        {
                            if (stateMachine == StateMachine.MACHINE_ON)
                                stateMachine = StateMachine.MACHINE_ON;
                            if (stateMachine == StateMachine.MACHINE_OFF)
                                stateMachine = StateMachine.MACHINE_OFF;
                            if (stateMachine == StateMachine.HYDRAULICS_PUMP)
                                stateMachine = StateMachine.HYDRAULICS_PUMP;
                            if (stateMachine == StateMachine.READY_HYDRAULICS_PUPM)
                                stateMachine = StateMachine.READY_HYDRAULICS_PUPM;
                        }
                        if (Orionsystem.sw_main_CPU == false & Orionsystem.sw_main_VPU == true)
                        {
                            stateMachine = StateMachine.REMOTE_CONTROL;
                        }
                        break;
                    case StateMachine.MACHINE_OFF:
                        if (Orionsystem.sw_main_CPU == true & Orionsystem.sw_main_VPU == false)
                        {
                            if (Orionsystem.sw_control_cpu_on == true)
                            {
                                stateMachine = StateMachine.MACHINE_ON;
                            }
                            if (Orionsystem.sw_control_cpu_on == false)
                            {
                                Orionsystem.sig_mainhas_pressure = false;
                                Orionsystem.sig_mainno_pressure = false;
                                mc1.offmachine();
                                mc2.offmachine();
                                mc3.offmachine();
                                Orionsystem.off_orion();
                                stateMachine = StateMachine.MACHINE_OFF;
                                Orionsystem.vl_oil_pressure_in_ptk_bearing = 8;
                            }
                        }
                        if (Orionsystem.sw_main_CPU == false & Orionsystem.sw_main_VPU == true)
                        {
                            stateMachine = StateMachine.REMOTE_CONTROL;
                        }
                        break;
                    case StateMachine.MACHINE_ON:
                        btn_only();
                        if (Orionsystem.sw_main_CPU == true & Orionsystem.sw_main_VPU == false)
                        {
                            if (Orionsystem.sw_control_cpu_on == false)
                            {
                                stateMachine = StateMachine.MACHINE_OFF;
                            }
                            if (Orionsystem.btn_checklight == true)
                                stateMachine = StateMachine.TEST;
                            countdown_hydraulics_pump = Params.COUNT_HYDRAULICS_PUMP;
                            stateMachine = StateMachine.HYDRAULICS_PUMP;
                            //stateMachine = StateMachine.HYDRAULICS_PUMP;
                            //stateMachine = StateMachine.MACHINE_OFF;
                            Orionsystem.sig_mainhas_pressure = false;
                            Orionsystem.sig_mainno_pressure = true;
                            //mc1.sig_nopressure = true;
                            //mc2.sig_nopressure = true;
                            //mc3.sig_nopressure = true;
                            mc1.sig_oil_no_pump = true;
                            mc2.sig_oil_no_pump = true;
                            mc3.sig_oil_no_pump = true;
                            mc1.sig_park = true;
                            mc2.sig_park = true;
                            mc3.sig_park = true;
                        }
                        break;
                    case StateMachine.HYDRAULICS_PUMP:
                        btn_only();
                        if (Orionsystem.sw_main_CPU == true & Orionsystem.sw_main_VPU == false)
                        {

                            if (Orionsystem.btn_checklight == true & stateMachine == StateMachine.MACHINE_ON)
                                stateMachine = StateMachine.TEST;
                            if (Orionsystem.btn_checklight == true & Orionsystem.sw_main_CPU == true)
                            {
                                stateMachine = StateMachine.TEST;
                            }
                            if (Orionsystem.sw_main_CPU == false)
                            {
                                stateMachine = StateMachine.MACHINE_OFF;
                            }
                            if (countdown_hydraulics_pump == 0)
                            {
                                stateMachine = StateMachine.READY_HYDRAULICS_PUPM;
                            }
                            Orionsystem.sig_mainno_pressure = true;
                            mc1.sig_oil_no_pump = true;
                            mc2.sig_oil_no_pump = true;
                            mc3.sig_oil_no_pump = true;
                            mc1.sig_park = true;
                            mc2.sig_park = true;
                            mc3.sig_park = true;
                            Orionsystem.vl_oil_pressure_in_ptk_bearing = 9 - (int)((Params.COUNT_HYDRAULICS_PUMP * 0.1 - countdown_hydraulics_pump * 0.1));
                        }
                        break;
                    case StateMachine.READY_HYDRAULICS_PUPM:
                        btn_only();
                        if (Orionsystem.sw_main_CPU == true & Orionsystem.sw_main_VPU == false)
                        {
                            if (Orionsystem.sw_main_CPU == false)
                            {
                                stateMachine = StateMachine.MACHINE_OFF;
                            }
                            if (Orionsystem.btn_checklight == true)
                                stateMachine = StateMachine.TEST;
                            Orionsystem.sig_mainhas_pressure = true;
                            Orionsystem.sig_mainno_pressure = false;
                            mc1.sig_oil_no_pump = false;
                            mc2.sig_oil_no_pump = false;
                            mc3.sig_oil_no_pump = false;

                            //mc1.sig_park = true;
                            //mc2.sig_park = true;
                            //mc3.sig_park = true;
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        while (Orionsystem.sw_main_VPU == true)
                        {
                            stateMachine = StateMachine.REMOTE_CONTROL;
                        }
                        stateMachine = StateMachine.STARTMACHINE;
                        break;
                    case StateMachine.W_OFFTEST:
                        btn_only();
                        //stateMachine = StateMachine.HYDRAULICS_PUMP;
                        if (Orionsystem.btn_checklight == true & Orionsystem.sw_main_CPU == true)
                        {
                            stateMachine = StateMachine.TEST;
                        }
                        if (Orionsystem.sw_main_CPU == false)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (countdown_hydraulics_pump == 0)
                        {
                            stateMachine = StateMachine.READY_HYDRAULICS_PUPM;
                        }
                        if (countdown_hydraulics_pump != 0)
                        {
                            stateMachine = StateMachine.HYDRAULICS_PUMP;
                        }
                        Orionsystem.sig_mainhas_pressure = true;
                        //mc1.off_all_sig();
                        //mc2.off_all_sig();
                        //mc3.off_all_sig();
                        Orionsystem.off_all_sig_main();
                        break;
                    case StateMachine.TEST:
                        if (Orionsystem.btn_checklight == false & Orionsystem.sw_main_CPU == true)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (Orionsystem.btn_checklight == false)
                        {
                            stateMachine = StateMachine.W_OFFTEST;
                        }
                        while (Orionsystem.btn_checklight == true)
                        {
                            mc1.on_all_sig();
                            mc2.on_all_sig();
                            mc3.on_all_sig();
                            Orionsystem.on_all_sig_main();
                        }
                        //Moi them vao
                        if (Orionsystem.btn_checklight == false & Orionsystem.sw_main_VPU == false)
                        {
                            if (stateMachine == StateMachine.MACHINE_ON)
                                stateMachine = StateMachine.MACHINE_ON;
                            if (stateMachine == StateMachine.MACHINE_OFF)
                                stateMachine = StateMachine.MACHINE_OFF;
                            if (stateMachine == StateMachine.HYDRAULICS_PUMP)
                                stateMachine = StateMachine.HYDRAULICS_PUMP;
                            if (stateMachine == StateMachine.READY_HYDRAULICS_PUPM)
                                stateMachine = StateMachine.READY_HYDRAULICS_PUPM;
                        }
                        break;
                }
                await Task.Delay(1);
            }
        }
        private async void parallel1_updateMachine()
        {
            while (true)
            {
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    btn_only();
                    stateMc1 = STMC.IDLE;
                    mc1.offmachine();
                    mc2.offmachine();
                    mc3.offmachine();
                    Orionsystem.off_orion();
                }
                if (stateMachine == StateMachine.STARTMACHINE)
                {
                    btn_only();
                    Console.WriteLine(stateMc1);
                    switch (stateMc1)
                    {
                        //Auto process
                        #region AUTO
                        case STMC.IDLE:
                            mc1.sig_park = true;
                            if (mc1.btn_auto_start == true)
                            {
                                stateMc1 = STMC.PROCESS_AUTO_W;
                            }
                            if (mc1.btn_auto_start == false)
                            {
                                stateMc1 = STMC.PROCESS_MANUAL;
                            }
                            break;
                        case STMC.PROCESS_AUTO_W:
                            if (mc1.btn_auto_start == false)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;  // 4s
                                stateMc1 = STMC.PROCESS_AUTO_START;
                            }
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc1.vl_oil_pressure_mainline = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump) * 0.1);
                            mc1.sig_mpa = true;
                            mc1.sig_moa = true;
                            if (coundown_preminary_pump == 0)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump = Params.COUNT_MAX_PREMINARY;
                                stateMc1 = STMC.PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc1.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_increte_pump) * 0.1);
                            mc1.sig_vnd = true;
                            mc1.sig_moa = true;
                            mc1.sig_mpa = true;
                            mc1.sig_vnd = true;
                            if (coundown_preminary_pump == 70)
                            {
                                mc1.sig_rotate1 = true;
                            }
                            if (coundown_preminary_pump == 40)
                            {
                                mc1.sig_rotate2 = true;
                            }
                            if (coundown_preminary_pump == 10)
                            {
                                mc1.sig_rotate3 = true;
                            }
                            if (coundown_preminary_pump == 0)
                            {
                                stateMc1 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                                coundown_temp_oil = Params.COUNT_TEMP_OIL_ENGINE;       //
                                coundown_temp_water = Params.COUNT_TEMP_WATER_ENGINE;   //
                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            mc1.sig_vnd = false;
                            mc1.sig_rotate1 = false;
                            mc1.sig_rotate2 = false;
                            mc1.sig_rotate3 = false;
                            mc1.sig_or_oil = true;
                            mc1.sig_vvd = true;
                            mc1.vl_temperature_engine = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine));
                            mc1.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine));
                            mc1.vl_temperature_water = ((Params.COUNT_TEMP_WATER_ENGINE - coundown_temp_water));
                            mc1.vl_temperature_oil = ((Params.COUNT_TEMP_OIL_ENGINE - coundown_temp_oil));
                            if (coundown_preminary_pump == 0)
                            {
                                mc1.sig_vnd = false;
                                mc1.sig_rotate1 = false;
                                mc1.sig_rotate2 = false;
                                mc1.sig_rotate3 = false;
                            }
                            if (coundown_speed_engine == 0)
                            {
                                stateMc1 = STMC.START_OK;
                            }
                            break;
                        #endregion
                        //Manual process
                        #region MANUAL
                        case STMC.PROCESS_MANUAL:
                            mc1.sig_mpa = false;
                            mc1.sig_vnd = false;
                            mc1.sig_vvd = false;
                            mc1.sig_rotate1 = false;
                            mc1.sig_rotate2 = false;
                            mc1.sig_rotate3 = false;
                            if (mc1.btn_on_MOA == true)
                            {
                                mc1.sig_moa = true;
                            }
                            if (mc1.btn_off_MOA == true)
                            {
                                mc1.sig_moa = false;
                            }
                            if (mc1.btn_auto_start == true)
                            {
                                stateMc1 = STMC.PROCESS_AUTO_W;
                            }
                            if (mc1.btn_on_MPA == true)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                                mc1.sig_mpa = true;
                                stateMc1 = STMC.PROCESS_MANUAL_START;
                            }
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            //mc1.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump) * 0.1);
                            mc1.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump) * 0.1);
                            if (mc1.btn_on_MOA == true)
                            {
                                mc1.sig_moa = true;
                            }
                            if (mc1.btn_off_MOA == true)
                            {
                                mc1.sig_moa = false;
                            }
                            if (mc1.btn_on_MPA == true)
                            {
                                mc1.sig_mpa = true;
                            }
                            if (mc1.btn_off_MPA == true)
                            {
                                mc1.sig_mpa = false;
                            }
                            if (mc1.btn_on_VND == true)
                            {
                                mc1.sig_vnd = true;
                                coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump = Params.COUNT_MAX_PREMINARY;
                                stateMc1 = STMC.MANUAL_PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            ///coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                            if (coundown_preminary_pump == 70)
                            {
                                mc1.sig_rotate1 = true;
                            }
                            if (coundown_preminary_pump == 40)
                            {
                                mc1.sig_rotate2 = true;
                            }
                            if (coundown_preminary_pump == 10)
                            {
                                mc1.sig_rotate3 = true;
                            }
                            if (mc1.btn_on_VVD == true)
                            {
                                stateMc1 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                                coundown_temp_oil = Params.COUNT_TEMP_OIL_ENGINE;       //
                                coundown_temp_water = Params.COUNT_TEMP_WATER_ENGINE;   //
                                stateMc1 = STMC.MANUAL_READY_HIGH_PRESURE;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            mc1.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine));
                            mc1.vl_temperature_engine = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine));
                            mc1.vl_temperature_water = ((Params.COUNT_TEMP_WATER_ENGINE - coundown_temp_water));
                            mc1.vl_temperature_oil = ((Params.COUNT_TEMP_OIL_ENGINE - coundown_temp_oil));
                            mc1.sig_mpa = true;
                            mc1.sig_vvd = true;
                            mc1.sig_or_oil = true;
                            mc1.sig_vnd = false;
                            mc1.sig_rotate1 = false;
                            mc1.sig_rotate2 = false;
                            mc1.sig_rotate3 = false;
                            if (coundown_preminary_pump == 0)
                            {
                                mc1.sig_vnd = false;
                                mc1.sig_rotate1 = false;
                                mc1.sig_rotate2 = false;
                                mc1.sig_rotate3 = false;
                            }
                            if (coundown_speed_engine == 0)
                            {
                                stateMc1 = STMC.START_OK;
                            }
                            break;
                        #endregion
                        case STMC.START_OK:
                            mc1.sig_moa = false;
                            mc1.sig_mpa = false;
                            mc1.sig_vvd = false;
                            mc1.sig_vnd = false;
                            mc1.sig_rotate1 = false;
                            mc1.sig_rotate2 = false;
                            mc1.sig_rotate3 = false;
                            mc1.sig_or_oil = false;
                            Console.WriteLine("Da khoi dong xong 1");
                            break;
                    }
                }
                //if (stateMachine == StateMachine.CONTROLSPEED)
                //--IF_START_OK_GO_TO_CONTROL_SPEED--//
                {
                    btn_only();
                    Console.WriteLine(stateMc1);
                    switch (stateMc1)
                    {
                        case STMC.START_OK:
                            #region START_OK
                            if (mc1.btn_estop == true)
                            {
                                mc1.offmachine();
                                mc1.park();
                                mc1.sig_oil_no_pump = true;
                                stateMc1 = STMC.IDLE;
                            }
                            if (mc1.btn_up == true)
                            {
                                stateMc1 = STMC.MACHINEUP;
                            }
                            if (mc1.btn_down == true)
                            {
                                stateMc1 = STMC.MACHINEDOWN;
                            }
                            if (mc1.btn_down == true)
                            {
                                coundown_speed_engine = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc1 = STMC.REVERSE;
                            }
                            if (mc1.sig_reverse == true & mc1.btn_up == true)
                            {
                                coundown_speed_engine = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc1 = STMC.STOP_POSITION;
                            }
                            //if(mc1.vl_speed_engine <=75)
                            //{
                            //    mc1.sig_gobehind = false;
                            //    mc1.sig_park = true;
                            //}    
                            break;
                        #endregion
                        case STMC.MACHINEUP:
                            #region MACHINEUP
                            coundown_speed_engine = Params.COUNT_STEP_ENGINE;           //Tăng tốc dần
                            mc1.sig_park = false;
                            mc1.sig_reverse = false;
                            if (mc1.btn_up == true)
                            {
                                stateMc1 = STMC.PROCESS_MACHINE_UP;
                            }
                            //Nếu dừng
                            if (mc1.btn_estop == true)
                            {
                                mc1.offmachine();
                                mc1.park();
                                mc1.sig_oil_no_pump = true;
                                stateMc1 = STMC.IDLE;
                            }
                            if (mc1.btn_down == true)
                            {
                                coundown_speed_engine = Params.COUNT_STEP_ENGINE;           //Giảm tốc dần
                                stateMc1 = STMC.MACHINEDOWN;
                            }
                            if (mc1.vl_speed_engine < 80)
                            {
                                mc1.sig_up = true;
                                mc1.sig_highspeed = false;
                                //mc1.sig_park = true;
                                //mc1.vl_speed_engine = 75;
                                //stateMc1 = STMC.START_OK;
                            }
                            if (mc1.vl_speed_engine < 75)
                            {
                                mc1.sig_up = false;
                                mc1.sig_highspeed = false;
                                mc1.sig_park = true;
                                mc1.vl_speed_engine = 75;
                                stateMc1 = STMC.START_OK;
                            }
                            if (mc1.btn_down == true)
                            {
                                mc1.sig_up = false;
                                mc1.sig_highspeed = false;
                                mc1.sig_park = false;
                                //mc1.sig_gobehind = true;
                            }
                            if (mc1.btn_quickdown == true)
                            {
                                coundown_speed_engine = mc1.vl_speed_engine - 80;
                                Params.COUNT_QUICKDOWN = mc1.vl_speed_engine - 80;
                                stateMc1 = STMC.QUICKDOWN;
                            }
                            break;
                        #endregion
                        case STMC.PROCESS_MACHINE_UP:
                            #region PROCESS_MACHINE_UP
                            mc1.vl_speed_engine = mc1.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine) / 10);
                            mc1.sig_up = true;
                            if (mc1.vl_speed_engine > 100)
                            {
                                mc1.sig_up = false;
                                mc1.sig_highspeed = true;
                            }
                            if (coundown_speed_engine == 0)
                            {
                                stateMc1 = STMC.MACHINEUP;
                            }
                            if (mc1.btn_up == true)
                            {
                                //mc1.vl_speed_engine = mc1.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine) / 10);
                                stateMc1 = STMC.PROCESS_MACHINE_UP;
                            }
                            break;
                        #endregion
                        case STMC.MACHINEDOWN:
                            #region MACHINEDOWN
                            mc1.vl_speed_engine = mc1.vl_speed_engine - ((Params.COUNT_STEP_ENGINE - coundown_speed_engine) / 10);
                            //if (mc1.vl_speed_engine < 100)
                            //{
                            //    mc1.sig_goahead = true;
                            //    mc1.sig_highspeed = false;
                            //    stateMc1 = STMC.MACHINEUP;
                            //}
                            if (mc1.vl_speed_engine > 80)
                            {
                                mc1.sig_up = true;
                                mc1.sig_park = false;
                                mc1.sig_highspeed = false;
                                stateMc1 = STMC.MACHINEUP;
                                //stateMc1 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc1.vl_speed_engine > 100)
                            {
                                mc1.sig_up = false;
                                mc1.sig_highspeed = true;
                                stateMc1 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc1.vl_speed_engine < 80)
                            {
                                mc1.sig_up = false;
                                mc1.sig_highspeed = false;
                                mc1.sig_park = true;
                                stateMc1 = STMC.MACHINEUP;
                                //mc1.vl_speed_engine = 80;
                            }
                            if (mc1.btn_up == true)     //Đang dùng nút nhấn mc2
                            {
                                stateMc1 = STMC.MACHINEUP;
                            }
                            if (mc1.btn_down == true)
                            {
                                stateMc1 = STMC.MACHINEDOWN;
                            }
                            break;
                        #endregion
                        case STMC.REVERSE:
                            #region REVERSE
                            mc1.vl_speed_engine = mc1.vl_speed_engine + ((Params.COUNT_SPEED_REVERS - coundown_speed_engine) / 10);
                            mc1.sig_reverse = true;
                            mc1.sig_park = false;
                            if (mc1.vl_speed_engine >= 80)
                            {
                                mc1.sig_reverse = true;
                                stateMc1 = STMC.START_OK;
                            }
                            //stateMc1 = STMC.REVERSE;
                            break;
                        #endregion
                        case STMC.STOP_POSITION:
                            #region STOP_POSITION
                            mc1.vl_speed_engine = mc1.vl_speed_engine - ((Params.COUNT_SPEED_REVERS - coundown_speed_engine) / 10);
                            if (mc1.vl_speed_engine < 75)                    //Đang sai phần cứng. cần sửa lại nút nhấn Up1
                            {
                                //coundown_speed_engine = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                mc1.sig_park = true;
                                mc1.sig_highspeed = false;
                                mc1.sig_up = false;
                                mc1.sig_reverse = false;
                                mc1.vl_speed_engine = 75;
                                stateMc1 = STMC.START_OK;
                            }
                            break;
                        #endregion
                        case STMC.QUICKDOWN:
                            #region QUICKDOWN
                            mc1.vl_speed_engine = mc1.vl_speed_engine - ((Params.COUNT_QUICKDOWN - coundown_speed_engine) / 10);
                            if (mc1.vl_speed_engine <= 79)
                            {
                                mc1.sig_park = true;
                                mc1.sig_highspeed = false;
                                mc1.sig_up = false;
                                stateMc1 = STMC.START_OK;
                            }
                            break;
                            #endregion
                    }
                }
                await Task.Delay(1);
            }
        }
        private async void parallel2_updateMachine()
        {
            while (true)
            {
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    btn_only();
                    stateMc2 = STMC.IDLE;
                    mc2.offmachine();
                    mc2.offmachine();
                    mc3.offmachine();
                    Orionsystem.off_orion();
                }
                if (stateMachine == StateMachine.STARTMACHINE)
                {
                    btn_only();
                    Console.WriteLine(stateMc2);
                    switch (stateMc2)
                    {
                        //Auto process
                        #region AUTO
                        case STMC.IDLE:
                            mc2.sig_park = true;
                            if (mc2.btn_auto_start == true)
                            {
                                stateMc2 = STMC.PROCESS_AUTO_W;
                            }
                            if (mc2.btn_auto_start == false)
                            {
                                stateMc2 = STMC.PROCESS_MANUAL;
                            }
                            break;
                        case STMC.PROCESS_AUTO_W:
                            if (mc2.btn_auto_start == false)
                            {
                                coundown_preminary_pump2 = Params.COUNT_MAX_PREMINARY;  // 4s
                                stateMc2 = STMC.PROCESS_AUTO_START;
                            }
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc2.vl_oil_pressure_mainline = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump2) * 0.1);
                            mc2.sig_mpa = true;
                            mc2.sig_moa = true;
                            if (coundown_preminary_pump2 == 0)
                            {
                                coundown_preminary_pump2 = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump2 = Params.COUNT_MAX_PREMINARY;
                                stateMc2 = STMC.PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc2.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_increte_pump2) * 0.1);
                            mc2.sig_vnd = true;
                            mc2.sig_moa = true;
                            mc2.sig_mpa = true;
                            mc2.sig_vnd = true;
                            if (coundown_preminary_pump2 == 70)
                            {
                                mc2.sig_rotate1 = true;
                            }
                            if (coundown_preminary_pump2 == 40)
                            {
                                mc2.sig_rotate2 = true;
                            }
                            if (coundown_preminary_pump2 == 10)
                            {
                                mc2.sig_rotate3 = true;
                            }
                            if (coundown_preminary_pump2 == 0)
                            {
                                stateMc2 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump2 = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine2 = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine2 = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                                coundown_temp_oil2 = Params.COUNT_TEMP_OIL_ENGINE;       //
                                coundown_temp_water2 = Params.COUNT_TEMP_WATER_ENGINE;   //
                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            mc2.sig_vnd = false;
                            mc2.sig_rotate1 = false;
                            mc2.sig_rotate2 = false;
                            mc2.sig_rotate3 = false;
                            mc2.sig_or_oil = true;
                            mc2.sig_vvd = true;
                            mc2.vl_temperature_engine = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine2));
                            mc2.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine2));
                            mc2.vl_temperature_water = ((Params.COUNT_TEMP_WATER_ENGINE - coundown_temp_water2));
                            mc2.vl_temperature_oil = ((Params.COUNT_TEMP_OIL_ENGINE - coundown_temp_oil2));
                            if (coundown_preminary_pump2 == 0)
                            {
                                mc2.sig_vnd = false;
                                mc2.sig_rotate1 = false;
                                mc2.sig_rotate2 = false;
                                mc2.sig_rotate3 = false;
                            }
                            if (coundown_speed_engine2 == 0)
                            {
                                stateMc2 = STMC.START_OK;
                            }
                            break;
                        #endregion
                        //Manual process
                        #region MANUAL
                        case STMC.PROCESS_MANUAL:
                            mc2.sig_mpa = false;
                            mc2.sig_vnd = false;
                            mc2.sig_vvd = false;
                            mc2.sig_rotate1 = false;
                            mc2.sig_rotate2 = false;
                            mc2.sig_rotate3 = false;
                            if (mc2.btn_on_MOA == true)
                            {
                                mc2.sig_moa = true;
                            }
                            if (mc2.btn_off_MOA == true)
                            {
                                mc2.sig_moa = false;
                            }
                            if (mc2.btn_auto_start == true)
                            {
                                stateMc2 = STMC.PROCESS_AUTO_W;
                            }
                            if (mc2.btn_on_MPA == true)
                            {
                                coundown_preminary_pump2 = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                                mc2.sig_mpa = true;
                                stateMc2 = STMC.PROCESS_MANUAL_START;
                            }
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            //mc2.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump2) * 0.1);
                            mc2.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump2) * 0.1);
                            if (mc2.btn_on_MOA == true)
                            {
                                mc2.sig_moa = true;
                            }
                            if (mc2.btn_off_MOA == true)
                            {
                                mc2.sig_moa = false;
                            }
                            if (mc2.btn_on_MPA == true)
                            {
                                mc2.sig_mpa = true;
                            }
                            if (mc2.btn_off_MPA == true)
                            {
                                mc2.sig_mpa = false;
                            }
                            if (mc2.btn_on_VND == true)
                            {
                                mc2.sig_vnd = true;
                                coundown_preminary_pump2 = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump2 = Params.COUNT_MAX_PREMINARY;
                                stateMc2 = STMC.MANUAL_PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            ///coundown_preminary_pump2 = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                            if (coundown_preminary_pump2 == 70)
                            {
                                mc2.sig_rotate1 = true;
                            }
                            if (coundown_preminary_pump2 == 40)
                            {
                                mc2.sig_rotate2 = true;
                            }
                            if (coundown_preminary_pump2 == 10)
                            {
                                mc2.sig_rotate3 = true;
                            }
                            if (mc2.btn_on_VVD == true)
                            {
                                stateMc2 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump2 = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine2 = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine2 = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                                coundown_temp_oil2 = Params.COUNT_TEMP_OIL_ENGINE;       //
                                coundown_temp_water2 = Params.COUNT_TEMP_WATER_ENGINE;   //
                                stateMc2 = STMC.MANUAL_READY_HIGH_PRESURE;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            mc2.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine2));
                            mc2.vl_temperature_engine = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine2));
                            mc2.vl_temperature_water = ((Params.COUNT_TEMP_WATER_ENGINE - coundown_temp_water2));
                            mc2.vl_temperature_oil = ((Params.COUNT_TEMP_OIL_ENGINE - coundown_temp_oil2));
                            mc2.sig_mpa = true;
                            mc2.sig_vvd = true;
                            mc2.sig_or_oil = true;
                            mc2.sig_vnd = false;
                            mc2.sig_rotate1 = false;
                            mc2.sig_rotate2 = false;
                            mc2.sig_rotate3 = false;
                            if (coundown_preminary_pump2 == 0)
                            {
                                mc2.sig_vnd = false;
                                mc2.sig_rotate1 = false;
                                mc2.sig_rotate2 = false;
                                mc2.sig_rotate3 = false;
                            }
                            if (coundown_speed_engine2 == 0)
                            {
                                stateMc2 = STMC.START_OK;
                            }
                            break;
                        #endregion
                        case STMC.START_OK:
                            mc2.sig_moa = false;
                            mc2.sig_mpa = false;
                            mc2.sig_vvd = false;
                            mc2.sig_vnd = false;
                            mc2.sig_rotate1 = false;
                            mc2.sig_rotate2 = false;
                            mc2.sig_rotate3 = false;
                            mc2.sig_or_oil = false;
                            Console.WriteLine("Da khoi dong xong 2");
                            break;
                    }
                }
                //if (stateMachine == StateMachine.CONTROLSPEED)
                //--IF_START_OK_GO_TO_CONTROL_SPEED--//
                {
                    btn_only();
                    Console.WriteLine(stateMc2);
                    switch (stateMc2)
                    {
                        case STMC.START_OK:
                            #region START_OK
                            if (mc2.btn_estop == true)
                            {
                                mc2.offmachine();
                                mc2.park();
                                mc2.sig_oil_no_pump = true;
                                stateMc2 = STMC.IDLE;
                            }
                            if (mc2.btn_up == true)
                            {
                                stateMc2 = STMC.MACHINEUP;
                            }
                            if (mc2.btn_down == true)
                            {
                                stateMc2 = STMC.MACHINEDOWN;
                            }
                            if (mc2.btn_down == true)
                            {
                                coundown_speed_engine2 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc2 = STMC.REVERSE;
                            }
                            if (mc2.sig_reverse == true & mc2.btn_up == true)
                            {
                                coundown_speed_engine2 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc2 = STMC.STOP_POSITION;
                            }
                            //if(mc2.vl_speed_engine <=75)
                            //{
                            //    mc2.sig_gobehind = false;
                            //    mc2.sig_park = true;
                            //}    
                            break;
                        #endregion
                        case STMC.MACHINEUP:
                            #region MACHINEUP
                            coundown_speed_engine2 = Params.COUNT_STEP_ENGINE;           //Tăng tốc dần
                            mc2.sig_park = false;
                            mc2.sig_reverse = false;
                            if (mc2.btn_up == true)
                            {
                                stateMc2 = STMC.PROCESS_MACHINE_UP;
                            }
                            //Nếu dừng
                            if (mc2.btn_estop == true)
                            {
                                mc2.offmachine();
                                mc2.park();
                                mc2.sig_oil_no_pump = true;
                                stateMc2 = STMC.IDLE;
                            }
                            if (mc2.btn_down == true)
                            {
                                coundown_speed_engine2 = Params.COUNT_STEP_ENGINE;           //Giảm tốc dần
                                stateMc2 = STMC.MACHINEDOWN;
                            }
                            if (mc2.vl_speed_engine < 80)
                            {
                                mc2.sig_up = true;
                                mc2.sig_highspeed = false;
                                //mc2.sig_park = true;
                                //mc2.vl_speed_engine = 75;
                                //stateMc2 = STMC.START_OK;
                            }
                            if (mc2.vl_speed_engine < 75)
                            {
                                mc2.sig_up = false;
                                mc2.sig_highspeed = false;
                                mc2.sig_park = true;
                                mc2.vl_speed_engine = 75;
                                stateMc2 = STMC.START_OK;
                            }
                            if (mc2.btn_down == true)
                            {
                                mc2.sig_up = false;
                                mc2.sig_highspeed = false;
                                mc2.sig_park = false;
                                //mc2.sig_gobehind = true;
                            }
                            if (mc2.btn_quickdown == true)
                            {
                                coundown_speed_engine2 = mc2.vl_speed_engine - 80;
                                Params.COUNT_QUICKDOWN = mc2.vl_speed_engine - 80;
                                stateMc2 = STMC.QUICKDOWN;
                            }
                            break;
                        #endregion
                        case STMC.PROCESS_MACHINE_UP:
                            #region PROCESS_MACHINE_UP
                            mc2.vl_speed_engine = mc2.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine2) / 10);
                            mc2.sig_up = true;
                            if (mc2.vl_speed_engine > 100)
                            {
                                mc2.sig_up = false;
                                mc2.sig_highspeed = true;
                            }
                            if (coundown_speed_engine2 == 0)
                            {
                                stateMc2 = STMC.MACHINEUP;
                            }
                            if (mc2.btn_up == true)
                            {
                                //mc2.vl_speed_engine = mc2.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine2) / 10);
                                stateMc2 = STMC.PROCESS_MACHINE_UP;
                            }
                            break;
                        #endregion
                        case STMC.MACHINEDOWN:
                            #region MACHINEDOWN
                            mc2.vl_speed_engine = mc2.vl_speed_engine - ((Params.COUNT_STEP_ENGINE - coundown_speed_engine2) / 10);
                            //if (mc2.vl_speed_engine < 100)
                            //{
                            //    mc2.sig_goahead = true;
                            //    mc2.sig_highspeed = false;
                            //    stateMc2 = STMC.MACHINEUP;
                            //}
                            if (mc2.vl_speed_engine > 80)
                            {
                                mc2.sig_up = true;
                                mc2.sig_park = false;
                                mc2.sig_highspeed = false;
                                stateMc2 = STMC.MACHINEUP;
                                //stateMc2 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc2.vl_speed_engine > 100)
                            {
                                mc2.sig_up = false;
                                mc2.sig_highspeed = true;
                                stateMc2 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc2.vl_speed_engine < 80)
                            {
                                mc2.sig_up = false;
                                mc2.sig_highspeed = false;
                                mc2.sig_park = true;
                                stateMc2 = STMC.MACHINEUP;
                                //mc2.vl_speed_engine = 80;
                            }
                            if (mc2.btn_up == true)     //Đang dùng nút nhấn mc2
                            {
                                stateMc2 = STMC.MACHINEUP;
                            }
                            if (mc2.btn_down == true)
                            {
                                stateMc2 = STMC.MACHINEDOWN;
                            }
                            break;
                        #endregion
                        case STMC.REVERSE:
                            #region REVERSE
                            mc2.vl_speed_engine = mc2.vl_speed_engine + ((Params.COUNT_SPEED_REVERS - coundown_speed_engine2) / 10);
                            mc2.sig_reverse = true;
                            mc2.sig_park = false;
                            if (mc2.vl_speed_engine >= 80)
                            {
                                mc2.sig_reverse = true;
                                stateMc2 = STMC.START_OK;
                            }
                            //stateMc2 = STMC.REVERSE;
                            break;
                        #endregion
                        case STMC.STOP_POSITION:
                            #region STOP_POSITION
                            mc2.vl_speed_engine = mc2.vl_speed_engine - ((Params.COUNT_SPEED_REVERS - coundown_speed_engine2) / 10);
                            if (mc2.vl_speed_engine < 75)                    //Đang sai phần cứng. cần sửa lại nút nhấn Up1
                            {
                                //coundown_speed_engine2 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                mc2.sig_park = true;
                                mc2.sig_highspeed = false;
                                mc2.sig_up = false;
                                mc2.sig_reverse = false;
                                mc2.vl_speed_engine = 75;
                                stateMc2 = STMC.START_OK;
                            }
                            break;
                        #endregion
                        case STMC.QUICKDOWN:
                            #region QUICKDOWN
                            mc2.vl_speed_engine = mc2.vl_speed_engine - ((Params.COUNT_QUICKDOWN - coundown_speed_engine2) / 10);
                            if (mc2.vl_speed_engine <= 79)
                            {
                                mc2.sig_park = true;
                                mc2.sig_highspeed = false;
                                mc2.sig_up = false;
                                stateMc2 = STMC.START_OK;
                            }
                            break;
                            #endregion
                    }
                }
                await Task.Delay(1);
            }
        }
        private async void parallel3_updateMachine()
        {
            while (true)
            {
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    btn_only();
                    stateMc3 = STMC.IDLE;
                    mc3.offmachine();
                    mc3.offmachine();
                    mc3.offmachine();
                    Orionsystem.off_orion();
                }
                if (stateMachine == StateMachine.STARTMACHINE)
                {
                    btn_only();
                    Console.WriteLine(stateMc3);
                    switch (stateMc3)
                    {
                        //Auto process
                        #region AUTO
                        case STMC.IDLE:
                            mc3.sig_park = true;
                            if (mc3.btn_auto_start == true)
                            {
                                stateMc3 = STMC.PROCESS_AUTO_W;
                            }
                            if (mc3.btn_auto_start == false)
                            {
                                stateMc3 = STMC.PROCESS_MANUAL;
                            }
                            break;
                        case STMC.PROCESS_AUTO_W:
                            if (mc3.btn_auto_start == false)
                            {
                                coundown_preminary_pump3 = Params.COUNT_MAX_PREMINARY;  // 4s
                                stateMc3 = STMC.PROCESS_AUTO_START;
                            }
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc3.vl_oil_pressure_mainline = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump3) * 0.1);
                            mc3.sig_mpa = true;
                            mc3.sig_moa = true;
                            if (coundown_preminary_pump3 == 0)
                            {
                                coundown_preminary_pump3 = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump3 = Params.COUNT_MAX_PREMINARY;
                                stateMc3 = STMC.PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc3.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_increte_pump3) * 0.1);
                            mc3.sig_vnd = true;
                            mc3.sig_moa = true;
                            mc3.sig_mpa = true;
                            mc3.sig_vnd = true;
                            if (coundown_preminary_pump3 == 70)
                            {
                                mc3.sig_rotate1 = true;
                            }
                            if (coundown_preminary_pump3 == 40)
                            {
                                mc3.sig_rotate2 = true;
                            }
                            if (coundown_preminary_pump3 == 10)
                            {
                                mc3.sig_rotate3 = true;
                            }
                            if (coundown_preminary_pump3 == 0)
                            {
                                stateMc3 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump3 = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine3 = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine3 = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                                coundown_temp_oil3 = Params.COUNT_TEMP_OIL_ENGINE;       //
                                coundown_temp_water3 = Params.COUNT_TEMP_WATER_ENGINE;   //
                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            mc3.sig_vnd = false;
                            mc3.sig_rotate1 = false;
                            mc3.sig_rotate2 = false;
                            mc3.sig_rotate3 = false;
                            mc3.sig_or_oil = true;
                            mc3.sig_vvd = true;
                            mc3.vl_temperature_engine = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine3));
                            mc3.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine3));
                            mc3.vl_temperature_water = ((Params.COUNT_TEMP_WATER_ENGINE - coundown_temp_water3));
                            mc3.vl_temperature_oil = ((Params.COUNT_TEMP_OIL_ENGINE - coundown_temp_oil3));
                            if (coundown_preminary_pump3 == 0)
                            {
                                mc3.sig_vnd = false;
                                mc3.sig_rotate1 = false;
                                mc3.sig_rotate2 = false;
                                mc3.sig_rotate3 = false;
                            }
                            if (coundown_speed_engine3 == 0)
                            {
                                stateMc3 = STMC.START_OK;
                            }
                            break;
                        #endregion
                        //Manual process
                        #region MANUAL
                        case STMC.PROCESS_MANUAL:
                            mc3.sig_mpa = false;
                            mc3.sig_vnd = false;
                            mc3.sig_vvd = false;
                            mc3.sig_rotate1 = false;
                            mc3.sig_rotate2 = false;
                            mc3.sig_rotate3 = false;
                            if (mc3.btn_on_MOA == true)
                            {
                                mc3.sig_moa = true;
                            }
                            if (mc3.btn_off_MOA == true)
                            {
                                mc3.sig_moa = false;
                            }
                            if (mc3.btn_auto_start == true)
                            {
                                stateMc3 = STMC.PROCESS_AUTO_W;
                            }
                            if (mc3.btn_on_MPA == true)
                            {
                                coundown_preminary_pump3 = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                                mc3.sig_mpa = true;
                                stateMc3 = STMC.PROCESS_MANUAL_START;
                            }
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            //mc3.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump3) * 0.1);
                            mc3.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump3) * 0.1);
                            if (mc3.btn_on_MOA == true)
                            {
                                mc3.sig_moa = true;
                            }
                            if (mc3.btn_off_MOA == true)
                            {
                                mc3.sig_moa = false;
                            }
                            if (mc3.btn_on_MPA == true)
                            {
                                mc3.sig_mpa = true;
                            }
                            if (mc3.btn_off_MPA == true)
                            {
                                mc3.sig_mpa = false;
                            }
                            if (mc3.btn_on_VND == true)
                            {
                                mc3.sig_vnd = true;
                                coundown_preminary_pump3 = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump3 = Params.COUNT_MAX_PREMINARY;
                                stateMc3 = STMC.MANUAL_PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            ///coundown_preminary_pump3 = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                            if (coundown_preminary_pump3 == 70)
                            {
                                mc3.sig_rotate1 = true;
                            }
                            if (coundown_preminary_pump3 == 40)
                            {
                                mc3.sig_rotate2 = true;
                            }
                            if (coundown_preminary_pump3 == 10)
                            {
                                mc3.sig_rotate3 = true;
                            }
                            if (mc3.btn_on_VVD == true)
                            {
                                stateMc3 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump3 = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine3 = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine3 = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                                coundown_temp_oil3 = Params.COUNT_TEMP_OIL_ENGINE;       //
                                coundown_temp_water3 = Params.COUNT_TEMP_WATER_ENGINE;   //
                                stateMc3 = STMC.MANUAL_READY_HIGH_PRESURE;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            mc3.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine3));
                            mc3.vl_temperature_engine = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine3));
                            mc3.vl_temperature_water = ((Params.COUNT_TEMP_WATER_ENGINE - coundown_temp_water3));
                            mc3.vl_temperature_oil = ((Params.COUNT_TEMP_OIL_ENGINE - coundown_temp_oil3));
                            mc3.sig_mpa = true;
                            mc3.sig_vvd = true;
                            mc3.sig_or_oil = true;
                            mc3.sig_vnd = false;
                            mc3.sig_rotate1 = false;
                            mc3.sig_rotate2 = false;
                            mc3.sig_rotate3 = false;
                            if (coundown_preminary_pump3 == 0)
                            {
                                mc3.sig_vnd = false;
                                mc3.sig_rotate1 = false;
                                mc3.sig_rotate2 = false;
                                mc3.sig_rotate3 = false;
                            }
                            if (coundown_speed_engine3 == 0)
                            {
                                stateMc3 = STMC.START_OK;
                            }
                            break;
                        #endregion
                        case STMC.START_OK:
                            mc3.sig_moa = false;
                            mc3.sig_mpa = false;
                            mc3.sig_vvd = false;
                            mc3.sig_vnd = false;
                            mc3.sig_rotate1 = false;
                            mc3.sig_rotate2 = false;
                            mc3.sig_rotate3 = false;
                            mc3.sig_or_oil = false;
                            Console.WriteLine("Da khoi dong xong 1");
                            break;
                    }
                }
                //if (stateMachine == StateMachine.CONTROLSPEED)
                //--IF_START_OK_GO_TO_CONTROL_SPEED--//
                {
                    btn_only();
                    Console.WriteLine(stateMc3);
                    switch (stateMc3)
                    {
                        case STMC.START_OK:
                            #region START_OK
                            if (mc3.btn_estop == true)
                            {
                                mc3.offmachine();
                                mc3.park();
                                mc3.sig_oil_no_pump = true;
                                stateMc3 = STMC.IDLE;
                            }
                            if (mc3.btn_up == true)
                            {
                                stateMc3 = STMC.MACHINEUP;
                            }
                            if (mc3.btn_down == true)
                            {
                                stateMc3 = STMC.MACHINEDOWN;
                            }
                            if (mc3.btn_down == true)
                            {
                                coundown_speed_engine3 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc3 = STMC.REVERSE;
                            }
                            if (mc3.sig_reverse == true & mc3.btn_up == true)
                            {
                                coundown_speed_engine3 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                stateMc3 = STMC.STOP_POSITION;
                            }
                            //if(mc3.vl_speed_engine <=75)
                            //{
                            //    mc3.sig_gobehind = false;
                            //    mc3.sig_park = true;
                            //}    
                            break;
                        #endregion
                        case STMC.MACHINEUP:
                            #region MACHINEUP
                            coundown_speed_engine3 = Params.COUNT_STEP_ENGINE;           //Tăng tốc dần
                            mc3.sig_park = false;
                            mc3.sig_reverse = false;
                            if (mc3.btn_up == true)
                            {
                                stateMc3 = STMC.PROCESS_MACHINE_UP;
                            }
                            //Nếu dừng
                            if (mc3.btn_estop == true)
                            {
                                mc3.offmachine();
                                mc3.park();
                                mc3.sig_oil_no_pump = true;
                                stateMc3 = STMC.IDLE;
                            }
                            if (mc3.btn_down == true)
                            {
                                coundown_speed_engine3 = Params.COUNT_STEP_ENGINE;           //Giảm tốc dần
                                stateMc3 = STMC.MACHINEDOWN;
                            }
                            if (mc3.vl_speed_engine < 80)
                            {
                                mc3.sig_up = true;
                                mc3.sig_highspeed = false;
                                //mc3.sig_park = true;
                                //mc3.vl_speed_engine = 75;
                                //stateMc3 = STMC.START_OK;
                            }
                            if (mc3.vl_speed_engine < 75)
                            {
                                mc3.sig_up = false;
                                mc3.sig_highspeed = false;
                                mc3.sig_park = true;
                                mc3.vl_speed_engine = 75;
                                stateMc3 = STMC.START_OK;
                            }
                            if (mc3.btn_down == true)
                            {
                                mc3.sig_up = false;
                                mc3.sig_highspeed = false;
                                mc3.sig_park = false;
                                //mc3.sig_gobehind = true;
                            }
                            if (mc3.btn_quickdown == true)
                            {
                                coundown_speed_engine3 = mc3.vl_speed_engine - 80;
                                Params.COUNT_QUICKDOWN = mc3.vl_speed_engine - 80;
                                stateMc3 = STMC.QUICKDOWN;
                            }
                            break;
                        #endregion
                        case STMC.PROCESS_MACHINE_UP:
                            #region PROCESS_MACHINE_UP
                            mc3.vl_speed_engine = mc3.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine3) / 10);
                            mc3.sig_up = true;
                            if (mc3.vl_speed_engine > 100)
                            {
                                mc3.sig_up = false;
                                mc3.sig_highspeed = true;
                            }
                            if (coundown_speed_engine3 == 0)
                            {
                                stateMc3 = STMC.MACHINEUP;
                            }
                            if (mc3.btn_up == true)
                            {
                                //mc3.vl_speed_engine = mc3.vl_speed_engine + ((Params.COUNT_STEP_ENGINE - coundown_speed_engine3) / 10);
                                stateMc3 = STMC.PROCESS_MACHINE_UP;
                            }
                            break;
                        #endregion
                        case STMC.MACHINEDOWN:
                            #region MACHINEDOWN
                            mc3.vl_speed_engine = mc3.vl_speed_engine - ((Params.COUNT_STEP_ENGINE - coundown_speed_engine3) / 10);
                            //if (mc3.vl_speed_engine < 100)
                            //{
                            //    mc3.sig_goahead = true;
                            //    mc3.sig_highspeed = false;
                            //    stateMc3 = STMC.MACHINEUP;
                            //}
                            if (mc3.vl_speed_engine > 80)
                            {
                                mc3.sig_up = true;
                                mc3.sig_park = false;
                                mc3.sig_highspeed = false;
                                stateMc3 = STMC.MACHINEUP;
                                //stateMc3 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc3.vl_speed_engine > 100)
                            {
                                mc3.sig_up = false;
                                mc3.sig_highspeed = true;
                                stateMc3 = STMC.PROCESS_MACHINE_UP;
                            }
                            if (mc3.vl_speed_engine < 80)
                            {
                                mc3.sig_up = false;
                                mc3.sig_highspeed = false;
                                mc3.sig_park = true;
                                stateMc3 = STMC.MACHINEUP;
                                //mc3.vl_speed_engine = 80;
                            }
                            if (mc3.btn_up == true)     //Đang dùng nút nhấn mc3
                            {
                                stateMc3 = STMC.MACHINEUP;
                            }
                            if (mc3.btn_down == true)
                            {
                                stateMc3 = STMC.MACHINEDOWN;
                            }
                            break;
                        #endregion
                        case STMC.REVERSE:
                            #region REVERSE
                            mc3.vl_speed_engine = mc3.vl_speed_engine + ((Params.COUNT_SPEED_REVERS - coundown_speed_engine3) / 10);
                            mc3.sig_reverse = true;
                            mc3.sig_park = false;
                            if (mc3.vl_speed_engine >= 80)
                            {
                                mc3.sig_reverse = true;
                                stateMc3 = STMC.START_OK;
                            }
                            //stateMc3 = STMC.REVERSE;
                            break;
                        #endregion
                        case STMC.STOP_POSITION:
                            #region STOP_POSITION
                            mc3.vl_speed_engine = mc3.vl_speed_engine - ((Params.COUNT_SPEED_REVERS - coundown_speed_engine3) / 10);
                            if (mc3.vl_speed_engine < 75)                    //Đang sai phần cứng. cần sửa lại nút nhấn Up1
                            {
                                //coundown_speed_engine3 = Params.COUNT_SPEED_REVERS;           //Giảm để lùi
                                mc3.sig_park = true;
                                mc3.sig_highspeed = false;
                                mc3.sig_up = false;
                                mc3.sig_reverse = false;
                                mc3.vl_speed_engine = 75;
                                stateMc3 = STMC.START_OK;
                            }
                            break;
                        #endregion
                        case STMC.QUICKDOWN:
                            #region QUICKDOWN
                            mc3.vl_speed_engine = mc3.vl_speed_engine - ((Params.COUNT_QUICKDOWN - coundown_speed_engine3) / 10);
                            if (mc3.vl_speed_engine <= 79)
                            {
                                mc3.sig_park = true;
                                mc3.sig_highspeed = false;
                                mc3.sig_up = false;
                                stateMc3 = STMC.START_OK;
                            }
                            break;
                            #endregion
                    }
                }
                await Task.Delay(1);
            }
        }
        public void btn_only()
        {
            //***********Tương tác độc lập SW
            if (Orionsystem.sw_control_cpu_off == true)
            {
                stateMachine = StateMachine.MACHINE_OFF;
            }
            {
                #region TEST_LAMP
                //if (Orionsystem.btn_checklight == true)
                //{
                //    stateMachine = StateMachine.TEST;
                //}
                #endregion
                //***************************************//
                #region btn for machine
                //mc1
                if (mc1.sw_on_van == true)
                {
                    mc1.sig_on_van_out_water = false;
                    mc1.sig_off_van_out_water = true;
                }
                if (mc1.sw_on_van == false)
                {
                    mc1.sig_on_van_out_water = true;
                    mc1.sig_off_van_out_water = false;
                }
                //mc2
                if (mc2.sw_on_van == true)
                {
                    mc2.sig_on_van_out_water = false;
                    mc2.sig_off_van_out_water = true;
                }
                if (mc2.sw_on_van == false)
                {
                    mc2.sig_on_van_out_water = true;
                    mc2.sig_off_van_out_water = false;
                }
                //mc3
                if (mc3.sw_on_van == true)
                {
                    mc3.sig_on_van_out_water = false;
                    mc3.sig_off_van_out_water = true;
                }
                if (mc3.sw_on_van == false)
                {
                    mc3.sig_on_van_out_water = true;
                    mc3.sig_off_van_out_water = false;
                }
                //***************************************//
                //mc1
                if (mc1.sw_on_out_air == true)
                {
                    mc1.sig_on_van_out_air = false;
                    mc1.sig_off_van_out_air = true;
                }
                if (mc1.sw_on_out_air == false)
                {
                    mc1.sig_on_van_out_air = true;
                    mc1.sig_off_van_out_air = false;
                }
                //mc2
                if (mc2.sw_on_out_air == true)
                {
                    mc2.sig_on_van_out_air = false;
                    mc2.sig_off_van_out_air = true;
                }
                if (mc2.sw_on_out_air == false)
                {
                    mc2.sig_on_van_out_air = true;
                    mc2.sig_off_van_out_air = false;
                }
                //mc3
                if (mc3.sw_on_out_air == true)
                {
                    mc3.sig_on_van_out_air = false;
                    mc3.sig_off_van_out_air = true;
                }
                if (mc3.sw_on_out_air == false)
                {
                    mc3.sig_on_van_out_air = true;
                    mc3.sig_off_van_out_air = false;
                }
                //***************************************//
                #endregion
                //***************************************//
                #region btn_main
                //btn_kmo
                if (Orionsystem.btn_call_kmo == true)
                {
                    Orionsystem.sig_main_KMO = true;
                }
                if (Orionsystem.btn_call_kmo == false)
                {
                    Orionsystem.sig_main_KMO = false;
                }
                //btn_HMO
                if (Orionsystem.btn_call_hmo == true)
                {
                    Orionsystem.sig_main_HMO = true;
                }
                if (Orionsystem.btn_call_hmo == false)
                {
                    Orionsystem.sig_main_HMO = false;
                }
                //btn_VPU
                if (Orionsystem.btn_call_vpu == true)
                {
                    Orionsystem.sig_main_VPU = true;
                }
                if (Orionsystem.btn_call_vpu == false)
                {
                    Orionsystem.sig_main_VPU = false;
                }
                #endregion
            }
        }
    }
}
