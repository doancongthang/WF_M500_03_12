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

        public double countdown_hydraulics_pump2 = 0;
        public double coundown_preminary_pump2 = 0;
        public double coundown_speed_engine2 = 0;
        public double coundown_increte_pump2 = 0;
        public double coundown_temp_engine2 = 0;

        public double countdown_hydraulics_pump3 = 0;
        public double coundown_preminary_pump3 = 0;
        public double coundown_speed_engine3 = 0;
        public double coundown_increte_pump3 = 0;
        public double coundown_temp_engine3 = 0;
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
            //Task stateEachMachine2 = Task.Run(() => parallel2_updateMachine());
            //Task stateEachMachine3 = Task.Run(() => parallel3_updateMachine());
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
                    stateMc2 = STMC.IDLE;
                    stateMc3 = STMC.IDLE;
                }
                if (stateMachine == StateMachine.STARTMACHINE)
                {
                    btn_only();
                    #region SW State
                    switch (stateMc1)
                    {
                        case STMC.IDLE:
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
                            //Mới thêm vào
                            //if (mc1.sw_start_auto == false)
                            //{
                            //    stateMc1 = STMC.PROCESS_MANUAL;
                            //}
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc1.vl_oil_pressure_mainline = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump) * 0.1);
                            if (coundown_preminary_pump == 0)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 10s
                                coundown_increte_pump = Params.COUNT_MAX_PREMINARY;
                                stateMc1 = STMC.PRESSURE_PREMINARY_PUMP;
                            }
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc1.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_increte_pump) * 0.1);
                            if (coundown_preminary_pump == 0)
                            {
                                stateMc1 = STMC.READY_HIGH_PRESURE;
                                coundown_preminary_pump = Params.COUNT_MAX_PREMINARY;  //1s
                                coundown_speed_engine = Params.COUNT_SPEED_ENGINE;      //Chuẩn bị khởi động
                                coundown_temp_engine = Params.COUNT_TEMPERATURE_ENGINE; //Chuẩn bị tăng nhiệt độ
                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
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
                        case STMC.START_OK:
                            Console.WriteLine("Da khoi dong xong 1");
                            //stateMachine = StateMachine.CONTROLSPEED;
                            break;
                        //***********************************************//
                        //Các điều kiện chuyển Manual
                        case STMC.PROCESS_MANUAL:
                            if (mc1.btn_on_MOA == true)
                            {
                                coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                                stateMc1 = STMC.PROCESS_MANUAL_START;
                            }
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            mc1.vl_oil_pressure_mainline = ((Params.COUNT_MAX_LOWPRESSURE - coundown_preminary_pump) * 0.1);
                            if (mc1.btn_on_VND == true)
                                stateMc1 = STMC.MANUAL_PRESSURE_PREMINARY_PUMP;
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            if (mc1.btn_on_VND == true)
                            {
                                coundown_speed_engine = Params.COUNT_SPEED_ENGINE;      //Tang toc dong co
                                coundown_temp_engine = Params.COUNT_TEMPERATURE_ENGINE; //Tăng nhiệt độ động cơ;
                                stateMc1 = STMC.MANUAL_READY_HIGH_PRESURE;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            if (coundown_speed_engine == 0)  // 
                            {
                                stateMc1 = STMC.START_OK;
                            }
                            //Khởi động xong thì qua điều khiển tốc độ
                            break;
                    }
                    #endregion
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    #region Action State
                    switch (stateMc1)
                    {
                        case STMC.IDLE:
                            mc1.sig_park = true;
                            break;

                        case STMC.PROCESS_AUTO_W:
                            break;
                        case STMC.PROCESS_AUTO_START:
                            mc1.sig_mpa = true;
                            mc1.vl_oil_pressure_mainline = ((Params.COUNT_MAX_PREMINARY - coundown_preminary_pump) * 0.1);
                            break;
                        case STMC.PRESSURE_PREMINARY_PUMP:
                            mc1.sig_vnd = true;
                            mc1.sig_moa = true;
                            mc1.sig_mpa = true;
                            mc1.sig_vnd = true;
                            if (coundown_preminary_pump % 5 == 0)
                            {
                                mc1.sig_rotate1 = true;
                            }
                            if (coundown_preminary_pump % 5 == 0)
                            {
                                mc1.sig_rotate2 = true;
                            }
                            if (coundown_preminary_pump % 5 == 0)
                            {
                                mc1.sig_rotate3 = true;
                            }
                            break;
                        case STMC.READY_HIGH_PRESURE:
                            mc1.sig_vnd = false;
                            mc1.sig_rotate1 = false;
                            mc1.sig_rotate2 = false;
                            mc1.sig_rotate3 = false;
                            mc1.sig_vvd = true;
                            mc1.vl_temperature_engine = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine));
                            mc1.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine));
                            break;
                        case STMC.START_OK:
                            mc1.sig_mpa = false;
                            mc1.sig_vvd = false;
                            mc1.sig_vnd = false;
                            mc1.sig_rotate1 = false;
                            mc1.sig_rotate2 = false;
                            mc1.sig_rotate3 = false;
                            break;
                        case STMC.PROCESS_MANUAL:
                            mc1.sig_mpa = false;
                            mc1.sig_vnd = false;
                            mc1.sig_vvd = false;
                            mc1.sig_rotate1 = false;
                            mc1.sig_rotate2 = false;
                            mc1.sig_rotate3 = false;
                            break;
                        case STMC.PROCESS_MANUAL_START:
                            if (mc1.btn_on_MOA == true)
                            {
                                mc1.sig_moa = true;
                            }
                            if (mc1.btn_on_MPA == true)
                            {
                                mc1.sig_mpa = true;
                            }

                            if (mc1.btn_off_MOA == true)                //Nút nhấn bị lỗi chưa fix. Đang dùng nút off của máy 2.
                                mc1.sig_mpa = false;
                            if (mc1.btn_off_MPA == true)
                                mc1.sig_mpa = false;
                            if (mc1.sig_vnd == true)
                            {
                                mc1.sig_vnd = true;

                            }
                            break;
                        case STMC.MANUAL_PRESSURE_PREMINARY_PUMP:
                            mc1.sig_vnd = true;
                            coundown_preminary_pump = Params.COUNT_MAX_LOWPRESSURE;  // 8s
                            if (coundown_preminary_pump % 5 == 0)
                            {
                                mc1.sig_rotate1 = !mc1.sig_rotate1;
                            }
                            if (coundown_preminary_pump % 5 == 0)
                            {
                                mc1.sig_rotate2 = !mc1.sig_rotate2;
                            }
                            if (coundown_preminary_pump % 5 == 0)
                            {
                                mc1.sig_rotate2 = !mc1.sig_rotate3;
                            }
                            break;
                        case STMC.MANUAL_READY_HIGH_PRESURE:
                            mc1.vl_speed_engine = ((Params.COUNT_SPEED_ENGINE - coundown_speed_engine));
                            mc1.vl_temperature_engine = ((Params.COUNT_TEMPERATURE_ENGINE - coundown_temp_engine));
                            mc1.sig_mpa = true;
                            mc1.sig_vvd = true;
                            mc1.sig_vnd = false;
                            mc1.sig_rotate1 = false;
                            mc1.sig_rotate2 = false;
                            mc1.sig_rotate3 = false;
                            break;
                    }
                    #endregion
                }
                if (stateMachine == StateMachine.MACHINE_OFF)
                {
                    btn_only();
                    mc1.offmachine();
                    mc2.offmachine();
                    mc3.offmachine();
                    Orionsystem.off_orion();
                }
                //if (stateMachine == StateMachine.CONTROLSPEED)
                {
                    btn_only();
                    Console.WriteLine(stateMc1);
                    switch (stateMc1)
                    {
                        case STMC.START_OK:
                            if (mc1.btn_estop == false)
                            {
                                mc1.offmachine();
                                mc1.park();
                                mc1.sig_oil_no_pump = true;
                                stateMc1 = STMC.IDLE;
                            }
                            if (mc1.btn_up == false)
                            {
                                stateMc1 = STMC.MACHINEUP;
                            }
                            if (mc1.btn_down == false)
                            {
                                stateMc1 = STMC.MACHINEDOWN;
                            }
                            if (mc1.btn_down == false)
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
                        case STMC.MACHINEUP:
                            coundown_speed_engine = Params.COUNT_STEP_ENGINE;           //Tăng tốc dần
                            mc1.sig_park = false;
                            mc1.sig_reverse = false;
                            if (mc1.btn_up == true)
                            {
                                stateMc1 = STMC.PROCESS_MACHINE_UP;
                            }
                            //Nếu dừng
                            if (mc1.btn_estop == false)
                            {
                                mc1.offmachine();
                                mc1.park();
                                mc1.sig_oil_no_pump = true;
                                stateMc1 = STMC.IDLE;
                            }

                            if (mc1.btn_down == false)
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
                            if (mc1.btn_down == false)
                            {
                                mc1.sig_up = false;
                                mc1.sig_highspeed = false;
                                mc1.sig_park = false;
                                //mc1.sig_gobehind = true;
                            }


                            if (mc1.btn_quickdown == false)
                            {
                                coundown_speed_engine = mc1.vl_speed_engine - 80;
                                Params.COUNT_QUICKDOWN = mc1.vl_speed_engine - 80;
                                stateMc1 = STMC.QUICKDOWN;
                            }
                            break;
                        case STMC.PROCESS_MACHINE_UP:
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
                            break;
                        case STMC.MACHINEDOWN:
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
                            if (mc1.btn_up == false)     //Đang dùng nút nhấn mc2
                            {
                                stateMc1 = STMC.MACHINEUP;
                            }
                            if (mc1.btn_down == false)
                            {
                                stateMc1 = STMC.MACHINEDOWN;
                            }

                            break;
                        case STMC.REVERSE:
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
                        case STMC.STOP_POSITION:

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
                        case STMC.QUICKDOWN:
                            mc1.vl_speed_engine = mc1.vl_speed_engine - ((Params.COUNT_QUICKDOWN - coundown_speed_engine) / 10);
                            if (mc1.vl_speed_engine <= 79)
                            {
                                mc1.sig_park = true;
                                mc1.sig_highspeed = false;
                                mc1.sig_up = false;
                                stateMc1 = STMC.START_OK;
                            }
                            break;
                    }
                }
                await Task.Delay(1);
                //Console.WriteLine(stateMc1);
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
