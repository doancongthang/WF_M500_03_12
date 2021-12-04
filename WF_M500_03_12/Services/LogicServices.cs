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
        MACHINE_OFF,
        MACHINE_ON,
        HYDRAULICS_PUMP,
        READY_HYDRAULICS_PUPM,
        W_OFFTEST,
        TEST,
        CONTROLSPEED,
        CONTROLSPEED2,
        CONTROLSPEED3
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
        private async void loopUpdateActionsStateMachine()
        {
            while (true)
            {
                Console.WriteLine(stateMachine);
                switch (stateMachine)  // KIEM TRA CAC DIEU KIEN
                {
                    case StateMachine.MACHINE_OFF:
                        if (Orionsystem.sw_control_cpu_on == true)
                        {
                            stateMachine = StateMachine.MACHINE_ON;
                        }
                        break;
                    case StateMachine.MACHINE_ON:
                        if (Orionsystem.sw_control_cpu_on == false)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (Orionsystem.btn_checklight == true)
                            stateMachine = StateMachine.TEST;
                        countdown_hydraulics_pump = Params.COUNT_HYDRAULICS_PUMP;
                        stateMachine = StateMachine.HYDRAULICS_PUMP;
                        //stateMachine = StateMachine.HYDRAULICS_PUMP;
                        break;
                    case StateMachine.HYDRAULICS_PUMP:
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
                        break;
                    case StateMachine.READY_HYDRAULICS_PUPM:
                        if (Orionsystem.sw_main_CPU == false)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (Orionsystem.btn_checklight == true)
                            stateMachine = StateMachine.TEST;
                        break;
                    case StateMachine.W_OFFTEST:
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
                        break;
                    case StateMachine.TEST:
                        if (Orionsystem.btn_checklight == false & Orionsystem.sw_main_CPU == false)
                        {
                            stateMachine = StateMachine.MACHINE_OFF;
                        }
                        if (Orionsystem.btn_checklight == false)
                        {
                            stateMachine = StateMachine.W_OFFTEST;
                        }
                        //Moi them vao
                        break;
                }
                //***********************************************************************************************************//
                switch (stateMachine)  // ACTIONS
                {
                    case StateMachine.MACHINE_OFF:
                        while (Orionsystem.sw_main_CPU == false)
                        {
                            Orionsystem.sig_mainhas_pressure = false;
                            Orionsystem.sig_mainno_pressure = false;
                            //mc1.offmachine();
                            //mc2.offmachine();
                            //mc3.offmachine();
                            Orionsystem.off_orion();
                        }
                        break;
                    case StateMachine.MACHINE_ON:
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
                        break;
                    case StateMachine.HYDRAULICS_PUMP:
                        Orionsystem.sig_mainno_pressure = true;
                        mc1.sig_oil_no_pump = true;
                        mc2.sig_oil_no_pump = true;
                        mc3.sig_oil_no_pump = true;
                        mc1.sig_park = true;
                        mc2.sig_park = true;
                        mc3.sig_park = true;
                        Orionsystem.vl_oil_pressure_in_ptk_bearing =8 - (int)((Params.COUNT_HYDRAULICS_PUMP * 0.3 - countdown_hydraulics_pump * 0.3));
                        break;
                    case StateMachine.READY_HYDRAULICS_PUPM:
                        Orionsystem.sig_mainhas_pressure = true;
                        Orionsystem.sig_mainno_pressure = false;
                        //mc1.sig_nopressure = false;
                        //mc2.sig_nopressure = false;
                        //mc3.sig_nopressure = false;

                        //mc1.sig_park = true;
                        //mc2.sig_park = true;
                        //mc3.sig_park = true;
                        break;
                    case StateMachine.W_OFFTEST:
                        Orionsystem.sig_mainhas_pressure = true;
                        //mc1.off_all_sig();
                        //mc2.off_all_sig();
                        //mc3.off_all_sig();
                        Orionsystem.off_all_sig_main();
                        break;
                    case StateMachine.TEST:
                        while (Orionsystem.btn_checklight == true)
                        {
                            mc1.on_all_sig();
                            mc2.on_all_sig();
                            mc3.on_all_sig();
                            Orionsystem.on_all_sig_main();
                        }
                        break;
                }
                await Task.Delay(1);
            }
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
            //Task stateEachMachine1 = Task.Run(() => parallel1_updateMachine());
            //Task stateEachMachine2 = Task.Run(() => parallel2_updateMachine());
            //Task stateEachMachine3 = Task.Run(() => parallel3_updateMachine());
        }
    }
}
