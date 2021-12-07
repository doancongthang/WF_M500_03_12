using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF_M500_03_12;
using System.Threading;

namespace WF_M500_03_12.Services
{
    class ModbusServices
    {
        private ModbusClient modbusClient;
        string receiveData = null;
        byte boardId = 1;
        private Machine mc1;
        private Machine mc2;
        private Machine mc3;
        public ModbusServices(Machine _mc1, Machine _mc2, Machine _mc3)
        {
            mc1 = _mc1;
            mc2 = _mc2;
            mc3 = _mc3;
            modbusClient = new WF_M500_03_12.ModbusClient();
            modbusClient.SerialPort = "COM3";
            modbusClient.Baudrate = 115200;
            modbusClient.Parity = System.IO.Ports.Parity.None;
            modbusClient.StopBits = System.IO.Ports.StopBits.One;
            modbusClient.UnitIdentifier = 1;
            //modbusClient.ReceiveDataChanged += new ModbusClient.ReceiveDataChangedHandler(UpdateReceiveData);
            //modbusClient.SendDataChanged += new EasyModbus.ModbusClient.SendDataChangedHandler(UpdateSendData);
            //modbusClient.ConnectedChanged += new EasyModbus.ModbusClient.ConnectedChangedHandler(UpdateConnectedChanged);
            //modbusClient.ReceiveDataChanged += new ModbusClient.ReceiveDataChangedHandler(updatedata);
            modbusClient.LogFileFilename = "logFiletxt.txt";
        }
        public void Subcribe()
        {
            //Task control = Task.Run(() => loopUpdateModbus());              // Khởi chạy loop services
            //Task controlLight = Task.Run(() => loopUpdateCoilsModbus());    // Khởi chạy loop services
            //Task blinktask = new Task(delegate { blink(); });
            //blinktask.Start();
            //Task bl = Task.Run(() => blink());
            Task control1 = Task.Run(() => updatedata());              // Khởi chạy loop services
        }
        public async void Connect()
        {
            try
            {
                if (modbusClient.Connected)
                {
                    modbusClient.Disconnect();
                }
                modbusClient.Connect();
            }
            catch (Exception exc)
            {
                //Console.WriteLine("Unable to connect to Server");
            }
        }
        public async void blink()
        {
            //Task blinktask = new Task(delegate { blink(); });
            //blinktask.Start();
            while (true)
            {
                modbusClient.UnitIdentifier++;
                if (modbusClient.UnitIdentifier > 3) modbusClient.UnitIdentifier = 1;

                for (modbusClient.UnitIdentifier = 1; modbusClient.UnitIdentifier <= 4; modbusClient.UnitIdentifier++)
                {
                    off_all_larm();
                    //Thread.Sleep(100);
                    await Task.Delay(100);
                }
                for (modbusClient.UnitIdentifier = 1; modbusClient.UnitIdentifier <= 4; modbusClient.UnitIdentifier++)
                {
                    on_all_larm();
                    //Thread.Sleep(100);
                    await Task.Delay(100);
                }
            }
        }
        public async void updatedata()
        {
            while (true)
            {
                //Console.WriteLine("Update light status");
                if (modbusClient.Connected)
                {
                    try
                    {
                        switch (boardId)
                        {
                            case 1:
                                #region Board_ID1
                                modbusClient.UnitIdentifier = 1;
                                bool[] result = modbusClient.ReadDiscreteInputs(0, 32);
                                mc3.btn_on_MOA = result[0];
                                mc3.btn_on_VND = result[1];
                                mc3.btn_on_MPA = result[2];
                                mc3.btn_auto_start = result[3];
                                mc2.btn_auto_start = result[4];
                                mc2.btn_on_VVD = result[5];
                                mc2.btn_off_MPA = result[6];
                                mc2.btn_off_MOA = result[7];
                                mc2.btn_on_VND = result[8];
                                mc2.btn_on_MPA = result[9];
                                mc2.btn_quickdown = result[10];
                                mc3.btn_quickdown = result[11];
                                mc1.btn_auto_start = result[12];
                                mc1.btn_on_MPA = result[13];
                                mc1.btn_on_MOA = result[14];
                                mc1.btn_on_VND = result[15];
                                mc1.btn_off_MOA = result[16];
                                mc1.btn_off_MPA = result[17];
                                //mc.          = result[18];
                                mc2.btn_on_MOA = result[19];
                                mc1.btn_quickdown = result[20];
                                mc3.btn_down = result[21];
                                mc2.btn_down = result[22];
                                mc1.btn_down = result[23];
                                mc3.btn_up = result[24];
                                mc2.btn_up = result[25];
                                mc1.btn_up = result[26];
                                mc1.btn_estop = result[27];
                                mc2.btn_estop = result[28];
                                mc3.btn_estop = result[29];
                                //mc.                     = result[30];
                                //mc.                     = result[31];
                                //mc.
                                //**********************************************************************************************//
                                bool[] coilsToSend1 = new bool[32];
                                coilsToSend1[0] = mc1.sig_starting_forbidden;
                                coilsToSend1[1] = mc1.sig_vvd;
                                coilsToSend1[2] = mc1.sig_or_oil;
                                coilsToSend1[3] = mc1.sig_rotate3;
                                coilsToSend1[4] = mc1.sig_rotate2;
                                coilsToSend1[5] = mc1.sig_rotate1;
                                coilsToSend1[6] = mc1.sig_vnd;
                                coilsToSend1[7] = mc1.sig_mpa;
                                coilsToSend1[8] = mc1.sig_moa;
                                coilsToSend1[9] = mc1.sig_off_van_out_air;
                                coilsToSend1[10] = mc3.sig_on_van_out_air;
                                coilsToSend1[11] = mc2.sig_moa;
                                coilsToSend1[12] = mc2.sig_mpa;
                                coilsToSend1[13] = mc2.sig_vnd;
                                coilsToSend1[14] = mc2.sig_rotate1;
                                coilsToSend1[15] = mc2.sig_rotate2;
                                coilsToSend1[16] = mc2.sig_rotate3;
                                coilsToSend1[17] = mc2.sig_or_oil;
                                coilsToSend1[18] = mc2.sig_vvd;
                                coilsToSend1[19] = mc2.sig_starting_forbidden;
                                coilsToSend1[20] = mc2.sig_on_van_out_air;
                                coilsToSend1[21] = mc1.sig_on_van_out_air;
                                coilsToSend1[22] = mc2.sig_empty_tank;
                                coilsToSend1[23] = mc3.sig_empty_tank;
                                coilsToSend1[24] = mc1.sig_green_normal;
                                coilsToSend1[25] = mc2.sig_green_normal;
                                coilsToSend1[26] = mc3.sig_green_normal;
                                coilsToSend1[27] = mc1.sig_warning_tank;
                                coilsToSend1[28] = mc2.sig_warning_tank;
                                coilsToSend1[29] = mc3.sig_warning_tank;
                                coilsToSend1[30] = mc3.sig_off_van_out_air;
                                coilsToSend1[31] = mc2.sig_off_van_out_air;
                                modbusClient.WriteMultipleCoils(0, coilsToSend1);

                                //int[] gausdwin8inch = new int[6];
                                //gausdwin8inch[0] = Orionsystem.vl_temperature_oil_out;
                                //gausdwin8inch[1] = Orionsystem.vl_temperature_oil_in;
                                //gausdwin8inch[2] = Orionsystem.vl_pressureptk;
                                //gausdwin8inch[3] = Orionsystem.vl_reverse_air_pressure;
                                //gausdwin8inch[4] = Orionsystem.vl_pressurefuel;
                                //gausdwin8inch[5] = Orionsystem.vl_hydraulicpressure_gidravl;
                                //modbusClient.WriteMultipleRegisters(0, gausdwin8inch);
                                #endregion
                                break;
                            case 2:
                                #region Board_ID2
                                modbusClient.UnitIdentifier = 2;
                                bool[] result2 = modbusClient.ReadDiscreteInputs(0, 32);
                                mc1.sw_off_inout_water_oil = result2[0];
                                mc2.sw_main_input_water = result2[1];
                                Orionsystem.sw_control_cpu_on = result2[2];
                                mc1.sw_main_input_water = result2[3];
                                mc1.sw_main_output_water = result2[4];
                                mc1.sw_main_input_oil = result2[5];
                                mc2.sw_main_output_oil = result2[6];
                                mc2.sw_main_input_water = result2[7];
                                mc2.sw_off_inout_water_oil = result2[8];
                                mc2.sw_main_input_oil = result2[9];
                                mc3.sw_off_inout_water_oil = result2[10];
                                mc3.sw_main_input_water = result2[11];
                                mc3.sw_main_output_water = result2[12];
                                mc3.sw_main_input_oil = result2[13];
                                mc3.sw_main_output_oil = result2[14];
                                mc1.sw_main_output_oil = result2[15];
                                Orionsystem.sw_control_cpu_off = result2[16];
                                Orionsystem.sw_main_pressure_off = result2[17];
                                Orionsystem.sw_main_pressure_for3 = result2[18];
                                Orionsystem.sw_main_pressure_for2 = result2[19];
                                Orionsystem.sw_main_pressure_for1 = result2[20];
                                Orionsystem.sw_main_pressure_off = result2[21];
                                Orionsystem.sw_start_pump_du = result2[22];
                                Orionsystem.btn_main_control_pressure = result2[23];
                                mc3.btn_burn_fe = result2[24];
                                mc3.sw_auto_protect = result2[25];
                                mc2.btn_burn_fe = result2[26];
                                mc2.sw_auto_protect = result2[27];
                                mc1.btn_burn_fe = result2[28];
                                mc1.sw_auto_protect = result2[29];
                                //mc. = result[30];
                                //**********************************************************************************************//
                                bool[] coilsToSend2 = new bool[32];
                                coilsToSend2[0] = Orionsystem.sig_green_P_main_pump;
                                coilsToSend2[1] = Orionsystem.sig_main_Du;
                                coilsToSend2[2] = Orionsystem.sig_main_OP;
                                coilsToSend2[3] = Orionsystem.sig_red_main_pump;
                                coilsToSend2[4] = mc1.sig_on_van_out_water;
                                coilsToSend2[5] = mc2.sig_on_van_out_water;
                                coilsToSend2[6] = mc3.sig_on_van_out_water;
                                coilsToSend2[7] = mc1.sig_off_van_out_water;
                                coilsToSend2[8] = mc2.sig_off_van_out_water;
                                coilsToSend2[9] = mc3.sig_off_van_out_water;
                                coilsToSend2[10] = mc3.sig_delta_P_oil;
                                coilsToSend2[11] = Orionsystem.sig_main_VPU;
                                coilsToSend2[12] = Orionsystem.sig_main_HMO;
                                coilsToSend2[13] = Orionsystem.sig_main_KMO;
                                coilsToSend2[14] = mc3.sig_oil_no_pump;
                                coilsToSend2[15] = mc2.sig_oil_no_pump;
                                coilsToSend2[16] = mc1.sig_oil_no_pump;
                                coilsToSend2[17] = Orionsystem.sig_overheat_rotate_3;
                                coilsToSend2[18] = Orionsystem.sig_overheat_rotate_2;
                                coilsToSend2[19] = Orionsystem.sig_overheat_rotate_1;
                                coilsToSend2[20] = mc3.sig_wn_low_T_oil;
                                coilsToSend2[21] = mc3.sig_moa;
                                coilsToSend2[22] = mc3.sig_mpa;
                                coilsToSend2[23] = mc3.sig_vnd;
                                coilsToSend2[24] = mc3.sig_rotate1;
                                coilsToSend2[25] = mc3.sig_rotate2;
                                coilsToSend2[26] = mc3.sig_rotate3;
                                coilsToSend2[27] = mc3.sig_or_oil;
                                coilsToSend2[28] = mc3.sig_vvd;
                                coilsToSend2[29] = mc3.sig_starting_forbidden;
                                coilsToSend2[30] = Orionsystem.sig_speaker;
                                //coilsToSend2[31] = mc.                                     ;
                                modbusClient.WriteMultipleCoils(0, coilsToSend2);
                                #endregion
                                break;
                            case 3:
                                #region Board_ID3
                                modbusClient.UnitIdentifier = 3;
                                bool[] result3 = modbusClient.ReadDiscreteInputs(0, 32);
                                mc1.btn_on_VVD = result3[1];
                                mc3.btn_off_MOA = result3[3];
                                mc3.btn_off_MPA = result3[5];
                                mc3.btn_on_VVD = result3[7];
                                //**********************************************************************************************//
                                bool[] coilsToSend3 = new bool[32];
                                coilsToSend3[0] = mc3.sig_wn_high_P_water;
                                coilsToSend3[1] = mc3.sig_wn_high_P_fuel;
                                coilsToSend3[2] = mc3.sig_wn_high_T_water;
                                coilsToSend3[3] = mc3.sig_wn_high_H_water;
                                coilsToSend3[4] = mc3.sig_red_FE3;
                                coilsToSend3[5] = mc3.sig_red_FE1;
                                coilsToSend3[6] = mc3.sig_red_high_P_tank;
                                coilsToSend3[7] = mc3.sig_red_FE2;
                                coilsToSend3[8] = mc3.sig_red_high_P_ptk;
                                coilsToSend3[9] = mc3.sig_red_high_P_master;
                                coilsToSend3[10] = mc2.sig_wn_high_P_water;
                                coilsToSend3[11] = mc2.sig_wn_high_P_fuel;
                                coilsToSend3[12] = mc2.sig_wn_high_T_water;
                                coilsToSend3[13] = mc2.sig_wn_high_H_water;
                                coilsToSend3[14] = mc2.sig_red_FE3;
                                coilsToSend3[15] = mc3.sig_red_FE1;
                                coilsToSend3[16] = mc2.sig_red_high_P_tank;
                                coilsToSend3[17] = mc2.sig_red_FE2;
                                coilsToSend3[18] = mc2.sig_red_high_P_ptk;
                                coilsToSend3[19] = mc2.sig_red_high_P_master;
                                coilsToSend3[20] = mc1.sig_red_high_P_master;
                                coilsToSend3[21] = mc1.sig_red_high_P_ptk;
                                coilsToSend3[22] = mc1.sig_red_FE2;
                                coilsToSend3[23] = mc1.sig_red_high_P_tank;
                                coilsToSend3[24] = mc1.sig_red_FE1;
                                coilsToSend3[25] = mc1.sig_red_FE3;
                                coilsToSend3[26] = mc1.sig_wn_high_H_water;
                                coilsToSend3[27] = mc1.sig_wn_high_T_water;
                                coilsToSend3[28] = mc1.sig_wn_high_P_fuel;
                                coilsToSend3[29] = mc1.sig_wn_high_P_water;
                                coilsToSend3[30] = mc1.sig_delta_P_oil;
                                coilsToSend3[31] = mc1.sig_wn_low_T_oil;
                                modbusClient.WriteMultipleCoils(0, coilsToSend3);
                                int[] gausdwin7inch = new int[6];
                                //gausdwin7inch[0] = Orionsystem.vl_time_hours;
                                //gausdwin7inch[1] = Orionsystem.vl_time_minute;
                                gausdwin7inch[2] = Orionsystem.vl_air_pressure_in_reverse_cylinders;
                                gausdwin7inch[3] = Orionsystem.vl_water_pressure_at_inlet_to_the_water_supply;
                                gausdwin7inch[4] = Orionsystem.vl_oil_pressure_in_ptk_bearing;
                                gausdwin7inch[5] = 20;
                                modbusClient.WriteMultipleRegisters(0, gausdwin7inch);
                                #endregion
                                break;
                            case 4:
                                #region Board_ID4
                                modbusClient.UnitIdentifier = 4;
                                bool[] result4 = modbusClient.ReadDiscreteInputs(0, 32);
                                ///= result4[0];
                                //= result4[1];
                                //= result4[2];
                                //= result4[3];
                                mc2.sw_on_out_air               = result4[3];
                                mc1.sw_on_out_air               = result4[4];
                                mc3.sw_on_out_air               = result4[5];
                                mc1.sw_on_van                   = result4[6];
                                mc2.sw_on_van                   = result4[7];
                                mc3.sw_on_van                   = result4[8];
                                Orionsystem.sw_main_start_vvr   = result4[9];
                                Orionsystem.sw_main_CPU         = result4[10];
                                Orionsystem.sw_main_VPU         = result4[11];
                                Orionsystem.btn_checklight      = result4[12];
                                Orionsystem.btn_off_speaker     = result4[13];
                                Orionsystem.btn_call_vpu        = result4[14];
                                Orionsystem.btn_call_hmo        = result4[15];
                                Orionsystem.btn_call_kmo        = result4[16];
                                //= result4[19];
                                //= result4[20];
                                //= result4[21];
                                //= result4[22];
                                //= result4[23];
                                //= result4[24];
                                //= result4[25];
                                //= result4[26];
                                //= result4[27];
                                //= result4[28];
                                //= result4[29];
                                //= result4[30];
                                //= result4[31];
                                //**********************************************************************************************//
                                bool[] coilsToSend4 = new bool[32];
                                coilsToSend4[0] = mc1.sig_overhigh_tank;
                                coilsToSend4[1] = mc2.sig_overhigh_tank;
                                coilsToSend4[2] = mc3.sig_overhigh_tank;
                                coilsToSend4[3] = mc1.sig_normal_tank;
                                coilsToSend4[4] = mc2.sig_normal_tank;
                                coilsToSend4[5] = mc3.sig_normal_tank;
                                coilsToSend4[6] = mc1.sig_low_tank;
                                coilsToSend4[7] = mc2.sig_low_tank;
                                coilsToSend4[8] = mc3.sig_low_tank;
                                coilsToSend4[9] = mc1.sig_empty_tank;
                                coilsToSend4[10] = mc3.sig_reverse;
                                coilsToSend4[11] = mc2.sig_reverse;
                                coilsToSend4[12] = mc1.sig_reverse;
                                coilsToSend4[13] = mc3.sig_park;
                                coilsToSend4[14] = mc2.sig_park;
                                coilsToSend4[15] = Orionsystem.sig_main_wn_PRB_oils;
                                coilsToSend4[16] = Orionsystem.sig_main_wn_cistrens;
                                coilsToSend4[17] = Orionsystem.sig_main_cystotrab_oil;
                                //coilsToSend4[18] = mc.                                                              ;
                                //coilsToSend4[19] = mc.                                                              ;
                                coilsToSend4[20] = mc1.sig_park;
                                coilsToSend4[21] = mc3.sig_up;
                                coilsToSend4[22] = mc2.sig_up;
                                coilsToSend4[23] = mc1.sig_up;
                                coilsToSend4[24] = mc3.sig_highspeed;
                                coilsToSend4[25] = mc2.sig_highspeed;
                                coilsToSend4[26] = mc1.sig_highspeed;
                                coilsToSend4[27] = mc3.sig_red_nopressuremachine;
                                coilsToSend4[28] = mc2.sig_red_nopressuremachine;
                                coilsToSend4[29] = mc1.sig_red_nopressuremachine;
                                coilsToSend4[30] = mc2.sig_wn_low_T_oil;
                                coilsToSend4[31] = mc2.sig_delta_P_oil;
                                modbusClient.WriteMultipleCoils(0, coilsToSend4);
                                #endregion
                                break;
                        }
                    }
                    catch (Exception exc)
                    {
                        //Console.WriteLine("Error write coils");
                    }
                }
                boardId++;
                if (boardId > 4) boardId = 1;
                await Task.Delay(1);
            }
        }
        public void on_all_larm()
        {
            try
            {
                if (!modbusClient.Connected)
                {

                }
                bool[] coilsToSend = new bool[32];
                for (int i = 0; i < 32; i++)
                {
                    coilsToSend[i] = true;
                }
                modbusClient.WriteMultipleCoils(0, coilsToSend);
                //modbusClient.WriteSingleCoil(0, true);
            }
            catch (Exception exc)
            {

            }
        }
        public void off_all_larm()
        {
            try
            {
                if (!modbusClient.Connected)
                {
                }
                bool[] coilsToSend = new bool[32];
                for (int i = 0; i < 32; i++)
                {
                    coilsToSend[i] = false;
                }
                modbusClient.WriteMultipleCoils(0, coilsToSend);
                //modbusClient.WriteSingleCoil(0, true);
                //bool[] result2 = modbusClient.ReadDiscreteInputs(0, 100);

            }
            catch (Exception exc)
            {

            }
        }
    }
}

