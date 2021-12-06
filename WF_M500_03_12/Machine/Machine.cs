using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_M500_03_12
{
    enum BoardID
    {
        Board1,
        Board2,
        Board3
    }
    public class Machine
    {
        #region Signal
        public bool sig_moa;
        public bool sig_mpa;
        public bool sig_vnd;
        public bool sig_rotate1;
        public bool sig_rotate2;
        public bool sig_rotate3;
        public bool sig_or_oil;
        public bool sig_vvd;
        public bool sig_starting_forbidden;

        //Gần chân vịt
        public bool sig_oil_no_pump;           // Hút dầu nhờn

        public bool sig_park;
        public bool sig_reverse;
        public bool sig_up;
        public bool sig_highspeed;
        public bool sig_red_nopressuremachine;

        public bool sig_on_van_out_air;         // Điều khiển mở khí xả
        public bool sig_off_van_out_air;        // Điều khiển đóng khí xả

        public bool sig_on_van_out_water;       // Điều khiển mở làm mát bằng nước thông qua van thông đáy
        public bool sig_off_van_out_water;      // Điều khiển đóng làm mát bằng nước thông qua van thông đáy

        //Báo mức két dầu nhờn
        public bool sig_overhigh_tank;          // Cao
        public bool sig_normal_tank;            // Trung bình
        public bool sig_low_tank;               // Thấp
        public bool sig_empty_tank;             // Quá thấp
        public bool sig_green_normal;           // 
        public bool sig_warning_tank;           // Đèn sự cố 
            
        //Warning signal
        public bool sig_red_high_P_master;      // Tụt áp suất dầu nhờn
        public bool sig_red_high_P_tank;        // Áp lực cột nước tăng
        public bool sig_red_high_P_ptk;         // Áp lực tuabin cao áp
        public bool sig_red_FE1;
        public bool sig_red_FE2;
        public bool sig_red_FE3;
        public bool sig_wn_high_H_water;        // Cột nước trong két nước giảm
        public bool sig_wn_high_P_water;        // Áp lực nước giảm
        public bool sig_wn_high_T_water;        // Nhiệt độ nước
        public bool sig_wn_low_T_oil;           // Nhiệt độ dầu nhờn tăng
        public bool sig_wn_high_P_fuel;         // Áp lực nhiên liệu giảm
        public bool sig_delta_P_oil;            // Chênh lệch áp lực trước và sau lọc khoảng 1.5kbar thì chấp nhận, quá thì phải vệ sinh

        //Value machine
        public double vl_speed_engine;
        public double vl_temperature_engine;
        public double vl_oil_pressure_mainline;
        public double vl_temperature_oil;
        public double vl_temperature_water;
        #endregion

        #region sw_btn
        public bool sw_on_out_air;
        //public bool sw_off_out_air;

        public bool sw_on_van;
        public bool sw_off_van;

        public bool sw_auto_protect;
        public bool btn_burn_fe;

        public bool sw_off_inout_water_oil;

        public bool sw_main_input_water;
        public bool sw_main_output_water;

        public bool sw_main_input_oil;
        public bool sw_main_output_oil;
        #endregion

        #region btn
        public bool btn_auto_start;
        public bool btn_on_MOA;
        public bool btn_off_MOA;
        public bool btn_on_MPA;
        public bool btn_off_MPA;
        public bool btn_on_VND;
        public bool btn_on_VVD;

        public bool btn_up;
        public bool btn_down;
        public bool btn_quickdown;
        public bool btn_estop;
        #endregion

        #region Khối chức năng

        public void on_all_sig()
        {
            sig_moa = true;
            sig_mpa = true;
            sig_vnd = true;
            sig_rotate1 = true;
            sig_rotate2 = true;
            sig_rotate3 = true;
            sig_or_oil = true;
            sig_vvd = true;
            sig_starting_forbidden = true;
            sig_oil_no_pump = true;
            sig_park = true;
            sig_reverse = true;
            sig_up = true;
            sig_highspeed = true;
            sig_red_nopressuremachine = true;
            sig_on_van_out_air = true;
            sig_off_van_out_air = true;
            sig_on_van_out_water = true;
            sig_off_van_out_water = true;
            sig_overhigh_tank = true;
            sig_normal_tank = true;
            sig_low_tank = true;
            sig_empty_tank = true;
            sig_green_normal = true;
            sig_warning_tank = true;
            sig_red_high_P_master = true;
            sig_red_high_P_tank = true;
            sig_red_high_P_ptk = true;
            sig_red_FE1 = true;
            sig_red_FE2 = true;
            sig_red_FE3 = true;
            sig_wn_high_H_water = true;
            sig_wn_high_P_water = true;
            sig_wn_high_T_water = true;
            sig_wn_low_T_oil = true;
            sig_wn_high_P_fuel = true;
            sig_delta_P_oil = true;
        }
        public void off_all_sig()
        {
            sig_moa = false;
            sig_mpa = false;
            sig_vnd = false;
            sig_rotate1 = false;
            sig_rotate2 = false;
            sig_rotate3 = false;
            sig_or_oil = false;
            sig_vvd = false;
            sig_starting_forbidden = false;
            sig_oil_no_pump = false;
            sig_park = false;
            sig_reverse = false;
            sig_up = false;
            sig_highspeed = false;
            sig_red_nopressuremachine = false;
            sig_on_van_out_air = false;
            sig_off_van_out_air = false;
            sig_on_van_out_water = false;
            sig_off_van_out_water = false;
            sig_overhigh_tank = false;
            sig_normal_tank = false;
            sig_low_tank = false;
            sig_empty_tank = false;
            sig_green_normal = false;
            sig_warning_tank = false;
            sig_red_high_P_master = false;
            sig_red_high_P_tank = false;
            sig_red_high_P_ptk = false;
            sig_red_FE1 = false;
            sig_red_FE2 = false;
            sig_red_FE3 = false;
            sig_wn_high_H_water = false;
            sig_wn_high_P_water = false;
            sig_wn_high_T_water = false;
            sig_wn_low_T_oil = false;
            sig_wn_high_P_fuel = false;
            sig_delta_P_oil = false;
        }
        public void offmachine()
        {
            sig_moa = false;
            sig_mpa = false;
            sig_vnd = false;
            sig_rotate1 = false;
            sig_rotate2 = false;
            sig_rotate3 = false;
            sig_or_oil = false;
            sig_vvd = false;
            sig_starting_forbidden = false;
            sig_oil_no_pump = false;
            sig_park = false;
            sig_reverse = false;
            sig_up = false;
            sig_highspeed = false;
            sig_red_nopressuremachine = false;
            sig_on_van_out_air = false;
            sig_off_van_out_air = false;
            sig_on_van_out_water = false;
            sig_off_van_out_water = false;
            sig_overhigh_tank = false;
            sig_normal_tank = false;
            sig_low_tank = false;
            sig_empty_tank = false;
            sig_green_normal = false;
            sig_warning_tank = false;
            sig_red_high_P_master = false;
            sig_red_high_P_tank = false;
            sig_red_high_P_ptk = false;
            sig_red_FE1 = false;
            sig_red_FE2 = false;
            sig_red_FE3 = false;
            sig_wn_high_H_water = false;
            sig_wn_high_P_water = false;
            sig_wn_high_T_water = false;
            sig_wn_low_T_oil = false;
            sig_wn_high_P_fuel = false;
            sig_delta_P_oil = false;

            vl_speed_engine = 0;
            vl_temperature_engine = 0;
            vl_oil_pressure_mainline = 0;
            vl_temperature_oil = 0;
            vl_temperature_water = 0;
    }
        public void park()
        {
            sig_park = true;
            //mc3.sig_nopressure = true;
        }
        #endregion
    }

    public class Orionsystem
    {
        #region Maincontrol
        //Tong so 11 bien
        //public static bool SW_power;              //SW Power
        public static bool sw_main_CPU;
        public static bool sw_main_VPU;
        public static bool sw_control_cpu_on;
        public static bool sw_control_cpu_off;

        //public static bool SW1;                     //SW1 Switch mode Oil, Water
        //public static bool SW2;                     //SW2 Switch mode on Energy
        //public static bool SW3;                     //SW3 Switch mode Pump

        public static bool btn_checklight;          //Bt check all light
        public static bool btn_off_speaker;

        //public static bool rswleft;                 //Rotate SW position left
        //public static bool rswright;                //Rotate SW position right
        //public static bool rswmid;                  //Rotate SW position middle

        public static bool btn_call_vpu;     //Bt call KMO   Gọi khoang máy sau
        public static bool btn_call_hmo;       //Bt call HMO   Gọi khoang máy trước
        public static bool btn_call_kmo;          //Bt call VBU

        public static bool btn_main_control_pressure;

        public static bool sw_main_pressure_off;
        public static bool sw_main_pressure_for3;
        public static bool sw_main_pressure_for2;
        public static bool sw_main_pressure_for1;

        public static bool sw_start_pump_du;
        public static bool sw_main_start_vvr;

        #endregion

        #region Main show
        // Hien thi len dong ho 9 bien + 1 bien thoi gian                      
        public static int vl_temperature_water_in;                          //Temperature water input           
        public static int vl_temperature_water_out;                         //Temperature water out             
        public static int vl_temperature_oil_in;                            //Temperature oil in                
        public static int vl_temperature_oil_out;                           //Temperature oil output            
        public static int vl_reverse_air_pressure;                          //Reverse air pressure              
        public static int vl_pressurefuel;                                  //Pressure fuel                     
        public static int vl_pressureptk;                                   //Pressure ptk
        public static int vl_hydraulicpressure_gidravl;                     //Pressure gidravl

        //public static int vl_hydraulics;                                  //Pressure hydraulics        phụ thuộc vào 2 giá trị bên dưới. công tắc bật trước lọc và sau lọc thì show giá trị này lên               
        //public static int vl_oilafterfilter;                              //Value oil after filter 
        //public static int vl_oilbeforefilter;                             //Value oil before filter         
        public static int vl_oil_pressure_in_ptk_bearing;                   //Giá trị áp suất dầu mang PTK. Đồng hồ 1
        public static int vl_water_pressure_at_inlet_to_the_water_supply;   //Giá trị áp lực nước ở đầu vào đến nguồn cấp nước. Đồng hồ 2
        public static int vl_air_pressure_in_reverse_cylinders;             //Giá trị áp suất không khí trong xi lanh ngược lại. Đồng hồ 3


        public static bool sig_speaker;

        public static bool sig_green_P_main_pump;
        public static bool sig_main_Du;
        public static bool sig_main_OP;
        public static bool sig_red_main_pump;

        public static bool sig_mainhas_pressure;
        public static bool sig_mainno_pressure;

        public static bool sig_main_KMO;
        public static bool sig_main_HMO;
        public static bool sig_main_VPU;
        public static bool sig_main_hobbyshirt;

        public static bool sig_main_wn_PRB_oils;            //Cảnh báo dầu PRB  trong két dự trữ   
        public static bool sig_main_wn_cistrens;            //bể nước           trong két dự trữ
        public static bool sig_main_cystotrab_oil;          //Cảnh báo dầu thải trong két dự trữ


        //Khu vực chân vịt
        public static bool sig_overheat_rotate_1;
        public static bool sig_overheat_rotate_2;
        public static bool sig_overheat_rotate_3;
        #endregion

        public static void on_all_sig_main()
        {
            sig_speaker = true;
            sig_green_P_main_pump = true;
            sig_main_Du = true;
            sig_main_OP = true;
            sig_red_main_pump = true;
            sig_mainhas_pressure = true;
            sig_mainno_pressure = true;
            sig_main_KMO = true;
            sig_main_HMO = true;
            sig_main_VPU = true;
            sig_main_hobbyshirt = true;
            sig_main_wn_PRB_oils = true;
            sig_main_wn_cistrens = true;
            sig_main_cystotrab_oil = true;
            sig_overheat_rotate_1 = true;
            sig_overheat_rotate_2 = true;
            sig_overheat_rotate_3 = true;
        }
        public static void off_all_sig_main()
        {
            sig_speaker = false;
            sig_green_P_main_pump = false;
            sig_main_Du = false;
            sig_main_OP = false;
            sig_red_main_pump = false;
            sig_mainhas_pressure = false;
            sig_mainno_pressure = false;
            sig_main_KMO = false;
            sig_main_HMO = false;
            sig_main_VPU = false;
            sig_main_hobbyshirt = false;
            sig_main_wn_PRB_oils = false;
            sig_main_wn_cistrens = false;
            sig_main_cystotrab_oil = false;
            sig_overheat_rotate_1 = false;
            sig_overheat_rotate_2 = false;
            sig_overheat_rotate_3 = false;

        }
        public static void off_orion()
        {
            vl_temperature_water_in = 0;
            vl_temperature_water_out = 0;
            vl_temperature_oil_in = 0;
            vl_temperature_oil_out = 0;
            vl_reverse_air_pressure = 0;
            //vl_hydraulics = 0;
            vl_pressurefuel = 0;
            vl_pressureptk = 8;
            vl_oil_pressure_in_ptk_bearing = 8;
            //vl_oilafterfilter = 0;
            //vl_oilbeforefilter = 0;
        }
    }
}
